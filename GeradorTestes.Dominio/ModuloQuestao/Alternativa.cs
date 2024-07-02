namespace GeradorTestes.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public char Letra { get; set; }
        public string Resposta { get; set; }
        public bool Correta { get; set; }

        public Alternativa(char letra, string resposta)
        {
            Letra = letra;
            Resposta = resposta;
            Correta = false;
        }

        public override string ToString()
        {
            return $"({Letra}) -> {Resposta}";
        }
    }
}
