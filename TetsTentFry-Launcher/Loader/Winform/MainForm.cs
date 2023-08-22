using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;
using static Guna.UI2.WinForms.Suite.Descriptions;

namespace tetstentfrylauncher.Loader.Winform
{
    public partial class MainForm : Form
    {
        private bool DownloadCompleted = false;
        public MainForm()
        {
            InitializeComponent();
          
            label35.Text = "Logged in as: " + Properties.Settings.Default["user"];
            Home.Visible = true;
            Launcher.Visible = false;
            Settings.Visible = false;
            label5.Text = "V " + Application.ProductVersion;   
            label23.Text = "V" + Application.ProductVersion;
            downloadStatusText.Visible = false;
        }




        private void MainForm_Load(object sender, EventArgs e)
        {
            base.TopMost = true;
            AppearancePanel.Visible = true;
            AccountPanel.Visible = false;
          
        }

   
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

      

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Home.Visible = true;
            Launcher.Visible = false;
            Settings.Visible = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Home.Visible = false;
            Launcher.Visible = true;
            Settings.Visible = false;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Home.Visible = false;
            Launcher.Visible = false;
            Settings.Visible = true;
        }

       

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {
            if(guna2CustomCheckBox1.Checked)
            {
                this.TopMost = true;
            }
            else { this.TopMost = false; }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AppearancePanel.Visible = true;
            AccountPanel.Visible = false;
          
        }


        private void guna2Button12_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            System.Environment.Exit(1);
        }



   
  
 

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // CHANGE THIS TO YOUR FORTNITE DOWNLOAD LINK
            string downloadUrl = "https://example.com/fortnite.zip";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string downloadPath = Path.Combine(documentsPath, "fortnite.zip");

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadProgressChanged += WebClientDownloadProgressChanged;
                webClient.DownloadFileCompleted += WebClientDownloadCompleted;

                downloadStatusText.Text = "Downloading...";
                webClient.DownloadFileAsync(new Uri(downloadUrl), downloadPath);

                // Block the main thread until download completes
                while (!DownloadCompleted)
                {
                    // You can do other tasks here if needed
                    Application.DoEvents(); // Update the UI
                }
            }

            downloadStatusText.Text = "Download completed!";

            // Extract the downloaded ZIP file
            string extractPath = Path.Combine(documentsPath, "fortnite");
            Directory.CreateDirectory(extractPath);

            ZipFile.ExtractToDirectory(downloadPath, extractPath);

            downloadStatusText.Text = "Extraction completed! Files are in: " + extractPath;

            // Optionally, you can delete the downloaded zip file
            File.Delete(downloadPath);
        }


        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Update the statusLabel with the download progress
            downloadStatusText.Text = $"Download Progress: {e.ProgressPercentage}%";
        }

        private void WebClientDownloadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DownloadCompleted = true;
        }

      


        private void logoutButton_click(object sender, EventArgs e)
        {
            tetstentfrylauncher.Login LogOutForm = new tetstentfrylauncher.Login();
            LogOutForm.Show();
            this.Hide();
        }

 

        private void selectButtonClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
            
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // You can change this to your desired starting path

              
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default["path"] = folderDialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }
        public static void DownloadFile(string Url, string Path) => new WebClient().DownloadFile(Url, Path);
        private void playButton_Click(object sender, EventArgs e)
        {

            if (Properties.Settings.Default["path"] == "" || Properties.Settings.Default["path"] == null || !File.Exists(Properties.Settings.Default["path"]+ "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"))
            {
                MessageBox.Show("Please select a valid Fortnite Path first", "INFORMATION");
            }
            else
            {
                playButton.Enabled = false;
                if (!File.Exists(Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe"))
                    DownloadFile("changingsoon", Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe");
                if (!File.Exists(Path.GetTempPath() + "\\FortniteLauncher.exe"))
                    DownloadFile("changingsoon", Path.GetTempPath() + "\\FortniteLauncher.exe");
                // I can recommend https://github.com/Beat-YT/Aurora.Runtime
                if (!File.Exists(Path.GetTempPath() + "\\" + "yourdllname.dll")) // change it to your DLL name!!
                    DownloadFile("https://downloadforyourdll.com", Path.GetTempPath() + "\\" + "yourdllname.dll");

                // Dont ask what i did here i thought i look smart
                ProcessStartInfo launcherSimulatorInfo = new ProcessStartInfo
                {
                    FileName = Path.GetTempPath()+ "\\FortniteLauncher.exe", 
                    UseShellExecute = false, 
                    CreateNoWindow = true, 
                };
                ProcessStartInfo beSimulatorInfo = new ProcessStartInfo
                {
                    FileName = Path.GetTempPath() + "\\FortniteClient-Win64-Shipping_BE.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                ProcessStartInfo actualFortnitePathInfo = new ProcessStartInfo
                {
                    FileName = Properties.Settings.Default["user"] + "\\FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe",
                    Arguments = $"-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -nosplash -caldera=eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50X2lkIjoiYmU5ZGE1YzJmYmVhNDQwN2IyZjQwZWJhYWQ4NTlhZDQiLCJnZW5lcmF0ZWQiOjE2Mzg3MTcyNzgsImNhbGRlcmFHdWlkIjoiMzgxMGI4NjMtMmE2NS00NDU3LTliNTgtNGRhYjNiNDgyYTg2IiwiYWNQcm92aWRlciI6IkVhc3lBbnRpQ2hlYXQiLCJub3RlcyI6IiIsImZhbGxiYWNrIjpmYWxzZX0.VAWQB67RTxhiWOxx7DBjnzDnXyyEnX7OljJm-j2d88G_WgwQ9wrE6lwMEHZHjBd1ISJdUO1UVUqkfLdU5nofBQ -AUTH_LOGIN={Properties.Settings.Default["user"]} -AUTH_PASSWORD={Properties.Settings.Default["pass"]} -AUTH_TYPE=epic",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
                Process procesLauncher = new Process
                {
                    StartInfo = launcherSimulatorInfo
                };
                Process processBe = new Process
                {
                    StartInfo = beSimulatorInfo
                };
                Process processFortnite = new Process
                {
                    StartInfo = actualFortnitePathInfo
                };
                procesLauncher.Start();
                processBe.Start();
                processFortnite.Start();
                Protection.Inject.InjectProtection(processFortnite.Id, Path.GetTempPath() + "\\" + "yourdllname.dll"); // And again change your DLL name here : )
                Environment.Exit(0); // exit because it does not load correctly 
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["path"] != null || Properties.Settings.Default["path"] != "")
            {
                Directory.Delete((string)Properties.Settings.Default["path"]);
            }
            else
            {
                MessageBox.Show("No path select to uninstall!");
            }
        }
    }
}
