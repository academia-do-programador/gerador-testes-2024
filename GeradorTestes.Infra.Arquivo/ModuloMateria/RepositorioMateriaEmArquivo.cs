using GeradorDeTestes.Infra.Arquivos.Compartilhado;
using GeradorTestes.Dominio.ModuloMateria;

namespace GeradorTestes.Infra.Arquivos.ModuloMateria
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
