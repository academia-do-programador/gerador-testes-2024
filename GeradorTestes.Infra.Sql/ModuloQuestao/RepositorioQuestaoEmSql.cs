using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.Data.SqlClient;

namespace GeradorTestes.Infra.Sql.ModuloQuestao
{
    public class RepositorioQuestaoEmSql : IRepositorioQuestao
    {
        private string enderecoBanco;

        public RepositorioQuestaoEmSql()
        {
            enderecoBanco = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GeradorTestesDb;Integrated Security=True;Pooling=False";
        }

        #region SQL queries
        private string sqlInserir =>
           @"INSERT INTO [TBQUESTAO]
            (
                [ENUNCIADO],
                [UTILIZADA_EM_TESTE],
                [MATERIA_ID]
	        )
	        VALUES
            (
                @ENUNCIADO,
                @UTILIZADA_EM_TESTE,
                @MATERIA_ID
            );SELECT SCOPE_IDENTITY();";

        private string sqlEditar =
            @"UPDATE [TBQUESTAO]
		    SET                    
                [ENUNCIADO] = @ENUNCIADO,
                [UTILIZADA_EM_TESTE] = @UTILIZADA_EM_TESTE,
                [MATERIA_ID] = @MATERIA_ID
		    WHERE
			    [ID] = @ID";

        private string sqlExcluir =
            @"DELETE FROM [TBQUESTAO]
		    WHERE
			    [ID] = @ID";

        private string sqlSelecionarTodos =
            @"SELECT 
	            Q.ID,
                Q.ENUNCIADO,
                Q.UTILIZADA_EM_TESTE,
                M.ID            MATERIA_ID,
                M.NOME          MATERIA_NOME,
                M.SERIE         MATERIA_SERIE,
                D.ID            DISCIPLINA_ID,
                D.NOME          DISCIPLINA_NOME
            FROM
	            TBQUESTAO Q 
    
                INNER JOIN TBMATERIA M 
                    ON Q.MATERIA_ID = M.ID
                    
                INNER JOIN TBDISCIPLINA D 
                    ON D.ID = M.DISCIPLINA_ID";

        private string sqlSelecionarPorId =
            @"SELECT 
	            Q.ID,
                Q.ENUNCIADO,
                Q.UTILIZADA_EM_TESTE,
                M.ID            MATERIA_ID,
	            M.NOME          MATERIA_NOME,
                M.SERIE         MATERIA_SERIE,
                   
	            D.ID            DISCIPLINA_ID,
	            D.NOME          DISCIPLINA_NOME
            FROM
	            TBQUESTAO Q 
    
                INNER JOIN TBMATERIA M 
                    ON Q.MATERIA_ID = M.ID
                    
                INNER JOIN TBDISCIPLINA D 
                    ON D.ID = M.DISCIPLINA_ID
            WHERE 
                Q.ID = @ID";

        private string sqlInserirAlternativa =
           @"INSERT INTO [TBALTERNATIVA]
            (
		        [QUESTAO_ID],
		        [LETRA],
		        [RESPOSTA],
		        [CORRETA]
	        )
                VALUES
            (
		        @QUESTAO_ID,
		        @LETRA,
		        @RESPOSTA,
		        @CORRETA
	        );";

        private string sqlSelecionarAlternativas =
           @"SELECT 
	            [ID],
                [LETRA],
                [RESPOSTA],
                [CORRETA]
            FROM 
	            [TBALTERNATIVA]
            WHERE 
	            [QUESTAO_ID] = @QUESTAO_ID";

        private string sqlExcluirAlternativas =
           @"DELETE FROM [TBALTERNATIVA]
		            WHERE
			            [QUESTAO_ID] = @QUESTAO_ID";
        #endregion

        public void Cadastrar(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosQuestao(questao, comandoInsercao);

            conexaoComBanco.Open();

            object id = comandoInsercao.ExecuteScalar();

            questao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            AdicionarAlternativas(questao);
        }

        public bool Editar(int id, Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            questao.Id = id;

            ConfigurarParametrosQuestao(questao, comandoEdicao);

            conexaoComBanco.Open();

            int numeroRegistrosAfetados = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            if (numeroRegistrosAfetados < 1)
                return false;

            AtualizarAlternativas(questao);

            return true;
        }

        public bool Excluir(int id)
        {
            Questao questao = SelecionarPorId(id);

            ExcluirAlternativas(questao);

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

        public Questao SelecionarPorId(int id)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", id);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Questao questao = null;

            if (leitor.Read())
                questao = ConverterParaQuestao(leitor);

            if (questao != null)
                CarregarAlternativas(questao);

            conexaoComBanco.Close();

            return questao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                CarregarAlternativas(questao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
        }

        private void AdicionarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();

            foreach (Alternativa alternativa in questao.Alternativas)
            {
                SqlCommand comandoInsercao =
                    new SqlCommand(sqlInserirAlternativa, conexaoComBanco);

                ConfigurarParametrosAlternativa(alternativa, comandoInsercao);

                comandoInsercao.ExecuteNonQuery();
            }

            conexaoComBanco.Close();
        }

        private void AtualizarAlternativas(Questao questao)
        {
            ExcluirAlternativas(questao);

            AdicionarAlternativas(questao);
        }

        private void ExcluirAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao =
                new SqlCommand(sqlExcluirAlternativas, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("QUESTAO_ID", questao.Id);

            conexaoComBanco.Open();

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            questao.RemoverAlternativas();
        }

        private void CarregarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarAlternativas, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("QUESTAO_ID", questao.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Alternativa> alternativas = new List<Alternativa>();

            while (leitorQuestao.Read())
            {
                Alternativa alternativa = ConverterParaAlternativa(leitorQuestao);

                alternativas.Add(alternativa);
            }

            foreach (Alternativa alternativa in alternativas)
                questao.AdicionarAlternativa(alternativa);
        }

        private void ConfigurarParametrosQuestao(Questao questao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", questao.Id);
            comando.Parameters.AddWithValue("ENUNCIADO", questao.Enunciado);
            comando.Parameters.AddWithValue("UTILIZADA_EM_TESTE", questao.UtilizadaEmTeste);
            comando.Parameters.AddWithValue("MATERIA_ID", questao.Materia.Id);
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

        private Materia ConverterParaMateria(SqlDataReader leitor)
        {
            Materia materia = new Materia()
            {
                Id = Convert.ToInt32(leitor["MATERIA_ID"]),
                Nome = Convert.ToString(leitor["MATERIA_NOME"]),
                Serie = (SerieMateriaEnum)leitor["MATERIA_SERIE"],
            };

            Disciplina disciplina = ConverterParaDisciplina(leitor);

            materia.AtribuirDisciplina(disciplina);

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

        private void ConfigurarParametrosAlternativa(Alternativa alternativa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("QUESTAO_ID", alternativa.Questao.Id);
            comando.Parameters.AddWithValue("LETRA", alternativa.Letra);
            comando.Parameters.AddWithValue("RESPOSTA", alternativa.Resposta);
            comando.Parameters.AddWithValue("CORRETA", alternativa.Correta);
        }

        private Alternativa ConverterParaAlternativa(SqlDataReader leitor)
        {
            Alternativa alternativa = new Alternativa()
            {
                Id = Convert.ToInt32(leitor["ID"]),
                Letra = Convert.ToChar(leitor["LETRA"]),
                Resposta = Convert.ToString(leitor["RESPOSTA"]),
                Correta = Convert.ToBoolean(leitor["CORRETA"]),
            };

            return alternativa;
        }
    }
}
