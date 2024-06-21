namespace GeradorTestes.WinApp.ModuloTeste
{
    partial class TelaTesteForm
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
            cmbDisciplinas = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            txtTitulo = new TextBox();
            label5 = new Label();
            chkProvaRecuperacao = new CheckBox();
            groupBox1 = new GroupBox();
            toolStrip1 = new ToolStrip();
            btnSortear = new ToolStripButton();
            listQuestoes = new ListBox();
            txtQtdQuestoes = new NumericUpDown();
            label1 = new Label();
            txtId = new TextBox();
            btnCancelar = new Button();
            btnGravar = new Button();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtQtdQuestoes).BeginInit();
            SuspendLayout();
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.Enabled = false;
            cmbMaterias.Font = new Font("Segoe UI", 11.25F);
            cmbMaterias.FormattingEnabled = true;
            cmbMaterias.Location = new Point(103, 140);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(195, 28);
            cmbMaterias.TabIndex = 32;
            cmbMaterias.SelectedIndexChanged += cmbMaterias_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F);
            label4.Location = new Point(34, 143);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 31;
            label4.Text = "Matéria:";
            // 
            // cmbDisciplinas
            // 
            cmbDisciplinas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplinas.Font = new Font("Segoe UI", 11.25F);
            cmbDisciplinas.FormattingEnabled = true;
            cmbDisciplinas.Location = new Point(103, 106);
            cmbDisciplinas.Name = "cmbDisciplinas";
            cmbDisciplinas.Size = new Size(195, 28);
            cmbDisciplinas.TabIndex = 30;
            cmbDisciplinas.SelectedIndexChanged += cmbDisciplinas_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F);
            label3.Location = new Point(20, 110);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 29;
            label3.Text = "Disciplina:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F);
            label2.Location = new Point(333, 111);
            label2.Name = "label2";
            label2.Size = new Size(105, 20);
            label2.TabIndex = 26;
            label2.Text = "Qtd. Questões:";
            // 
            // txtTitulo
            // 
            txtTitulo.Font = new Font("Segoe UI", 11.25F);
            txtTitulo.Location = new Point(103, 73);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.Size = new Size(394, 27);
            txtTitulo.TabIndex = 34;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F);
            label5.Location = new Point(47, 76);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 33;
            label5.Text = "Título:";
            // 
            // chkProvaRecuperacao
            // 
            chkProvaRecuperacao.AutoSize = true;
            chkProvaRecuperacao.Font = new Font("Segoe UI", 11.25F);
            chkProvaRecuperacao.Location = new Point(310, 142);
            chkProvaRecuperacao.Margin = new Padding(3, 2, 3, 2);
            chkProvaRecuperacao.Name = "chkProvaRecuperacao";
            chkProvaRecuperacao.RightToLeft = RightToLeft.Yes;
            chkProvaRecuperacao.Size = new Size(187, 24);
            chkProvaRecuperacao.TabIndex = 38;
            chkProvaRecuperacao.Text = "? Prova de Recuperação";
            chkProvaRecuperacao.TextAlign = ContentAlignment.MiddleCenter;
            chkProvaRecuperacao.UseVisualStyleBackColor = true;
            chkProvaRecuperacao.CheckedChanged += chkProvao_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(listQuestoes);
            groupBox1.Font = new Font("Segoe UI", 11.25F);
            groupBox1.Location = new Point(20, 198);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(485, 259);
            groupBox1.TabIndex = 39;
            groupBox1.TabStop = false;
            groupBox1.Text = "Questões Selecionadas:";
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnSortear });
            toolStrip1.Location = new Point(3, 22);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(479, 50);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnSortear
            // 
            btnSortear.ImageScaling = ToolStripItemImageScaling.None;
            btnSortear.ImageTransparentColor = Color.Magenta;
            btnSortear.Margin = new Padding(5);
            btnSortear.Name = "btnSortear";
            btnSortear.Padding = new Padding(5);
            btnSortear.Size = new Size(58, 40);
            btnSortear.Text = "Sortear";
            btnSortear.Click += btnSortear_Click;
            // 
            // listQuestoes
            // 
            listQuestoes.Dock = DockStyle.Bottom;
            listQuestoes.FormattingEnabled = true;
            listQuestoes.ItemHeight = 20;
            listQuestoes.Location = new Point(3, 73);
            listQuestoes.Margin = new Padding(3, 2, 3, 2);
            listQuestoes.Name = "listQuestoes";
            listQuestoes.Size = new Size(479, 184);
            listQuestoes.TabIndex = 0;
            // 
            // txtQtdQuestoes
            // 
            txtQtdQuestoes.Font = new Font("Segoe UI", 11.25F);
            txtQtdQuestoes.Location = new Point(444, 108);
            txtQtdQuestoes.Margin = new Padding(3, 2, 3, 2);
            txtQtdQuestoes.Name = "txtQtdQuestoes";
            txtQtdQuestoes.Size = new Size(53, 27);
            txtQtdQuestoes.TabIndex = 42;
            txtQtdQuestoes.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F);
            label1.Location = new Point(72, 43);
            label1.Name = "label1";
            label1.Size = new Size(25, 20);
            label1.TabIndex = 44;
            label1.Text = "Id:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Font = new Font("Segoe UI", 11.25F);
            txtId.Location = new Point(103, 40);
            txtId.Name = "txtId";
            txtId.PlaceholderText = "0";
            txtId.Size = new Size(60, 27);
            txtId.TabIndex = 43;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(406, 500);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(99, 35);
            btnCancelar.TabIndex = 45;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            btnGravar.DialogResult = DialogResult.OK;
            btnGravar.Location = new Point(301, 500);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(99, 35);
            btnGravar.TabIndex = 46;
            btnGravar.Text = "Gravar";
            btnGravar.UseVisualStyleBackColor = true;
            btnGravar.Click += btnGravar_Click;
            // 
            // TelaTesteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(530, 545);
            Controls.Add(btnCancelar);
            Controls.Add(btnGravar);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(txtQtdQuestoes);
            Controls.Add(groupBox1);
            Controls.Add(chkProvaRecuperacao);
            Controls.Add(txtTitulo);
            Controls.Add(label5);
            Controls.Add(cmbMaterias);
            Controls.Add(label4);
            Controls.Add(cmbDisciplinas);
            Controls.Add(label3);
            Controls.Add(label2);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TelaTesteForm";
            Text = "Geração de Testes";
            groupBox1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtQtdQuestoes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMaterias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDisciplinas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkProvaRecuperacao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSortear;
        private System.Windows.Forms.ListBox listQuestoes;
        private System.Windows.Forms.NumericUpDown txtQtdQuestoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtId;
        private Button btnCancelar;
        private Button btnGravar;
    }
}