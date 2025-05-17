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
    public partial class ChangePlayersEfficiencyForm : Form
    {
        public string Player;
        public int SelectedPositionId;
        public int SelectedTeamId;
        public string GP;
        public string MIN;
        public string FGM;
        public string FGA;
        public string FG;
        public string TPM;
        public string TPA;
        public string TP;
        public string FTM;
        public string FTA;
        public string FT;
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NBA Regular Season 2024/25;Integrated Security=True";

        public ChangePlayersEfficiencyForm()
        {
            InitializeComponent();
            comboBox1.IntegralHeight = false;
            comboBox1.MaxDropDownItems = 5;
            comboBox2.IntegralHeight = false;
            comboBox2.MaxDropDownItems = 5;
            LoadTeams();
            LoadPositions();
        }

        public ChangePlayersEfficiencyForm(string player, string gp, string min, string fgm, string fga, string fg, string tpm, string tpa, string tp, string ftm, string fta, string ft, int teamId, int positionId) : this()
        {
            textBoxPlayer.Text = player;
            textBoxGP.Text = gp;
            textBoxMIN.Text = min;
            textBoxFGM.Text = fgm;
            textBoxFGA.Text = fga;
            textBoxFG.Text = fg;
            textBox3PM.Text = tpm;
            textBox3PA.Text = tpa;
            textBox3P.Text = tp;
            textBoxFTM.Text = ftm;
            textBoxFTA.Text = fta;
            textBoxFT.Text = ft;

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
            string efficiencyPattern = @"^$|^(100|0|0\.\d+|[1-9]\d?(\.\d+)?)$";

            Player = textBoxPlayer.Text.Trim();
            GP = textBoxGP.Text.Trim();
            MIN = textBoxMIN.Text.Trim();
            FGM = textBoxFGM.Text.Trim();
            FGA = textBoxFGA.Text.Trim();
            FG = textBoxFG.Text.Trim();
            TPM = textBox3PM.Text.Trim();
            TPA = textBox3PA.Text.Trim();
            TP = textBox3P.Text.Trim();
            FTM = textBoxFTM.Text.Trim();
            FTA = textBoxFTA.Text.Trim();
            FT = textBoxFT.Text.Trim();

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
                MessageBox.Show("Число минут не соответствует требованиям! Выберите неотрицательное число, либо оставьте поле пустым.");
                return false;
            }

            if (!Regex.IsMatch(FGM, floatPattern) || !Regex.IsMatch(FGA, floatPattern))
            {
                MessageBox.Show("Число попаданий и число бросков должны быть корректными неотрицательными числами.");
                return false;
            }

            if (!Regex.IsMatch(TPM, floatPattern) || !Regex.IsMatch(TPA, floatPattern))
            {
                MessageBox.Show("Число 3-очковых попаданий и бросков должны быть корректными неотрицательными числами.");
                return false;
            }

            if (!Regex.IsMatch(FTM, floatPattern) || !Regex.IsMatch(FTA, floatPattern))
            {
                MessageBox.Show("Число штрафных попаданий и бросков должны быть корректными неотрицательными числами.");
                return false;
            }

            if (!Regex.IsMatch(FG, efficiencyPattern) || !Regex.IsMatch(TP, efficiencyPattern) || !Regex.IsMatch(FT, efficiencyPattern))
            {
                MessageBox.Show("Процентные показатели должны быть корректными неотрицательными числами от 0 до 100.");
                return false;
            }

            if (decimal.TryParse(FGM.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fgmValue) &&
                decimal.TryParse(FGA.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal fgaValue))
            {
                fgmValue = Math.Round(fgmValue, 2);
                fgaValue = Math.Round(fgaValue, 2);

                if (fgmValue > fgaValue)
                {
                    MessageBox.Show($"Ошибка: Количество попаданий ({fgmValue}) не может быть больше количества бросков ({fgaValue})!");
                    return false;
                }
            }

            if (decimal.TryParse(TPM.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tpmValue) &&
                decimal.TryParse(TPA.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tpaValue))
            {
                tpmValue = Math.Round(tpmValue, 2);
                tpaValue = Math.Round(tpaValue, 2);

                if (tpmValue > tpaValue)
                {
                    MessageBox.Show($"Ошибка: Количество 3-очковых попаданий ({tpmValue}) не может быть больше количества 3-очковых бросков ({tpaValue})!");
                    return false;
                }
            }

            if (decimal.TryParse(FTM.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ftmValue) &&
                decimal.TryParse(FTA.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ftaValue))
            {
                ftmValue = Math.Round(ftmValue, 2);
                ftaValue = Math.Round(ftaValue, 2);

                if (ftmValue > ftaValue)
                {
                    MessageBox.Show($"Ошибка: Количество штрафных попаданий ({ftmValue}) не может быть больше количества штрафных бросков ({ftaValue})!");
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