using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloTeste;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class ControladorTeste : ControladorBase, IControladorDuplicavel, IControladorVisualizavel
    {
        public override string TipoCadastro => "Testes";
        public override string ToolTipAdicionar => "Cadastrar um novo Teste";
        public override string ToolTipEditar => "Editar um Teste existente";
        public override string ToolTipExcluir => "Excluir um Teste existente";
        public string ToolTipDuplicar => "Duplicar um teste existente";
        public string ToolTipVisualizar => "Visualizar detalhes de um teste existente";

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
            int idSelecionado = tabelaTeste.ObterRegistroSelecionado();

            Teste testeSelecionado = repositorioTeste.SelecionarPorId(idSelecionado);

            if (testeSelecionado == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resposta = MessageBox.Show(
             $"Você deseja realmente excluir o registro \"{testeSelecionado.Titulo}\" ",
             "Confirmar Exclusão",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Warning
             );

            if (resposta != DialogResult.Yes)
                return;

            repositorioTeste.Excluir(idSelecionado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{testeSelecionado.Titulo}\" foi excluído com sucesso!");
        }

        public void Duplicar()
        {
            int idSelecionado = tabelaTeste.ObterRegistroSelecionado();

            Teste testeSelecionado = repositorioTeste.SelecionarPorId(idSelecionado);

            if (testeSelecionado == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaTesteForm telaTeste = new TelaTesteForm(disciplinasCadastradas);

            telaTeste.Teste = testeSelecionado;

            DialogResult resultado = telaTeste.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Teste registroDuplicado = telaTeste.Teste;

            repositorioTeste.Cadastrar(registroDuplicado);

            repositorioTeste.AdicionarQuestoes(registroDuplicado, telaTeste.QuestoesSorteadas);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{registroDuplicado.Titulo}\" foi criado em uma duplicação com sucesso!");
        }

        public void Visualizar()
        {
            int idSelecionado = tabelaTeste.ObterRegistroSelecionado();

            Teste testeSelecionado = repositorioTeste.SelecionarPorId(idSelecionado);

            if (testeSelecionado == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            TelaVisualizacaoTesteForm telaVisualizacao =
                new TelaVisualizacaoTesteForm(testeSelecionado);

            telaVisualizacao.ShowDialog();
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
