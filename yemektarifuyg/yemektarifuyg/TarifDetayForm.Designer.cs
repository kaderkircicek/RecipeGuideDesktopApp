namespace yemektarifuyg
{
    partial class TarifDetayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TarifDetayForm));
            pictureBox1 = new PictureBox();
            tarifadilabel = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            kategorilabel = new Label();
            hazirlamasure = new Label();
            talimatlarRichTextBox = new RichTextBox();
            pictureBox2 = new PictureBox();
            tarifigüncellebutton = new Button();
            tarifisilbutton = new Button();
            label1 = new Label();
            label5 = new Label();
            malzemerichTextBox1 = new RichTextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(297, 62);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(186, 145);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tarifadilabel
            // 
            tarifadilabel.AutoSize = true;
            tarifadilabel.BackColor = Color.BlanchedAlmond;
            tarifadilabel.Location = new Point(475, 220);
            tarifadilabel.Name = "tarifadilabel";
            tarifadilabel.Size = new Size(50, 20);
            tarifadilabel.TabIndex = 1;
            tarifadilabel.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.BlanchedAlmond;
            label2.Location = new Point(187, 256);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.TabIndex = 2;
            label2.Text = "Kategori";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.BlanchedAlmond;
            label3.Location = new Point(187, 298);
            label3.Name = "label3";
            label3.Size = new Size(120, 20);
            label3.TabIndex = 3;
            label3.Text = "Hazırlama Süresi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.BlanchedAlmond;
            label4.Location = new Point(187, 220);
            label4.Name = "label4";
            label4.Size = new Size(68, 20);
            label4.TabIndex = 4;
            label4.Text = "Tarif İsmi";
            // 
            // kategorilabel
            // 
            kategorilabel.AutoSize = true;
            kategorilabel.BackColor = Color.BlanchedAlmond;
            kategorilabel.Location = new Point(475, 256);
            kategorilabel.Name = "kategorilabel";
            kategorilabel.Size = new Size(50, 20);
            kategorilabel.TabIndex = 5;
            kategorilabel.Text = "label5";
            // 
            // hazirlamasure
            // 
            hazirlamasure.AutoSize = true;
            hazirlamasure.BackColor = Color.BlanchedAlmond;
            hazirlamasure.Location = new Point(475, 298);
            hazirlamasure.Name = "hazirlamasure";
            hazirlamasure.Size = new Size(50, 20);
            hazirlamasure.TabIndex = 6;
            hazirlamasure.Text = "label6";
            // 
            // talimatlarRichTextBox
            // 
            talimatlarRichTextBox.Location = new Point(93, 387);
            talimatlarRichTextBox.Name = "talimatlarRichTextBox";
            talimatlarRichTextBox.ReadOnly = true;
            talimatlarRichTextBox.Size = new Size(360, 189);
            talimatlarRichTextBox.TabIndex = 7;
            talimatlarRichTextBox.Text = "";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(802, 717);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // tarifigüncellebutton
            // 
            tarifigüncellebutton.BackColor = Color.Brown;
            tarifigüncellebutton.Location = new Point(187, 598);
            tarifigüncellebutton.Name = "tarifigüncellebutton";
            tarifigüncellebutton.Size = new Size(117, 49);
            tarifigüncellebutton.TabIndex = 10;
            tarifigüncellebutton.Text = "Güncelle";
            tarifigüncellebutton.UseVisualStyleBackColor = false;
            tarifigüncellebutton.Click += tarifigüncellebutton_Click;
            // 
            // tarifisilbutton
            // 
            tarifisilbutton.BackColor = Color.Brown;
            tarifisilbutton.Location = new Point(503, 598);
            tarifisilbutton.Name = "tarifisilbutton";
            tarifisilbutton.Size = new Size(123, 49);
            tarifisilbutton.TabIndex = 9;
            tarifisilbutton.Text = "Sil";
            tarifisilbutton.UseVisualStyleBackColor = false;
            tarifisilbutton.Click += tarifisilbutton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.BlanchedAlmond;
            label1.Location = new Point(230, 364);
            label1.Name = "label1";
            label1.Size = new Size(74, 20);
            label1.TabIndex = 11;
            label1.Text = "Talimatlar";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.BlanchedAlmond;
            label5.Location = new Point(550, 348);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 12;
            label5.Text = "Malzemeler";
            // 
            // malzemerichTextBox1
            // 
            malzemerichTextBox1.Enabled = false;
            malzemerichTextBox1.Location = new Point(475, 387);
            malzemerichTextBox1.Name = "malzemerichTextBox1";
            malzemerichTextBox1.ReadOnly = true;
            malzemerichTextBox1.Size = new Size(222, 189);
            malzemerichTextBox1.TabIndex = 13;
            malzemerichTextBox1.Text = "";
            // 
            // button1
            // 
            button1.BackColor = Color.Brown;
            button1.Enabled = false;
            button1.Location = new Point(213, 7);
            button1.Name = "button1";
            button1.Size = new Size(377, 40);
            button1.TabIndex = 14;
            button1.Text = "Tarif Detayları Görüntüleme";
            button1.UseVisualStyleBackColor = false;
            // 
            // TarifDetayForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 711);
            Controls.Add(button1);
            Controls.Add(malzemerichTextBox1);
            Controls.Add(label5);
            Controls.Add(label1);
            Controls.Add(tarifigüncellebutton);
            Controls.Add(tarifisilbutton);
            Controls.Add(talimatlarRichTextBox);
            Controls.Add(hazirlamasure);
            Controls.Add(kategorilabel);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tarifadilabel);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            MaximizeBox = false;
            Name = "TarifDetayForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tarif Detayları";
            Load += TarifDetayForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label tarifadilabel;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label kategorilabel;
        private Label hazirlamasure;
        private RichTextBox talimatlarRichTextBox;
        private PictureBox pictureBox2;
        private Button tarifigüncellebutton;
        private Button tarifisilbutton;
        private Label label1;
        private Label label5;
        private RichTextBox malzemerichTextBox1;
        private Button button1;
    }
}