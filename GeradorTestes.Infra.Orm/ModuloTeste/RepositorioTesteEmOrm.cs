using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorTestes.Infra.Orm.ModuloTeste
{
    public class RepositorioTesteEmOrm : IRepositorioTeste
    {
        private GeradorTestesDbContext dbContext;

        public RepositorioTesteEmOrm(GeradorTestesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AdicionarQuestoes(Teste teste, List<Questao> questoes)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Teste teste)
        {
            dbContext.Testes.Add(teste);
            dbContext.SaveChanges();
        }

        public bool Editar(int id, Teste testeAtualizada)
        {
            Teste teste = dbContext.Testes.Find(id)!;

            if (teste == null)
                return false;

            teste.AtualizarRegistro(testeAtualizada);

            dbContext.Testes.Update(teste);
            dbContext.SaveChanges();
            return true;
        }

        public bool Excluir(int id)
        {
            Teste teste = dbContext.Testes.Find(id)!;

            if (teste == null)
                return false;

            dbContext.Testes.Remove(teste);
            dbContext.SaveChanges();

            return true;
        }

        public Teste SelecionarPorId(int id)
        {
            return dbContext.Testes.Find(id)!;
        }

        public List<Teste> SelecionarTodos()
        {
            return dbContext.Testes
                .Include(x => x.Disciplina)
                .Include(x => x.Materia)                
                .ToList();
        }
    }
}
