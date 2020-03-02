USE InLock_Games_Tarde;

SELECT * FROM Usuarios;

SELECT * FROM Estudios;

SELECT * FROM Jogos;

SELECT Jogos.NomeJogo, Jogos.DescricaoJogo, Jogos.DataLancamento, Estudios.NomeEstudio
FROM Jogos 
INNER JOIN Estudios ON Estudios.IdEstudio = Jogos.IdEstudio

SELECT Estudios.NomeEstudio, Jogos.NomeJogo
FROM Estudios
LEFT JOIN Jogos ON Jogos.IdEstudio = Estudios.IdEstudio


SELECT Usuarios.Email, Usuarios.Senha
FROM Usuarios
WHERE IdTipoUsuario = 2;

SELECT * FROM Jogos
WHERE IdJogo = 1;

SELECT * FROM Estudios
WHERE IdEstudio = 1;
