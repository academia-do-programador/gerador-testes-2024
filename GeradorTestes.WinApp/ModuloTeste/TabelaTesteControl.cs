using GeradorDeTestes.WinApp.Compartilhado;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public partial class TabelaTesteControl : UserControl
    {
        public TabelaTesteControl()
        {
            InitializeComponent();

            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" },
                new DataGridViewTextBoxColumn { Name = "Titulo", HeaderText = "Título", },
                new DataGridViewTextBoxColumn { Name = "Disciplina.Nome", HeaderText = "Disciplina" },
                new DataGridViewTextBoxColumn { Name = "ProvaRecuperacao", HeaderText = "Matéria" },
                new DataGridViewTextBoxColumn { Name = "Questoes", HeaderText = "Qtd. de Questões" },
            };

            return colunas;
        }

        public int ObterRegistroSelecionado()
        {
            return grid.SelecionarId();
        }

        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.Rows.Clear();

            foreach (Teste teste in testes)
            {
                string materia = teste.ProvaRecuperacao ? "Prova de Recuperação" : teste.Materia.Nome;

                grid.Rows.Add(teste.Id, teste.Titulo, teste.Disciplina, materia, teste.Questoes.Count);
            }
        }
    }
}
