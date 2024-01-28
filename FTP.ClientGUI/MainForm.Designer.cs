namespace FTP.ClientGUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnMkdir = new Button();
            btnRmdir = new Button();
            btnGet = new Button();
            btnPut = new Button();
            input1 = new TextBox();
            radioAscii = new RadioButton();
            radioBinary = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            input2 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            responseLabel = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnMkdir
            // 
            btnMkdir.Location = new Point(82, 68);
            btnMkdir.Name = "btnMkdir";
            btnMkdir.Size = new Size(132, 44);
            btnMkdir.TabIndex = 0;
            btnMkdir.Text = "Wywołaj komendę MKDIR";
            btnMkdir.UseVisualStyleBackColor = true;
            btnMkdir.Click += btnMkdir_Click;
            // 
            // btnRmdir
            // 
            btnRmdir.Location = new Point(242, 68);
            btnRmdir.Name = "btnRmdir";
            btnRmdir.Size = new Size(132, 44);
            btnRmdir.TabIndex = 1;
            btnRmdir.Text = "Wywołaj komendę RMDIR";
            btnRmdir.UseVisualStyleBackColor = true;
            btnRmdir.Click += btnRmdir_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(400, 68);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(132, 44);
            btnGet.TabIndex = 2;
            btnGet.Text = "Wywołaj komendę GET";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // btnPut
            // 
            btnPut.Location = new Point(552, 68);
            btnPut.Name = "btnPut";
            btnPut.Size = new Size(132, 44);
            btnPut.TabIndex = 3;
            btnPut.Text = "Wywołaj komendę PUT";
            btnPut.UseVisualStyleBackColor = true;
            btnPut.Click += btnPut_Click;
            // 
            // input1
            // 
            input1.Location = new Point(82, 260);
            input1.Name = "input1";
            input1.Size = new Size(269, 23);
            input1.TabIndex = 4;
            // 
            // radioAscii
            // 
            radioAscii.AutoSize = true;
            radioAscii.Location = new Point(502, 130);
            radioAscii.Name = "radioAscii";
            radioAscii.Size = new Size(78, 19);
            radioAscii.TabIndex = 5;
            radioAscii.Text = "Tryb ASCII";
            radioAscii.UseVisualStyleBackColor = true;
            radioAscii.CheckedChanged += radioAscii_CheckedChanged;
            // 
            // radioBinary
            // 
            radioBinary.AutoSize = true;
            radioBinary.Checked = true;
            radioBinary.Location = new Point(502, 169);
            radioBinary.Name = "radioBinary";
            radioBinary.Size = new Size(91, 19);
            radioBinary.TabIndex = 6;
            radioBinary.TabStop = true;
            radioBinary.Text = "Tryb BINARY";
            radioBinary.UseVisualStyleBackColor = true;
            radioBinary.CheckedChanged += radioBinary_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 242);
            label1.Name = "label1";
            label1.Size = new Size(231, 15);
            label1.TabIndex = 7;
            label1.Text = "Wpisz parametr metody którą chcesz użyć:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(82, 286);
            label2.Name = "label2";
            label2.Size = new Size(201, 15);
            label2.TabIndex = 8;
            label2.Text = "mkdir - nazwa folderu do utworzenia";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 311);
            label3.Name = "label3";
            label3.Size = new Size(191, 15);
            label3.TabIndex = 9;
            label3.Text = "rmdir - nazwa folderu do usunięcia";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(82, 336);
            label4.Name = "label4";
            label4.Size = new Size(164, 15);
            label4.TabIndex = 10;
            label4.Text = "get - nazwa pliku do pobrania";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(82, 364);
            label5.Name = "label5";
            label5.Size = new Size(477, 15);
            label5.TabIndex = 11;
            label5.Text = "put - 1 parametr: nazwa pliku do przesłania, 2 parametr: folder w jakim ma się zapisać plik";
            // 
            // input2
            // 
            input2.Location = new Point(400, 260);
            input2.Name = "input2";
            input2.Size = new Size(252, 23);
            input2.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(400, 242);
            label6.Name = "label6";
            label6.Size = new Size(335, 15);
            label6.TabIndex = 13;
            label6.Text = "Drugi parametr. Uzupełnij tylko gdy wykonujesz komendę PUT";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(82, 134);
            label7.Name = "label7";
            label7.Size = new Size(130, 15);
            label7.TabIndex = 14;
            label7.Text = "Odpowiedź od serwera:";
            // 
            // responseLabel
            // 
            responseLabel.AutoSize = true;
            responseLabel.Location = new Point(82, 159);
            responseLabel.Name = "responseLabel";
            responseLabel.Size = new Size(0, 15);
            responseLabel.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(601, 394);
            button1.Name = "button1";
            button1.Size = new Size(163, 27);
            button1.TabIndex = 16;
            button1.Text = "Rozłącz z serwerem";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnExit_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(responseLabel);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(input2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(radioBinary);
            Controls.Add(radioAscii);
            Controls.Add(input1);
            Controls.Add(btnPut);
            Controls.Add(btnGet);
            Controls.Add(btnRmdir);
            Controls.Add(btnMkdir);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnMkdir;
        private Button btnRmdir;
        private Button btnGet;
        private Button btnPut;
        private TextBox input1;
        private RadioButton radioAscii;
        private RadioButton radioBinary;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox input2;
        private Label label6;
        private Label label7;
        private Label responseLabel;
        private Button button1;
    }
}
