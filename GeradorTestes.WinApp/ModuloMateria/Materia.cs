using GeradorDeTestes.ConsoleApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;
using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public class Materia : EntidadeBase
    {
        public string Nome { get; set; }

        public SerieMateriaEnum Serie { get; set; }

        public Disciplina Disciplina { get; set; }

        public List<Questao> Questoes { get; set; }

        public Materia()
        {
            Questoes = new List<Questao>();
        }

        public Materia(string nome, SerieMateriaEnum serie, Disciplina disciplina) : this()
        {
            Nome = nome;
            Serie = serie;
            Disciplina = disciplina;
        }

        public bool AtribuirDisciplina()
        {
            bool conseguiuAdicionar = Disciplina.AdicionarMateria(this);

            return conseguiuAdicionar;
        }

        public bool RemoverDisciplina()
        {
            bool conseguiuRemover = Disciplina.RemoverMateria(this);

            if (!conseguiuRemover)
                return false;

            Disciplina = null;

            return true;
        }

        public void AdicionarQuestao(Questao questao)
        {
            if (Questoes.Contains(questao))
                return;

            Questoes.Add(questao);
        }

        public void RemoverQuestao(Questao questao)
        {
            if (!Questoes.Contains(questao))
                return;

            Questoes.Remove(questao);
        }

        public List<Questao> ObterQuestoesAleatorias(int quantidadeQuestoes)
        {
            Random random = new Random();

            return Questoes
                .OrderBy(q => random.Next())
                .Take(quantidadeQuestoes)
                .ToList();
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Materia materiaEditada = (Materia)novoRegistro;

            Nome = materiaEditada.Nome;
            Serie = materiaEditada.Serie;
            Disciplina = materiaEditada.Disciplina;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add($"O nome da matéria deve estar preenchido!");

            if (Nome.Length < 2)
                erros.Add($"O nome da matéria deve ter mais de 2 letras!");

            return erros;
        }

        public override string ToString()
        {
            return $"{Nome}";
        }
    }
}
