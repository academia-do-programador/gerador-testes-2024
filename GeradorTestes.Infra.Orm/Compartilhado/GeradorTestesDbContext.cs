using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;

namespace GeradorTestes.Infra.Orm.Compartilhado
{
    public class GeradorTestesDbContext : DbContext
    {
        public DbSet<Disciplina> Disciplinas { get; internal set; }

        public DbSet<Materia> Materias { get; set; }

        public DbSet<Questao> Questoes { get; set; }

        public DbSet<Teste> Testes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
               "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=GeradorTestesOrm;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Disciplina>(disciplinaBuilder =>
            {
                disciplinaBuilder.ToTable("TBDisciplina");

                disciplinaBuilder.Property(d => d.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                disciplinaBuilder.Property(d => d.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });

            modelBuilder.Entity<Materia>(materiaBuilder =>
            {
                materiaBuilder.ToTable("TBMateria");
                materiaBuilder.Property(m => m.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                materiaBuilder.Property(m => m.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(250)");

                materiaBuilder.Property(m => m.Serie)
                    .IsRequired()
                    .HasConversion<int>();

                materiaBuilder.HasOne(m => m.Disciplina)
                    .WithMany(d => d.Materias)
                    .IsRequired()
                    .HasForeignKey("Disciplina_Id")
                    .HasConstraintName("FK_TBMateria_TBDisciplina")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Questao>(questaoBuilder =>
            {

                questaoBuilder.ToTable("TBQuestao");
                questaoBuilder.Property(q => q.Id).IsRequired().ValueGeneratedOnAdd();
                questaoBuilder.Property(q => q.Enunciado).HasColumnType("varchar(500)").IsRequired();
                questaoBuilder.Property(q => q.UtilizadaEmTeste).IsRequired();

                questaoBuilder.HasOne(q => q.Materia)
                    .WithMany(m => m.Questoes)
                    .IsRequired()
                    .HasForeignKey("Materia_Id")
                    .HasConstraintName("FK_TBQuestao_TBMateria")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Alternativa>(alternativaBuilder =>
            {
                alternativaBuilder.ToTable("TBAlternativa");
                alternativaBuilder.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
                alternativaBuilder.Property(a => a.Letra).IsRequired();
                alternativaBuilder.Property(a => a.Resposta).HasColumnType("varchar(100)").IsRequired();
                alternativaBuilder.Property(a => a.Correta).IsRequired();

                alternativaBuilder.HasOne(a => a.Questao)
                    .WithMany(q => q.Alternativas)
                    .IsRequired()
                    .HasForeignKey("Questao_Id")
                    .HasConstraintName("FK_TBAlternativa_TBQuestao")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Teste>(testeBuilder =>
            {
                testeBuilder.ToTable("TBTeste");
                testeBuilder.Property(t => t.Id).IsRequired(true).ValueGeneratedOnAdd();
                testeBuilder.Property(t => t.Titulo).HasColumnType("varchar(250)").IsRequired();
                testeBuilder.Property(t => t.DataGeracao).IsRequired();
                testeBuilder.Property(t => t.ProvaRecuperacao).IsRequired();

                testeBuilder.HasOne(t => t.Disciplina)
                    .WithMany()
                    .IsRequired()
                    .HasForeignKey("Disciplina_Id")
                    .HasConstraintName("FK_TBTeste_TBDisciplina")
                    .OnDelete(DeleteBehavior.NoAction);

                testeBuilder.HasOne(t => t.Materia)
                    .WithMany()
                    .IsRequired(false)
                    .HasForeignKey("Materia_Id")
                    .HasConstraintName("FK_TBTeste_TBMateria")
                .OnDelete(DeleteBehavior.NoAction);

                testeBuilder.HasMany(t => t.Questoes)
                    .WithMany()
                    .UsingEntity(x => x.ToTable("TBTeste_TBQuestao"));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
