using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace NBA_Regular_Season_24_25
{
    public partial class MainForm : Form
    {
        public const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NBA Regular Season 2024/25;Integrated Security=True";
        public bool IsGuest { get; set; } = false;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAverageStats_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.PlayersAverageStats);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnPlayers_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Players);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnPlayersTotalStats_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.PlayersTotalStats);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnEfficiency_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.PlayersEfficiency);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void btnTeams_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.Teams);
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnRecordsWest_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.TeamsRecords, "Western");
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnRecordsEast_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm(this, Tables.TeamsRecords, "Eastern");
            if (dataViewForm.ShowDialog() == DialogResult.OK)
            {

            }

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Руководство пользователя.docx");

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл 'Руководство пользователя.docx' не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string url = "https://docs.google.com/forms/d/e/1FAIpQLScagGTMdJZehWProFBe5kGcNHg2-ckMwXxjQuFtNCNK-6CrXg/viewform?usp=sharing";

            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть браузер: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
