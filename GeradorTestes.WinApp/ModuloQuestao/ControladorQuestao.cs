using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloMateria;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public class ControladorQuestao : ControladorBase
    {
        public override string TipoCadastro => "Questões";
        public override string ToolTipAdicionar => "Cadastrar uma nova Questão";
        public override string ToolTipEditar => "Editar uma Questão existente";
        public override string ToolTipExcluir => "Excluir uma Questão existente";

        TabelaQuestaoControl tabelaQuestao;

        IRepositorioQuestao repositorioQuestao;
        IRepositorioMateria repositorioMateria;

        public ControladorQuestao(IRepositorioQuestao repositorioQuestao, IRepositorioMateria repositorioMateria)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioMateria = repositorioMateria;
        }

        public override void Adicionar()
        {
            List<Materia> materiasCadastradas = repositorioMateria.SelecionarTodos();

            TelaQuestaoForm telaQuestao = new TelaQuestaoForm(materiasCadastradas);

            DialogResult resultado = telaQuestao.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Questao novoRegistro = telaQuestao.Questao;

            repositorioQuestao.Cadastrar(novoRegistro);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{novoRegistro.Enunciado}\" foi criado com sucesso!");
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
            if (tabelaQuestao == null)
                tabelaQuestao = new TabelaQuestaoControl();

            CarregarRegistros();

            return tabelaQuestao;
        }

        public override void CarregarRegistros()
        {
            List<Questao> questoes = repositorioQuestao.SelecionarTodos();

            tabelaQuestao.AtualizarRegistros(questoes);
        }
    }
}
