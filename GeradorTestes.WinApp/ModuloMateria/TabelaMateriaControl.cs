using GeradorDeTestes.WinApp.Compartilhado.Extensions;
using GeradorTestes.Dominio.ModuloMateria;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public partial class TabelaMateriaControl : UserControl
    {
        public TabelaMateriaControl()
        {
            InitializeComponent();

            grid.Columns.AddRange(ObterColunas());

            grid.ConfigurarGridSomenteLeitura();
            grid.ConfigurarGridZebrado();
        }

        public void AtualizarRegistros(List<Materia> materias)
        {
            grid.Rows.Clear();

            foreach (Materia d in materias)
                grid.Rows.Add(d.Id, d.Nome, d.Disciplina.Nome, d.Serie.ToString());
        }

        public int ObterRegistroSelecionado()
        {
            return grid.SelecionarId();
        }

        private DataGridViewColumn[] ObterColunas()
        {
            return new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id" },
                new DataGridViewTextBoxColumn { DataPropertyName = "Nome", HeaderText = "Nome" },
                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina", HeaderText = "Disciplina" },
                new DataGridViewTextBoxColumn { DataPropertyName = "Serie", HeaderText = "Série" }
            };
        }
    }
}
