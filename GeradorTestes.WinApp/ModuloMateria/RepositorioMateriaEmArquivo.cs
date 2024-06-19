using GeradorDeTestes.WinApp.Compartilhado;

namespace GeradorTestes.WinApp.ModuloMateria
{
    public class RepositorioMateriaEmArquivo : RepositorioBaseEmArquivo<Materia>, IRepositorioMateria
    {
        public RepositorioMateriaEmArquivo(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Materia> ObterRegistros()
        {
            return contexto.Materias;
        }
    }
}
