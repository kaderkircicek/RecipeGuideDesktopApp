namespace yemektarifuyg
{
    partial class MalzemeGuncellemeForm
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
            malzemeDataGridView = new DataGridView();
            kaydetButton = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)malzemeDataGridView).BeginInit();
            SuspendLayout();
            // 
            // malzemeDataGridView
            // 
            malzemeDataGridView.BackgroundColor = Color.AntiqueWhite;
            malzemeDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            malzemeDataGridView.Location = new Point(12, 73);
            malzemeDataGridView.Name = "malzemeDataGridView";
            malzemeDataGridView.RowHeadersWidth = 51;
            malzemeDataGridView.RowTemplate.Height = 29;
            malzemeDataGridView.Size = new Size(756, 393);
            malzemeDataGridView.TabIndex = 0;
            // 
            // kaydetButton
            // 
            kaydetButton.BackColor = Color.AntiqueWhite;
            kaydetButton.Location = new Point(333, 482);
            kaydetButton.Name = "kaydetButton";
            kaydetButton.Size = new Size(94, 29);
            kaydetButton.TabIndex = 1;
            kaydetButton.Text = "Kaydet";
            kaydetButton.UseVisualStyleBackColor = false;
            kaydetButton.Click += kaydetButton_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.AntiqueWhite;
            button1.Enabled = false;
            button1.Location = new Point(250, 21);
            button1.Name = "button1";
            button1.Size = new Size(240, 29);
            button1.TabIndex = 2;
            button1.Text = "Malzeme Güncelleme";
            button1.UseVisualStyleBackColor = false;
            // 
            // MalzemeGuncellemeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Brown;
            ClientSize = new Size(778, 535);
            Controls.Add(button1);
            Controls.Add(kaydetButton);
            Controls.Add(malzemeDataGridView);
            MaximizeBox = false;
            Name = "MalzemeGuncellemeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Malzeme Güncelleme";
            ((System.ComponentModel.ISupportInitialize)malzemeDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView malzemeDataGridView;
        private Button kaydetButton;
        private Button button1;
    }
}