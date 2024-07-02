using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.Data.SqlClient;

namespace GeradorTestes.Infra.Sql.ModuloTeste
{
    public class RepositorioTesteEmSql : IRepositorioTeste
    {
        private string enderecoBanco;

        public RepositorioTesteEmSql()
        {
            enderecoBanco = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GeradorTestesDb;Integrated Security=True;Pooling=False";
        }

        #region SQL queries
        private const string sqlInserir =
          @"INSERT INTO [TBTESTE]
            (
                [TITULO],                    
                [DATA_GERACAO],
                [PROVA_RECUPERACAO],
                [MATERIA_ID],
                [DISCIPLINA_ID]
	        )
	        VALUES
            (
                @TITULO,
                @DATA_GERACAO,
                @PROVA_RECUPERACAO,
                @MATERIA_ID,
                @DISCIPLINA_ID
            );";

        private const string sqlExcluir =
           @"DELETE FROM [TBTESTE]
		    WHERE
			    [ID] = @ID";

        private const string sqlAdicionarQuestaoTeste =
          @"INSERT INTO [TBTESTE_TBQUESTAO]
            (
                [TESTE_ID],                    
                [QUESTAO_ID]
	        )
	        VALUES
            (
                @TESTE_ID,
                @QUESTAO_ID

            );";

        private const string sqlRemoverQuestoesTeste =
          @"DELETE FROM [TBTESTE_TBQUESTAO]
		    WHERE
			    [TESTE_ID] = @TESTE_ID;";

        private const string sqlSelecionarTodos =
             @"SELECT        
	            T.ID,
	            T.TITULO,            
	            T.DATA_GERACAO,       
	            T.PROVA_RECUPERACAO,            
                   
	            D.ID                    DISCIPLINA_ID,
	            D.NOME                  DISCIPLINA_NOME,
	               
	            M.ID                    MATERIA_ID,
	            M.NOME                  MATERIA_NOME,
	            M.SERIE                 MATERIA_SERIE	
            FROM  
	            TBTESTE T 
                    
                INNER JOIN TBDISCIPLINA D
                    ON T.DISCIPLINA_ID = D.ID

                LEFT JOIN TBMATERIA M 
                    ON T.MATERIA_ID = M.ID";

        private const string sqlSelecionarPorId =
            @"SELECT        
	            T.ID,
	            T.TITULO,
	            T.DATA_GERACAO,
	            T.PROVA_RECUPERACAO,
                   
	            D.ID                    DISCIPLINA_ID,
	            D.NOME                  DISCIPLINA_NOME,
	               
	            M.ID                    MATERIA_ID,
	            M.NOME                  MATERIA_NOME,
	            M.SERIE                 MATERIA_SERIE	
            FROM  
	            TBTESTE T 
                    
                INNER JOIN TBDISCIPLINA D
                    ON T.DISCIPLINA_ID = D.ID

                LEFT JOIN TBMATERIA M 
                    ON T.MATERIA_ID = M.ID
            WHERE 
	            T.[ID] = @ID";

        private const string sqlSelecionarQuestoesDoTeste =
            @"SELECT 
	            Q.ID,
	            Q.ENUNCIADO,
	            Q.UTILIZADA_EM_TESTE,

	            M.ID            [MATERIA_ID],
	            M.NOME          [MATERIA_NOME],
                M.SERIE         [MATERIA_SERIE],
            FROM 
	            TBQUESTAO AS Q 

            INNER JOIN TBTESTE_TBQUESTAO AS TQ
                ON Q.ID = TQ.QUESTAO_ID

            INNER JOIN TBMATERIA M 
                ON Q.MATERIA_ID = M.ID                    

            WHERE 
	            TQ.TESTE_ID = @TESTE_ID";

        private const string sqlSelecionarAlternativas =
          @"SELECT 
	            [ID],                   
                [LETRA],
                [RESPOSTA],
                [CORRETA]
            FROM 
	            [TBALTERNATIVA]
            WHERE 
	            [QUESTAO_ID] = @QUESTAO_ID";

        private const string sqlExisteRegistro =
             @"SELECT 
		        COUNT(*)
	        FROM 
		        [TBTESTE]
		    WHERE
                [ID] = @ID";
        #endregion

        public void Cadastrar(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTeste(teste, comandoInsercao);

            conexaoComBanco.Open();

            object id = comandoInsercao.ExecuteScalar();

            teste.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();
        }

        public bool Excluir(int id)
        {
            DesvincularQuestoes(id);

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

        public Teste SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Teste teste = null;

            if (leitor.Read())
                teste = ConverterParaTeste(leitor);

            if (teste != null)
                CarregarQuestoes(teste);

            conexaoComBanco.Close();

            return teste;
        }

        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Teste> testes = new List<Teste>();

            while (leitorQuestao.Read())
            {
                Teste teste = ConverterParaTeste(leitorQuestao);

                testes.Add(teste);
            }

            conexaoComBanco.Close();

            return testes;
        }

        public void AdicionarQuestoes(Teste teste, List<Questao> questoes)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();

            foreach (Questao questao in questoes)
            {
                teste.AdicionarQuestao(questao);

                SqlCommand comandoInsercao =
                    new SqlCommand(sqlAdicionarQuestaoTeste, conexaoComBanco);

                comandoInsercao.Parameters.AddWithValue("TESTE_ID", teste.Id);
                comandoInsercao.Parameters.AddWithValue("QUESTAO_ID", questao.Id);

                comandoInsercao.ExecuteNonQuery();
            }

            conexaoComBanco.Close();
        }

        private void DesvincularQuestoes(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao =
                new SqlCommand(sqlRemoverQuestoesTeste, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("TESTE_ID", id);

            conexaoComBanco.Open();

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }

        private void CarregarQuestoes(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarQuestoesDoTeste, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("TESTE_ID", teste.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                questoes.Add(questao);
            }

            foreach (Questao questao in questoes)
                teste.AdicionarQuestao(questao);
        }

        private void ConfigurarParametrosTeste(Teste teste, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", teste.Id);
            comando.Parameters.AddWithValue("TITULO", teste.Titulo);
            comando.Parameters.AddWithValue("DATA_GERACAO", teste.DataGeracao);
            comando.Parameters.AddWithValue("PROVA_RECUPERACAO", teste.ProvaRecuperacao);
            comando.Parameters.AddWithValue("MATERIA_ID", teste.Materia.Id);
            comando.Parameters.AddWithValue("DISCIPLINA_ID", teste.Disciplina.Id);
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

            Materia materiaSelecionada = ConverterParaMateria(leitor);

            teste.AtribuirMateria(materiaSelecionada);

            Disciplina disciplinaSelecionada = ConverterParaDisciplina(leitor);

            teste.AtribuirDisciplina(disciplinaSelecionada);

            return teste;
        }

        private Materia ConverterParaMateria(SqlDataReader leitor)
        {
            Materia materia = new Materia()
            {
                Id = Convert.ToInt32(leitor["MATERIA_ID"]),
                Nome = Convert.ToString(leitor["MATERIA_NOME"]),
                Serie = (SerieMateriaEnum)leitor["MATERIA_SERIE"],
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

            Materia materiaSelecionada = ConverterParaMateria(leitor);

            questao.AtribuirMateria(materiaSelecionada);

            return questao;
        }
    }
}
