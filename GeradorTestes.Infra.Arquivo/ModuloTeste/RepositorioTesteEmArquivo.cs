using GeradorDeTestes.Infra.Arquivos.Compartilhado;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;

namespace GeradorTestes.Infra.Arquivos.ModuloTeste
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

        public override bool Excluir(int id)
        {
            Teste testeSelecionado = SelecionarPorId(id);

            for (int i = 0; i < testeSelecionado.Questoes.Count; i++)
            {
                Questao questao = testeSelecionado.Questoes[i];

                questao.UtilizadaEmTeste = false;
                testeSelecionado.RemoverQuestao(questao);
            }

            return base.Excluir(id);
        }

        protected override List<Teste> ObterRegistros()
        {
            return contexto.Testes;
        }
    }
}
