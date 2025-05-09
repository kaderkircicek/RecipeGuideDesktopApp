namespace yemektarifuyg
{
    partial class MalzemeMiktarıGir
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
            dataGridViewMalzemeler = new DataGridView();
            btnKaydet = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMalzemeler).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewMalzemeler
            // 
            dataGridViewMalzemeler.BackgroundColor = Color.AntiqueWhite;
            dataGridViewMalzemeler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMalzemeler.Location = new Point(12, 47);
            dataGridViewMalzemeler.Name = "dataGridViewMalzemeler";
            dataGridViewMalzemeler.RowHeadersWidth = 51;
            dataGridViewMalzemeler.RowTemplate.Height = 29;
            dataGridViewMalzemeler.Size = new Size(464, 342);
            dataGridViewMalzemeler.TabIndex = 0;
            // 
            // btnKaydet
            // 
            btnKaydet.BackColor = Color.AntiqueWhite;
            btnKaydet.Location = new Point(215, 395);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(94, 29);
            btnKaydet.TabIndex = 1;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = false;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.AntiqueWhite;
            button1.Enabled = false;
            button1.Location = new Point(136, 12);
            button1.Name = "button1";
            button1.Size = new Size(224, 29);
            button1.TabIndex = 2;
            button1.Text = "Malzeme Miktarı Gir";
            button1.UseVisualStyleBackColor = false;
            // 
            // MalzemeMiktarıGir
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Brown;
            ClientSize = new Size(500, 438);
            Controls.Add(button1);
            Controls.Add(btnKaydet);
            Controls.Add(dataGridViewMalzemeler);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MalzemeMiktarıGir";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Malzeme Miktarı Girme";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMalzemeler).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewMalzemeler;
        private Button btnKaydet;
        private Button button1;
    }
}