namespace ManageAdministerExalt
{
    partial class job_edit_services
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_plus = new System.Windows.Forms.Button();
            this.btn_minus = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.lv_on_job = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_Services = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plus1EinheitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minus1EinheitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.lv_avaiable = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.cms_Services.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.btn_plus, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_minus, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_add, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_remove, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lv_on_job, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.lv_avaiable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(659, 306);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_plus
            // 
            this.btn_plus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_plus.Location = new System.Drawing.Point(594, 3);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(62, 60);
            this.btn_plus.TabIndex = 0;
            this.btn_plus.Text = "+";
            this.btn_plus.UseVisualStyleBackColor = true;
            this.btn_plus.Click += new System.EventHandler(this.btn_plus_Click);
            // 
            // btn_minus
            // 
            this.btn_minus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_minus.Location = new System.Drawing.Point(594, 69);
            this.btn_minus.Name = "btn_minus";
            this.btn_minus.Size = new System.Drawing.Size(62, 60);
            this.btn_minus.TabIndex = 1;
            this.btn_minus.Text = "-";
            this.btn_minus.UseVisualStyleBackColor = true;
            this.btn_minus.Click += new System.EventHandler(this.btn_minus_Click);
            // 
            // btn_add
            // 
            this.btn_add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_add.Location = new System.Drawing.Point(200, 69);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(59, 60);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = ">>>";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_remove.Location = new System.Drawing.Point(200, 135);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(59, 60);
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Text = "<<<";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // lv_on_job
            // 
            this.lv_on_job.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_on_job.ContextMenuStrip = this.cms_Services;
            this.lv_on_job.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_on_job.GridLines = true;
            this.lv_on_job.Location = new System.Drawing.Point(265, 3);
            this.lv_on_job.Name = "lv_on_job";
            this.tableLayoutPanel1.SetRowSpan(this.lv_on_job, 4);
            this.lv_on_job.Size = new System.Drawing.Size(323, 258);
            this.lv_on_job.TabIndex = 5;
            this.lv_on_job.UseCompatibleStateImageBehavior = false;
            this.lv_on_job.View = System.Windows.Forms.View.Details;
            this.lv_on_job.Resize += new System.EventHandler(this.lv_on_job_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 26;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 242;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Einheiten";
            // 
            // cms_Services
            // 
            this.cms_Services.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.löschenToolStripMenuItem,
            this.plus1EinheitToolStripMenuItem,
            this.minus1EinheitToolStripMenuItem});
            this.cms_Services.Name = "cms_Services";
            this.cms_Services.Size = new System.Drawing.Size(156, 70);
            this.cms_Services.Opening += new System.ComponentModel.CancelEventHandler(this.cms_Services_Opening);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.löschenToolStripMenuItem.Text = "Löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // plus1EinheitToolStripMenuItem
            // 
            this.plus1EinheitToolStripMenuItem.Name = "plus1EinheitToolStripMenuItem";
            this.plus1EinheitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.plus1EinheitToolStripMenuItem.Text = "Plus 1 Einheit";
            this.plus1EinheitToolStripMenuItem.Click += new System.EventHandler(this.plus1EinheitToolStripMenuItem_Click);
            // 
            // minus1EinheitToolStripMenuItem
            // 
            this.minus1EinheitToolStripMenuItem.Name = "minus1EinheitToolStripMenuItem";
            this.minus1EinheitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.minus1EinheitToolStripMenuItem.Text = "Minus 1 Einheit";
            this.minus1EinheitToolStripMenuItem.Click += new System.EventHandler(this.minus1EinheitToolStripMenuItem_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Location = new System.Drawing.Point(3, 267);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(191, 36);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btn_save, 2);
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(265, 267);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(391, 36);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "Speichern und Schließen";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // lv_avaiable
            // 
            this.lv_avaiable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.lv_avaiable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_avaiable.FullRowSelect = true;
            this.lv_avaiable.GridLines = true;
            this.lv_avaiable.Location = new System.Drawing.Point(3, 3);
            this.lv_avaiable.Name = "lv_avaiable";
            this.tableLayoutPanel1.SetRowSpan(this.lv_avaiable, 4);
            this.lv_avaiable.Size = new System.Drawing.Size(191, 258);
            this.lv_avaiable.TabIndex = 8;
            this.lv_avaiable.UseCompatibleStateImageBehavior = false;
            this.lv_avaiable.View = System.Windows.Forms.View.Details;
            this.lv_avaiable.Resize += new System.EventHandler(this.lv_avaiable_Resize);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            this.columnHeader4.Width = 31;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 134;
            // 
            // job_edit_services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(591, 319);
            this.Name = "job_edit_services";
            this.Text = "job_edit_services";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.cms_Services.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.Button btn_minus;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.ListView lv_on_job;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip cms_Services;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plus1EinheitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minus1EinheitToolStripMenuItem;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ListView lv_avaiable;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}