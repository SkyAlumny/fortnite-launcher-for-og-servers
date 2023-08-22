using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tetstentfrylauncher.Loader.Winform.Login
{
    
    public partial class SuccessfullyLoggedIn_MSGBOX : Form
    {
        private int opacityIncrement = 10;
        private int opacityMax = 255;
        private int count = 0;
        public SuccessfullyLoggedIn_MSGBOX()
        {
            InitializeComponent();
            
        }

        private void SuccessfullyLoggedIn_MSGBOX_Load(object sender, EventArgs e)
        {
            timer1.Start();
            siticoneShadowForm1.SetShadowForm(this);
            base.TopMost = true;
         
           
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            count++;
            // Change this to the time the windows should hide
            if (count == 3)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
        }
    }
}
