using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;

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
            int idSelecionado = tabelaQuestao.ObterRegistroSelecionado();

            Questao questaoSelecionada = repositorioQuestao.SelecionarPorId(idSelecionado);

            if (questaoSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<Materia> materiasCadastradas = repositorioMateria.SelecionarTodos();

            TelaQuestaoForm telaQuestao = new TelaQuestaoForm(materiasCadastradas);

            telaQuestao.Questao = questaoSelecionada;

            DialogResult resultado = telaQuestao.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Questao registroEditado = telaQuestao.Questao;

            repositorioQuestao.Editar(idSelecionado, registroEditado);

            List<Alternativa> alternativasSelecionadas = telaQuestao.AlternativasSelecionadas;

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{registroEditado.Enunciado}\" foi editado com sucesso!");
        }

        public override void Excluir()
        {
            int idSelecionado = tabelaQuestao.ObterRegistroSelecionado();

            Questao questaoSelecionada = repositorioQuestao.SelecionarPorId(idSelecionado);

            if (questaoSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (questaoSelecionada.UtilizadaEmTeste)
            {
                MessageBox.Show(
                    "Não é possível excluir uma questão sendo utilizada em um teste!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resposta = MessageBox.Show(
             $"Você deseja realmente excluir o registro \"{questaoSelecionada.Enunciado}\" ",
             "Confirmar Exclusão",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Warning
             );

            if (resposta != DialogResult.Yes)
                return;

            repositorioQuestao.Excluir(idSelecionado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{questaoSelecionada.Enunciado}\" foi excluído com sucesso!");
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
