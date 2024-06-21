using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public partial class TelaVisualizacaoTesteForm : Form
    {
        public TelaVisualizacaoTesteForm(Teste testeSelecionado)
        {
            InitializeComponent();

            ConfigurarVisualizacao(testeSelecionado);
        }

        private void ConfigurarVisualizacao(Teste testeSelecionado)
        {
            lblTitulo.Text = testeSelecionado.Titulo;
            lblDisciplina.Text = testeSelecionado.Disciplina.Nome;

            lblMateria.Text =
                testeSelecionado.ProvaRecuperacao ? "Prova de Recuperação" : testeSelecionado.Materia.Nome;

            foreach (Questao q in testeSelecionado.Questoes)
                listQuestoes.Items.Add(q);
        }
    }
}
