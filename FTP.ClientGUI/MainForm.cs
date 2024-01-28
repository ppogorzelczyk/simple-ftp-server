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
                MessageBox.Show("Uzupe³nij 1 parametr by wywo³aæ komendê MKDIR.");
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
                MessageBox.Show("Uzupe³nij 1 parametr by wywo³aæ komendê RMDIR.");
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
                MessageBox.Show("Uzupe³nij 1 parametr by wywo³aæ komendê GET.");
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
                MessageBox.Show("Uzupe³nij 1 parametr nazw¹ pliku do przes³ania by wywo³aæ komendê PUT.");
                return;
            }

            if (string.IsNullOrEmpty(input2.Text))
            {
                MessageBox.Show("Uzupe³nij 1 parametr œcie¿k¹ do zapisu by wywo³aæ komendê PUT.");
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
