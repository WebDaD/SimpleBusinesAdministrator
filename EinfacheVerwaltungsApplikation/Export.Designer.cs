namespace ManageAdministerExalt
{
    partial class Export
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_data_head = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_export_type = new System.Windows.Forms.ComboBox();
            this.cb_template = new System.Windows.Forms.ComboBox();
            this.cb_open_export = new System.Windows.Forms.CheckBox();
            this.cb_Mail = new System.Windows.Forms.CheckBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_data_head, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cb_export_type, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_template, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_open_export, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cb_Mail, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_exit, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_export, 1, 5);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 186);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datensatz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Exportformat";
            // 
            // lb_data_head
            // 
            this.lb_data_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb_data_head.Location = new System.Drawing.Point(170, 0);
            this.lb_data_head.Name = "lb_data_head";
            this.lb_data_head.Size = new System.Drawing.Size(162, 23);
            this.lb_data_head.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vorlage";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Export-Datei öffnen?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mail vorbereiten?";
            // 
            // cb_export_type
            // 
            this.cb_export_type.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_export_type.FormattingEnabled = true;
            this.cb_export_type.Location = new System.Drawing.Point(170, 26);
            this.cb_export_type.Name = "cb_export_type";
            this.cb_export_type.Size = new System.Drawing.Size(162, 21);
            this.cb_export_type.TabIndex = 6;
            // 
            // cb_template
            // 
            this.cb_template.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_template.FormattingEnabled = true;
            this.cb_template.Location = new System.Drawing.Point(170, 53);
            this.cb_template.Name = "cb_template";
            this.cb_template.Size = new System.Drawing.Size(162, 21);
            this.cb_template.TabIndex = 7;
            // 
            // cb_open_export
            // 
            this.cb_open_export.AutoSize = true;
            this.cb_open_export.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_open_export.Location = new System.Drawing.Point(170, 80);
            this.cb_open_export.Name = "cb_open_export";
            this.cb_open_export.Size = new System.Drawing.Size(162, 14);
            this.cb_open_export.TabIndex = 8;
            this.cb_open_export.UseVisualStyleBackColor = true;
            // 
            // cb_Mail
            // 
            this.cb_Mail.AutoSize = true;
            this.cb_Mail.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_Mail.Location = new System.Drawing.Point(170, 100);
            this.cb_Mail.Name = "cb_Mail";
            this.cb_Mail.Size = new System.Drawing.Size(162, 14);
            this.cb_Mail.TabIndex = 9;
            this.cb_Mail.UseVisualStyleBackColor = true;
            // 
            // btn_exit
            // 
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_exit.Location = new System.Drawing.Point(3, 120);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(161, 63);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Abbrechen";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_export
            // 
            this.btn_export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_export.Location = new System.Drawing.Point(170, 120);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(162, 63);
            this.btn_export.TabIndex = 11;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 186);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Export";
            this.Text = "MAX :: Export";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_data_head;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_export_type;
        private System.Windows.Forms.ComboBox cb_template;
        private System.Windows.Forms.CheckBox cb_open_export;
        private System.Windows.Forms.CheckBox cb_Mail;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_export;
    }
}