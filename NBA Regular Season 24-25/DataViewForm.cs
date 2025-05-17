using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    public partial class DataViewForm : Form
    {
        MainForm mainForm;
        Tables table;
        bool uploaded = false;
        private bool isGuest;
        public Dictionary<string, int> groupsName = new Dictionary<string, int>();
        public DataTable dataTable = new DataTable();
        private DataTable _dataTable;
        private string conferenceFilter;

        public DataViewForm(MainForm _mainForm, Tables _table)
        {
            this.mainForm = _mainForm;
            this.table = _table;
            this.isGuest = _mainForm.IsGuest;
            InitializeComponent();
            InitializeButtons();

            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
            btnSearch.Click += btnSearch_Click;
        }

        private void PerformSearch()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (_dataTable == null)
                return;

            DataView dv = new DataView(_dataTable);

            if (!string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = $"[Баскетболист] LIKE '%{searchText}%'";
            }
            else
            {
                dv.RowFilter = "";
            }

            dataGridView.DataSource = dv;
        }

        private void InitializeButtons()
        {
            if (isGuest)
            {
                if (table == Tables.Teams || table == Tables.TeamsRecords)
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;
                    btnDelete.Visible = false;
                    btnDelete.Enabled = false;
                    btnChange.Visible = false;
                    btnChange.Enabled = false;
                    txtSearch.Visible = false;
                    txtSearch.Enabled = false;
                    btnSearch.Visible = false;
                    btnSearch.Enabled = false;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;
                    btnDelete.Visible = false;
                    btnDelete.Enabled = false;
                    btnChange.Visible = false;
                    btnChange.Enabled = false;
                    txtSearch.Visible = true;
                    txtSearch.Enabled = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                }
            }
            else
            {
                if (table == Tables.Teams || table == Tables.TeamsRecords)
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;
                    btnDelete.Visible = false;
                    btnDelete.Enabled = false;
                    btnChange.Visible = false;
                    btnChange.Enabled = false;
                    txtSearch.Visible = false;
                    txtSearch.Enabled = false;
                    btnSearch.Visible = false;
                    btnSearch.Enabled = false;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                }
                else if (table == Tables.Players)
                {
                    btnAdd.Visible = true;
                    btnAdd.Enabled = true;
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                    btnChange.Visible = true;
                    btnChange.Enabled = true;
                    txtSearch.Visible = true;
                    txtSearch.Enabled = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                }
                else if (table != Tables.Teams && table != Tables.TeamsRecords)
                {
                    btnAdd.Visible = false;
                    btnAdd.Enabled = false;
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                    btnChange.Visible = true;
                    btnChange.Enabled = true;
                    txtSearch.Visible = true;
                    txtSearch.Enabled = true;
                    btnSearch.Visible = true;
                    btnSearch.Enabled = true;
                    btnExport.Visible = true;
                    btnExport.Enabled = true;
                }
            }
        }

        public DataViewForm(MainForm _mainForm, Tables _table, string conferenceFilter = null) : this(_mainForm, _table)
        {
            this.conferenceFilter = conferenceFilter;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            switch (table)
            {

                case Tables.PlayersAverageStats:
                    uploaded = true;
                    PlayersAverageStats.UploadPlayersAverageStats(this);
                    dataGridView.Columns["Player_ID"].Visible = false;
                    dataGridView.Columns["Position_ID"].Visible = false;
                    dataGridView.Columns["Team_ID"].Visible = false;
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    var customFont = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                case Tables.Players:
                    uploaded = true;
                    Players.UploadPlayers(this);
                    dataGridView.Columns["Player_ID"].Visible = false;
                    dataGridView.Columns["Position_ID"].Visible = false;
                    dataGridView.Columns["Team_ID"].Visible = false;
                    _dataTable = ((DataTable)dataGridView.DataSource).Copy();
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    var customFont1 = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont1;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont1;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                case Tables.PlayersTotalStats:
                    uploaded = true;
                    PlayersTotalStats.UploadPlayersTotalStats(this);
                    dataGridView.Columns["Player_ID"].Visible = false;
                    dataGridView.Columns["Position_ID"].Visible = false;
                    dataGridView.Columns["Team_ID"].Visible = false;
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    var customFont2 = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont2;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont2;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                case Tables.PlayersEfficiency:
                    uploaded = true;
                    PlayersEfficiency.UploadPlayersEfficiency(this);
                    dataGridView.Columns["Player_ID"].Visible = false;
                    dataGridView.Columns["Position_ID"].Visible = false;
                    dataGridView.Columns["Team_ID"].Visible = false;
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    var customFont3 = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont3;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont3;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                case Tables.Teams:
                    uploaded = true;
                    Teams.UploadTeams(this);
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;                    
                    var customFont4 = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont4;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont4;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                case Tables.TeamsRecords:
                    uploaded = true;
                    TeamsRecords.UploadTeamsRecords(this);
                    ApplyFilter();
                    dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    var customFont5 = new Font("Segoe UI Semibold", 10.25F);
                    dataGridView.DefaultCellStyle.Font = customFont5;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = customFont5;
                    dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                    dataGridView.DefaultCellStyle.BackColor = Color.White;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    dataGridView.EnableHeadersVisualStyles = false;
                    break;
                default:
                    break;
            }
            _dataTable = ((DataTable)dataGridView.DataSource).Copy();
            dataGridView.CurrentCell = null;
        }

        private void ApplyFilter()
        {
            if (conferenceFilter == null || dataTable.Rows.Count == 0)
            {
                return;
            }

            var easternDivisions = new List<string> { "Atlantic", "Central", "SouthEast" };
            var westernDivisions = new List<string> { "NorthWest", "Pacific", "SouthWest" };

            IEnumerable<DataRow> filteredRows = dataTable.AsEnumerable();

            if (conferenceFilter == "Eastern")
            {
                filteredRows = filteredRows.Where(r => easternDivisions.Contains(r.Field<string>("Дивизион")));
            }
            else if (conferenceFilter == "Western")
            {
                filteredRows = filteredRows.Where(r => westernDivisions.Contains(r.Field<string>("Дивизион")));
            }

            try
            {
                var filteredTable = filteredRows.CopyToDataTable();
                dataGridView.DataSource = filteredTable;
            }
            catch
            {
                var emptyTable = dataTable.Clone();
                dataGridView.DataSource = emptyTable;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                Players.AddPlayers(this);
            }
            else MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null) MessageBox.Show("Вы не выбрали ячейку для удаления!");
                else
                {
                    int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                    int idToDelete = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["Player_ID"].Value);
                    Players.DeletePlayers(this, idToDelete);
                }
            }
            else MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
                e.SuppressKeyPress = true; 
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                if (dataGridView.CurrentCell == null)
                {
                    MessageBox.Show("Вы не выбрали запись для изменения!");
                    return;
                }

                int selectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
                int recordID = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["Player_ID"].Value);
                string recordName = dataGridView.Rows[selectedRowIndex].Cells["Баскетболист"].Value.ToString();
                int teamId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["Team_ID"].Value);
                int positionId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["Position_ID"].Value);

                switch (table)
                {
                    case Tables.Players:
                        string country = dataGridView.Rows[selectedRowIndex].Cells["Страна"].Value.ToString();
                        string age = dataGridView.Rows[selectedRowIndex].Cells["Возраст"].Value.ToString();
                        ChangePlayersForm changePlayersForm = new ChangePlayersForm(recordName, country, age, teamId, positionId);
                        if (changePlayersForm.ShowDialog() == DialogResult.OK)
                        {
                            Players.ChangePlayers(this, recordID, changePlayersForm);
                        }
                        break;
                    case Tables.PlayersAverageStats:
                        string gp = dataGridView.Rows[selectedRowIndex].Cells["Игр сыграно"].Value.ToString();
                        string min = dataGridView.Rows[selectedRowIndex].Cells["Минут за игру"].Value.ToString();
                        string pts = dataGridView.Rows[selectedRowIndex].Cells["Очки за игру"].Value.ToString();
                        string reb = dataGridView.Rows[selectedRowIndex].Cells["Подборы за игру"].Value.ToString();
                        string ast = dataGridView.Rows[selectedRowIndex].Cells["Передачи за игру"].Value.ToString();
                        string stl = dataGridView.Rows[selectedRowIndex].Cells["Перехваты за игру"].Value.ToString();
                        string blk = dataGridView.Rows[selectedRowIndex].Cells["Блокшоты за игру"].Value.ToString();
                        string to = dataGridView.Rows[selectedRowIndex].Cells["Потери за игру"].Value.ToString();
                        string dd2 = dataGridView.Rows[selectedRowIndex].Cells["Количество дабл-даблов"].Value.ToString();
                        string td3 = dataGridView.Rows[selectedRowIndex].Cells["Количество трипл-даблов"].Value.ToString();

                        ChangePlayersAverageStatsForm changeStatsForm = new ChangePlayersAverageStatsForm(recordName, gp, min, pts, reb, ast, stl, blk, to, dd2, td3, teamId, positionId);
                        if (changeStatsForm.ShowDialog() == DialogResult.OK)
                        {
                            PlayersAverageStats.ChangePlayersAverageStats(this, recordID, changeStatsForm);
                        }
                        break;
                    case Tables.PlayersEfficiency:
                        string gp2 = dataGridView.Rows[selectedRowIndex].Cells["Игр сыграно"].Value.ToString();
                        string min2 = dataGridView.Rows[selectedRowIndex].Cells["Минут за игру"].Value.ToString();
                        string fgm = dataGridView.Rows[selectedRowIndex].Cells["Попаданий за игру"].Value.ToString();
                        string fga = dataGridView.Rows[selectedRowIndex].Cells["Бросков за игру"].Value.ToString();
                        string fg = dataGridView.Rows[selectedRowIndex].Cells["Процент попаданий в среднем"].Value.ToString();
                        string tpm = dataGridView.Rows[selectedRowIndex].Cells["3-очковых попаданий за игру"].Value.ToString();
                        string tpa = dataGridView.Rows[selectedRowIndex].Cells["3-очковых бросков за игру"].Value.ToString();
                        string tp = dataGridView.Rows[selectedRowIndex].Cells["Процент 3-очковых попаданий в среднем"].Value.ToString();
                        string ftm = dataGridView.Rows[selectedRowIndex].Cells["Штрафных попаданий за игру"].Value.ToString();
                        string fta = dataGridView.Rows[selectedRowIndex].Cells["Штрафных бросков за игру"].Value.ToString();
                        string ft = dataGridView.Rows[selectedRowIndex].Cells["Процент штрафных попаданий в среднем"].Value.ToString();

                        ChangePlayersEfficiencyForm changeEfficiencyForm = new ChangePlayersEfficiencyForm(recordName, gp2, min2, fgm, fga, fg, tpm, tpa, tp, ftm, fta, ft, teamId, positionId);
                        if (changeEfficiencyForm.ShowDialog() == DialogResult.OK)
                        {
                            PlayersEfficiency.ChangePlayersEfficiency(this, recordID, changeEfficiencyForm);
                        }
                        break;
                    case Tables.PlayersTotalStats:
                        string gp3 = dataGridView.Rows[selectedRowIndex].Cells["Игр сыграно"].Value.ToString();
                        string min3 = dataGridView.Rows[selectedRowIndex].Cells["Минут за игру"].Value.ToString();
                        string totalpts = dataGridView.Rows[selectedRowIndex].Cells["Тотал очков за сезон"].Value.ToString();
                        string totalreb = dataGridView.Rows[selectedRowIndex].Cells["Тотал подборов за сезон"].Value.ToString();
                        string totalast = dataGridView.Rows[selectedRowIndex].Cells["Тотал передач за сезон"].Value.ToString();
                        string totalstl = dataGridView.Rows[selectedRowIndex].Cells["Тотал перехватов за сезон"].Value.ToString();
                        string totalblk = dataGridView.Rows[selectedRowIndex].Cells["Тотал блокшотов за сезон"].Value.ToString();
                        string dd23 = dataGridView.Rows[selectedRowIndex].Cells["Количество дабл-даблов"].Value.ToString();
                        string td33 = dataGridView.Rows[selectedRowIndex].Cells["Количество трипл-даблов"].Value.ToString();

                        ChangePlayersTotalStatsForm changeTotalStatsForm = new ChangePlayersTotalStatsForm(recordName, gp3, min3, totalpts, totalreb, totalast, totalstl, totalblk, dd23, td33, teamId, positionId);
                        if (changeTotalStatsForm.ShowDialog() == DialogResult.OK)
                        {
                            PlayersTotalStats.ChangePlayersTotalStats(this, recordID, changeTotalStatsForm);
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Вы не загрузили данные!");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (uploaded)
            {
                switch (table)
                {
                    case Tables.Players:
                        Players.ExportToExcel(dataGridView);
                        break;
                    case Tables.PlayersAverageStats:
                        PlayersAverageStats.ExportToExcel(dataGridView);
                        break;
                    case Tables.PlayersEfficiency:
                        PlayersEfficiency.ExportToExcel(dataGridView);
                        break;
                    case Tables.PlayersTotalStats:
                        PlayersTotalStats.ExportToExcel(dataGridView);
                        break;
                    case Tables.Teams:
                        Teams.ExportToExcel(dataGridView);
                        break;
                    case Tables.TeamsRecords:
                        if (conferenceFilter == "Eastern")
                        {
                            TeamsRecords.ExportToExcel(dataGridView, "Eastern");
                        }
                        else if (conferenceFilter == "Western")
                        {
                            TeamsRecords.ExportToExcel(dataGridView, "Western");
                        }
                        break;
                }
            }
            else MessageBox.Show("Ошибка! Вы не загрузили данные!");
        }
    }
}