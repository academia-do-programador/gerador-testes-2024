namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public partial class TelaDisciplinaForm : Form
    {
        public Disciplina Disciplina
        {
            get => disciplina;
            set
            {
                txtId.Text = value.Id.ToString();
                txtNome.Text = value.Nome;
            }
        }
        private Disciplina disciplina;


        private List<Disciplina> disciplinasCadastradas;

        public TelaDisciplinaForm(List<Disciplina> disciplinasCadastradas)
        {
            InitializeComponent();

            this.disciplinasCadastradas = disciplinasCadastradas;
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            disciplina = new Disciplina(txtNome.Text);

            List<string> erros = disciplina.Validar();

            if (DisciplinaTemNomeDuplicado())
                erros.Add("Já existe uma disciplina com este nome cadastrada, tente utilizar outro!");

            if (erros.Count > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }

        private bool DisciplinaTemNomeDuplicado()
        {
            return disciplinasCadastradas.Any(d => d.Nome == disciplina.Nome);
        }
    }
}
