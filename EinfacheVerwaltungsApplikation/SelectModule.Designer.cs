namespace ManageAdministerExalt
{
    partial class SelectModule
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_module = new System.Windows.Forms.ComboBox();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_path = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_user = new System.Windows.Forms.TextBox();
            this.tb_pwd = new System.Windows.Forms.TextBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_module
            // 
            this.cb_module.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_module.FormattingEnabled = true;
            this.cb_module.Location = new System.Drawing.Point(73, 3);
            this.cb_module.Name = "cb_module";
            this.cb_module.Size = new System.Drawing.Size(204, 21);
            this.cb_module.TabIndex = 0;
            this.cb_module.SelectionChangeCommitted += new System.EventHandler(this.cb_module_SelectionChangeCommitted);
            // 
            // tb_path
            // 
            this.tb_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_path.Location = new System.Drawing.Point(73, 30);
            this.tb_path.Name = "tb_path";
            this.tb_path.Size = new System.Drawing.Size(204, 20);
            this.tb_path.TabIndex = 1;
            this.tb_path.Click += new System.EventHandler(this.tb_path_Click);
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(283, 30);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(64, 20);
            this.btn_browse.TabIndex = 2;
            this.btn_browse.Text = "...";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_exit.Location = new System.Drawing.Point(3, 134);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(64, 23);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ok.Location = new System.Drawing.Point(73, 134);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(204, 23);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "eva_db.sqlite";
            this.ofd.ReadOnlyChecked = true;
            this.ofd.RestoreDirectory = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_module, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_browse, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_ok, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.tb_path, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_path, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_exit, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tb_user, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_pwd, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tb_name, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 159);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datenquelle";
            // 
            // lb_path
            // 
            this.lb_path.AutoSize = true;
            this.lb_path.Location = new System.Drawing.Point(3, 27);
            this.lb_path.Name = "lb_path";
            this.lb_path.Size = new System.Drawing.Size(29, 13);
            this.lb_path.TabIndex = 1;
            this.lb_path.Text = "Pfad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Passwort";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "DB-Name";
            // 
            // tb_user
            // 
            this.tb_user.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_user.Enabled = false;
            this.tb_user.Location = new System.Drawing.Point(73, 56);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(204, 20);
            this.tb_user.TabIndex = 5;
            // 
            // tb_pwd
            // 
            this.tb_pwd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_pwd.Enabled = false;
            this.tb_pwd.Location = new System.Drawing.Point(73, 82);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.Size = new System.Drawing.Size(204, 20);
            this.tb_pwd.TabIndex = 6;
            // 
            // tb_name
            // 
            this.tb_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_name.Enabled = false;
            this.tb_name.Location = new System.Drawing.Point(73, 108);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(204, 20);
            this.tb_name.TabIndex = 7;
            // 
            // SelectModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 159);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectModule";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MAX :: Modul-Auswahl";
            this.Load += new System.EventHandler(this.SelectModule_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_module;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.TextBox tb_pwd;
        private System.Windows.Forms.TextBox tb_name;
    }
}

