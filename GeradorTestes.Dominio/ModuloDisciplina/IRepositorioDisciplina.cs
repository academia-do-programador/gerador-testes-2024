namespace GeradorTestes.Dominio.ModuloDisciplina
{
    public interface IRepositorioDisciplina
    {
        void Cadastrar(Disciplina disciplina);
        bool Editar(int id, Disciplina disciplina);
        bool Excluir(int id);

        Disciplina SelecionarPorId(int id);
        List<Disciplina> SelecionarTodos();
    }
}
