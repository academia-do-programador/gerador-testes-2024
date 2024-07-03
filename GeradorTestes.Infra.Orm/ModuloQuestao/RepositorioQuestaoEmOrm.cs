using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    public class RepositorioQuestaoEmOrm : IRepositorioQuestao
    {
        private GeradorTestesDbContext dbContext;

        public RepositorioQuestaoEmOrm(GeradorTestesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AdicionarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas)
        {
            throw new NotImplementedException();
        }

        public void AtualizarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Questao questao)
        {
            dbContext.Questoes.Add(questao);
            dbContext.SaveChanges();
        }

        public bool Editar(int id, Questao questaoAtualizada)
        {
            Questao questao = dbContext.Questoes.Find(id)!;

            if (questao == null)
                return false;

            questao.AtualizarRegistro(questaoAtualizada);

            dbContext.Questoes.Update(questao);
            dbContext.SaveChanges();
            return true;
        }

        public bool Excluir(int id)
        {
            Questao questao = dbContext.Questoes.Find(id)!;

            if (questao == null)
                return false;

            dbContext.Questoes.Remove(questao);
            dbContext.SaveChanges();

            return true;
        }

        public Questao SelecionarPorId(int id)
        {
            return dbContext.Questoes.Include(x => x.Alternativas).FirstOrDefault(x => x.Id == id)!;
        }

        public List<Questao> SelecionarTodos()
        {
            return dbContext.Questoes.Include(x => x.Materia).ToList();
        }
    }
}
