using GeradorDeTestes.ConsoleApp.Compartilhado;
using GeradorTestes.WinApp.ModuloDisciplina;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public class Materia : EntidadeBase
    {
        public string Nome { get; set; }

        public SerieMateriaEnum Serie { get; set; }

        public Disciplina Disciplina { get; set; }

        public Materia() { }

        public Materia(string nome, SerieMateriaEnum serie, Disciplina disciplina)
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
    }
}
