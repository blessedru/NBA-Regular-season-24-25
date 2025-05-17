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
    internal class PlayersTotalStats
    {
        public static void UploadPlayersTotalStats(DataViewForm _DataViewForm)
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
                pts.[Total Points] as [Тотал очков за сезон], 
                pts.[Total Rebounds] as [Тотал подборов за сезон], 
                pts.[Total Assists] as [Тотал передач за сезон], 
                pts.[Total Steals] as [Тотал перехватов за сезон], 
                pts.[Total Blocks] as [Тотал блокшотов за сезон], 
                pus.DD2 as [Количество дабл-даблов],
                pus.TD3 as [Количество трипл-даблов]
            FROM Players p 
            JOIN Positions pos ON p.Position_ID = pos.Position_ID 
            JOIN Teams t ON p.Team_ID = t.Team_ID 
            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
            LEFT JOIN PlayersTotalStats pts ON p.PlayerTotalStats_ID = pts.PlayerTotalStats_ID
            LEFT JOIN PlayersUniqueStats pus ON p.PlayerUniqueStats_ID = pus.PlayerUniqueStats_ID";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
            }
        }

        public static void ChangePlayersTotalStats(DataViewForm _DataViewForm, int playerID, ChangePlayersTotalStatsForm changeForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();

                string sqlCheck = @"SELECT Name, Position_ID, Team_ID, GP, MIN, [Total Points], [Total Rebounds], [Total Assists], [Total Steals], [Total Blocks], DD2, TD3
                            FROM Players p
                            LEFT JOIN PlayersTime pt ON p.PlayerTime_ID = pt.PlayerTime_ID
                            LEFT JOIN PlayersTotalStats pas ON p.PlayerTotalStats_ID = pas.PlayerTotalStats_ID
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
                            string currentTotalPTS = reader["Total Points"].ToString();
                            string currentTotalREB = reader["Total Rebounds"].ToString();
                            string currentTotalAST = reader["Total Assists"].ToString();
                            string currentTotalSTL = reader["Total Steals"].ToString();
                            string currentTotalBLK = reader["Total Blocks"].ToString();
                            string currentDD2 = reader["DD2"].ToString();
                            string currentTD3 = reader["TD3"].ToString();

                            if (currentName == changeForm.Player && currentPositionID == changeForm.SelectedPositionId &&
                                currentTeamID == changeForm.SelectedTeamId && currentGP == changeForm.GP &&
                                currentMIN == changeForm.MIN && currentTotalPTS == changeForm.TotalPTS &&
                                currentTotalREB == changeForm.TotalREB && currentTotalAST == changeForm.TotalAST &&
                                currentTotalSTL == changeForm.TotalSTL && currentTotalBLK == changeForm.TotalBLK &&
                                currentDD2 == changeForm.DD2 && currentTD3 == changeForm.TD3)
                            {
                                MessageBox.Show("Никаких изменений не произошло!");
                                return;
                            }
                        }
                    }
                }

                string sqlUpdate = @"UPDATE [dbo].[Players] SET Name = @Name, Position_ID = @PositionID, Team_ID = @TeamID WHERE Player_ID = @PlayerID;
                                     UPDATE [dbo].[PlayersTime] SET GP = @GP, MIN = @MIN WHERE PlayerTime_ID = (SELECT PlayerTime_ID FROM Players WHERE Player_ID = @PlayerID);
                                     UPDATE [dbo].[PlayersTotalStats] SET [Total Points] = @TotalPTS, [Total Rebounds] = @TotalREB, [Total Assists] = @TotalAST, [Total Steals] = @TotalSTL, [Total Blocks] = @TotalBLK WHERE PlayerTotalStats_ID = (SELECT PlayerTotalStats_ID FROM Players WHERE Player_ID = @PlayerID);
                                     UPDATE [dbo].[PlayersUniqueStats] SET DD2 = @DD2, TD3 = @TD3 WHERE PlayerUniqueStats_ID = (SELECT PlayerUniqueStats_ID FROM Players WHERE Player_ID = @PlayerID);";

                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, cn))
                {
                    cmdUpdate.Parameters.AddWithValue("@PlayerID", playerID);
                    cmdUpdate.Parameters.AddWithValue("@Name", changeForm.Player);
                    cmdUpdate.Parameters.AddWithValue("@PositionID", changeForm.SelectedPositionId);
                    cmdUpdate.Parameters.AddWithValue("@TeamID", changeForm.SelectedTeamId);
                    cmdUpdate.Parameters.AddWithValue("@GP", changeForm.GP);
                    cmdUpdate.Parameters.AddWithValue("@MIN", changeForm.MIN);
                    cmdUpdate.Parameters.AddWithValue("@TotalPTS", changeForm.TotalPTS);
                    cmdUpdate.Parameters.AddWithValue("@TotalREB", changeForm.TotalREB);
                    cmdUpdate.Parameters.AddWithValue("@TotalAST", changeForm.TotalAST);
                    cmdUpdate.Parameters.AddWithValue("@TotalSTL", changeForm.TotalSTL);
                    cmdUpdate.Parameters.AddWithValue("@TotalBLK", changeForm.TotalBLK);
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

            UploadPlayersTotalStats(_DataViewForm);
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

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PlayersTotalStatsData.xlsx");
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
