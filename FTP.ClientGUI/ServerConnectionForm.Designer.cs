namespace FTP.ClientGUI
{
    partial class ServerConnectionForm
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
            txtServerIp = new TextBox();
            label1 = new Label();
            btnConnect = new Button();
            SuspendLayout();
            // 
            // txtServerIp
            // 
            txtServerIp.Location = new Point(63, 69);
            txtServerIp.Name = "txtServerIp";
            txtServerIp.Size = new Size(289, 23);
            txtServerIp.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 51);
            label1.Name = "label1";
            label1.Size = new Size(382, 15);
            label1.TabIndex = 1;
            label1.Text = "Wpisz adres IP serwera (zostaw puste dla domyślnej wartości localhost):";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(141, 131);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(116, 59);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Połącz";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // ServerConnectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(402, 297);
            Controls.Add(btnConnect);
            Controls.Add(label1);
            Controls.Add(txtServerIp);
            Name = "ServerConnectionForm";
            Text = "ServerConnectionForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtServerIp;
        private Label label1;
        private Button btnConnect;
    }
}