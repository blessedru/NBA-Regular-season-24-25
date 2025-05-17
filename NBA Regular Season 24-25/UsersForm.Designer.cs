using System.Windows.Forms;

namespace NBA_Regular_Season_24_25
{
    partial class UsersForm
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
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnGuest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdmin
            // 
            this.btnAdmin.BackColor = System.Drawing.Color.White;
            this.btnAdmin.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAdmin.ForeColor = System.Drawing.Color.Black;
            this.btnAdmin.Location = new System.Drawing.Point(57, 187);
            this.btnAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(199, 84);
            this.btnAdmin.TabIndex = 2;
            this.btnAdmin.Text = "Войти с правами редактора";
            this.btnAdmin.UseVisualStyleBackColor = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // btnGuest
            // 
            this.btnGuest.BackColor = System.Drawing.Color.White;
            this.btnGuest.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGuest.ForeColor = System.Drawing.Color.Black;
            this.btnGuest.Location = new System.Drawing.Point(57, 283);
            this.btnGuest.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuest.Name = "btnGuest";
            this.btnGuest.Size = new System.Drawing.Size(199, 84);
            this.btnGuest.TabIndex = 3;
            this.btnGuest.Text = "Войти с правами обычного пользователя";
            this.btnGuest.UseVisualStyleBackColor = false;
            this.btnGuest.Click += new System.EventHandler(this.btnGuest_Click);
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BackgroundImage = global::NBA_Regular_Season_24_25.Properties.Resources.NBALogo;
            this.ClientSize = new System.Drawing.Size(574, 579);
            this.Controls.Add(this.btnGuest);
            this.Controls.Add(this.btnAdmin);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "UsersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnGuest;
    }
}