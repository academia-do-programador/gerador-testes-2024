namespace GeradorTestes.WinApp.ModuloQuestao
{
    public interface IRepositorioQuestao
    {
        void Cadastrar(Questao questao);
        bool Editar(int id, Questao questao);
        bool Excluir(int id);

        Questao SelecionarPorId(int id);
        List<Questao> SelecionarTodos();
    }
}
