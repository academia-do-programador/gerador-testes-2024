namespace GeradorTestes.WinApp.ModuloQuestao
{
    partial class TelaQuestaoForm
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
            cmbMaterias = new ComboBox();
            label4 = new Label();
            txtEnunciado = new TextBox();
            label2 = new Label();
            groupBox1 = new GroupBox();
            listAlternativas = new CheckedListBox();
            toolStrip1 = new ToolStrip();
            btnRemover = new ToolStripButton();
            btnAdicionar = new Button();
            txtResposta = new TextBox();
            label5 = new Label();
            txtId = new TextBox();
            label1 = new Label();
            btnCancelar = new Button();
            btnGravar = new Button();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.Font = new Font("Segoe UI", 11.25F);
            cmbMaterias.FormattingEnabled = true;
            cmbMaterias.Location = new Point(119, 74);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(217, 28);
            cmbMaterias.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F);
            label4.Location = new Point(50, 77);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 0;
            label4.Text = "Matéria:";
            // 
            // txtEnunciado
            // 
            txtEnunciado.Font = new Font("Segoe UI", 11.25F);
            txtEnunciado.Location = new Point(119, 108);
            txtEnunciado.Multiline = true;
            txtEnunciado.Name = "txtEnunciado";
            txtEnunciado.Size = new Size(389, 44);
            txtEnunciado.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F);
            label2.Location = new Point(32, 121);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 0;
            label2.Text = "Enunciado:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listAlternativas);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Font = new Font("Segoe UI", 11.25F);
            groupBox1.Location = new Point(36, 214);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(472, 279);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Alternativas";
            // 
            // listAlternativas
            // 
            listAlternativas.FormattingEnabled = true;
            listAlternativas.Location = new Point(8, 70);
            listAlternativas.Name = "listAlternativas";
            listAlternativas.Size = new Size(458, 202);
            listAlternativas.TabIndex = 0;
            listAlternativas.ItemCheck += listAlternativas_ItemCheck;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnRemover });
            toolStrip1.Location = new Point(3, 23);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(466, 40);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnRemover
            // 
            btnRemover.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnRemover.ImageTransparentColor = Color.Magenta;
            btnRemover.Name = "btnRemover";
            btnRemover.Size = new Size(58, 37);
            btnRemover.Text = "Remover";
            btnRemover.Click += btnRemover_Click;
            // 
            // btnAdicionar
            // 
            btnAdicionar.Font = new Font("Segoe UI", 11.25F);
            btnAdicionar.Location = new Point(415, 158);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(93, 32);
            btnAdicionar.TabIndex = 6;
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Click += btnAdicionar_Click;
            // 
            // txtResposta
            // 
            txtResposta.Font = new Font("Segoe UI", 11.25F);
            txtResposta.Location = new Point(119, 158);
            txtResposta.Multiline = true;
            txtResposta.Name = "txtResposta";
            txtResposta.Size = new Size(290, 32);
            txtResposta.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F);
            label5.Location = new Point(41, 164);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 0;
            label5.Text = "Resposta:";
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Font = new Font("Segoe UI", 11.25F);
            txtId.Location = new Point(119, 41);
            txtId.Name = "txtId";
            txtId.Size = new Size(60, 27);
            txtId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F);
            label1.Location = new Point(88, 44);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 0;
            label1.Text = "Id:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Font = new Font("Segoe UI", 11.25F);
            btnCancelar.Location = new Point(409, 513);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(99, 35);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            btnGravar.DialogResult = DialogResult.OK;
            btnGravar.Font = new Font("Segoe UI", 11.25F);
            btnGravar.Location = new Point(304, 513);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(99, 35);
            btnGravar.TabIndex = 8;
            btnGravar.Text = "Gravar";
            btnGravar.UseVisualStyleBackColor = true;
            btnGravar.Click += btnGravar_Click;
            // 
            // TelaQuestaoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 559);
            Controls.Add(btnCancelar);
            Controls.Add(btnGravar);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(groupBox1);
            Controls.Add(btnAdicionar);
            Controls.Add(cmbMaterias);
            Controls.Add(txtResposta);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(txtEnunciado);
            Controls.Add(label2);
            Name = "TelaQuestaoForm";
            Text = "Cadastro de Questões";
            groupBox1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMaterias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEnunciado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.TextBox txtResposta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRemover;
        private System.Windows.Forms.ComboBox cmbAlternativaCorreta;
        private System.Windows.Forms.CheckedListBox listAlternativas;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private Button btnCancelar;
        private Button btnGravar;
    }
}