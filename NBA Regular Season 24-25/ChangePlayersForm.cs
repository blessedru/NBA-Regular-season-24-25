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
    public partial class ChangePlayersForm : Form
    {
        public string Player;
        public string Country;
        public string Age;
        public int SelectedTeamId;
        public int SelectedPositionId;
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NBA Regular Season 2024/25;Integrated Security=True";

        public ChangePlayersForm()
        {
            InitializeComponent();
            comboBox1.IntegralHeight = false;
            comboBox1.MaxDropDownItems = 5;
            comboBox2.IntegralHeight = false;
            comboBox2.MaxDropDownItems = 5;
            LoadTeams();
            LoadPositions();
        }

        public ChangePlayersForm(string player, string country, string age, int teamId, int positionId) : this()
        {
            textBoxPlayer.Text = player;
            textBoxCountry.Text = country;
            textBoxAge.Text = age;

            comboBox1.SelectedValue = teamId;
            comboBox2.SelectedValue = positionId;
        }

        private void LoadTeams()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT Team_ID,[Team Name] FROM Teams";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable teamsTable = new DataTable();
                    adapter.Fill(teamsTable);

                    comboBox1.DataSource = teamsTable;
                    comboBox1.DisplayMember = "Team Name";
                    comboBox1.ValueMember = "Team_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки команд: " + ex.Message);
            }
        }

        private void LoadPositions()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT Position_ID,POS FROM Positions";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable positionsTable = new DataTable();
                    adapter.Fill(positionsTable);

                    comboBox2.DataSource = positionsTable;
                    comboBox2.DisplayMember = "POS";
                    comboBox2.ValueMember = "Position_ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки позиций: " + ex.Message);
            }
        }

        public bool ValidateInput()
        {
            string englishPattern = @"^$|^[a-zA-Z\s-]+$";
            string floatPattern = @"^$|^(18|19|[2-4]\d|50)(\.\d+)?$";

            Player = textBoxPlayer.Text.Trim();
            Country = textBoxCountry.Text.Trim();
            Age = textBoxAge.Text.Trim();

            if (string.IsNullOrWhiteSpace(Player))
            {
                MessageBox.Show("Имя игрока не может быть пустым.");
                return false;
            }

            if (!Regex.IsMatch(Player, englishPattern))
            {
                MessageBox.Show("Имя игрока не соответствует требованиям! Используйте только английские буквы.");
                return false;
            }

            if (!Regex.IsMatch(Country, englishPattern))
            {
                MessageBox.Show("Страна не соответствует требованиям! Используйте только английские буквы, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(Age, floatPattern))
            {
                MessageBox.Show("Возраст не соответствует требованиям! Используйте только числа от 18 до 51 невключительно, либо оставте поле пустым.");
                return false;
            }

            SelectedTeamId = (int)comboBox1.SelectedValue;
            SelectedPositionId = (int)comboBox2.SelectedValue;

            return true;
        }



        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void PlayersForm_Load(object sender, EventArgs e)
        {

        }
    }
}