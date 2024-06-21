using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase
    {
        public override string TipoCadastro => "Testes";
        public override string ToolTipAdicionar => "Cadastrar um novo Teste";
        public override string ToolTipEditar => "Editar um Teste existente";
        public override string ToolTipExcluir => "Excluir um Teste existente";

        TabelaTesteControl tabelaTeste;

        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioTeste repositorioTeste;

        public ControladorTeste(IRepositorioDisciplina repositorioDisciplina, IRepositorioTeste repositorioTeste)
        {
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioTeste = repositorioTeste;
        }

        public override void Adicionar()
        {
            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaTesteForm telaTeste = new TelaTesteForm(disciplinasCadastradas);

            DialogResult resultado = telaTeste.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Teste novoRegistro = telaTeste.Teste;

            repositorioTeste.Cadastrar(novoRegistro);

            repositorioTeste.AdicionarQuestoes(novoRegistro, telaTeste.QuestoesSorteadas);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{novoRegistro.Titulo}\" foi criado com sucesso!");
        }

        public override void Editar()
        {
            return;
        }

        public override void Excluir()
        {
            throw new NotImplementedException();
        }

        public override UserControl ObterListagem()
        {
            if (tabelaTeste == null)
                tabelaTeste = new TabelaTesteControl();

            CarregarRegistros();

            return tabelaTeste;
        }

        public override void CarregarRegistros()
        {
            List<Teste> questoes = repositorioTeste.SelecionarTodos();

            tabelaTeste.AtualizarRegistros(questoes);
        }
    }
}
