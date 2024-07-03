using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloMateria
{
    public class RepositorioMateriaEmOrm : IRepositorioMateria
    {
        GeradorTestesDbContext dbContext;

        public RepositorioMateriaEmOrm(GeradorTestesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(Materia materia)
        {
            dbContext.Materias.Add(materia);
            dbContext.SaveChanges();
        }

        public bool Editar(int id, Materia materiaAtualizada)
        {
            Materia materia = dbContext.Materias.Find(id)!;

            if (materia == null)
                return false;

            materia.AtualizarRegistro(materiaAtualizada);

            dbContext.Materias.Update(materia);
            dbContext.SaveChanges();

            return true;
        }

        public bool Excluir(int id)
        {
            Materia materia = dbContext.Materias.Find(id)!;

            if (materia == null)
                return false;

            dbContext.Materias.Remove(materia);
            dbContext.SaveChanges();

            return true;
        }

        public Materia SelecionarPorId(int id)
        {
            return dbContext.Materias.Find(id)!;
        }

        public List<Materia> SelecionarTodos()
        {
            return dbContext.Materias.Include(x => x.Disciplina).ToList();                 
        }
    }
}
