using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace AmazoomServer
{
    public partial class FormServer : Form
    {
        public class ProductData
        {
            public string Product
            { get; }

            public int Quantity
            { get; }

            public ProductData(string product, int quantity)
            {
                this.Product = product;
                this.Quantity = quantity;
            }
        }

        private SimpleTcpServer server;
        private readonly List<int> clientIds;
        private readonly Dictionary<int, List<ProductData>> warehouseInventory;

        public FormServer()
        {
            InitializeComponent();

            this.clientIds = new List<int>();
            this.warehouseInventory = new Dictionary<int, List<ProductData>>();
        }

        private void buttonPort_Click(object sender, EventArgs e)
        {
            IPAddress address;
            try
            {
                address = IPAddress.Parse(textBoxIP.Text);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Error: Incorrect IP Format");
                return;
            }
            catch (FormatException)
            {
                MessageBox.Show("Error: Incorrect IP Format");
                return;
            }

            int port;
            if (string.IsNullOrEmpty(textBoxPort.Text))
            {
                MessageBox.Show("Please fill in Port Number");
                return;
            }
            try
            {
                port = Convert.ToInt32(textBoxPort.Text);
                if (port == 0)
                {
                    MessageBox.Show("Error: Invalid Port");
                    return;
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Error: Incorrect Port Format");
                return;
            }

            try
            {
                this.server = new SimpleTcpServer().Start(address, port);
                this.server.Delimiter = 0x13;  // enter
                this.server.StringEncoder = Encoding.UTF8;
                this.server.DelimiterDataReceived += ServerDataReceived;
                buttonPort.Enabled = false;
            }
            catch (SocketException)
            {
                MessageBox.Show("Error: Cannot connect to network.");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            textBoxWarehouse.Clear();
            foreach (var warehouseId in this.warehouseInventory.Keys)
            {
                textBoxWarehouse.Text += warehouseId.ToString() + "\r\n";
            }

            textBoxClient.Clear();
            foreach (var clientId in this.clientIds)
            {
                textBoxClient.Text += clientId.ToString() + "\r\n";
            }
        }

        private void broadcast(string command, string sender, string receiver,
            string payload)
        {
            if (string.IsNullOrEmpty(sender))
            {
                sender = "-";
            }
            if (string.IsNullOrEmpty(receiver))
            {
                receiver = "-";
            }
            if (string.IsNullOrEmpty(payload))
            {
                payload = "-";
            }
            this.server.BroadcastLine(command + "/"
                + sender + "/"
                + receiver + "/"
                + payload);
        }

        private void sendInventory()
        {
            string payload = "";
            // Assume inventory of each warehouse is unique
            foreach (var inventory in this.warehouseInventory)
            {
                foreach (var productData in inventory.Value)
                {
                    payload += productData.Product
                        + "*"
                        + productData.Quantity.ToString()
                        + ",";
                }
            }
            broadcast("InventoryToClient", null, null, payload);
            return;
        }

        private void sendDeletedProduct(string name, int quantity)
        {
            string payload = name + "*" + quantity.ToString();
            broadcast("ClientDeleteProduct", null, null, payload);
            return;
        }

        private void ServerDataReceived(object sender, SimpleTCP.Message e)
        {
            string[] message = e.MessageString.Split('/');
            if (message.Length != 4)
            {
                return;
            }
            string messageCommand = message[0];
            string messageSender = message[1];
            string messageReceiver = message[2];
            string messagePayload = message[3];

            int clientId;
            int warehouseId;
            switch (messageCommand)
            {
                case "ClientStart":
                    clientId = Convert.ToInt32(messageSender);
                    if (clientId != 0 &&
                        !this.clientIds.Contains(clientId))
                    {
                        this.clientIds.Add(clientId);
                    }
                    sendInventory();
                    break;
                case "WarehouseStart":
                    warehouseId = Convert.ToInt32(messageSender);
                    if (warehouseId != 0 &&
                        !this.warehouseInventory.Keys.Contains(warehouseId))
                    {
                        string[] productQuantities = messagePayload.Split(',');
                        string[] productQuantity;
                        List<ProductData> inv = new List<ProductData>();
                        for (int i = 0; i < productQuantities.Length; i++)
                        {
                            productQuantity = productQuantities[i].Split('*');
                            if (productQuantity.Length == 2)
                            {
                                string product = productQuantity[0];
                                string quantity = productQuantity[1];
                                inv.Add(new ProductData(
                                    product, Convert.ToInt32(quantity)));
                            }
                        }
                        this.warehouseInventory.Add(
                            Convert.ToInt32(messageSender), inv);
                        // Update client regarding the inventory
                        sendInventory();
                    }
                    break;
                case "WarehouseNewProduct":
                    warehouseId = Convert.ToInt32(messageSender);
                    if (this.warehouseInventory.Keys.Contains(warehouseId))
                    {
                        List<ProductData> inv = 
                            this.warehouseInventory[warehouseId];
                        string[] productQuantity = messagePayload.Split('*');
                        if (productQuantity.Length == 2)
                        {
                            string product = productQuantity[0];
                            string quantity = productQuantity[1];
                            inv.Add(new ProductData(
                                product, Convert.ToInt32(quantity)));
                        }

                        this.warehouseInventory[warehouseId] = inv;
                        sendInventory();
                    }
                    break;
                case "WarehouseDeleteProduct":
                    warehouseId = Convert.ToInt32(messageSender);
                    if (this.warehouseInventory.Keys.Contains(warehouseId))
                    {
                        List<ProductData> inv =
                            this.warehouseInventory[warehouseId];

                        string[] productQuantity = messagePayload.Split('*');
                        string product = productQuantity[0];
                        string quantity = productQuantity[1];

                        int j = 0;
                        foreach (ProductData productData in inv)
                        {
                            if (productData.Product == product)
                            {
                                quantity = productData.Quantity.ToString();

                                //inv.Remove(new ProductData(
                                //    product, Convert.ToInt32(quantity)));
                                inv.RemoveAt(j);

                                this.warehouseInventory[warehouseId] = inv;
                                sendDeletedProduct(
                                    product, Convert.ToInt32(quantity));
                                break;
                            }
                            j++;
                        }
                    }
                    break;
                case "OrderFromClient":
                    // No available warehouse
                    if (this.warehouseInventory.Count == 0)
                    {
                        broadcast("OrderToClient", null, messageSender,
                            "Denied");
                        break;
                    }

                    // Assume each order contains one product only
                    string[] productDataOrder = messagePayload.Split('*');
                    if (productDataOrder.Length < 2)
                    {
                        broadcast("OrderToClient", null, messageSender,
                            "Denied");
                        break;
                    }

                    // Check which warehouse to send order
                    bool orderSent = false;
                    foreach (var inventory in this.warehouseInventory)
                    {
                        foreach (var productData in inventory.Value)
                        {
                            if (productData.Product == productDataOrder[0])
                            {
                                broadcast("OrderToWarehouse", messageSender,
                                    inventory.Key.ToString(), messagePayload);
                                orderSent = true;
                                break;
                            }
                        }
                        if (orderSent)
                        {
                            break;
                        }
                    }

                    // Order cannot be fulfilled
                    if (!orderSent)
                    {
                        broadcast("OrderToClient", null, messageSender,
                            "Denied");
                    }
                    break;
                case "OrderFromWarehouse":
                    broadcast("OrderToClient", null,
                        messageReceiver, messagePayload);
                    break;
                case "ClientStop":
                    clientId = Convert.ToInt32(messageSender);
                    this.clientIds.Remove(clientId);
                    break;
                case "WarehouseStop":
                    warehouseId = Convert.ToInt32(messageSender);
                    this.warehouseInventory.Remove(warehouseId);
                    sendInventory();
                    break;
            }
        }
    }
}
