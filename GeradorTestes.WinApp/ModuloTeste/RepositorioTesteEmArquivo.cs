using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioBaseEmArquivo<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        public void AdicionarQuestoes(Teste teste, List<Questao> questoes)
        {
            foreach (Questao q in questoes)
            {
                teste.AdicionarQuestao(q);
                q.UtilizadaEmTeste = true;
            }

            contexto.Gravar();
        }

        public void RemoverQuestoes(Teste teste)
        {
            foreach (Questao q in teste.Questoes)
            {
                q.UtilizadaEmTeste = false;
                teste.RemoverQuestao(q);
            }

            contexto.Gravar();
        }

        protected override List<Teste> ObterRegistros()
        {
            return contexto.Testes;
        }
    }
}
