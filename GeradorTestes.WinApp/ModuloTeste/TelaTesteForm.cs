using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;

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

        private List<Materia> materiasCadastradas;

        public TelaTesteForm(List<Disciplina> disciplinasCadastradas, List<Materia> materiasCadastradas)
        {
            InitializeComponent();

            this.materiasCadastradas = materiasCadastradas;

            foreach (Disciplina disciplina in disciplinasCadastradas)
                cmbDisciplinas.Items.Add(disciplina);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            teste = ObterTeste();

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

            int quantidadeQuestoes = Convert.ToInt32(txtQtdQuestoes.Value);

            List<Questao> questoesSelecionadas = ObterTeste().SortearQuestoes();

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

            foreach (Materia m in materiasCadastradas)
            {
                if (m.Disciplina.Id == disciplinaSelecionada.Id)
                    disciplinaSelecionada.AdicionarMateria(m);
            }

            cmbMaterias.Items.Clear();

            foreach (Materia m in disciplinaSelecionada.Materias)
                cmbMaterias.Items.Add(m);
        }

        private Teste ObterTeste()
        {
            string titulo = txtTitulo.Text;

            Disciplina disciplina = (Disciplina)cmbDisciplinas.SelectedItem;
            Materia materia = (Materia)cmbMaterias.SelectedItem;

            bool provaRecuperacao = chkProvaRecuperacao.Checked;

            int quantidadeQuestoes = (int)txtQtdQuestoes.Value;

            return new Teste(titulo, disciplina, materia, provaRecuperacao, quantidadeQuestoes);
        }

    }
}
