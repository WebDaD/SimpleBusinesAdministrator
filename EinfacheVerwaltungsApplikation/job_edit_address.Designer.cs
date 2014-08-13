namespace ManageAdministerExalt
{
    partial class job_edit_address
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
            this.rb_customer = new System.Windows.Forms.RadioButton();
            this.rb_custom = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_streetnr = new System.Windows.Forms.TextBox();
            this.tb_plz = new System.Windows.Forms.TextBox();
            this.tb_city = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btn_cancel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.rb_customer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rb_custom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 160);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_cancel.Location = new System.Drawing.Point(3, 131);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 26);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Abbrechen";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(235, 131);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(226, 26);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Speichern und Schließen";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // rb_customer
            // 
            this.rb_customer.AutoSize = true;
            this.rb_customer.Location = new System.Drawing.Point(3, 3);
            this.rb_customer.Name = "rb_customer";
            this.rb_customer.Size = new System.Drawing.Size(103, 17);
            this.rb_customer.TabIndex = 2;
            this.rb_customer.TabStop = true;
            this.rb_customer.Text = "Kunden-Adresse";
            this.rb_customer.UseVisualStyleBackColor = true;
            this.rb_customer.CheckedChanged += new System.EventHandler(this.rb_customer_CheckedChanged);
            // 
            // rb_custom
            // 
            this.rb_custom.AutoSize = true;
            this.rb_custom.Location = new System.Drawing.Point(235, 3);
            this.rb_custom.Name = "rb_custom";
            this.rb_custom.Size = new System.Drawing.Size(99, 17);
            this.rb_custom.TabIndex = 3;
            this.rb_custom.TabStop = true;
            this.rb_custom.Text = "Eigene Adresse";
            this.rb_custom.UseVisualStyleBackColor = true;
            this.rb_custom.CheckedChanged += new System.EventHandler(this.rb_custom_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tb_streetnr, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tb_plz, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tb_city, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(235, 35);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(226, 90);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Straße, Nr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PLZ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ort";
            // 
            // tb_streetnr
            // 
            this.tb_streetnr.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_streetnr.Location = new System.Drawing.Point(70, 3);
            this.tb_streetnr.Name = "tb_streetnr";
            this.tb_streetnr.Size = new System.Drawing.Size(153, 20);
            this.tb_streetnr.TabIndex = 3;
            this.tb_streetnr.TextChanged += new System.EventHandler(this.tb_changed);
            // 
            // tb_plz
            // 
            this.tb_plz.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_plz.Location = new System.Drawing.Point(70, 32);
            this.tb_plz.Name = "tb_plz";
            this.tb_plz.Size = new System.Drawing.Size(153, 20);
            this.tb_plz.TabIndex = 4;
            this.tb_plz.TextChanged += new System.EventHandler(this.tb_changed);
            // 
            // tb_city
            // 
            this.tb_city.Dock = System.Windows.Forms.DockStyle.Top;
            this.tb_city.Location = new System.Drawing.Point(70, 61);
            this.tb_city.Name = "tb_city";
            this.tb_city.Size = new System.Drawing.Size(153, 20);
            this.tb_city.TabIndex = 5;
            this.tb_city.TextChanged += new System.EventHandler(this.tb_changed);
            // 
            // job_edit_address
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 160);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "job_edit_address";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "job_edit_adres";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.RadioButton rb_customer;
        private System.Windows.Forms.RadioButton rb_custom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_streetnr;
        private System.Windows.Forms.TextBox tb_plz;
        private System.Windows.Forms.TextBox tb_city;
    }
}