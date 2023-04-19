Create database  DatabaseAlcatea

use DatabaseAlcatea

Create table tblPermissao(
permissao int not null primary key,
descricao varchar(60) 
)  

Create table tblFuncionarios(
CodFunc int not null primary key identity(1,1),
permissao int foreign key references tblPermissao(permissao),
nome varchar(30),
email varchar(30),
endereco varchar(30),
telefone varchar(15),
senha varchar (30)
)

create table tblClientes(
CodCliente int not null primary key identity(1,1),
Nome varchar(30),
senha varchar(30),  
email varchar(30),
telefone varchar(15)
)  

create table tblProdutos(
CodProduto int not null primary key identity(1,1),
NomeProduto varchar(30),
Preco varchar(30), 
descricao varchar (80),
Tipo_de_Produto int
)

create table tblPedidos(
CodPedidos int not null primary key identity(1,1),
CodMesa int not null foreign key references tblMesa(CodMesa),
CodFunc int not null foreign key references tblFuncionarios(CodFunc)
)

create table tblInsumos(
CodInsumos int not null primary key identity(1,1),
NomeInsumo varchar(30),
QtdInsumo int,
ValidadeInsumo date 
)

create table tblInsumos_Produtos(
CodProduto int not null foreign key references tblProdutos(CodProduto),
CodInsumos int not null foreign key references tblInsumos(CodInsumos),
Quantidade int
)

create table tblPedidos_Produtos(
CodPedidos int not null foreign key references tblPedidos(CodPedidos),
CodProduto int not null foreign key references tblProdutos(CodProduto),
Quantidade int
)

CREATE INDEX XtblPermissao ON tblPermissao(permissao)
CREATE INDEX XtblFuncionarios ON tblFuncionarios(CodFunc)
CREATE INDEX XtblClientes ON tblClientes(CodCliente)
CREATE INDEX XtblProdutos ON tblProdutos(CodProduto)
CREATE INDEX XtblInsumos ON tblInsumos(CodInsumos)
CREATE INDEX XtblInsumos_Produtos ON tblInsumos_Produtos(CodProduto, CodInsumos)
CREATE INDEX XtblPedidos ON tblPedidos(CodPedidos)

insert into tblPermissao(permissao, descricao)
values (1, 'gerente'),
(2, 'garçom'),
(3, 'Caixa'),
(4, 'Cozinha')

insert into tblFuncionarios(permissao,nome,email,endereco,telefone,senha)
values ( 1, 'Jorge', 'jonatass@gmail.com', 'Rua Aburame 3', 991508763,'a12'),
( 2, 'Clara Matos', 'claramatos@gmail.com', 'Rua Aburame 34', 972847282,'a13'),
( 2, 'Julia Santos', 'jujusan@gmail.com', 'Rua nagazaki 4', 903261738,'a14'),
( 1, 'Fernando Ranmos', 'feramos@gmail.com', 'Rua siqueira silva 234', 987263723,'a15'),
( 1, 'Nelio Hernandez', 'neher@gmail.com', 'Rua amador bueno 23', 941325148,'a16'),
( 1,'Carlos', 'Carlos@gmail.com', 'Rua Nilza', 11991439076, '12345')

insert into tblClientes( Nome, senha, email, telefone)
values ('João Ricardo', '23678912', 'joao.r@gmail.com', '11981723828'),
('Pedro Henrique', '01234567', 'ph2002@gmail.com', '11940028922'),
('Jorge Pereira', '89083928', 'pereira.jorge02@gmail.com', '11972636726')

insert into tblProdutos( NomeProduto, Tipo_de_Produto, Preco)
values ('Macarrão', 1, $20.00),
('Lasanha a Bolobnhesa', 1, $25.00),
('Arroz e Strogonof', 3, $20.00),
('PF', 4, $15.00)

insert into tblInsumos( NomeInsumo, QtdInsumo, ValidadeInsumo)
values ('Carne Moida', 40, '27/12/2018'),
('Massa Lasanha', 20, '12/06/2019'),
('Mussarela', 20, '12/06/2019' ),
('Presunto', 15, '12/06/2019')

insert into tblInsumos_Produtos(CodInsumos,CodProduto)
values(1, 1),
(1, 2),
(2, 2),
(3, 2),
(4, 2)

insert into tblPedidos(CodFunc,CodMesa)
values(2, 6),
(2, 6),
(3, 7),
(3, 3),
(2, 2)


GO
CREATE PROCEDURE usp_aspLogin
	@ClienteASP varchar(50),
	@SenhaASP varchar(50)
AS
	DECLARE @getCliente varchar(50), @getSenha varchar(50)

	SET @getCliente = (SELECT LoginCliente FROM tblClientes 
					WHERE LoginCliente = @ClienteASP)
	SET @getSenha = (SELECT senha FROM tblClientes 
					WHERE senha = @SenhaASP)

	IF(@SenhaASP = @getSenha)
		BEGIN
			SELECT ('Usuário pode ser logado pois os dados conferem') AS logger
		END
	ELSE
		BEGIN
			SELECT ('Usuário não pode ser logado pois os dados não conferem') AS logger
		END
GO



CREATE PROCEDURE usp_cFunc
	@FuncCSharp varchar(50),
	@SenhaCSharp varchar(50)
AS
	DECLARE @getFunc varchar(50), @getSenha varchar(50)

	SET @getFunc = (SELECT nome FROM tblFuncionarios 
					WHERE nome = @FuncCSharp)
	SET @getSenha = (SELECT senha FROM tblFuncionarios 
					WHERE nome = @FuncCSharp)

	IF(@SenhaCSharp = @getSenha)
		BEGIN
			SELECT ('Funcionário pode ser logado pois os dados conferem') AS logger
		END
	ELSE
		BEGIN
			SELECT ('Funcionário não pode ser logado pois os dados não conferem') AS logger
		END
GO