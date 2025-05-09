namespace yemektarifuyg
{
    partial class YeniMalzemeEklemeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YeniMalzemeEklemeForm));
            txtMalzemeAdi = new TextBox();
            txtBirimFiyat = new TextBox();
            txtToplamMiktar = new TextBox();
            txtMalzemeMiktar = new TextBox();
            comboBoxMalzemeBirim = new ComboBox();
            btnKaydet = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtMalzemeAdi
            // 
            txtMalzemeAdi.BackColor = Color.OldLace;
            txtMalzemeAdi.Location = new Point(392, 111);
            txtMalzemeAdi.Name = "txtMalzemeAdi";
            txtMalzemeAdi.Size = new Size(151, 27);
            txtMalzemeAdi.TabIndex = 0;
            // 
            // txtBirimFiyat
            // 
            txtBirimFiyat.BackColor = Color.OldLace;
            txtBirimFiyat.Location = new Point(392, 386);
            txtBirimFiyat.Name = "txtBirimFiyat";
            txtBirimFiyat.Size = new Size(151, 27);
            txtBirimFiyat.TabIndex = 1;
            // 
            // txtToplamMiktar
            // 
            txtToplamMiktar.BackColor = Color.OldLace;
            txtToplamMiktar.Location = new Point(392, 248);
            txtToplamMiktar.Name = "txtToplamMiktar";
            txtToplamMiktar.Size = new Size(151, 27);
            txtToplamMiktar.TabIndex = 2;
            // 
            // txtMalzemeMiktar
            // 
            txtMalzemeMiktar.BackColor = Color.OldLace;
            txtMalzemeMiktar.Location = new Point(392, 316);
            txtMalzemeMiktar.Name = "txtMalzemeMiktar";
            txtMalzemeMiktar.Size = new Size(151, 27);
            txtMalzemeMiktar.TabIndex = 3;
            // 
            // comboBoxMalzemeBirim
            // 
            comboBoxMalzemeBirim.BackColor = Color.OldLace;
            comboBoxMalzemeBirim.FormattingEnabled = true;
            comboBoxMalzemeBirim.Items.AddRange(new object[] { "kg", "adet", "litre" });
            comboBoxMalzemeBirim.Location = new Point(392, 181);
            comboBoxMalzemeBirim.Name = "comboBoxMalzemeBirim";
            comboBoxMalzemeBirim.Size = new Size(151, 28);
            comboBoxMalzemeBirim.TabIndex = 4;
            // 
            // btnKaydet
            // 
            btnKaydet.BackColor = Color.Brown;
            btnKaydet.Location = new Point(273, 441);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(152, 29);
            btnKaydet.TabIndex = 5;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = false;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.OldLace;
            label1.Location = new Point(108, 111);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 6;
            label1.Text = "Malzeme Adı";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(692, 503);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.OldLace;
            label2.Location = new Point(108, 248);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 8;
            label2.Text = "Depodaki Miktarı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.OldLace;
            label3.Location = new Point(108, 393);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 9;
            label3.Text = "Birim Fiyatı";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.OldLace;
            label4.Location = new Point(108, 316);
            label4.Name = "label4";
            label4.Size = new Size(179, 20);
            label4.TabIndex = 10;
            label4.Text = "Tarifte Kullanılacak Miktar";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.OldLace;
            label5.Location = new Point(108, 189);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 11;
            label5.Text = "Birimi";
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.Enabled = false;
            button1.Location = new Point(227, 12);
            button1.Name = "button1";
            button1.Size = new Size(246, 39);
            button1.TabIndex = 12;
            button1.Text = "Yeni Malzeme Ekleme";
            button1.UseVisualStyleBackColor = false;
            // 
            // YeniMalzemeEklemeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(693, 499);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnKaydet);
            Controls.Add(comboBoxMalzemeBirim);
            Controls.Add(txtMalzemeMiktar);
            Controls.Add(txtToplamMiktar);
            Controls.Add(txtBirimFiyat);
            Controls.Add(txtMalzemeAdi);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "YeniMalzemeEklemeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yeni Malzeme Ekleme";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMalzemeAdi;
        private TextBox txtBirimFiyat;
        private TextBox txtToplamMiktar;
        private TextBox txtMalzemeMiktar;
        private ComboBox comboBoxMalzemeBirim;
        private Button btnKaydet;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button1;
    }
}