USE InLock_Games_Tarde; 
GO

INSERT INTO Estudios (NomeEstudio)
VALUES	('Blizzard') , ('Rockstar Studios') ,('Square Enix');
GO

INSERT INTO Jogos (NomeJogo, DescricaoJogo, DataLancamento, ValorJogo, IdEstudio) 
VALUES ('Diablo 3', 'é um jogo que contém bastante ação e é viciante, seja você um novato ou um fã', '15/05/2012', 99.00 , 1) ,
		('Red Dead Redemption II', 'jogo eletrônico de ação-aventura western', '26/10/2018', 120.00, 2);

GO

INSERT INTO TiposUsuarios (NomeTipoUsuario)
VALUES ('Admin') , ('Cliente');
GO

INSERT INTO Usuarios (Email, Senha, IdTipoUsuario)
VALUES ('Cliente@email.com', '123', 2), ('Administrador@email.com', '321', 1)
GO