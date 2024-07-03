using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.WinApp.Compartilhado.Extensions;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public partial class TelaQuestaoForm : Form
    {
        public Questao Questao
        {
            get => questao;
            set
            {
                txtId.Text = value.Id.ToString();
                txtEnunciado.Text = value.Enunciado;

                cmbMaterias.SelectedItem = value.Materia;

                int qtdAlternativas = 0;

                foreach (Alternativa a in value.Alternativas)
                {
                    listAlternativas.Items.Add(a);

                    if (a.Correta)
                        listAlternativas.SetItemChecked(qtdAlternativas, true);

                    qtdAlternativas++;
                }
            }
        }
        private Questao questao;

        public List<Alternativa> AlternativasSelecionadas
        {
            get
            {
                return listAlternativas.Items.Cast<Alternativa>().ToList();
            }
        }

        public TelaQuestaoForm(List<Materia> materiasCadastradas)
        {
            InitializeComponent();

            foreach (Materia m in materiasCadastradas)
                cmbMaterias.Items.Add(m);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            string enunciado = txtEnunciado.Text;
            Materia materia = (Materia)cmbMaterias.SelectedItem;

            questao = new Questao(enunciado, materia);

            List<string> erros = questao.Validar();

            if (erros.Count > 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape(erros[0]);

                DialogResult = DialogResult.None;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            string resposta = txtResposta.Text;

            char letra = GerarLetraDaAlternativa();

            Alternativa alternativa = new Alternativa(letra, resposta);

            listAlternativas.Items.Add(alternativa);
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Alternativa alternativa = (Alternativa)listAlternativas.SelectedItem;

            if (alternativa == null)
                return;

            listAlternativas.Items.Remove(alternativa);

            RecarregarAlternativas();
        }

        private void listAlternativas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int itemSelecionado = e.Index;

            for (int i = 0; i < listAlternativas.Items.Count; i++)
            {
                if (i != itemSelecionado)
                {
                    listAlternativas.SetItemChecked(i, false);

                    Alternativa alternativaNaoChecada = (Alternativa)listAlternativas.Items[i];

                    alternativaNaoChecada.Correta = false;
                }
            }

            Alternativa alternativaChecada = (Alternativa)listAlternativas.Items[itemSelecionado];

            alternativaChecada.Correta = !alternativaChecada.Correta;
        }

        private char GerarLetraDaAlternativa()
        {
            return AlternativasSelecionadas.Count == 0 ? 'A' :
                AlternativasSelecionadas.Select(x => x.Letra).Last().Proximo();
        }

        private void RecarregarAlternativas()
        {
            List<Alternativa> alternativas = AlternativasSelecionadas;

            listAlternativas.Items.Clear();

            foreach (Alternativa a in alternativas)
            {
                char letra = GerarLetraDaAlternativa();

                Alternativa alternativaRecriada = new Alternativa(letra, a.Resposta);

                listAlternativas.Items.Add(alternativaRecriada);
            }
        }
    }
}
