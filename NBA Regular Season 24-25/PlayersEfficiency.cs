using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace NBA_Regular_Season_24_25
{
    internal class PlayersEfficiency
    {
        public static void UploadPlayersEfficiency(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT 
                p.Player_ID as Player_ID,
                p.Name as Баскетболист, 
                pos.Position_ID,
                pos.POS as Позиция, 
                t.Team_ID,
                t.[Team Name] as Команда,
                pt.GP as [Игр сыграно],
                pt.MIN as [Минут за игру],
                pfg.FGM as [Попаданий за игру],
                pfg.FGA as [Бросков за игру],
                pP.[FG%] as [Процент попаданий в среднем],
                pfg.[3PM] as [3-очковых попаданий за игру],
                pfg.[3PA] as [3-очковых бросков за игру],
                pP.[3P%] as [Процент 3-очковых попаданий в среднем],
                pfg.FTM as [Штрафных попаданий за игру],
                pfg.FTA as [Штрафных бросков за игру],
                pP.[FT%] as [Процент штрафных попаданий в среднем]
            FROM Players p 
            JOIN Positions pos ON p.Position_ID = pos.Position_ID 
            JOIN Teams t ON p.Team_ID = t.Team_ID 
            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
            LEFT JOIN PlayersFieldGoals pfg ON p.PlayerFieldGoals_ID = pfg.PlayerFieldGoals_ID
            LEFT JOIN PlayersPercentages pp ON p.PlayerPercentages_ID = pp.PlayerPercentages_ID";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
            }
        }

        public static void ChangePlayersEfficiency(DataViewForm _DataViewForm, int playerID, ChangePlayersEfficiencyForm changeForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();

                string sqlCheck = @"SELECT Name, Position_ID, Team_ID, GP, MIN, FGM, FGA, [FG%], [3PM], [3PA], [3P%], FTM, FTA, [FT%] 
                            FROM Players p
                            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
                            LEFT JOIN PlayersFieldGoals pfg ON p.PlayerFieldGoals_ID = pfg.PlayerFieldGoals_ID
                            LEFT JOIN PlayersPercentages pp ON p.PlayerPercentages_ID = pp.PlayerPercentages_ID
                            WHERE p.Player_ID = @PlayerID";

                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, cn))
                {
                    cmdCheck.Parameters.AddWithValue("@PlayerID", playerID);
                    using (SqlDataReader reader = cmdCheck.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentName = reader["Name"].ToString();
                            int currentPositionID = Convert.ToInt32(reader["Position_ID"]);
                            int currentTeamID = Convert.ToInt32(reader["Team_ID"]);
                            string currentGP = reader["GP"].ToString();
                            string currentMIN = reader["MIN"].ToString();
                            string currentFGM = reader["FGM"].ToString();
                            string currentFGA = reader["FGA"].ToString();
                            string currentFG = reader["FG%"].ToString();
                            string currentTPM = reader["3PM"].ToString();
                            string currentTPA = reader["3PA"].ToString();
                            string currentTP = reader["3P%"].ToString();
                            string currentFTM = reader["FTM"].ToString();
                            string currentFTA = reader["FTA"].ToString();
                            string currentFT = reader["FT%"].ToString();

                            if (currentName == changeForm.Player && currentPositionID == changeForm.SelectedPositionId &&
                                currentTeamID == changeForm.SelectedTeamId && currentGP == changeForm.GP &&
                                currentMIN == changeForm.MIN && currentFGM == changeForm.FGM &&
                                currentFGA == changeForm.FGA && currentFG == changeForm.FG &&
                                currentTPM == changeForm.TPM && currentTPA == changeForm.TPA &&
                                currentTP == changeForm.TP && currentFTM == changeForm.FTM &&
                                currentFTA == changeForm.FTA && currentFT == changeForm.FT)
                            {
                                MessageBox.Show("Никаких изменений не произошло!");
                                return;
                            }
                        }
                    }
                }

                string sqlUpdate = @"UPDATE [dbo].[Players] SET Name = @Name, Position_ID = @PositionID, Team_ID = @TeamID WHERE Player_ID = @PlayerID;
                             UPDATE [dbo].[PlayersTime] SET GP = @GP, MIN = @MIN WHERE PlayerTime_ID = (SELECT PlayerTime_ID FROM Players WHERE Player_ID = @PlayerID);
                             UPDATE [dbo].[PlayersFieldGoals] SET FGM = @FGM, FGA = @FGA, [3PM] = @TPM, [3PA] = @TPA, FTM = @FTM, FTA = @FTA WHERE PlayerFieldGoals_ID = (SELECT PlayerFieldGoals_ID FROM Players WHERE Player_ID = @PlayerID);
                             UPDATE [dbo].[PlayersPercentages] SET [FG%] = @FG, [3P%] = @TP, [FT%] = @FT WHERE PlayerPercentages_ID = (SELECT PlayerPercentages_ID FROM Players WHERE Player_ID = @PlayerID);";

                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, cn))
                {
                    cmdUpdate.Parameters.AddWithValue("@PlayerID", playerID);
                    cmdUpdate.Parameters.AddWithValue("@Name", changeForm.Player);
                    cmdUpdate.Parameters.AddWithValue("@PositionID", changeForm.SelectedPositionId);
                    cmdUpdate.Parameters.AddWithValue("@TeamID", changeForm.SelectedTeamId);
                    cmdUpdate.Parameters.AddWithValue("@GP", changeForm.GP);
                    cmdUpdate.Parameters.AddWithValue("@MIN", changeForm.MIN);
                    cmdUpdate.Parameters.AddWithValue("@FGM", changeForm.FGM);
                    cmdUpdate.Parameters.AddWithValue("@FGA", changeForm.FGA);
                    cmdUpdate.Parameters.AddWithValue("@FG", changeForm.FG);
                    cmdUpdate.Parameters.AddWithValue("@TPM", changeForm.TPM);
                    cmdUpdate.Parameters.AddWithValue("@TPA", changeForm.TPA);
                    cmdUpdate.Parameters.AddWithValue("@TP", changeForm.TP);
                    cmdUpdate.Parameters.AddWithValue("@FTM", changeForm.FTM);
                    cmdUpdate.Parameters.AddWithValue("@FTA", changeForm.FTA);
                    cmdUpdate.Parameters.AddWithValue("@FT", changeForm.FT);

                    try
                    {
                        cmdUpdate.ExecuteNonQuery();
                        MessageBox.Show("Данные игрока и его статистика успешно обновлены!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Ошибка при изменении данных игрока: {ex.Message}");
                    }
                }
            }

            UploadPlayersEfficiency(_DataViewForm);
        }

        public static void ExportToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        Excel.Range cell = worksheet.Cells[i + 2, j + 1];
                        cell.NumberFormat = "@";
                        cell.Value = dgv.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                worksheet.Columns.AutoFit();

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PlayersEfficiencyData.xlsx");
                worksheet.SaveAs(filePath);
                excelApp.Quit();

                MessageBox.Show($"Файл успешно сохранён на рабочем столе: {filePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Excel: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
