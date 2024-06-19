using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public class ControladorMateria : ControladorBase
    {
        public override string TipoCadastro => "Matérias";

        public override string ToolTipAdicionar => "Cadastrar uma nova Matéria";

        public override string ToolTipEditar => "Editar uma Matéria existente";

        public override string ToolTipExcluir => "Excluir uma Matéria existente";

        TabelaMateriaControl tabelaMateria;

        IRepositorioMateria repositorioMateria;
        IRepositorioDisciplina repositorioDisciplina;

        public ControladorMateria(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public override void Adicionar()
        {
            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaMateriaForm telaMateria = new TelaMateriaForm(disciplinasCadastradas);

            DialogResult resultado = telaMateria.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Materia novoRegistro = telaMateria.Materia;

            novoRegistro.AtribuirDisciplina();

            repositorioMateria.Cadastrar(novoRegistro);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{novoRegistro.Nome}\" foi criado com sucesso!");
        }

        public override void Editar()
        {
            throw new NotImplementedException();
        }

        public override void Excluir()
        {
            throw new NotImplementedException();
        }

        public override UserControl ObterListagem()
        {
            if (tabelaMateria == null)
                tabelaMateria = new TabelaMateriaControl();

            CarregarRegistros();

            return tabelaMateria;
        }

        public override void CarregarRegistros()
        {
            List<Materia> materias = repositorioMateria.SelecionarTodos();

            tabelaMateria.AtualizarRegistros(materias);
        }
    }
}
