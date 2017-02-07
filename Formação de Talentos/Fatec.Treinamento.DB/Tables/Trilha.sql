CREATE TABLE [dbo].[Trilha] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Nome]  VARCHAR (100) NOT NULL,
    [Ativa] BIT           CONSTRAINT [DF_Trilha_Ativa] DEFAULT ((1)) NOT NULL,
    [Imagem] VARCHAR(100) NOT NULL DEFAULT 'default.png', 
    CONSTRAINT [PK_Trilha] PRIMARY KEY CLUSTERED ([Id] ASC)
);

