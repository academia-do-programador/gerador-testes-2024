using GeradorTestes.WinApp.ModuloDisciplina;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public partial class TelaMateriaForm : Form
    {
        public Materia Materia
        {
            get => materia;
            set
            {
                txtId.Text = value.Id.ToString();
                txtNome.Text = value.Nome;

                cmbDisciplinas.SelectedItem = value.Disciplina;

                if (value.Serie == SerieMateriaEnum.PrimeiraSerie)
                    rdbPrimeiraSerie.Checked = true;
                else
                    rdbSegundaSerie.Checked = true;
            }
        }

        private Materia materia;

        private List<Materia> materiasCadastradas;

        public TelaMateriaForm(List<Materia> materiasCadastradas, List<Disciplina> disciplinasCadastradas)
        {
            InitializeComponent();

            this.materiasCadastradas = materiasCadastradas;

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

            if (MateriaTemNomeDuplicado())
                erros.Add("Já existe uma matéria com este nome cadastrada, tente utilizar outro!");

            if (erros.Count > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }

        private bool MateriaTemNomeDuplicado()
        {
            return materiasCadastradas.Any(m => m.Nome == materia.Nome);
        }
    }
}
