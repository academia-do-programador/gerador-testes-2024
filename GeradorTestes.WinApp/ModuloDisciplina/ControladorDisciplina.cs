using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.Dominio.ModuloDisciplina;

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
            int idSelecionado = tabelaDisciplina.ObterRegistroSelecionado();

            Disciplina disciplinaSelecionada = repositorioDisciplina.SelecionarPorId(idSelecionado);

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaDisciplinaForm telaDisciplina = new TelaDisciplinaForm(disciplinasCadastradas);

            telaDisciplina.Disciplina = disciplinaSelecionada;

            DialogResult resultado = telaDisciplina.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Disciplina registroEditado = telaDisciplina.Disciplina;

            repositorioDisciplina.Editar(idSelecionado, registroEditado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{registroEditado.Nome}\" foi editado com sucesso!");
        }

        public override void Excluir()
        {
            int idSelecionado = tabelaDisciplina.ObterRegistroSelecionado();

            Disciplina disciplinaSelecionada = repositorioDisciplina.SelecionarPorId(idSelecionado);

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (disciplinaSelecionada.Materias.Count > 0)
            {
                MessageBox.Show(
                    "Você precisa remover as matérias relacionadas antes de excluir a disciplina!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (disciplinaSelecionada.Testes.Count > 0)
            {
                MessageBox.Show(
                    "Você precisa remover os testes relacionados antes de excluir a disciplina!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resposta = MessageBox.Show(
                $"Você deseja realmente excluir o registro \"{disciplinaSelecionada.Nome}\" ",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );

            if (resposta != DialogResult.Yes)
                return;

            repositorioDisciplina.Excluir(idSelecionado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{disciplinaSelecionada.Nome}\" foi exluído com sucesso!");
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
