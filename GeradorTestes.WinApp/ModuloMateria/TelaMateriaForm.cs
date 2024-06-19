using GeradorTestes.WinApp.ModuloDisciplina;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public partial class TelaMateriaForm : Form
    {
        public Materia Materia
        {
            get => materia;
        }

        private Materia materia;

        public TelaMateriaForm(List<Disciplina> disciplinasCadastradas)
        {
            InitializeComponent();

            foreach (Disciplina disciplina in disciplinasCadastradas)
                cmbDisciplinas.Items.Add(disciplina);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;

            Disciplina disciplina = (Disciplina)cmbDisciplinas.SelectedItem;

            SerieMateriaEnum serie;

            if (rdbPrimeiraSerie.Checked)
                serie = SerieMateriaEnum.PrimeiraSerie;
            else
                serie = SerieMateriaEnum.SegundaSerie;

            materia = new Materia(nome, serie, disciplina);

            List<string> erros = materia.Validar();

            if (erros.Count > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }
    }
}
