using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        public bool ValidateInput()
        {
            string enteredLogin = textBoxLogin.Text.Trim();
            string enteredPass = textBoxPass.Text.Trim();

            string correctLogin = "NBAKing";
            string correctPass = "905040";

            if (enteredLogin != correctLogin || enteredPass != correctPass)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return false;
            }

            string englishPattern = @"^[a-zA-Z0-9 ]+$";

            if (!Regex.IsMatch(enteredLogin, englishPattern))
            {
                MessageBox.Show("Логин содержит недопустимые символы.");
                return false;
            }

            if (!Regex.IsMatch(enteredPass, englishPattern))
            {
                MessageBox.Show("Пароль содержит недопустимые символы.");
                return false;
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}