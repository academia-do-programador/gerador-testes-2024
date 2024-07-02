using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.Data.SqlClient;

namespace GeradorTestes.Infra.Sql.ModuloDisciplina
{
    public class RepositorioDisciplinaEmSql : IRepositorioDisciplina
    {
        private string enderecoBanco;

        public RepositorioDisciplinaEmSql()
        {
            enderecoBanco = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GeradorTestesDb;Integrated Security=True;Pooling=False";
        }

        #region slq queries
        private string sqlInserir =
            @"INSERT INTO [TBDISCIPLINA]
                (
                    [NOME]
                )    
                VALUES
                (
                    @NOME
                )";

        private string sqlEditar =
            @"UPDATE [TBDISCIPLINA]	
		        SET
			        [NOME] = @NOME
		        WHERE
			        [ID] = @ID";

        private string sqlExcluir =
            @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [ID] = @ID";

        private string sqlSelecionarTodos =
            @"SELECT 
		        [ID],
                [NOME]
	        FROM 
		        [TBDISCIPLINA]";

        private string sqlSelecionarPorId =
            @"SELECT 
		        [ID],
                [NOME]
	        FROM 
		        [TBDISCIPLINA]
		    WHERE
                [ID] = @ID";

        private string sqlSelecionarPorNome =
            @"SELECT 
		        [ID],
                [NOME]
	        FROM 
		        [TBDISCIPLINA]

		    WHERE
                [NOME] = @NOME";

        private string sqlSelecionarMateriasDaDisciplina =>
            @"SELECT 
		        [ID],
                [NOME],
                [SERIE]
	        FROM 
		        [TBMATERIA]
		    WHERE
                [DISCIPLINA_ID] = @DISCIPLINA_ID";

        private string sqlSelecionarQuestoesDaMateria =>
            @"SELECT 
		        [ID],
                [ENUNCIADO],
                [UTILIZA_DA_EM_TESTE]                
	        FROM 
		        [TBQUESTAO]

		    WHERE
                [MATERIA_ID] = @MATERIA_ID AND [UTILIZA_DA_EM_TESTE] = 0";

        protected string sqlExisteRegistro =>
             @"SELECT 
		        COUNT(*)
	        FROM 
		        [TBDISCIPLINA]
		    WHERE
                [ID] = @ID";
        #endregion

        public void Cadastrar(Disciplina novaDisciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosDisciplina(novaDisciplina, comandoInsercao);

            conexaoComBanco.Open();

            object id = comandoInsercao.ExecuteScalar();

            novaDisciplina.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public bool Editar(int id, Disciplina disciplinaEditada)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            disciplinaEditada.Id = id;

            ConfigurarParametrosDisciplina(disciplinaEditada, comandoEdicao);

            conexaoComBanco.Open();

            int numeroRegistrosAfetados = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (numeroRegistrosAfetados < 1)
                return false;

            return true;
        }

        public bool Excluir(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (numeroRegistrosExcluidos < 1)
                return false;

            return true;
        }

        public Disciplina SelecionarPorId(int idSelecionado)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", idSelecionado);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Disciplina disciplina = null;

            if (leitor.Read())
                disciplina = ConverterParaDisciplina(leitor);

            conexaoComBanco.Close();

            if (disciplina != null)
                CarregarMaterias(disciplina);

            return disciplina;
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> contatos = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                contatos.Add(disciplina);
            }

            conexaoComBanco.Close();

            return contatos;
        }

        private Disciplina ConverterParaDisciplina(SqlDataReader leitor)
        {
            Disciplina disciplina = new Disciplina()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Nome = Convert.ToString(leitor["NOME"]),
            };

            return disciplina;
        }

        private void ConfigurarParametrosDisciplina(Disciplina disciplina, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", disciplina.Id);
            comando.Parameters.AddWithValue("NOME", disciplina.Nome);
        }

        private void CarregarMaterias(Disciplina disciplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarMateriasDaDisciplina, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("DISCIPLINA_ID", disciplina.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorMaterias = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorMaterias.Read())
            {
                Materia materia = ConverterParaMateria(leitorMaterias);

                materias.Add(materia);
            }

            foreach (Materia materia in materias)
                disciplina.AdicionarMateria(materia);
        }

        private Materia ConverterParaMateria(SqlDataReader leitor)
        {
            Materia materia = new Materia()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Nome = Convert.ToString(leitor["NOME"]),
                Serie = (SerieMateriaEnum)leitor["SERIE"],
            };

            CarregarQuestoes(materia);

            return materia;
        }

        private void CarregarQuestoes(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarQuestoesDaMateria, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("MATERIA_ID", materia.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestoes = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestoes.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestoes);

                questoes.Add(questao);
            }

            foreach (Questao questao in questoes)
                materia.AdicionarQuestao(questao);
        }

        private Questao ConverterParaQuestao(SqlDataReader leitor)
        {
            Questao questao = new Questao()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Enunciado = Convert.ToString(leitor["ENUNCIADO"]),
                UtilizadaEmTeste = Convert.ToBoolean(leitor["UTILIZADA_EM_TESTE"]),
            };

            return questao;
        }
    }
}
