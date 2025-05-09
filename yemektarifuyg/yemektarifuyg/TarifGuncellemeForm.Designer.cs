namespace yemektarifuyg
{
    partial class TarifGuncellemeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarifGuncellemeForm));
            label1 = new Label();
            txtTarifAdi = new TextBox();
            kategoricomboBox = new ComboBox();
            txtHazirlamaSuresi = new TextBox();
            clbMalzemeler = new CheckedListBox();
            malzemeozellikleriguncellebutton = new Button();
            yenimalzemeeklebutton = new Button();
            pictureBox1 = new PictureBox();
            talimatrichTextBox = new RichTextBox();
            btnKaydet = new Button();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label2 = new Label();
            button1 = new Button();
            btnResmiGuncelle = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.OldLace;
            label1.Location = new Point(484, 61);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 0;
            label1.Text = "Tarif Adı";
            // 
            // txtTarifAdi
            // 
            txtTarifAdi.BackColor = Color.OldLace;
            txtTarifAdi.Location = new Point(656, 54);
            txtTarifAdi.Name = "txtTarifAdi";
            txtTarifAdi.Size = new Size(151, 27);
            txtTarifAdi.TabIndex = 1;
            // 
            // kategoricomboBox
            // 
            kategoricomboBox.BackColor = Color.OldLace;
            kategoricomboBox.FormattingEnabled = true;
            kategoricomboBox.Items.AddRange(new object[] { "Çorba", "Ana Yemek", "Ara Sıcak", "Meze", "Tatlı" });
            kategoricomboBox.Location = new Point(647, 111);
            kategoricomboBox.Name = "kategoricomboBox";
            kategoricomboBox.Size = new Size(151, 28);
            kategoricomboBox.TabIndex = 2;
            // 
            // txtHazirlamaSuresi
            // 
            txtHazirlamaSuresi.BackColor = Color.OldLace;
            txtHazirlamaSuresi.Location = new Point(647, 169);
            txtHazirlamaSuresi.Name = "txtHazirlamaSuresi";
            txtHazirlamaSuresi.Size = new Size(151, 27);
            txtHazirlamaSuresi.TabIndex = 3;
            // 
            // clbMalzemeler
            // 
            clbMalzemeler.BackColor = Color.OldLace;
            clbMalzemeler.FormattingEnabled = true;
            clbMalzemeler.Location = new Point(176, 265);
            clbMalzemeler.Name = "clbMalzemeler";
            clbMalzemeler.Size = new Size(283, 224);
            clbMalzemeler.TabIndex = 5;
            // 
            // malzemeozellikleriguncellebutton
            // 
            malzemeozellikleriguncellebutton.BackColor = Color.OldLace;
            malzemeozellikleriguncellebutton.Location = new Point(91, 506);
            malzemeozellikleriguncellebutton.Name = "malzemeozellikleriguncellebutton";
            malzemeozellikleriguncellebutton.Size = new Size(212, 29);
            malzemeozellikleriguncellebutton.TabIndex = 6;
            malzemeozellikleriguncellebutton.Text = "Malzeme Bilgisi Güncelleme";
            malzemeozellikleriguncellebutton.UseVisualStyleBackColor = false;
            malzemeozellikleriguncellebutton.Click += malzemeozellikleriguncellebutton_Click;
            // 
            // yenimalzemeeklebutton
            // 
            yenimalzemeeklebutton.BackColor = Color.OldLace;
            yenimalzemeeklebutton.Location = new Point(347, 506);
            yenimalzemeeklebutton.Name = "yenimalzemeeklebutton";
            yenimalzemeeklebutton.Size = new Size(176, 29);
            yenimalzemeeklebutton.TabIndex = 7;
            yenimalzemeeklebutton.Text = "Yeni Malzeme Ekle";
            yenimalzemeeklebutton.UseVisualStyleBackColor = false;
            yenimalzemeeklebutton.Click += yenimalzemeeklebutton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.OldLace;
            pictureBox1.Location = new Point(151, 60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(210, 133);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // talimatrichTextBox
            // 
            talimatrichTextBox.BackColor = Color.OldLace;
            talimatrichTextBox.Location = new Point(484, 265);
            talimatrichTextBox.Name = "talimatrichTextBox";
            talimatrichTextBox.Size = new Size(364, 218);
            talimatrichTextBox.TabIndex = 9;
            talimatrichTextBox.Text = "";
            // 
            // btnKaydet
            // 
            btnKaydet.BackColor = Color.Brown;
            btnKaydet.Location = new Point(735, 506);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(104, 45);
            btnKaydet.TabIndex = 10;
            btnKaydet.Text = "Kaydet";
            btnKaydet.UseVisualStyleBackColor = false;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(-5, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(868, 578);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.OldLace;
            label3.Location = new Point(484, 111);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 12;
            label3.Text = "Kategorisi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.OldLace;
            label4.Location = new Point(484, 169);
            label4.Name = "label4";
            label4.Size = new Size(128, 20);
            label4.TabIndex = 13;
            label4.Text = "Hazırlanma Süresi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.OldLace;
            label5.Location = new Point(83, 265);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 14;
            label5.Text = "Malzemeler";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.OldLace;
            label2.Location = new Point(632, 233);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 15;
            label2.Text = "Talimatlar";
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.Enabled = false;
            button1.Location = new Point(290, 0);
            button1.Name = "button1";
            button1.Size = new Size(309, 39);
            button1.TabIndex = 16;
            button1.Text = "TARİF GÜNCELLEME";
            button1.UseVisualStyleBackColor = false;
            // 
            // btnResmiGuncelle
            // 
            btnResmiGuncelle.BackColor = Color.OldLace;
            btnResmiGuncelle.Location = new Point(176, 208);
            btnResmiGuncelle.Name = "btnResmiGuncelle";
            btnResmiGuncelle.Size = new Size(148, 29);
            btnResmiGuncelle.TabIndex = 17;
            btnResmiGuncelle.Text = "Yeni Resim Seç";
            btnResmiGuncelle.UseVisualStyleBackColor = false;
            btnResmiGuncelle.Click += btnResmiGuncelle_Click;
            // 
            // TarifGuncellemeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 575);
            Controls.Add(btnResmiGuncelle);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnKaydet);
            Controls.Add(talimatrichTextBox);
            Controls.Add(pictureBox1);
            Controls.Add(yenimalzemeeklebutton);
            Controls.Add(malzemeozellikleriguncellebutton);
            Controls.Add(clbMalzemeler);
            Controls.Add(txtHazirlamaSuresi);
            Controls.Add(kategoricomboBox);
            Controls.Add(txtTarifAdi);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            MaximizeBox = false;
            Name = "TarifGuncellemeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tarif Güncelleme";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTarifAdi;
        private ComboBox kategoricomboBox;
        private TextBox txtHazirlamaSuresi;
        private CheckedListBox clbMalzemeler;
        private Button malzemeozellikleriguncellebutton;
        private Button yenimalzemeeklebutton;
        private PictureBox pictureBox1;
        private RichTextBox talimatrichTextBox;
        private Button btnKaydet;
        private PictureBox pictureBox2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label2;
        private Button button1;
        private Button btnResmiGuncelle;
    }
}