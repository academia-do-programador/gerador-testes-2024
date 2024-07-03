using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GeradorTestes.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            GeradorTesteDbContext dbContext = new GeradorTesteDbContext();

            //------Disciplinas
            //Disciplina disciplina = new Disciplina("Matemática");
            //dbContext.Disciplinas.Add(disciplina);
            //dbContext.SaveChanges();

            //------ Materias
            //Disciplina disciplina = dbContext.Disciplinas.FirstOrDefault()!;
            //Materia materia = new Materia("Adição", SerieMateriaEnum.SegundaSerie, disciplina);
            //dbContext.Materias.Add(materia);
            //dbContext.SaveChanges();


            //------ Questoes - Adicionando Questão e Alternativas
            //Materia materia = dbContext.Materias.FirstOrDefault(x => x.Nome.Contains("Adição"))!;

            //List<Alternativa> alternativas = new List<Alternativa>();
            //alternativas.Add(new Alternativa('A', "1"));
            //alternativas.Add(new Alternativa('B', "2"));
            //alternativas.Add(new Alternativa('C', "3"));
            //alternativas.Add(new Alternativa('D', "4"));

            //Questao questao1 = new Questao("Quanto é 2+2?", materia, alternativas);
            //dbContext.Questoes.Add(questao1);
            //dbContext.SaveChanges();

            //------ Questoes - Atualizando Questão e atualizando alternativas

            //Questao questao = dbContext.Questoes                
            //    .Include(x => x.Alternativas)
            //    .FirstOrDefault(x => x.Id == 1)!;

            //questao.Enunciado = "Quanto é a soma de 2 e 2 ?";
            //questao.Alternativas[3].Correta = true;
            //questao.Alternativas.RemoveAt(0);
            //questao.Alternativas.Add(new Alternativa('E', "5"));
            //dbContext.Questoes.Update(questao);
            //dbContext.SaveChanges();

            //------ Testes

            Disciplina disciplina = dbContext.Disciplinas.FirstOrDefault()!;
            Materia materia = dbContext.Materias.FirstOrDefault(x => x.Nome.Contains("Adição"))!;

            for (int i = 0; i < 10; i++)
            {
                List<Alternativa> alternativas = new List<Alternativa>();
                int resposta = 0;

                for (int j = 65; j < 70; j++)
                {
                    Alternativa a = new Alternativa(Convert.ToChar(j), ((i + 1) * resposta).ToString());
                    alternativas.Add(a);
                    resposta++;
                }

                Questao questao = new Questao($"Quanto é 2 + {i + 1} ?", materia, alternativas);

                dbContext.Questoes.Add(questao);
            }

            dbContext.SaveChanges();

            Teste novoTeste = new Teste("Revisão Adição - Matemática", disciplina, materia, true, 5);
            novoTeste.SortearQuestoes();

            dbContext.Testes.Add(novoTeste);

            dbContext.SaveChanges();
            Console.ReadLine();
        }
    }

    //DbContext -> Classe Principal do Entity Framework
    public class GeradorTesteDbContext : DbContext
    {
        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Materia> Materias { get; set; }

        public DbSet<Questao> Questoes { get; set; }

        public DbSet<Teste> Testes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
                "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=GeradorTestesOrm;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

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

            modelBuilder.Entity<Teste>( testeBuilder =>
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