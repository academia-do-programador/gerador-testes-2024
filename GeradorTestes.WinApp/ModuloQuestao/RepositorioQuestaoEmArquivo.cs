using GeradorDeTestes.WinApp.Compartilhado;
using GeradorTestes.WinApp.ModuloMateria;

namespace GeradorTestes.WinApp.ModuloQuestao
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
