-- Disciplinas

CREATE TABLE [dbo].[TBDisciplina] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Nome] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Matérias

CREATE TABLE [dbo].[TBMateria] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Nome]          VARCHAR (200) NOT NULL,
    [Serie]         INT           NOT NULL,
    [Disciplina_Id] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBMateria_TBDisciplina] FOREIGN KEY ([Disciplina_Id]) REFERENCES [dbo].[TBDisciplina] ([Id])
);

-- Questões

CREATE TABLE [dbo].[TBQuestao] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Enunciado]          VARCHAR (200) NOT NULL,
    [Utilizada_Em_Teste] BIT           NOT NULL,
    [Materia_Id]         INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBQuestao_TBMateria] FOREIGN KEY ([Materia_Id]) REFERENCES [dbo].[TBMateria] ([Id])
);

-- Alternativas

CREATE TABLE [dbo].[TBAlternativa]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Letra] CHAR(1) NOT NULL, 
    [Resposta] VARCHAR(200) NOT NULL, 
    [Correta] BIT NOT NULL, 
    [Questao_Id] INT NOT NULL, 
    CONSTRAINT [FK_TBAlternativa_TBQuestao] FOREIGN KEY ([Questao_Id]) REFERENCES [TBQuestao]([Id])
)

-- Testes

CREATE TABLE [dbo].[TBTeste] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]              VARCHAR (200) NOT NULL,
    [Data_Geracao]        DATETIME2 (7) NOT NULL,
    [Prova_Recuperacao]   BIT           NOT NULL,
    [Materia_Id]          INT           NULL,
    [Disciplina_Id]       INT           NOT NULL,
    [Quantidade_Questoes] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBTeste_TBDisciplina] FOREIGN KEY ([Disciplina_Id]) REFERENCES [dbo].[TBDisciplina] ([Id]),
    CONSTRAINT [FK_TBTeste_TBMateria] FOREIGN KEY ([Materia_Id]) REFERENCES [dbo].[TBMateria] ([Id])
);

-- Questões em Testes

CREATE TABLE [dbo].[TBTeste_TBQuestao] (
    [Teste_Id]   INT NOT NULL,
    [Questao_Id] INT NOT NULL,
    CONSTRAINT [FK_TBTeste_TBQuestaoTBTeste] FOREIGN KEY ([Teste_Id]) REFERENCES [dbo].[TBTeste] ([Id]),
    CONSTRAINT [FK_TBTesteTBQuestao_TBQuestao] FOREIGN KEY ([Questao_Id]) REFERENCES [dbo].[TBQuestao] ([Id])
);