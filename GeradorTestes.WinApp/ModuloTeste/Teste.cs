using GeradorDeTestes.ConsoleApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;
using GeradorTestes.WinApp.ModuloMateria;
using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class Teste : EntidadeBase
    {
        public string Titulo { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia Materia { get; set; }

        public List<Questao> Questoes { get; set; }

        public DateTime DataGeracao { get; set; }

        public bool ProvaRecuperacao { get; set; }

        public Teste()
        {
            Questoes = new List<Questao>();
        }

        public Teste(string titulo, Disciplina disciplina, Materia materia, bool provaRecuperacao) : this()
        {
            Titulo = titulo;
            Disciplina = disciplina;
            Materia = materia;
            ProvaRecuperacao = provaRecuperacao;

            DataGeracao = DateTime.Now;
        }

        public void AdicionarQuestao(Questao questao)
        {
            Questoes.Add(questao);
        }

        public void RemoverQuestao(Questao questao)
        {
            Questoes.Remove(questao);
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Titulo))
                erros.Add($"O 'título' do teste deve estar preenchido");

            if (Titulo.Length <= 2)
                erros.Add($"O 'título' do teste deve ter mais de 3 letras");

            if (Disciplina == null)
                erros.Add($"A 'disciplina' do teste deve estar preenchida");

            if (DataGeracao == DateTime.MinValue)
                erros.Add($"A 'data' do teste deve estar preenchida");

            if (ProvaRecuperacao == false && Materia == null)
            {
                erros.Add($"A 'matéria' do teste deve estar preenchida");
            }

            if (ProvaRecuperacao)
            {
                if (Materia != null && Materia.Questoes.Count < 1)
                    erros.Add("Matéria deve ter no mínimo uma questão");
            }

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Teste testeEditado = (Teste)novoRegistro;

            Titulo = testeEditado.Titulo;
            Disciplina = testeEditado.Disciplina;
            Materia = testeEditado.Materia;
            Questoes = Questoes;
            ProvaRecuperacao = testeEditado.ProvaRecuperacao;
        }
    }
}
