namespace GeradorTestes.WinApp.Compartilhado.Extensions
{
    public static class CharExtensions
    {
        public static char Proximo(this char caractereAtual)
        {
            int numero = caractereAtual;

            numero++;

            return (char)numero;
        }
    }
}
