using NBA_Regular_Season_24_25;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    public partial class UsersForm : Form
    {

        public UsersForm()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();

            if (adminForm.ShowDialog() == DialogResult.OK)
            {
                MainForm mainForm = new MainForm();
                mainForm.IsGuest = false; 
                mainForm.Show();
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.IsGuest = true;
            mainForm.Show();
        }
    }
}