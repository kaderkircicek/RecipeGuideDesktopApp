namespace yemektarifuyg
{
    partial class AnaSayfaMalzemeEkleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaSayfaMalzemeEkleme));
            label1 = new Label();
            txtMalzemeAdi = new TextBox();
            btnKaydet = new Button();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            txtToplamMiktar = new TextBox();
            txtBirimFiyat = new TextBox();
            comboBoxMalzemeBirim = new ComboBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.OldLace;
            label1.Location = new Point(199, 79);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 0;
            label1.Text = "Malzeme Adı";
            // 
            // txtMalzemeAdi
            // 
            txtMalzemeAdi.BackColor = Color.OldLace;
            txtMalzemeAdi.Location = new Point(469, 79);
            txtMalzemeAdi.Name = "txtMalzemeAdi";
            txtMalzemeAdi.Size = new Size(151, 27);
            txtMalzemeAdi.TabIndex = 1;
            // 
            // btnKaydet
            // 
            btnKaydet.BackColor = Color.Brown;
            btnKaydet.Location = new Point(339, 366);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(94, 29);
            btnKaydet.TabIndex = 2;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = false;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, -3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(802, 456);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.OldLace;
            label2.Location = new Point(199, 147);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 4;
            label2.Text = "Depodaki Miktarı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.OldLace;
            label3.Location = new Point(199, 216);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 5;
            label3.Text = "Birim Fiyatı";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.OldLace;
            label5.Location = new Point(199, 298);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 7;
            label5.Text = "Birimi";
            // 
            // txtToplamMiktar
            // 
            txtToplamMiktar.BackColor = Color.OldLace;
            txtToplamMiktar.Location = new Point(469, 147);
            txtToplamMiktar.Name = "txtToplamMiktar";
            txtToplamMiktar.Size = new Size(151, 27);
            txtToplamMiktar.TabIndex = 8;
            // 
            // txtBirimFiyat
            // 
            txtBirimFiyat.BackColor = Color.OldLace;
            txtBirimFiyat.Location = new Point(469, 216);
            txtBirimFiyat.Name = "txtBirimFiyat";
            txtBirimFiyat.Size = new Size(151, 27);
            txtBirimFiyat.TabIndex = 9;
            // 
            // comboBoxMalzemeBirim
            // 
            comboBoxMalzemeBirim.BackColor = Color.OldLace;
            comboBoxMalzemeBirim.FormattingEnabled = true;
            comboBoxMalzemeBirim.Items.AddRange(new object[] { "kg", "litre", "adet" });
            comboBoxMalzemeBirim.Location = new Point(469, 298);
            comboBoxMalzemeBirim.Name = "comboBoxMalzemeBirim";
            comboBoxMalzemeBirim.Size = new Size(151, 28);
            comboBoxMalzemeBirim.TabIndex = 12;
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.Enabled = false;
            button1.Location = new Point(270, 12);
            button1.Name = "button1";
            button1.Size = new Size(241, 43);
            button1.TabIndex = 13;
            button1.Text = "Yeni Malzeme Ekleme";
            button1.UseVisualStyleBackColor = false;
            // 
            // AnaSayfaMalzemeEkleme
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(comboBoxMalzemeBirim);
            Controls.Add(txtBirimFiyat);
            Controls.Add(txtToplamMiktar);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnKaydet);
            Controls.Add(txtMalzemeAdi);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "AnaSayfaMalzemeEkleme";
            Text = "Malzeme Ekleme";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtMalzemeAdi;
        private Button btnKaydet;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label5;
        private TextBox txtToplamMiktar;
        private TextBox txtBirimFiyat;
        private ComboBox comboBoxMalzemeBirim;
        private Button button1;
    }
}