
namespace AmazoomServer
{
    partial class FormServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.buttonPort = new System.Windows.Forms.Button();
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.labelClient = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.textBoxWarehouse = new System.Windows.Forms.TextBox();
            this.textBoxClient = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(58, 36);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(135, 20);
            this.textBoxPort.TabIndex = 0;
            this.textBoxPort.Text = "8911";
            // 
            // labelPort
            // 
            this.labelPort.Location = new System.Drawing.Point(12, 37);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(40, 20);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPort
            // 
            this.buttonPort.Location = new System.Drawing.Point(199, 10);
            this.buttonPort.Name = "buttonPort";
            this.buttonPort.Size = new System.Drawing.Size(75, 47);
            this.buttonPort.TabIndex = 2;
            this.buttonPort.Text = "Start";
            this.buttonPort.UseVisualStyleBackColor = true;
            this.buttonPort.Click += new System.EventHandler(this.buttonPort_Click);
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.Location = new System.Drawing.Point(12, 86);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(100, 20);
            this.labelWarehouse.TabIndex = 4;
            this.labelWarehouse.Text = "Warehouse";
            this.labelWarehouse.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelClient
            // 
            this.labelClient.Location = new System.Drawing.Point(146, 86);
            this.labelClient.Name = "labelClient";
            this.labelClient.Size = new System.Drawing.Size(100, 20);
            this.labelClient.TabIndex = 8;
            this.labelClient.Text = "Client";
            this.labelClient.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelIP
            // 
            this.labelIP.Location = new System.Drawing.Point(12, 11);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(40, 20);
            this.labelIP.TabIndex = 11;
            this.labelIP.Text = "IP";
            this.labelIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(58, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(135, 20);
            this.textBoxIP.TabIndex = 12;
            this.textBoxIP.Text = "127.0.0.1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // textBoxWarehouse
            // 
            this.textBoxWarehouse.Location = new System.Drawing.Point(12, 109);
            this.textBoxWarehouse.Multiline = true;
            this.textBoxWarehouse.Name = "textBoxWarehouse";
            this.textBoxWarehouse.ReadOnly = true;
            this.textBoxWarehouse.Size = new System.Drawing.Size(125, 124);
            this.textBoxWarehouse.TabIndex = 13;
            // 
            // textBoxClient
            // 
            this.textBoxClient.Location = new System.Drawing.Point(149, 109);
            this.textBoxClient.Multiline = true;
            this.textBoxClient.Name = "textBoxClient";
            this.textBoxClient.ReadOnly = true;
            this.textBoxClient.Size = new System.Drawing.Size(125, 124);
            this.textBoxClient.TabIndex = 14;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 245);
            this.Controls.Add(this.textBoxClient);
            this.Controls.Add(this.textBoxWarehouse);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.labelClient);
            this.Controls.Add(this.labelWarehouse);
            this.Controls.Add(this.buttonPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormServer";
            this.Text = "Amazoom Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Button buttonPort;
        private System.Windows.Forms.Label labelWarehouse;
        private System.Windows.Forms.Label labelClient;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox textBoxWarehouse;
        private System.Windows.Forms.TextBox textBoxClient;
    }
}

