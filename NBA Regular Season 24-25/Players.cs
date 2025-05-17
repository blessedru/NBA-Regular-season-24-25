using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace NBA_Regular_Season_24_25
{
    internal class Players
    {
        public static void UploadPlayers(DataViewForm _DataViewForm)
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
                p.Country as Страна,
                p.Age as Возраст
            FROM Players p 
            JOIN Positions pos ON p.Position_ID = pos.Position_ID 
            JOIN Teams t ON p.Team_ID = t.Team_ID";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
            }
        }

        public static void AddPlayers(DataViewForm _DataViewForm)
        {
            PlayersForm playersForm = new PlayersForm();
            if (playersForm.ShowDialog() == DialogResult.OK)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();

                    int playerTimeID = 0, playerAverageStatsID = 0, playerUniqueStatsID = 0, playerFieldGoalsID = 0, playerPercentagesID = 0, playerTotalStatsID = 0;

                    using (SqlCommand cmdEfficiency = new SqlCommand("INSERT INTO PlayersTime (GP, MIN) OUTPUT INSERTED.PlayerTime_ID VALUES (NULL,NULL)", cn))
                    {
                        playerTimeID = (int)cmdEfficiency.ExecuteScalar();
                    }

                    using (SqlCommand cmdStats = new SqlCommand("INSERT INTO PlayersAverageStats (PTS, REB, AST, STL, BLK, [TO]) OUTPUT INSERTED.PlayerAverageStats_ID VALUES (NULL,NULL,NULL,NULL,NULL,NULL)", cn))
                    {
                        playerAverageStatsID = (int)cmdStats.ExecuteScalar();
                    }

                    using (SqlCommand cmdEfficiency = new SqlCommand("INSERT INTO PlayersUniqueStats (DD2, TD3) OUTPUT INSERTED.PlayerUniqueStats_ID VALUES (NULL,NULL)", cn))
                    {
                        playerUniqueStatsID = (int)cmdEfficiency.ExecuteScalar();
                    }

                    using (SqlCommand cmdEfficiency = new SqlCommand("INSERT INTO PlayersFieldGoals (FGM, FGA, [3PM], [3PA], FTM, FTA) OUTPUT INSERTED.PlayerFieldGoals_ID VALUES (NULL,NULL,NULL,NULL,NULL,NULL)", cn))
                    {
                        playerFieldGoalsID = (int)cmdEfficiency.ExecuteScalar();
                    }

                    using (SqlCommand cmdEfficiency = new SqlCommand("INSERT INTO PlayersPercentages ([FG%], [3P%], [FT%]) OUTPUT INSERTED.PlayerPercentages_ID VALUES (NULL,NULL,NULL)", cn))
                    {
                        playerPercentagesID = (int)cmdEfficiency.ExecuteScalar();
                    }

                    using (SqlCommand cmdEfficiency = new SqlCommand("INSERT INTO PlayersTotalStats ([Total Points], [Total Rebounds], [Total Assists], [Total Steals], [Total Blocks]) OUTPUT INSERTED.PlayerTotalStats_ID VALUES (NULL,NULL,NULL,NULL,NULL)", cn))
                    {
                        playerTotalStatsID = (int)cmdEfficiency.ExecuteScalar();
                    }

                    using (SqlCommand cmdPlayer = new SqlCommand("INSERT INTO Players (Name, Position_ID, Team_ID, Country, Age, PlayerTime_ID, PlayerAverageStats_ID, PlayerUniqueStats_ID, PlayerFieldGoals_ID, PlayerPercentages_ID, PlayerTotalStats_ID) VALUES (@Name, @PositionID, @TeamID, @Country, @Age, @PlayerTimeID, @PlayerAverageStatsID, @PlayerUniqueStatsID, @PlayerFieldGoalsID, @PlayerPercentagesID, @PlayerTotalStatsID)", cn))
                    {
                        cmdPlayer.Parameters.AddWithValue("@Name", playersForm.Player);
                        cmdPlayer.Parameters.AddWithValue("@PositionID", playersForm.SelectedPositionId);
                        cmdPlayer.Parameters.AddWithValue("@TeamID", playersForm.SelectedTeamId);
                        cmdPlayer.Parameters.AddWithValue("@Country", playersForm.Country);
                        cmdPlayer.Parameters.AddWithValue("@Age", playersForm.Age);
                        cmdPlayer.Parameters.AddWithValue("@PlayerTimeID", playerTimeID);
                        cmdPlayer.Parameters.AddWithValue("@PlayerAverageStatsID", playerAverageStatsID);
                        cmdPlayer.Parameters.AddWithValue("@PlayerUniqueStatsID", playerUniqueStatsID);
                        cmdPlayer.Parameters.AddWithValue("@PlayerFieldGoalsID", playerFieldGoalsID);
                        cmdPlayer.Parameters.AddWithValue("@PlayerPercentagesID", playerPercentagesID);
                        cmdPlayer.Parameters.AddWithValue("@PlayerTotalStatsID", playerTotalStatsID);
                        cmdPlayer.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Игрок успешно добавлен в таблицу.");
                    UploadPlayers(_DataViewForm);
                }
            }
        }

        public static bool IsPlayerExists(string playerName)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();
                    string sql = @"SELECT COUNT(*) FROM Players WHERE Name = @Name";
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Name", playerName);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке имени игрока: {ex.Message}");
                return false;
            }
        }

        public static void DeletePlayers(DataViewForm _DataViewForm, int playerID)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить этого игрока из таблицы?","Подтверждение удаления",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
                {
                    cn.Open();

                    var getIDsSql = @"SELECT PlayerTime_ID, PlayerAverageStats_ID, PlayerUniqueStats_ID, PlayerFieldGoals_ID, PlayerPercentages_ID, PlayerTotalStats_ID FROM Players WHERE Player_ID = @PlayerID";
                    int playerTimeID = 0, playerAverageStatsID = 0, playerUniqueStatsID = 0, playerFieldGoalsID = 0, playerPercentagesID = 0, playerTotalStatsID = 0;

                    using (SqlCommand cmd = new SqlCommand(getIDsSql, cn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", playerID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                playerTimeID = reader["PlayerTime_ID"] != DBNull.Value ? reader.GetInt32(0) : 0;
                                playerAverageStatsID = reader["PlayerAverageStats_ID"] != DBNull.Value ? reader.GetInt32(1) : 0;
                                playerUniqueStatsID = reader["PlayerUniqueStats_ID"] != DBNull.Value ? reader.GetInt32(2) : 0;
                                playerFieldGoalsID = reader["PlayerFieldGoals_ID"] != DBNull.Value ? reader.GetInt32(3) : 0;
                                playerPercentagesID = reader["PlayerPercentages_ID"] != DBNull.Value ? reader.GetInt32(4) : 0;
                                playerTotalStatsID = reader["PlayerTotalStats_ID"] != DBNull.Value ? reader.GetInt32(5) : 0;
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE Players SET PlayerTime_ID = NULL, PlayerAverageStats_ID = NULL, PlayerUniqueStats_ID = NULL, PlayerFieldGoals_ID = NULL, PlayerPercentages_ID = NULL, PlayerTotalStats_ID = NULL WHERE Player_ID = @PlayerID", cn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerID", playerID);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(@"DELETE FROM PlayersTime WHERE PlayerTime_ID = @PlayerTimeID;
                                                     DELETE FROM PlayersAverageStats WHERE PlayerAverageStats_ID = @PlayerAverageStatsID;
                                                     DELETE FROM PlayersUniqueStats WHERE PlayerUniqueStats_ID = @PlayerUniqueStatsID;
                                                     DELETE FROM PlayersFieldGoals WHERE PlayerFieldGoals_ID = @PlayerFieldGoalsID;
                                                     DELETE FROM PlayersPercentages WHERE PlayerPercentages_ID = @PlayerPercentagesID;
                                                     DELETE FROM PlayersTotalStats WHERE PlayerTotalStats_ID = @PlayerTotalStatsID;
                                                     DELETE FROM Players WHERE Player_ID = @PlayerID;", cn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerTimeID", playerTimeID);
                        cmd.Parameters.AddWithValue("@PlayerAverageStatsID", playerAverageStatsID);
                        cmd.Parameters.AddWithValue("@PlayerUniqueStatsID", playerUniqueStatsID);
                        cmd.Parameters.AddWithValue("@PlayerFieldGoalsID", playerFieldGoalsID);
                        cmd.Parameters.AddWithValue("@PlayerPercentagesID", playerPercentagesID);
                        cmd.Parameters.AddWithValue("@PlayerTotalStatsID", playerTotalStatsID);
                        cmd.Parameters.AddWithValue("@PlayerID", playerID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Игрок и его статистика успешно удалёны из таблицы.");
                }

                UploadPlayers(_DataViewForm);
            }
        }

        public static void ChangePlayers(DataViewForm _DataViewForm, int playerID, ChangePlayersForm changePlayersForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                string sqlCheck = @"SELECT Name, Country, Age, Position_ID, Team_ID FROM Players WHERE Player_ID = @PlayerID";
                var cmdCheck = new SqlCommand(sqlCheck, cn);
                cmdCheck.Parameters.AddWithValue("@PlayerID", playerID);

                using (SqlDataReader reader = cmdCheck.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string currentName = reader["Name"].ToString();
                        string currentCountry = reader["Country"].ToString();
                        string currentAge = reader["Age"].ToString();
                        int currentPositionId = Convert.ToInt32(reader["Position_ID"]);
                        int currentTeamId = Convert.ToInt32(reader["Team_ID"]);

                        if (currentName == changePlayersForm.Player &&
                            currentCountry == changePlayersForm.Country &&
                            currentAge == changePlayersForm.Age &&
                            currentPositionId == changePlayersForm.SelectedPositionId &&
                            currentTeamId == changePlayersForm.SelectedTeamId)
                        {
                            MessageBox.Show("Никаких изменений не произошло!");
                            return;
                        }
                    }
                }

                var sqlUpdate = @"UPDATE [dbo].[Players] SET 
                        Name = @Name, 
                        Country = @Country, 
                        Age = @Age, 
                        Position_ID = @PositionID, 
                        Team_ID = @TeamID 
                        WHERE Player_ID = @PlayerID";

                var cmdUpdate = new SqlCommand(sqlUpdate, cn);
                cmdUpdate.Parameters.AddWithValue("@PlayerID", playerID);
                cmdUpdate.Parameters.AddWithValue("@Name", changePlayersForm.Player);
                cmdUpdate.Parameters.AddWithValue("@Country", changePlayersForm.Country);
                cmdUpdate.Parameters.AddWithValue("@Age", changePlayersForm.Age);
                cmdUpdate.Parameters.AddWithValue("@PositionID", changePlayersForm.SelectedPositionId);
                cmdUpdate.Parameters.AddWithValue("@TeamID", changePlayersForm.SelectedTeamId);

                try
                {
                    cmdUpdate.ExecuteNonQuery();
                    MessageBox.Show("Данные игрока успешно изменены!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Ошибка при изменении данных игрока: {ex.Message}");
                }
            }

            UploadPlayers(_DataViewForm);
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

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PlayersData.xlsx");
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