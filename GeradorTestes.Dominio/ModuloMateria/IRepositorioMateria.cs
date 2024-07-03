namespace GeradorTestes.Dominio.ModuloMateria
{
    public interface IRepositorioMateria
    {
        
        void Cadastrar(Materia materia);
        bool Editar(int id, Materia materia);
        bool Excluir(int id);

        Materia SelecionarPorId(int id);
        List<Materia> SelecionarTodos();
    }
}
