using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;

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
            List<Materia> materiasCadastradas = repositorioMateria.SelecionarTodos();
            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaMateriaForm telaMateria = new TelaMateriaForm(materiasCadastradas, disciplinasCadastradas);

            DialogResult resultado = telaMateria.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Materia novoRegistro = telaMateria.Materia;

            novoRegistro.AtribuirDisciplina(novoRegistro.Disciplina);

            repositorioMateria.Cadastrar(novoRegistro);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{novoRegistro.Nome}\" foi criado com sucesso!");
        }

        public override void Editar()
        {
            int idSelecionado = tabelaMateria.ObterRegistroSelecionado();

            Materia materiaSelecionada = repositorioMateria.SelecionarPorId(idSelecionado);

            if (materiaSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            List<Materia> materiasCadastradas = repositorioMateria.SelecionarTodos();
            List<Disciplina> disciplinasCadastradas = repositorioDisciplina.SelecionarTodos();

            TelaMateriaForm telaMateria = new TelaMateriaForm(materiasCadastradas, disciplinasCadastradas);

            telaMateria.Materia = materiaSelecionada;

            DialogResult resultado = telaMateria.ShowDialog();

            if (resultado != DialogResult.OK)
                return;

            Materia registroEditado = telaMateria.Materia;

            materiaSelecionada.RemoverDisciplina();

            registroEditado.AtribuirDisciplina(registroEditado.Disciplina);

            repositorioMateria.Editar(idSelecionado, registroEditado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{registroEditado.Nome}\" foi editado com sucesso!");
        }

        public override void Excluir()
        {
            int idSelecionado = tabelaMateria.ObterRegistroSelecionado();

            Materia materiaSelecionada = repositorioMateria.SelecionarPorId(idSelecionado);

            if (materiaSelecionada == null)
            {
                MessageBox.Show(
                    "Você precisa selecionar um registro para executar esta ação!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (materiaSelecionada.Questoes.Count(q => q.UtilizadaEmTeste) > 0)
            {
                MessageBox.Show(
                    "Você precisa remover as questões relacionadas antes de excluir a matéria!",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult resposta = MessageBox.Show(
                $"Você deseja realmente excluir o registro \"{materiaSelecionada.Nome}\" ",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );

            if (resposta != DialogResult.Yes)
                return;

            materiaSelecionada.RemoverDisciplina();

            repositorioMateria.Excluir(idSelecionado);

            CarregarRegistros();

            TelaPrincipalForm.Instancia.AtualizarRodape($"O registro \"{materiaSelecionada.Nome}\" foi exluído com sucesso!");
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
