-- Cria um banco de dados projeto_medicals
create database projeto_medicals;
go

-- Define o banco de dados que será utilizado
use projeto_medicals;
go

-- Cria a tabela tipoUsuario
create table tipoUsuario
(
	idTipoUsuario int primary key identity
	,tituloTipoUsuario varchar (200) unique not null
);
go

-- Cria a tabela Usuario
create table Usuario
(
	idUsuario int primary key identity
	,idTipoUsuario int foreign key references tipoUsuario(idTipoUsuario)
	,email varchar (150) unique not null
	,senha varchar (100) not null

);
go

-- Cria a tabela especialidade
create table especialidade
(
	idEspecialidade int primary key identity
	,nomeEspecialidade varchar (100) unique not null
);
go

-- Cria a tabela clinica
create table clinica
(
	idClinica int primary key identity
	,CNPJ char (14) unique
	,horarioAbertura time not null
	,horarioFechamento time not null
	,endereco varchar (200) unique not null
	,nomeFantasia varchar (100) unique not null
	,razaoSocial varchar (150) unique not null
);
go

-- Cria a tabela medico
create table medico
(
	idMedico int primary key identity
	,idUsuario int foreign key references Usuario(idUsuario)
	,idEspecialidade int foreign key references especialidade(idEspecialidade)
	,idClinica int foreign key references clinica(idClinica)
	,nomeMedico varchar (100) not null
	,CRM varchar (50) unique not null
);
go

-- Cria a tabela paciente
create table paciente
(
	idPaciente int primary key identity
	,idUsuario int foreign key references Usuario(idUsuario)
	,dataNascimento date not null
	,nomePaciente varchar (100) not null
	,RG char (9) unique not null
	,CPF char (11) unique not null
	,telefone varchar (11) 
	,endereco varchar (100) unique not null
);
go

-- Cria a tabela situacao
create table situacao
(
	idSituacao int primary key identity
	,situacao varchar(30) not null
);
go

-- Cria a tabela consulta
create table consulta
(
	idConsulta int primary key identity
	,idMedico int foreign key references medico(idMedico)
	,idPaciente int foreign key references paciente(idPaciente)
	,idSituacao int foreign key references situacao(idSituacao)
	,dataConsulta datetime not null
	,descricao varchar (150) default ('Descrição não informada')
);
go