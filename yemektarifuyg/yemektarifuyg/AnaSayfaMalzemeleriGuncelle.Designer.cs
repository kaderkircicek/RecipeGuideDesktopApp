namespace yemektarifuyg
{
    partial class AnaSayfaMalzemeleriGuncelle
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
            dataGridView1 = new DataGridView();
            btnkaydet = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.OldLace;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 68);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(776, 428);
            dataGridView1.TabIndex = 0;
            // 
            // btnkaydet
            // 
            btnkaydet.BackColor = Color.OldLace;
            btnkaydet.Location = new Point(364, 502);
            btnkaydet.Name = "btnkaydet";
            btnkaydet.Size = new Size(94, 29);
            btnkaydet.TabIndex = 1;
            btnkaydet.Text = "Kaydet";
            btnkaydet.UseVisualStyleBackColor = false;
            btnkaydet.Click += btnkaydet_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.OldLace;
            button1.Enabled = false;
            button1.Location = new Point(292, 21);
            button1.Name = "button1";
            button1.Size = new Size(244, 41);
            button1.TabIndex = 2;
            button1.Text = "Malzemeleri Güncelle";
            button1.UseVisualStyleBackColor = false;
            // 
            // AnaSayfaMalzemeleriGuncelle
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Brown;
            ClientSize = new Size(810, 543);
            Controls.Add(button1);
            Controls.Add(btnkaydet);
            Controls.Add(dataGridView1);
            MaximizeBox = false;
            Name = "AnaSayfaMalzemeleriGuncelle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Malzemeleri Güncelle";
            Load += MalzemeleriGuncelle_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnkaydet;
        private Button button1;
    }
}