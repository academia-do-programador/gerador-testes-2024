using GeradorDeTestes.WinApp.Compartilhado;

namespace GeradorTestes.WinApp.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioBaseEmArquivo<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Questao> ObterRegistros()
        {
            return contexto.Questoes;
        }
    }
}
