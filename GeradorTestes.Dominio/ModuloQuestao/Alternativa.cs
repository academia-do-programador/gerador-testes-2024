
namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public int Id { get; set; }
        public char Letra { get; set; }
        public string Resposta { get; set; }
        public bool Correta { get; set; }
        public Questao Questao { get; set; }

        public Alternativa() { }

        public Alternativa(char letra, string resposta)
        {
            Letra = letra;
            Resposta = resposta;
            Correta = false;
        }

        public void AtribuirQuestao(Questao questao)
        {
            Questao = questao;
        }

        public override string ToString()
        {
            return $"({Letra}) -> {Resposta}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Alternativa alternativa &&
                   Id == alternativa.Id &&
                   Letra == alternativa.Letra &&
                   Resposta == alternativa.Resposta &&
                   Correta == alternativa.Correta;
        }
    }
}
