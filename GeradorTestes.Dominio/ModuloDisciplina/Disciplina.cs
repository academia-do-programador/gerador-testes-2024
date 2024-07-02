using GeradorDeTestes.ConsoleApp.Compartilhado;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;

namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase
    {
        public string Nome { get; set; }

        public List<Materia> Materias { get; set; }
        public List<Questao> Questoes { get; set; }

        public Disciplina()
        {
            Materias = new List<Materia>();
            Questoes = new List<Questao>();
        }

        public Disciplina(string nome) : this()
        {
            Nome = nome;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Disciplina novaDisciplina = (Disciplina)novoRegistro;

            Nome = novaDisciplina.Nome;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O nome da disciplina deve ser preenchido!");

            else if (Nome.Trim().Length < 3)
                erros.Add("O nome da disciplina deve conter ao menos 3 caracteres!");

            return erros;
        }

        public bool AdicionarMateria(Materia materia)
        {
            if (Materias.Contains(materia))
                return false;

            Materias.Add(materia);

            return true;
        }

        public bool RemoverMateria(Materia materia)
        {
            if (!Materias.Contains(materia))
                return false;

            Materias.Remove(materia);

            return true;
        }

        public override string ToString()
        {
            return $"{Nome}";
        }

        public List<Questao> ObterQuestoesAleatorias(int quantidadeQuestoes)
        {
            List<Questao> questoesRelacionadas = new List<Questao>();

            foreach (Materia mat in Materias)
                questoesRelacionadas.AddRange(mat.Questoes);

            Random random = new Random();

            return questoesRelacionadas
                .OrderBy(q => random.Next())
                .Take(quantidadeQuestoes)
                .ToList();
        }
    }
}
