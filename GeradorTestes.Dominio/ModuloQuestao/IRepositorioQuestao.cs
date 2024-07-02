namespace GeradorTestes.Dominio.ModuloQuestao
{
    public interface IRepositorioQuestao
    {
        void Cadastrar(Questao questao);
        bool Editar(int id, Questao questao);
        bool Excluir(int id);
        Questao SelecionarPorId(int id);
        List<Questao> SelecionarTodos();

        void AdicionarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas);
        void AtualizarAlternativas(Questao questao, List<Alternativa> alternativasSelecionadas);
    }
}
