using GeradorDeTestes.ConsoleApp.Compartilhado;

namespace GeradorTestes.WinApp.ModuloDisciplina
{
    public class Disciplina : EntidadeBase
    {
        public string Nome { get; set; }

        public Disciplina() { }

        public Disciplina(string nome)
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
    }
}
