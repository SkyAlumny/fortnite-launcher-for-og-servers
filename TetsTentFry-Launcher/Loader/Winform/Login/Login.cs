using tetstentfrylauncher.Loader.Winform.Login;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Auth;

namespace tetstentfrylauncher
{

    public partial class Login : Form
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlock);
        // Change text here
        private string targetText = "LOGIN | TTY LAUNCHER";
        private string targetText2 = "TTY";
        private string targetText3 = "LAUNCHER";
        private int AInterval = 50;

        [Obfuscation(Feature = "-virtualization", Exclude = false)]

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;


        public Login()
        {
            InitializeComponent();
            // Some protection for people that should not use the Launcher : ) If you know then you know
            bool ugay = true;
            if (ugay)
            {
                Environment.Exit(1);
            }



            this.Opacity = 0;
            // Set the text to null, this will be changed with AnimateLabel
            label5.Text = null;
            label2.Text = null;
            label1.Text = null;


        }


        private void AnimateLabel()
        {
            foreach (char c in targetText)
            {
                Invoke(new Action(() => label1.Text += c));
                Thread.Sleep(AInterval);
            }
            foreach (char cc in targetText2)
            {
                Invoke(new Action(() => label5.Text += cc));
                Thread.Sleep(AInterval);
            }
            foreach (char ccc in targetText3)
            {
                Invoke(new Action(() => label2.Text += ccc));
                Thread.Sleep(AInterval);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Reset Login Informations back to blank
            Username.Text = "";
            Password.Text = "";
            // If you dont want to override all other apps change this to false
            base.TopMost = true;

            guna2Elipse1.SetElipse(Username);
            guna2Elipse2.SetElipse(Password);

            Thread animationThread = new Thread(AnimateLabel);
            animationThread.Start();
            IntPtr consoleHandle = GetConsoleWindow();
            ShowWindow(consoleHandle, SW_HIDE);

            // Load the Login informations if exisitng
   
                Username.Text = Properties.Settings.Default["user"].ToString();
                Password.Text = Properties.Settings.Default["pass"].ToString();
         
            this.Opacity = 100;


        }




        private bool LoginCheck()
        {
            try
            {
                // If your backend has a Authenication api make the request here : )
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }


        private void LoginButton_Clicked(object sender, EventArgs e)
        {

            AuthenticationInput.Username = Username.Text;
            AuthenticationInput.Password = Password.Text;



            if (LoginCheck())
            {
                loginButton.Text = "Logging in...";



                Properties.Settings.Default["user"] = AuthenticationInput.Username;
                Properties.Settings.Default["pass"] = AuthenticationInput.Password;

                Properties.Settings.Default.Save();

                Thread.Sleep(2000);

                this.Hide();
                SuccessfullyLoggedIn_MSGBOX SuccessForm = new SuccessfullyLoggedIn_MSGBOX();
                SuccessForm.Show();
            }

            else
            {
                if (!LoginCheck())
                {
                    MessageBox.Show("Login Failed", "Invaild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            // Change this to your actual Discord Invite or Website
            Process.Start("discord.com/exampleinvite");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
