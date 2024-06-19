using GeradorDeTestes.WinApp.Compartilhado;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class ControladorDisciplina : ControladorBase
    {
        public override string TipoCadastro => "Disciplinas";

        public override string ToolTipAdicionar => "Cadastrar uma nova Disciplina";

        public override string ToolTipEditar => "Editar uma Disciplina existente";

        public override string ToolTipExcluir => "Excluir uma Disciplina existente";

        TabelaDisciplinaControl tabelaDisciplina;

        IRepositorioDisciplina repositorioDisciplina;

        public ControladorDisciplina(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina;
        }

        public override void Adicionar()
        {
            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaDisciplinaForm telaDisciplina = new TelaDisciplinaForm(disciplinasCadastradas);

            DialogResult resultado = telaDisciplina.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Disciplina novoRegistro = telaDisciplina.Disciplina;

            repositorioDisciplina.Cadastrar(novoRegistro);

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
            if (tabelaDisciplina == null)
                tabelaDisciplina = new TabelaDisciplinaControl();

            CarregarRegistros();

            return tabelaDisciplina;
        }

        public override void CarregarRegistros()
        {
            List<Disciplina> disciplinas = repositorioDisciplina.SelecionarTodos();

            tabelaDisciplina.AtualizarRegistros(disciplinas);
        }
    }
}
