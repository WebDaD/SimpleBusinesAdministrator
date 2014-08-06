namespace ManageAdministerExalt
{
    partial class Items_Edit
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_import = new System.Windows.Forms.RadioButton();
            this.rb_export = new System.Windows.Forms.RadioButton();
            this.cb_items = new System.Windows.Forms.ComboBox();
            this.nu_count = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_count)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rb_import, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rb_export, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_items, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nu_count, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(321, 125);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel.Location = new System.Drawing.Point(3, 79);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(154, 43);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save.Location = new System.Drawing.Point(163, 79);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(155, 43);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Speichern";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gegenstand:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rb_import
            // 
            this.rb_import.AutoSize = true;
            this.rb_import.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rb_import.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_import.Location = new System.Drawing.Point(3, 30);
            this.rb_import.Name = "rb_import";
            this.rb_import.Size = new System.Drawing.Size(154, 17);
            this.rb_import.TabIndex = 4;
            this.rb_import.TabStop = true;
            this.rb_import.Text = "Import";
            this.rb_import.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rb_import.UseVisualStyleBackColor = true;
            // 
            // rb_export
            // 
            this.rb_export.AutoSize = true;
            this.rb_export.Dock = System.Windows.Forms.DockStyle.Top;
            this.rb_export.Location = new System.Drawing.Point(163, 30);
            this.rb_export.Name = "rb_export";
            this.rb_export.Size = new System.Drawing.Size(155, 17);
            this.rb_export.TabIndex = 5;
            this.rb_export.TabStop = true;
            this.rb_export.Text = "Export";
            this.rb_export.UseVisualStyleBackColor = true;
            // 
            // cb_items
            // 
            this.cb_items.Dock = System.Windows.Forms.DockStyle.Top;
            this.cb_items.FormattingEnabled = true;
            this.cb_items.Location = new System.Drawing.Point(163, 3);
            this.cb_items.Name = "cb_items";
            this.cb_items.Size = new System.Drawing.Size(155, 21);
            this.cb_items.TabIndex = 6;
            // 
            // nu_count
            // 
            this.nu_count.Dock = System.Windows.Forms.DockStyle.Top;
            this.nu_count.Location = new System.Drawing.Point(163, 53);
            this.nu_count.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nu_count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nu_count.Name = "nu_count";
            this.nu_count.Size = new System.Drawing.Size(155, 20);
            this.nu_count.TabIndex = 7;
            this.nu_count.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Anzahl:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Items_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 125);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Items_Edit";
            this.Text = "Items_Edit";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nu_count)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_import;
        private System.Windows.Forms.RadioButton rb_export;
        private System.Windows.Forms.ComboBox cb_items;
        private System.Windows.Forms.NumericUpDown nu_count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
    }
}