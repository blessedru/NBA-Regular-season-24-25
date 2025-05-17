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
    internal class PlayersAverageStats
    {
        public static void UploadPlayersAverageStats(DataViewForm _DataViewForm)
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
                pas.PTS as [Очки за игру], 
                pas.REB as [Подборы за игру], 
                pas.AST as [Передачи за игру], 
                pas.STL as [Перехваты за игру], 
                pas.BLK as [Блокшоты за игру], 
                pas.[TO] as [Потери за игру],
                pus.DD2 as [Количество дабл-даблов],
                pus.TD3 as [Количество трипл-даблов]
            FROM Players p 
            JOIN Positions pos ON p.Position_ID = pos.Position_ID 
            JOIN Teams t ON p.Team_ID = t.Team_ID 
            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
            LEFT JOIN PlayersAverageStats pas ON p.PlayerAverageStats_ID = pas.PlayerAverageStats_ID
            LEFT JOIN PlayersUniqueStats pus ON p.PlayerUniqueStats_ID = pus.PlayerUniqueStats_ID";
                
                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
            }
        }

        public static void ChangePlayersAverageStats(DataViewForm _DataViewForm, int playerID, ChangePlayersAverageStatsForm changeForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();

                string sqlCheck = @"SELECT Name, Position_ID, Team_ID, GP, MIN, PTS, REB, AST, STL, BLK, [TO], DD2, TD3
                            FROM Players p
                            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
                            LEFT JOIN PlayersAverageStats pas ON p.PlayerAverageStats_ID = pas.PlayerAverageStats_ID
                            LEFT JOIN PlayersUniqueStats pus ON p.PlayerUniqueStats_ID = pus.PlayerUniqueStats_ID
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
                            string currentPTS = reader["PTS"].ToString();
                            string currentREB = reader["REB"].ToString();
                            string currentAST = reader["AST"].ToString();
                            string currentSTL = reader["STL"].ToString();
                            string currentBLK = reader["BLK"].ToString();
                            string currentTO = reader["TO"].ToString();
                            string currentDD2 = reader["DD2"].ToString();
                            string currentTD3 = reader["TD3"].ToString();

                            if (currentName == changeForm.Player && currentPositionID == changeForm.SelectedPositionId &&
                                currentTeamID == changeForm.SelectedTeamId && currentGP == changeForm.GP &&
                                currentMIN == changeForm.MIN && currentPTS == changeForm.PTS &&
                                currentREB == changeForm.REB && currentAST == changeForm.AST &&
                                currentSTL == changeForm.STL && currentBLK == changeForm.BLK &&
                                currentTO == changeForm.TO && currentDD2 == changeForm.DD2 &&
                                currentTD3 == changeForm.TD3)
                            {
                                MessageBox.Show("Никаких изменений не произошло!");
                                return;
                            }
                        }
                    }
                }

                string sqlUpdate = @"UPDATE [dbo].[Players] SET Name = @Name, Position_ID = @PositionID, Team_ID = @TeamID WHERE Player_ID = @PlayerID;
                                     UPDATE [dbo].[PlayersTime] SET GP = @GP, MIN = @MIN WHERE PlayerTime_ID = (SELECT PlayerTime_ID FROM Players WHERE Player_ID = @PlayerID);
                                     UPDATE [dbo].[PlayersAverageStats] SET PTS = @PTS, REB = @REB, AST = @AST, STL = @STL, BLK = @BLK, [TO] = @TO WHERE PlayerAverageStats_ID = (SELECT PlayerAverageStats_ID FROM Players WHERE Player_ID = @PlayerID);
                                     UPDATE [dbo].[PlayersUniqueStats] SET DD2 = @DD2, TD3 = @TD3 WHERE PlayerUniqueStats_ID = (SELECT PlayerUniqueStats_ID FROM Players WHERE Player_ID = @PlayerID);";

                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, cn))
                {
                    cmdUpdate.Parameters.AddWithValue("@PlayerID", playerID);
                    cmdUpdate.Parameters.AddWithValue("@Name", changeForm.Player);
                    cmdUpdate.Parameters.AddWithValue("@PositionID", changeForm.SelectedPositionId);
                    cmdUpdate.Parameters.AddWithValue("@TeamID", changeForm.SelectedTeamId);
                    cmdUpdate.Parameters.AddWithValue("@GP", changeForm.GP);
                    cmdUpdate.Parameters.AddWithValue("@MIN", changeForm.MIN);
                    cmdUpdate.Parameters.AddWithValue("@PTS", changeForm.PTS);
                    cmdUpdate.Parameters.AddWithValue("@REB", changeForm.REB);
                    cmdUpdate.Parameters.AddWithValue("@AST", changeForm.AST);
                    cmdUpdate.Parameters.AddWithValue("@STL", changeForm.STL);
                    cmdUpdate.Parameters.AddWithValue("@BLK", changeForm.BLK);
                    cmdUpdate.Parameters.AddWithValue("@TO", changeForm.TO);
                    cmdUpdate.Parameters.AddWithValue("@DD2", changeForm.DD2);
                    cmdUpdate.Parameters.AddWithValue("@TD3", changeForm.TD3);

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

            UploadPlayersAverageStats(_DataViewForm);
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

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PlayersAverageStatsData.xlsx");
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