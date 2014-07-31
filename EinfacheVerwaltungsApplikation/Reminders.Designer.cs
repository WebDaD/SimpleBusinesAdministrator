namespace ManageAdministerExalt
{
    partial class Reminders
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lv_reminders = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_reminders = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_id = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.nu_period = new System.Windows.Forms.NumericUpDown();
            this.cb_type = new System.Windows.Forms.ComboBox();
            this.nu_value = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cms_reminders.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_period)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nu_value)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lv_reminders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(540, 290);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 0;
            // 
            // lv_reminders
            // 
            this.lv_reminders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_reminders.ContextMenuStrip = this.cms_reminders;
            this.lv_reminders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_reminders.GridLines = true;
            this.lv_reminders.Location = new System.Drawing.Point(0, 0);
            this.lv_reminders.Name = "lv_reminders";
            this.lv_reminders.Size = new System.Drawing.Size(180, 290);
            this.lv_reminders.TabIndex = 0;
            this.lv_reminders.UseCompatibleStateImageBehavior = false;
            this.lv_reminders.View = System.Windows.Forms.View.Details;
            this.lv_reminders.SelectedIndexChanged += new System.EventHandler(this.lv_reminders_SelectedIndexChanged);
            this.lv_reminders.Resize += new System.EventHandler(this.lv_reminders_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 38;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 93;
            // 
            // cms_reminders
            // 
            this.cms_reminders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.löschenToolStripMenuItem});
            this.cms_reminders.Name = "cms_reminders";
            this.cms_reminders.Size = new System.Drawing.Size(119, 48);
            this.cms_reminders.Opening += new System.ComponentModel.CancelEventHandler(this.cms_reminders_Opening);
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.neuToolStripMenuItem.Text = "Neu";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_id, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btn_exit, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tb_name, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nu_period, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cb_type, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.nu_value, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 290);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // lb_id
            // 
            this.lb_id.Dock = System.Windows.Forms.DockStyle.Top;
            this.lb_id.Location = new System.Drawing.Point(181, 0);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(172, 23);
            this.lb_id.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mahnfrist (in Tagen)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Kostentyp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kosten";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Location = new System.Drawing.Point(3, 131);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(172, 50);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save.Location = new System.Drawing.Point(181, 131);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(172, 50);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_exit
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btn_exit, 2);
            this.btn_exit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_exit.Location = new System.Drawing.Point(3, 237);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(350, 50);
            this.btn_exit.TabIndex = 8;
            this.btn_exit.Text = "Schließen";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // tb_name
            // 
            this.tb_name.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_name.Location = new System.Drawing.Point(181, 26);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(172, 20);
            this.tb_name.TabIndex = 9;
            this.tb_name.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // nu_period
            // 
            this.nu_period.Dock = System.Windows.Forms.DockStyle.Top;
            this.nu_period.Location = new System.Drawing.Point(181, 52);
            this.nu_period.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nu_period.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nu_period.Name = "nu_period";
            this.nu_period.Size = new System.Drawing.Size(172, 20);
            this.nu_period.TabIndex = 10;
            this.nu_period.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nu_period.ValueChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // cb_type
            // 
            this.cb_type.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_type.FormattingEnabled = true;
            this.cb_type.Items.AddRange(new object[] {
            "Prozent",
            "Festbetrag"});
            this.cb_type.Location = new System.Drawing.Point(181, 78);
            this.cb_type.Name = "cb_type";
            this.cb_type.Size = new System.Drawing.Size(172, 21);
            this.cb_type.TabIndex = 11;
            this.cb_type.SelectedIndexChanged += new System.EventHandler(this.cb_type_SelectedIndexChanged);
            // 
            // nu_value
            // 
            this.nu_value.DecimalPlaces = 2;
            this.nu_value.Dock = System.Windows.Forms.DockStyle.Top;
            this.nu_value.Location = new System.Drawing.Point(181, 105);
            this.nu_value.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nu_value.Name = "nu_value";
            this.nu_value.Size = new System.Drawing.Size(172, 20);
            this.nu_value.TabIndex = 12;
            this.nu_value.ValueChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // Reminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 290);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(556, 328);
            this.Name = "Reminders";
            this.Text = "MAX :: Mahnungen";
            this.Load += new System.EventHandler(this.Reminders_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cms_reminders.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_period)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nu_value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lv_reminders;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.NumericUpDown nu_period;
        private System.Windows.Forms.ComboBox cb_type;
        private System.Windows.Forms.NumericUpDown nu_value;
        private System.Windows.Forms.ContextMenuStrip cms_reminders;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
    }
}