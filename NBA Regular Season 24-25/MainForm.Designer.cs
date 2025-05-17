namespace NBA_Regular_Season_24_25
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlayersAverageStats = new System.Windows.Forms.Button();
            this.btnPlayers = new System.Windows.Forms.Button();
            this.btnPlayersTotalStats = new System.Windows.Forms.Button();
            this.btnEfficiency = new System.Windows.Forms.Button();
            this.btnTeams = new System.Windows.Forms.Button();
            this.btnRecordsWest = new System.Windows.Forms.Button();
            this.btnRecordsEast = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPlayersAverageStats
            // 
            this.btnPlayersAverageStats.BackColor = System.Drawing.Color.White;
            this.btnPlayersAverageStats.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlayersAverageStats.ForeColor = System.Drawing.Color.Black;
            this.btnPlayersAverageStats.Location = new System.Drawing.Point(157, 479);
            this.btnPlayersAverageStats.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlayersAverageStats.Name = "btnPlayersAverageStats";
            this.btnPlayersAverageStats.Size = new System.Drawing.Size(200, 69);
            this.btnPlayersAverageStats.TabIndex = 1;
            this.btnPlayersAverageStats.Text = "Средняя статистика игроков по сезону";
            this.btnPlayersAverageStats.UseVisualStyleBackColor = false;
            this.btnPlayersAverageStats.Click += new System.EventHandler(this.btnAverageStats_Click);
            // 
            // btnPlayers
            // 
            this.btnPlayers.BackColor = System.Drawing.Color.White;
            this.btnPlayers.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlayers.ForeColor = System.Drawing.Color.Black;
            this.btnPlayers.Location = new System.Drawing.Point(276, 327);
            this.btnPlayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlayers.Name = "btnPlayers";
            this.btnPlayers.Size = new System.Drawing.Size(171, 69);
            this.btnPlayers.TabIndex = 2;
            this.btnPlayers.Text = "Информация об игроках";
            this.btnPlayers.UseVisualStyleBackColor = false;
            this.btnPlayers.Click += new System.EventHandler(this.btnPlayers_Click);
            // 
            // btnPlayersTotalStats
            // 
            this.btnPlayersTotalStats.BackColor = System.Drawing.Color.White;
            this.btnPlayersTotalStats.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlayersTotalStats.ForeColor = System.Drawing.Color.Black;
            this.btnPlayersTotalStats.Location = new System.Drawing.Point(361, 479);
            this.btnPlayersTotalStats.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlayersTotalStats.Name = "btnPlayersTotalStats";
            this.btnPlayersTotalStats.Size = new System.Drawing.Size(200, 69);
            this.btnPlayersTotalStats.TabIndex = 3;
            this.btnPlayersTotalStats.Text = "Статистика тоталов игроков за сезон";
            this.btnPlayersTotalStats.UseVisualStyleBackColor = false;
            this.btnPlayersTotalStats.Click += new System.EventHandler(this.btnPlayersTotalStats_Click);
            // 
            // btnEfficiency
            // 
            this.btnEfficiency.BackColor = System.Drawing.Color.White;
            this.btnEfficiency.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEfficiency.ForeColor = System.Drawing.Color.Black;
            this.btnEfficiency.Location = new System.Drawing.Point(276, 652);
            this.btnEfficiency.Margin = new System.Windows.Forms.Padding(2);
            this.btnEfficiency.Name = "btnEfficiency";
            this.btnEfficiency.Size = new System.Drawing.Size(171, 69);
            this.btnEfficiency.TabIndex = 4;
            this.btnEfficiency.Text = "Эффективность игроков";
            this.btnEfficiency.UseVisualStyleBackColor = false;
            this.btnEfficiency.Click += new System.EventHandler(this.btnEfficiency_Click);
            // 
            // btnTeams
            // 
            this.btnTeams.BackColor = System.Drawing.Color.White;
            this.btnTeams.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTeams.ForeColor = System.Drawing.Color.Black;
            this.btnTeams.Location = new System.Drawing.Point(276, 209);
            this.btnTeams.Margin = new System.Windows.Forms.Padding(2);
            this.btnTeams.Name = "btnTeams";
            this.btnTeams.Size = new System.Drawing.Size(171, 69);
            this.btnTeams.TabIndex = 5;
            this.btnTeams.Text = "Информация о командах";
            this.btnTeams.UseVisualStyleBackColor = false;
            this.btnTeams.Click += new System.EventHandler(this.btnTeams_Click);
            // 
            // btnRecordsWest
            // 
            this.btnRecordsWest.BackColor = System.Drawing.Color.White;
            this.btnRecordsWest.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRecordsWest.ForeColor = System.Drawing.Color.Black;
            this.btnRecordsWest.Location = new System.Drawing.Point(361, 112);
            this.btnRecordsWest.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecordsWest.Name = "btnRecordsWest";
            this.btnRecordsWest.Size = new System.Drawing.Size(181, 69);
            this.btnRecordsWest.TabIndex = 6;
            this.btnRecordsWest.Text = "Положение команд - Запад";
            this.btnRecordsWest.UseVisualStyleBackColor = false;
            this.btnRecordsWest.Click += new System.EventHandler(this.btnRecordsWest_Click);
            // 
            // btnRecordsEast
            // 
            this.btnRecordsEast.BackColor = System.Drawing.Color.White;
            this.btnRecordsEast.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRecordsEast.ForeColor = System.Drawing.Color.Black;
            this.btnRecordsEast.Location = new System.Drawing.Point(176, 112);
            this.btnRecordsEast.Margin = new System.Windows.Forms.Padding(2);
            this.btnRecordsEast.Name = "btnRecordsEast";
            this.btnRecordsEast.Size = new System.Drawing.Size(181, 69);
            this.btnRecordsEast.TabIndex = 7;
            this.btnRecordsEast.Text = "Положение команд - Восток";
            this.btnRecordsEast.UseVisualStyleBackColor = false;
            this.btnRecordsEast.Click += new System.EventHandler(this.btnRecordsEast_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.White;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUsers.ForeColor = System.Drawing.Color.Black;
            this.btnUsers.Location = new System.Drawing.Point(176, 11);
            this.btnUsers.Margin = new System.Windows.Forms.Padding(2);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(181, 69);
            this.btnUsers.TabIndex = 8;
            this.btnUsers.Text = "Руководство пользователя";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.BackColor = System.Drawing.Color.White;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHelp.ForeColor = System.Drawing.Color.Black;
            this.btnHelp.Location = new System.Drawing.Point(361, 11);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(181, 69);
            this.btnHelp.TabIndex = 9;
            this.btnHelp.Text = "Обратная связь";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NBA_Regular_Season_24_25.Properties.Resources.NBARivals;
            this.ClientSize = new System.Drawing.Size(734, 732);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.btnRecordsEast);
            this.Controls.Add(this.btnRecordsWest);
            this.Controls.Add(this.btnTeams);
            this.Controls.Add(this.btnEfficiency);
            this.Controls.Add(this.btnPlayersTotalStats);
            this.Controls.Add(this.btnPlayers);
            this.Controls.Add(this.btnPlayersAverageStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NBA Regular Season 2024-25";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnPlayersAverageStats;
        private System.Windows.Forms.Button btnPlayers;
        private System.Windows.Forms.Button btnPlayersTotalStats;
        private System.Windows.Forms.Button btnEfficiency;
        private System.Windows.Forms.Button btnTeams;
        private System.Windows.Forms.Button btnRecordsWest;
        private System.Windows.Forms.Button btnRecordsEast;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnHelp;
    }
}