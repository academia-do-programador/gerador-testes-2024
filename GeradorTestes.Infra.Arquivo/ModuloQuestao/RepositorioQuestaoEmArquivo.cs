using GeradorDeTestes.Infra.Arquivos.Compartilhado;
using GeradorTestes.Infra.Arquivos.ModuloMateria;

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
    }
}
