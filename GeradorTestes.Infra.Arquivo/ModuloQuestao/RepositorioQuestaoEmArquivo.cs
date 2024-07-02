using GeradorDeTestes.Infra.Arquivos.Compartilhado;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;

namespace GeradorTestes.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioBaseEmArquivo<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        public override void Cadastrar(Questao novoRegistro)
        {
            Materia materiaSelecionada = novoRegistro.Materia;

            materiaSelecionada.AdicionarQuestao(novoRegistro);

            base.Cadastrar(novoRegistro);
        }

        protected override List<Questao> ObterRegistros()
        {
            return contexto.Questoes;
        }

        public void AdicionarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas)
        {
            foreach (Alternativa a in alternativasSelecionadas)
                questao.AdicionarAlternativa(a);

            contexto.Gravar();
        }

        public void AtualizarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas)
        {
            questao.Alternativas.Clear();

            contexto.Gravar();

            AdicionarAlternativas(questao, alternativasSelecionadas);
        }
    }
}
