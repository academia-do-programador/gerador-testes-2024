using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Infra.Arquivos.Compartilhado;
using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.Arquivos.ModuloDisciplina;
using GeradorTestes.Infra.Arquivos.ModuloMateria;
using GeradorTestes.Infra.Arquivos.ModuloQuestao;
using GeradorTestes.Infra.Arquivos.ModuloTeste;
using GeradorTestes.WinApp.ModuloDisciplina;
using GeradorTestes.WinApp.ModuloMateria;
using GeradorTestes.WinApp.ModuloQuestao;
using GeradorTestes.WinApp.ModuloTeste;

namespace GeradorTestes.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        public static TelaPrincipalForm Instancia { get; private set; }

        ControladorBase controlador;
        ContextoDados contexto;

        IRepositorioDisciplina repositorioDisciplina;
        IRepositorioMateria repositorioMateria;
        IRepositorioQuestao repositorioQuestao;
        IRepositorioTeste repositorioTeste;

        public TelaPrincipalForm()
        {
            InitializeComponent();
            Instancia = this;

            contexto = new ContextoDados(true);

            repositorioDisciplina = new RepositorioDisciplinaEmArquivo();
            repositorioMateria = new RepositorioMateriaEmArquivo();
            repositorioQuestao = new RepositorioQuestaoEmArquivo();
            repositorioTeste = new RepositorioTesteEmArquivo();
        }

        public void AtualizarRodape(string texto)
        {
            statusLabelPrincipal.Text = texto;
        }

        private void disciplinaMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorDisciplina(repositorioDisciplina);

            ConfigurarTelaPrincipal(controlador);
        }

        private void materiasMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorMateria(repositorioMateria, repositorioDisciplina);

            ConfigurarTelaPrincipal(controlador);
        }

        private void questoesMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorQuestao(repositorioQuestao, repositorioMateria);

            ConfigurarTelaPrincipal(controlador);
        }

        private void testesMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorTeste(repositorioDisciplina, repositorioTeste);

            ConfigurarTelaPrincipal(controlador);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            controlador.Adicionar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorVisualizavel controladorVisualizavel)
                controladorVisualizavel.Visualizar();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorDuplicavel controladorDuplicavel)
                controladorDuplicavel.Duplicar();
        }

        private void ConfigurarTelaPrincipal(ControladorBase controladorSelecionado)
        {
            lblTipoCadastro.Text = "Cadastro de " + controladorSelecionado.TipoCadastro;

            ConfigurarToolBox(controladorSelecionado);
            ConfigurarListagem(controladorSelecionado);
        }

        private void ConfigurarToolBox(ControladorBase controladorSelecionado)
        {
            btnAdicionar.Enabled = controladorSelecionado is ControladorBase;
            btnEditar.Enabled = controladorSelecionado is ControladorBase;
            btnExcluir.Enabled = controladorSelecionado is ControladorBase;

            btnFiltrar.Enabled = controladorSelecionado is IControladorFiltravel;
            btnDuplicar.Enabled = controladorSelecionado is IControladorDuplicavel;
            btnVisualizar.Enabled = controladorSelecionado is IControladorVisualizavel;

            ConfigurarToolTips(controladorSelecionado);
        }

        private void ConfigurarToolTips(ControladorBase controladorSelecionado)
        {
            btnAdicionar.ToolTipText = controladorSelecionado.ToolTipAdicionar;
            btnEditar.ToolTipText = controladorSelecionado.ToolTipEditar;
            btnExcluir.ToolTipText = controladorSelecionado.ToolTipExcluir;

            if (controladorSelecionado is IControladorFiltravel controladorFiltravel)
                btnFiltrar.ToolTipText = controladorFiltravel.ToolTipFiltrar;

            if (controladorSelecionado is IControladorDuplicavel controladorDuplicavel)
                btnDuplicar.ToolTipText = controladorDuplicavel.ToolTipDuplicar;

            if (controladorSelecionado is IControladorVisualizavel controladorVisualizavel)
                btnVisualizar.ToolTipText = controladorVisualizavel.ToolTipVisualizar;
        }

        private void ConfigurarListagem(ControladorBase controladorSelecionado)
        {
            UserControl listagemContato = controladorSelecionado.ObterListagem();
            listagemContato.Dock = DockStyle.Fill;

            pnlRegistros.Controls.Clear();
            pnlRegistros.Controls.Add(listagemContato);
        }
    }
}
