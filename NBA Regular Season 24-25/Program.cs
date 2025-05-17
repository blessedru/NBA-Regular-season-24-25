using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Mutex mutex = new Mutex(true, "NBA_Regular_Season_24_25_Mutex");

        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Приложение уже запущено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UsersForm());

            mutex.ReleaseMutex();
        }
    }
}