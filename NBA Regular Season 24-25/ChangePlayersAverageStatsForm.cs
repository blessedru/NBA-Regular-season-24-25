using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    public partial class ChangePlayersAverageStatsForm : Form
    {
        public string Player;
        public int SelectedPositionId;
        public int SelectedTeamId;
        public string GP;
        public string MIN;
        public string PTS;
        public string REB;
        public string AST;
        public string STL;
        public string BLK;
        public string TO;
        public string DD2;
        public string TD3;
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NBA Regular Season 2024/25;Integrated Security=True";

        public ChangePlayersAverageStatsForm()
        {
            InitializeComponent();
            comboBox1.IntegralHeight = false;
            comboBox1.MaxDropDownItems = 5;
            comboBox2.IntegralHeight = false;
            comboBox2.MaxDropDownItems = 5;
            LoadTeams();
            LoadPositions();
        }

        public ChangePlayersAverageStatsForm(string player, string gp, string min, string pts, string reb, string ast, string stl, string blk, string to, string dd2, string td3, int teamId, int positionId) : this()
        {
            textBoxPlayer.Text = player;
            textBoxGP.Text = gp;
            textBoxMIN.Text = min;
            textBoxPTS.Text = pts;
            textBoxREB.Text = reb;
            textBoxAST.Text = ast;
            textBoxSTL.Text = stl;
            textBoxBLK.Text = blk;
            textBoxTO.Text = to;
            textBoxDD2.Text = dd2;
            textBoxTD3.Text = td3;

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
            string floatPattern = @"^$|^(0|0\.\d+|0*[1-9]\d*(\.\d+)?)$";
            string gamesPattern = @"^$|^(?:[1-9]|[1-7]\d|8[0-2])$";
            string intPattern = @"^$|^(?:0|[1-9]|[1-7]\d|8[0-2])$";

            Player = textBoxPlayer.Text.Trim();
            GP = textBoxGP.Text.Trim();
            MIN = textBoxMIN.Text.Trim();
            PTS = textBoxPTS.Text.Trim();
            REB = textBoxREB.Text.Trim();
            AST = textBoxAST.Text.Trim();
            STL = textBoxSTL.Text.Trim();
            BLK = textBoxBLK.Text.Trim();
            TO = textBoxTO.Text.Trim();
            DD2 = textBoxDD2.Text.Trim();
            TD3 = textBoxTD3.Text.Trim();

            if (string.IsNullOrWhiteSpace(Player))
            {
                MessageBox.Show("Имя и фамилия игрока не могут быть пустыми.");
                return false;
            }

            if (!Regex.IsMatch(Player, englishPattern))
            {
                MessageBox.Show("Имя и фамилия игрока не соответствуют требованиям! Используйте только английские буквы.");
                return false;
            }

            if (!Regex.IsMatch(GP, gamesPattern))
            {
                MessageBox.Show("Количество игр не соответствует требованиям! Выберите целое число игр от 1 до 82 включительно.");
                return false;
            }

            if (!Regex.IsMatch(MIN, floatPattern))
            {
                MessageBox.Show("Число минут не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(PTS, floatPattern))
            {
                MessageBox.Show("Число очков не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(REB, floatPattern))
            {
                MessageBox.Show("Число подборов не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(AST, floatPattern))
            {
                MessageBox.Show("Число передач не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(STL, floatPattern))
            {
                MessageBox.Show("Число перехватов не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(BLK, floatPattern))
            {
                MessageBox.Show("Число блокшотов не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(TO, floatPattern))
            {
                MessageBox.Show("Число потерь не соответствует требованиям! Выберите неотрицательное число, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(DD2, intPattern))
            {
                MessageBox.Show("Число дабл-даблов не соответствует требованиям! Выберите ЦЕЛОЕ число от 0 до 82 включительно, либо оставте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(TD3, intPattern))
            {
                MessageBox.Show("Число трипл-даблов не соответствует требованиям! Выберите ЦЕЛОЕ число от 0 до 82 включительно, либо оставте поле пустым.");
                return false;
            }

            if (decimal.TryParse(GP.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal gpValue) &&
                decimal.TryParse(DD2.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dd2Value))
            {
                gpValue = Math.Round(gpValue, 2);
                dd2Value = Math.Round(dd2Value, 2);

                if (dd2Value > gpValue)
                {
                    MessageBox.Show($"Ошибка: Количество дабл-даблов ({dd2Value}) не может быть больше количества игр ({gpValue})!");
                    return false;
                }
            }

            if (decimal.TryParse(GP.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal gp2Value) &&
                decimal.TryParse(TD3.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal td3Value))
            {
                gpValue = Math.Round(gp2Value, 2);
                dd2Value = Math.Round(td3Value, 2);

                if (td3Value > gp2Value)
                {
                    MessageBox.Show($"Ошибка: Количество трипл-даблов ({td3Value}) не может быть больше количества игр ({gp2Value})!");
                    return false;
                }
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