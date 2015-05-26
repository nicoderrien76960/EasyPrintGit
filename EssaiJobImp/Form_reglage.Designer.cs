namespace EssaiJobImp
{
    partial class Form_reglage
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
            this.label1 = new System.Windows.Forms.Label();
            this.cBProfil = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSupProfil = new System.Windows.Forms.Button();
            this.btnAjoutProfil = new System.Windows.Forms.Button();
            this.lBProfil = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Valider = new System.Windows.Forms.Button();
            this.tBModifImp = new System.Windows.Forms.TextBox();
            this.btn_SupImp = new System.Windows.Forms.Button();
            this.btn_ModifImp = new System.Windows.Forms.Button();
            this.btn_AjoutImp = new System.Windows.Forms.Button();
            this.lBImprimante = new System.Windows.Forms.ListBox();
            this.btnValidAjout = new System.Windows.Forms.Button();
            this.tbAjoutProfil = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnValiderAjoutPro = new System.Windows.Forms.Button();
            this.tbAjoutImprimante = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Brown;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paramètre de l\'application ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cBProfil
            // 
            this.cBProfil.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cBProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBProfil.FormattingEnabled = true;
            this.cBProfil.Location = new System.Drawing.Point(13, 14);
            this.cBProfil.Name = "cBProfil";
            this.cBProfil.Size = new System.Drawing.Size(305, 22);
            this.cBProfil.TabIndex = 1;
            this.cBProfil.SelectedIndexChanged += new System.EventHandler(this.cBProfil_SelectedIndexChanged);
            this.cBProfil.SelectedValueChanged += new System.EventHandler(this.cBProfil_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(11, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Brown;
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.tbAjoutImprimante);
            this.splitContainer1.Panel1.Controls.Add(this.btnValiderAjoutPro);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.tbAjoutProfil);
            this.splitContainer1.Panel1.Controls.Add(this.btnSupProfil);
            this.splitContainer1.Panel1.Controls.Add(this.btnAjoutProfil);
            this.splitContainer1.Panel1.Controls.Add(this.lBProfil);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Brown;
            this.splitContainer1.Panel2.Controls.Add(this.btnValidAjout);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Valider);
            this.splitContainer1.Panel2.Controls.Add(this.tBModifImp);
            this.splitContainer1.Panel2.Controls.Add(this.btn_SupImp);
            this.splitContainer1.Panel2.Controls.Add(this.btn_ModifImp);
            this.splitContainer1.Panel2.Controls.Add(this.btn_AjoutImp);
            this.splitContainer1.Panel2.Controls.Add(this.lBImprimante);
            this.splitContainer1.Panel2.Controls.Add(this.cBProfil);
            this.splitContainer1.Size = new System.Drawing.Size(836, 310);
            this.splitContainer1.SplitterDistance = 364;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnSupProfil
            // 
            this.btnSupProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupProfil.Location = new System.Drawing.Point(165, 181);
            this.btnSupProfil.Name = "btnSupProfil";
            this.btnSupProfil.Size = new System.Drawing.Size(183, 39);
            this.btnSupProfil.TabIndex = 3;
            this.btnSupProfil.Text = "Supprimer";
            this.btnSupProfil.UseVisualStyleBackColor = true;
            this.btnSupProfil.Click += new System.EventHandler(this.btnSupProfil_Click);
            // 
            // btnAjoutProfil
            // 
            this.btnAjoutProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjoutProfil.Location = new System.Drawing.Point(3, 181);
            this.btnAjoutProfil.Name = "btnAjoutProfil";
            this.btnAjoutProfil.Size = new System.Drawing.Size(156, 39);
            this.btnAjoutProfil.TabIndex = 1;
            this.btnAjoutProfil.Text = "Ajouter Profil";
            this.btnAjoutProfil.UseVisualStyleBackColor = true;
            this.btnAjoutProfil.Click += new System.EventHandler(this.btnAjoutProfil_Click);
            // 
            // lBProfil
            // 
            this.lBProfil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lBProfil.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBProfil.FormattingEnabled = true;
            this.lBProfil.ItemHeight = 14;
            this.lBProfil.Location = new System.Drawing.Point(15, 14);
            this.lBProfil.MultiColumn = true;
            this.lBProfil.Name = "lBProfil";
            this.lBProfil.Size = new System.Drawing.Size(341, 144);
            this.lBProfil.TabIndex = 0;
            this.lBProfil.SelectedIndexChanged += new System.EventHandler(this.lBProfil_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Champ de modification :";
            this.label2.Visible = false;
            // 
            // btn_Valider
            // 
            this.btn_Valider.Location = new System.Drawing.Point(156, 263);
            this.btn_Valider.Name = "btn_Valider";
            this.btn_Valider.Size = new System.Drawing.Size(137, 40);
            this.btn_Valider.TabIndex = 7;
            this.btn_Valider.Text = "Valider";
            this.btn_Valider.UseVisualStyleBackColor = true;
            this.btn_Valider.Visible = false;
            this.btn_Valider.Click += new System.EventHandler(this.btn_Valider_Click);
            // 
            // tBModifImp
            // 
            this.tBModifImp.Location = new System.Drawing.Point(13, 263);
            this.tBModifImp.Multiline = true;
            this.tBModifImp.Name = "tBModifImp";
            this.tBModifImp.Size = new System.Drawing.Size(137, 40);
            this.tBModifImp.TabIndex = 6;
            this.tBModifImp.Visible = false;
            // 
            // btn_SupImp
            // 
            this.btn_SupImp.Location = new System.Drawing.Point(13, 192);
            this.btn_SupImp.Name = "btn_SupImp";
            this.btn_SupImp.Size = new System.Drawing.Size(137, 40);
            this.btn_SupImp.TabIndex = 5;
            this.btn_SupImp.Text = "Supprimer";
            this.btn_SupImp.UseVisualStyleBackColor = true;
            this.btn_SupImp.Click += new System.EventHandler(this.btn_SupImp_Click);
            // 
            // btn_ModifImp
            // 
            this.btn_ModifImp.Location = new System.Drawing.Point(156, 146);
            this.btn_ModifImp.Name = "btn_ModifImp";
            this.btn_ModifImp.Size = new System.Drawing.Size(137, 40);
            this.btn_ModifImp.TabIndex = 4;
            this.btn_ModifImp.Text = "Modifier";
            this.btn_ModifImp.UseVisualStyleBackColor = true;
            this.btn_ModifImp.Click += new System.EventHandler(this.btn_ModifImp_Click);
            // 
            // btn_AjoutImp
            // 
            this.btn_AjoutImp.Location = new System.Drawing.Point(13, 146);
            this.btn_AjoutImp.Name = "btn_AjoutImp";
            this.btn_AjoutImp.Size = new System.Drawing.Size(137, 40);
            this.btn_AjoutImp.TabIndex = 3;
            this.btn_AjoutImp.Text = "Ajouter";
            this.btn_AjoutImp.UseVisualStyleBackColor = true;
            this.btn_AjoutImp.Click += new System.EventHandler(this.btn_AjoutImp_Click);
            // 
            // lBImprimante
            // 
            this.lBImprimante.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lBImprimante.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBImprimante.FormattingEnabled = true;
            this.lBImprimante.ItemHeight = 14;
            this.lBImprimante.Location = new System.Drawing.Point(13, 56);
            this.lBImprimante.Name = "lBImprimante";
            this.lBImprimante.Size = new System.Drawing.Size(305, 74);
            this.lBImprimante.TabIndex = 2;
            // 
            // btnValidAjout
            // 
            this.btnValidAjout.Location = new System.Drawing.Point(156, 263);
            this.btnValidAjout.Name = "btnValidAjout";
            this.btnValidAjout.Size = new System.Drawing.Size(137, 40);
            this.btnValidAjout.TabIndex = 9;
            this.btnValidAjout.Text = "Valider";
            this.btnValidAjout.UseVisualStyleBackColor = true;
            this.btnValidAjout.Visible = false;
            this.btnValidAjout.Click += new System.EventHandler(this.btnValidAjout_Click);
            // 
            // tbAjoutProfil
            // 
            this.tbAjoutProfil.Location = new System.Drawing.Point(3, 263);
            this.tbAjoutProfil.Multiline = true;
            this.tbAjoutProfil.Name = "tbAjoutProfil";
            this.tbAjoutProfil.Size = new System.Drawing.Size(106, 40);
            this.tbAjoutProfil.TabIndex = 7;
            this.tbAjoutProfil.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nom du profil (Code) :";
            this.label3.Visible = false;
            // 
            // btnValiderAjoutPro
            // 
            this.btnValiderAjoutPro.Location = new System.Drawing.Point(272, 263);
            this.btnValiderAjoutPro.Name = "btnValiderAjoutPro";
            this.btnValiderAjoutPro.Size = new System.Drawing.Size(84, 40);
            this.btnValiderAjoutPro.TabIndex = 10;
            this.btnValiderAjoutPro.Text = "Valider";
            this.btnValiderAjoutPro.UseVisualStyleBackColor = true;
            this.btnValiderAjoutPro.Visible = false;
            this.btnValiderAjoutPro.Click += new System.EventHandler(this.btnValiderAjoutPro_Click);
            // 
            // tbAjoutImprimante
            // 
            this.tbAjoutImprimante.Location = new System.Drawing.Point(115, 263);
            this.tbAjoutImprimante.Multiline = true;
            this.tbAjoutImprimante.Name = "tbAjoutImprimante";
            this.tbAjoutImprimante.Size = new System.Drawing.Size(131, 40);
            this.tbAjoutImprimante.TabIndex = 11;
            this.tbAjoutImprimante.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Nom Imprimante :";
            this.label4.Visible = false;
            // 
            // Form_reglage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(852, 384);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(1500, 423);
            this.MinimumSize = new System.Drawing.Size(863, 423);
            this.Name = "Form_reglage";
            this.Text = "Form_reglafge";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBProfil;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lBProfil;
        private System.Windows.Forms.Button btnSupProfil;
        private System.Windows.Forms.Button btnAjoutProfil;
        private System.Windows.Forms.ListBox lBImprimante;
        private System.Windows.Forms.Button btn_SupImp;
        private System.Windows.Forms.Button btn_ModifImp;
        private System.Windows.Forms.Button btn_AjoutImp;
        private System.Windows.Forms.TextBox tBModifImp;
        private System.Windows.Forms.Button btn_Valider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnValidAjout;
        private System.Windows.Forms.Button btnValiderAjoutPro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAjoutProfil;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAjoutImprimante;
    }
}