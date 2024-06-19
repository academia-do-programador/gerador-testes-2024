namespace GeradorDeTestes.WinApp.Compartilhado
{
    public class ContextoDados
    {
        private string caminho = $"C:\\temp\\GeradorDeTestes\\dados.json";

        //public List<Disciplina> Disciplinas { get; private set; }

        public ContextoDados()
        {
            //Disciplinas = new List<Disciplina>();
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

            //Disciplinas = ctx.Disciplinas;
        }
    }
}
