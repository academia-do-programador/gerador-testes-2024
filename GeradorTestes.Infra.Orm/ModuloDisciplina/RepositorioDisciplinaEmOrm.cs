using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Infra.Orm.Compartilhado;

namespace GeradorTestes.Infra.Orm.ModuloDisciplina
{
    public class RepositorioDisciplinaEmOrm : IRepositorioDisciplina
    {
        GeradorTestesDbContext dbContext;

        public RepositorioDisciplinaEmOrm(GeradorTestesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(Disciplina disciplina)
        {
            dbContext.Disciplinas.Add(disciplina);
            dbContext.SaveChanges();
        }

        public bool Editar(int id, Disciplina disciplinaAtualizada)
        {
            Disciplina disciplina = dbContext.Disciplinas.Find(id)!;

            if (disciplina == null)
                return false;
                
            disciplina.AtualizarRegistro(disciplinaAtualizada);

            dbContext.Disciplinas.Update(disciplina);
            dbContext.SaveChanges();

            return true;
        }

        public bool Excluir(int id)
        {
            Disciplina disciplina = dbContext.Disciplinas.Find(id)!;

            if (disciplina == null)
                return false;

            dbContext.Disciplinas.Remove(disciplina);
            dbContext.SaveChanges();

            return true;
        }

        public Disciplina SelecionarPorId(int id)
        {
            return dbContext.Disciplinas.Find(id)!;
        }

        public List<Disciplina> SelecionarTodos()
        {            
            return dbContext.Disciplinas.ToList();
        }
    }
}
