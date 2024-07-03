namespace GeradorTestes.Dominio.ModuloTeste
{
    public interface IRepositorioTeste
    {
        void Cadastrar(Teste teste);
        bool Excluir(int id);
        Teste SelecionarPorId(int id);
        List<Teste> SelecionarTodos();
    }
}
