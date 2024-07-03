using GeradorDeTestes.ConsoleApp.Compartilhado;
using GeradorTestes.Dominio.ModuloMateria;

namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase
    {
        public string Enunciado { get; set; }

        public Materia Materia { get; set; }

        public List<Alternativa> Alternativas { get; set; }

        public Alternativa AlternativaCorreta
        {
            get { return Alternativas.Find(a => a.Correta); }
        }

        public bool UtilizadaEmTeste { get; set; }

        public Questao()
        {
            Alternativas = new List<Alternativa>();
            UtilizadaEmTeste = false;
        }

        public Questao(string enunciado, Materia materia, List<Alternativa> alternativas) : this()
        {
            Enunciado = enunciado;
            Materia = materia;

            foreach (Alternativa a in alternativas)
                AdicionarAlternativa(a);
        }

        public void AtribuirMateria(Materia materia)
        {
            Materia = materia;

            Materia.AdicionarQuestao(this);
        }

        public void AdicionarAlternativa(Alternativa alternativa)
        {
            alternativa.AtribuirQuestao(this);

            Alternativas.Add(alternativa);

            return;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Enunciado.Trim()))
                erros.Add($"O nome do enunciado deve estar preenchido!");

            if (Enunciado.Length < 2)
                erros.Add($"O nome do enunciado deve ter mais de 2 letras!");

            if (Alternativas.Count(x => x.Correta) == 0)
                erros.Add("Nenhuma alternativa correta foi informada");

            if (Alternativas.Count(x => x.Correta) > 1)
                erros.Add("Apenas uma alternativa pode ser correta");

            if (Alternativas.Count < 3)
                erros.Add("No mínimo 3 alternativas precisam ser informadas");

            if (Alternativas.Count > 5)
                erros.Add("No máximo 5 alternativas devem ser informadas");

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Questao questaoEditada = (Questao)novoRegistro;

            Enunciado = questaoEditada.Enunciado;
            Materia = questaoEditada.Materia;
            Alternativas = questaoEditada.Alternativas;
        }

        public override string ToString()
        {
            return Enunciado;
        }

        public override bool Equals(object? obj)
        {
            return obj is Questao questao &&
                   Id == questao.Id &&
                   Enunciado == questao.Enunciado &&
                   UtilizadaEmTeste == questao.UtilizadaEmTeste;
        }

        public void RemoverAlternativas()
        {
            Alternativas.Clear();
        }
    }
}
