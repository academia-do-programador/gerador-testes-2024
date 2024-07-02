using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.Data.SqlClient;

namespace GeradorTestes.Infra.Sql.ModuloMateria
{
    public class RepositorioMateriaEmSql : IRepositorioMateria
    {
        private string enderecoBanco;

        public RepositorioMateriaEmSql()
        {
            enderecoBanco = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GeradorTestesDb;Integrated Security=True;Pooling=False";
        }

        #region SQL queries
        private const string sqlInserir =
            @"INSERT INTO [TBMATERIA]
            (
                [NOME],
                [SERIE],
                [DISCIPLINA_ID]
            )
            VALUES
            (
                @NOME,
                @SERIE,
                @DISCIPLINA_ID
            );";

        private const string sqlEditar =
            @"UPDATE [TBMATERIA]
		    SET
                [NOME] = @NOME,
                [SERIE] = @SERIE,
			    [DISCIPLINA_ID] = @DISCIPLINA_ID
		    WHERE
			    [ID] = @ID";

        private string sqlExcluir =>
            @"DELETE FROM [TBMATERIA]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
	            MT.ID,
                MT.NOME,
                MT.SERIE,
                D.ID        DISCIPLINA_ID,
                D.NOME      DISCIPLINA_NOME
            FROM
	            TBMATERIA AS MT INNER JOIN
                TBDISCIPLINA AS D                     
                    ON MT.DISCIPLINA_ID = D.ID";

        private const string sqlSelecionarPorId =
            @"SELECT 
	            MT.ID,
                MT.NOME,
                MT.SERIE,
                D.ID        DISCIPLINA_ID,
                D.NOME      DISCIPLINA_NOME
            FROM
	            TBMATERIA AS MT 
                INNER JOIN TBDISCIPLINA AS D                     
                    ON MT.DISCIPLINA_ID = D.ID
            WHERE
                MT.ID = @ID";

        private const string sqlExisteRegistro =
             @"SELECT 
		        COUNT(*)
	        FROM 
		        [TBMATERIA]
		    WHERE
                [ID] = @ID";

        private const string sqlSelecionarQuestoesDaMateria =
            @"SELECT 
		                [ID],
                        [ENUNCIADO],
                        [UTILIZA_DA_EM_TESTE]                
	                FROM 
		                [TBQUESTAO]

		            WHERE
                        [MATERIA_ID] = @MATERIA_ID AND";

        private const string sqlSelecionarTestesMateria =
            @"SELECT 
		        [ID],
                [TITULO],
                [DATA_GERACAO],
                [PROVA_RECUPERACAO],
	        FROM 
		        [TBTESTE]
		    WHERE
                [MATERIA_ID] = @MATERIA_ID";
        #endregion

        public void Cadastrar(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMateria(materia, comandoInsercao);

            conexaoComBanco.Open();

            object id = comandoInsercao.ExecuteScalar();

            materia.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public bool Editar(int id, Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            materia.Id = id;

            ConfigurarParametrosMateria(materia, comandoEdicao);

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

        public Materia SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Materia materia = null;

            if (leitor.Read())
                materia = ConverterParaMateria(leitor);

            conexaoComBanco.Close();

            if (materia != null)
            {
                CarregarQuestoes(materia);
                CarregarTestes(materia);
            }

            return materia;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorDisciplina.Read())
            {
                Materia disciplina = ConverterParaMateria(leitorDisciplina);

                materias.Add(disciplina);
            }

            conexaoComBanco.Close();

            return materias;
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

        private void CarregarTestes(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTestesMateria, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("MATERIA_ID", materia.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            List<Teste> testes = new List<Teste>();

            while (leitorTeste.Read())
            {
                Teste questao = ConverterParaTeste(leitorTeste);

                testes.Add(questao);
            }

            foreach (Teste teste in testes)
                teste.AtribuirMateria(materia);
        }

        private void ConfigurarParametrosMateria(Materia materia, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", materia.Id);
            comando.Parameters.AddWithValue("NOME", materia.Nome);
            comando.Parameters.AddWithValue("SERIE", materia.Serie);
            comando.Parameters.AddWithValue("DISCIPLINA_ID", materia.Disciplina.Id);
        }

        private Materia ConverterParaMateria(SqlDataReader leitor)
        {
            Materia materia = new Materia()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Nome = Convert.ToString(leitor["NOME"]),
                Serie = (SerieMateriaEnum)leitor["SERIE"],
                Disciplina = ConverterParaDisciplina(leitor)
            };

            materia.AtribuirDisciplina();

            return materia;
        }

        private Disciplina ConverterParaDisciplina(SqlDataReader leitor)
        {
            Disciplina disciplina = new Disciplina()
            {
                Id = Convert.ToInt32(leitor["DISCIPLINA_ID"]),
                Nome = Convert.ToString(leitor["DISCIPLINA_NOME"]),
            };

            return disciplina;
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

        private Teste ConverterParaTeste(SqlDataReader leitor)
        {
            Teste teste = new Teste()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Titulo = Convert.ToString(leitor["TITULO"]),
                DataGeracao = Convert.ToDateTime(leitor["DATA_GERACAO"]),
                ProvaRecuperacao = Convert.ToBoolean(leitor["PROVA_RECUPERACAO"]),
            };

            return teste;
        }
    }
}
