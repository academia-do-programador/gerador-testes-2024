using GeradorDeTestes.Infra.Arquivos.Compartilhado.Extensoes;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;

namespace GeradorDeTestes.Infra.Arquivos.Compartilhado
{
    public class ContextoDados
    {
        private string caminho = $"C:\\temp\\GeradorDeTestes\\dados.json";

        public List<Disciplina> Disciplinas { get; set; }
        public List<Materia> Materias { get; set; }
        public List<Questao> Questoes { get; set; }
        public List<Teste> Testes { get; set; }

        public ContextoDados()
        {
            Disciplinas = new List<Disciplina>();
            Materias = new List<Materia>();
            Questoes = new List<Questao>();
            Testes = new List<Teste>();
        }

        public ContextoDados(bool carregarDados) : this()
        {
            if (carregarDados)
                CarregarDados();
        }

        public void Gravar()
        {
            FileInfo arquivo = new FileInfo(caminho);

            arquivo.Serializar(this, preservarReferencias: true);
        }

        protected void CarregarDados()
        {
            FileInfo arquivo = new FileInfo(caminho);

            if (!arquivo.Exists)
                return;

            ContextoDados ctx = arquivo.Deserializar<ContextoDados>(preservarReferencias: true);

            if (ctx == null)
                return;

            Disciplinas = ctx.Disciplinas;
            Materias = ctx.Materias;
            Questoes = ctx.Questoes;
            Testes = ctx.Testes;
        }
    }
}
