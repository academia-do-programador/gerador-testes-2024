namespace GeradorTestes.WinApp.ModuloTeste
{
    partial class TelaVisualizacaoTesteForm
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
            label1 = new Label();
            lblTitulo = new Label();
            lblDisciplina = new Label();
            label4 = new Label();
            lblMateria = new Label();
            label6 = new Label();
            groupBox1 = new GroupBox();
            listQuestoes = new ListBox();
            btnGravar = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F);
            label1.Location = new Point(66, 50);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "Título:";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblTitulo.Location = new Point(122, 50);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(62, 20);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "[Titulo]";
            // 
            // lblDisciplina
            // 
            lblDisciplina.AutoSize = true;
            lblDisciplina.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblDisciplina.Location = new Point(122, 79);
            lblDisciplina.Name = "lblDisciplina";
            lblDisciplina.Size = new Size(88, 20);
            lblDisciplina.TabIndex = 3;
            lblDisciplina.Text = "[Disciplina]";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F);
            label4.Location = new Point(39, 79);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 2;
            label4.Text = "Disciplina:";
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblMateria.Location = new Point(122, 108);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new Size(75, 20);
            lblMateria.TabIndex = 5;
            lblMateria.Text = "[Matéria]";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F);
            label6.Location = new Point(53, 108);
            label6.Name = "label6";
            label6.Size = new Size(63, 20);
            label6.TabIndex = 4;
            label6.Text = "Matéria:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listQuestoes);
            groupBox1.Font = new Font("Segoe UI", 11.25F);
            groupBox1.Location = new Point(25, 183);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(468, 250);
            groupBox1.TabIndex = 40;
            groupBox1.TabStop = false;
            groupBox1.Text = "Questões Selecionadas:";
            // 
            // listQuestoes
            // 
            listQuestoes.Dock = DockStyle.Bottom;
            listQuestoes.FormattingEnabled = true;
            listQuestoes.ItemHeight = 20;
            listQuestoes.Location = new Point(3, 24);
            listQuestoes.Margin = new Padding(3, 2, 3, 2);
            listQuestoes.Name = "listQuestoes";
            listQuestoes.Size = new Size(462, 224);
            listQuestoes.TabIndex = 0;
            // 
            // btnGravar
            // 
            btnGravar.DialogResult = DialogResult.OK;
            btnGravar.Location = new Point(394, 450);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(99, 35);
            btnGravar.TabIndex = 47;
            btnGravar.Text = "Voltar";
            btnGravar.UseVisualStyleBackColor = true;
            // 
            // TelaVisualizacaoTesteForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 497);
            Controls.Add(btnGravar);
            Controls.Add(groupBox1);
            Controls.Add(lblMateria);
            Controls.Add(label6);
            Controls.Add(lblDisciplina);
            Controls.Add(label4);
            Controls.Add(lblTitulo);
            Controls.Add(label1);
            Name = "TelaVisualizacaoTesteForm";
            Text = "Visualização do Teste";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDisciplina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listQuestoes;
        private Button btnGravar;
    }
}