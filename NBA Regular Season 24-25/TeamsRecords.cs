using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    internal class TeamsRecords
    {
        public static void UploadTeamsRecords(DataViewForm _DataViewForm)
        {
            using (SqlConnection cn = new SqlConnection(MainForm.connectionString))
            {
                cn.Open();
                var sql = @"SELECT 
                t.[Team Name] as Команда, 
                td.Division as Дивизион,
                ttr.Wins as Победы,
                ttr.Losses as Поражения,
                ttr.[Winning Percentage] as [Процент побед],
                tsr.[Home Record] as [Результат дома],
                tsr.[Away Record] as [Результат в гостях],
                tsr.[Division Record] as [Результат внутри дивизиона],
                tsr.[Conference Record] as [Результат внутри конференции],
                ts.PPG as [Забитых очков за матч],
                ts.[OPP PPG] as [Пропущенных очков за матч],
                ts.DIFF as [Разница забитых и пропущенных очков за матч]
            FROM Teams t 
            JOIN TeamsDivisions td ON t.Division_ID = td.Division_ID
            JOIN TeamsTotalRecords ttr ON t.Team_ID = ttr.Team_ID
            JOIN TeamsSpecificRecords tsr ON t.Team_ID = tsr.Team_ID
            JOIN TeamsStats ts ON t.Team_ID = ts.Team_ID";

                var cmd = new SqlCommand(sql, cn);

                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                _DataViewForm.dataTable = dataTable;

                ds.Fill(dataTable);
                _DataViewForm.dataGridView.DataSource = dataTable;
            }
        }

        public static void ExportToExcel(DataGridView dgv, string conferenceFilter)
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

                string fileName = conferenceFilter == "Eastern" ? "EasternConferenceStats.xlsx" : "WesternConferenceStats.xlsx";
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

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
