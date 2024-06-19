namespace GeradorTestes.WinApp.ModuloMateria
{
    partial class TelaMateriaForm
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
            lblId = new Label();
            txtId = new TextBox();
            lblNome = new Label();
            txtNome = new TextBox();
            btnGravar = new Button();
            btnCancelar = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            rdbSegundaSerie = new RadioButton();
            rdbPrimeiraSerie = new RadioButton();
            cmbDisciplinas = new ComboBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(73, 39);
            lblId.Name = "lblId";
            lblId.Size = new Size(25, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Id:";
            // 
            // txtId
            // 
            txtId.Enabled = false;
            txtId.Location = new Point(104, 36);
            txtId.Name = "txtId";
            txtId.PlaceholderText = "0";
            txtId.Size = new Size(100, 27);
            txtId.TabIndex = 1;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Location = new Point(45, 72);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(53, 20);
            lblNome.TabIndex = 0;
            lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            txtNome.Location = new Point(104, 69);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(376, 27);
            txtNome.TabIndex = 0;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // btnGravar
            // 
            btnGravar.DialogResult = DialogResult.OK;
            btnGravar.Location = new Point(276, 215);
            btnGravar.Name = "btnGravar";
            btnGravar.Size = new Size(99, 35);
            btnGravar.TabIndex = 7;
            btnGravar.Text = "Gravar";
            btnGravar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(381, 215);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(99, 35);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 105);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "Disciplina:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rdbSegundaSerie);
            groupBox1.Controls.Add(rdbPrimeiraSerie);
            groupBox1.Location = new Point(104, 145);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(376, 54);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Escolha de Série";
            // 
            // rdbSegundaSerie
            // 
            rdbSegundaSerie.AutoSize = true;
            rdbSegundaSerie.Location = new Point(131, 24);
            rdbSegundaSerie.Name = "rdbSegundaSerie";
            rdbSegundaSerie.Size = new Size(78, 24);
            rdbSegundaSerie.TabIndex = 4;
            rdbSegundaSerie.Text = "2ª Série";
            rdbSegundaSerie.UseVisualStyleBackColor = true;
            // 
            // rdbPrimeiraSerie
            // 
            rdbPrimeiraSerie.AutoSize = true;
            rdbPrimeiraSerie.Checked = true;
            rdbPrimeiraSerie.Location = new Point(22, 24);
            rdbPrimeiraSerie.Name = "rdbPrimeiraSerie";
            rdbPrimeiraSerie.Size = new Size(78, 24);
            rdbPrimeiraSerie.TabIndex = 3;
            rdbPrimeiraSerie.TabStop = true;
            rdbPrimeiraSerie.Text = "1ª Série";
            rdbPrimeiraSerie.UseVisualStyleBackColor = true;
            // 
            // cmbDisciplinas
            // 
            cmbDisciplinas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisciplinas.FormattingEnabled = true;
            cmbDisciplinas.Location = new Point(104, 102);
            cmbDisciplinas.Name = "cmbDisciplinas";
            cmbDisciplinas.Size = new Size(209, 28);
            cmbDisciplinas.TabIndex = 1;
            // 
            // TelaMateriaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 262);
            Controls.Add(cmbDisciplinas);
            Controls.Add(groupBox1);
            Controls.Add(btnCancelar);
            Controls.Add(btnGravar);
            Controls.Add(label1);
            Controls.Add(txtNome);
            Controls.Add(lblNome);
            Controls.Add(txtId);
            Controls.Add(lblId);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "TelaMateriaForm";
            Text = "Cadastro de Matéria";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblId;
        private TextBox txtId;
        private Label lblNome;
        private TextBox txtNome;
        private Button btnGravar;
        private Button btnCancelar;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton rdbSegundaSerie;
        private RadioButton rdbPrimeiraSerie;
        private ComboBox cmbDisciplinas;
    }
}