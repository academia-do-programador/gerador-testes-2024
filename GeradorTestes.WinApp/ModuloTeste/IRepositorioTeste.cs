using GeradorTestes.WinApp.ModuloQuestao;

namespace GeradorTestes.WinApp.ModuloTeste
{
    public interface IRepositorioTeste
    {
        void Cadastrar(Teste teste);
        bool Excluir(int id);
        Teste SelecionarPorId(int id);
        List<Teste> SelecionarTodos();

        void AdicionarQuestoes(Teste teste, List<Questao> questoes);
        void RemoverQuestoes(Teste teste);
    }
}
