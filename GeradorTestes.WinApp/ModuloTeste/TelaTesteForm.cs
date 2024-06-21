using GeradorTestes.WinApp.ModuloDisciplina;
using GeradorTestes.WinApp.ModuloMateria;
using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public partial class TelaTesteForm : Form
    {
        public Teste Teste
        {
            get => teste;
            set
            {
                this.Text = "Duplicação de Teste";

                txtTitulo.PlaceholderText = "Digite um novo título para o teste duplicado";

                cmbDisciplinas.SelectedItem = value.Disciplina;

                if (value.ProvaRecuperacao)
                    chkProvaRecuperacao.Checked = true;
                else
                    cmbMaterias.SelectedItem = value.Materia;

                txtQtdQuestoes.Value = value.Questoes.Count;
            }
        }
        private Teste teste;

        public List<Questao> QuestoesSorteadas
        {
            get => listQuestoes.Items.Cast<Questao>().ToList();
        }

        public TelaTesteForm(List<Disciplina> disciplinasCadastradas)
        {
            InitializeComponent();

            foreach (Disciplina disciplina in disciplinasCadastradas)
                cmbDisciplinas.Items.Add(disciplina);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text;

            Disciplina disciplina = (Disciplina)cmbDisciplinas.SelectedItem;
            Materia materia = (Materia)cmbMaterias.SelectedItem;

            bool provaRecuperacao = chkProvaRecuperacao.Checked;

            teste = new Teste(titulo, disciplina, materia, provaRecuperacao);

            List<string> erros = teste.Validar();

            if (QuestoesSorteadas.Count <= 1)
                erros.Add("A quantidade de questões deve ser maior que 1");

            if (erros.Count > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }

        private void btnSortear_Click(object sender, EventArgs e)
        {
            if (cmbDisciplinas.SelectedItem == null ||
                cmbMaterias.Enabled && cmbMaterias.SelectedItem == null)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Você precisa selecionar uma disciplina e matérias / prova de recuperação!");
                return;
            }

            List<Questao> questoesSelecionadas;

            int quantidadeQuestoes = Convert.ToInt32(txtQtdQuestoes.Value);

            if (chkProvaRecuperacao.Checked)
            {
                Disciplina disciplinaSelecionada = (Disciplina)cmbDisciplinas.SelectedItem;

                questoesSelecionadas = disciplinaSelecionada.ObterQuestoesAleatorias(quantidadeQuestoes);
            }
            else
            {
                Materia materiaSelecionada = (Materia)cmbMaterias.SelectedItem;

                questoesSelecionadas = materiaSelecionada.ObterQuestoesAleatorias(quantidadeQuestoes);
            }

            listQuestoes.Items.Clear();

            foreach (Questao q in questoesSelecionadas)
                listQuestoes.Items.Add(q);
        }

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkProvaRecuperacao.Checked)
                cmbMaterias.Enabled = true;

            CarregarMateriasDaDisciplina();
        }

        private void cmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            listQuestoes.Items.Clear();
        }

        private void chkProvao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProvaRecuperacao.Checked)
            {
                cmbMaterias.Items.Clear();
                cmbMaterias.Enabled = false;
            }
            else
            {
                cmbMaterias.Enabled = true;
                CarregarMateriasDaDisciplina();
            }

            listQuestoes.Items.Clear();
        }

        private void CarregarMateriasDaDisciplina()
        {
            Disciplina disciplinaSelecionada = (Disciplina)cmbDisciplinas.SelectedItem;
            if (disciplinaSelecionada == null)
                return;

            cmbMaterias.Items.Clear();

            foreach (Materia m in disciplinaSelecionada.Materias)
                cmbMaterias.Items.Add(m);
        }
    }
}
