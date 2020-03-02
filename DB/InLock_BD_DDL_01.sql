CREATE DATABASE InLock_Games_Tarde;

GO
USE InLock_Games_Tarde;
GO
CREATE TABLE Estudios (
	IdEstudio INT PRIMARY KEY IDENTITY,
	NomeEstudio VARCHAR (255) NOT NULL UNIQUE
);

CREATE TABLE Jogos (
	IdJogo INT PRIMARY KEY IDENTITY,
	NomeJogo VARCHAR (255),
	DescricaoJogo VARCHAR (255),
	DataLancamento DATE,
	ValorJogo MONEY,
	IdEstudio INT FOREIGN KEY REFERENCES Estudios (IdEstudio) 
);

CREATE TABLE TiposUsuarios (
	IdTipoUsuario INT PRIMARY KEY IDENTITY,
	NomeTipoUsuario VARCHAR (255) NOT NULL
);

CREATE TABLE Usuarios (
	IdUsuario INT PRIMARY KEY IDENTITY,
	Email VARCHAR (255) NOT NULL,
	Senha VARCHAR (255) NOT NULL,
	IdTipoUsuario INT FOREIGN KEY REFERENCES  TiposUsuarios (IdTipoUsuario)
);
GO



