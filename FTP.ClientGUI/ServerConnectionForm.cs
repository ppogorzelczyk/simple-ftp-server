namespace FTP.ClientGUI;

public partial class ServerConnectionForm : Form
{
    public static Client.Client ClientInstance;

    public ServerConnectionForm()
    {
        InitializeComponent();
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        string serverIp = txtServerIp.Text;
        if (string.IsNullOrEmpty(serverIp))
        {
            serverIp = "127.0.0.1";
        }

        ClientInstance = new Client.Client(8090, serverIp);

        var isConnected = ClientInstance.Run();
        if (isConnected)
        {
            ClientInstance.IsConnected = true;
            this.Hide();
            MainForm mainForm = new MainForm();
            //mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }
        else
        {
            MessageBox.Show("Nie udało się połączyć z podanym serwerem. Sprawdź podane IP i spróbuj ponownie.");
        }
    }
}