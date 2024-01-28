using FTP.Client;

namespace FTP.ClientGUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            if (!ServerConnectionForm.ClientInstance.IsConnected)
            {
                MessageBox.Show("ERROR");
            }
        }

        private void btnMkdir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(input1.Text))
            {
                MessageBox.Show("Uzupe�nij 1 parametr by wywo�a� komend� MKDIR.");
                return;
            }

            var result = ServerConnectionForm.ClientInstance.HandleDirCommand(ControlCommands.Mkdir, input1.Text);
            responseLabel.Text = result;
            input1.Text = "";
        }

        private void btnRmdir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(input1.Text))
            {
                MessageBox.Show("Uzupe�nij 1 parametr by wywo�a� komend� RMDIR.");
                return;
            }

            var result = ServerConnectionForm.ClientInstance.HandleDirCommand(ControlCommands.Rmdir, input1.Text);
            responseLabel.Text = result;
            input1.Text = "";
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(input1.Text))
            {
                MessageBox.Show("Uzupe�nij 1 parametr by wywo�a� komend� GET.");
                return;
            }

            var result = ServerConnectionForm.ClientInstance.HandleGetCommand(input1.Text);
            MessageBox.Show(result);
            input1.Text = "";
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(input1.Text))
            {
                MessageBox.Show("Uzupe�nij 1 parametr nazw� pliku do przes�ania by wywo�a� komend� PUT.");
                return;
            }

            if (string.IsNullOrEmpty(input2.Text))
            {
                MessageBox.Show("Uzupe�nij 1 parametr �cie�k� do zapisu by wywo�a� komend� PUT.");
                return;
            }

            var result = ServerConnectionForm.ClientInstance.HandlePutCommand(input1.Text, input2.Text);
            responseLabel.Text = result;
            input1.Text = "";
            input2.Text = "";
        }

        private void radioAscii_CheckedChanged(object sender, EventArgs e)
        {
            ServerConnectionForm.ClientInstance.Ascii = radioAscii.Checked;
            radioBinary.Checked = !radioAscii.Checked;
        }

        private void radioBinary_CheckedChanged(object sender, EventArgs e)
        {
            ServerConnectionForm.ClientInstance.Binary = radioBinary.Checked;
            radioAscii.Checked = !radioBinary.Checked;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ServerConnectionForm.ClientInstance.IsConnected = false;
            ServerConnectionForm.ClientInstance.EndConnection();
            ServerConnectionForm form = new ServerConnectionForm();
            form.Show();
            this.Close();
        }
    }
}
