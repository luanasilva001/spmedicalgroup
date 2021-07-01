-- Define o banco de dados que será utilizado
use projeto_medicals;
go

-- Lista todos os tipos de usuário cadastrados
select * from tipoUsuario;

-- Lista todos os usuários cadastrados
select * from Usuario;

-- Lista todas as especialidades cadastradas
select * from especialidade;

-- Lista a clinica que foi cadastrada
select * from clinica;

-- Lista todos os medicos cadastrados
select * from medico;
select * from Paciente;
select * from Usuario;
-- Lista todos os pacientes cadastrados
select * from paciente;

-- Lista as situacoes cadastradas
select * from situacao;

-- Lista todas as consultas cadastradas
select * from consulta;

-- Seleciona os dados dos usuarios mostrando o tipo do usuario
select idUsuario, tituloTipoUsuario, email from Usuario
inner join tipoUsuario
on  usuario.idTipoUsuario = tipoUsuario.idTipoUsuario;

--Seleciona os nomes dos medicos que trabalham na clinica
select nomeMedico, nomeFantasia from consulta
inner join medico
on consulta.idMedico = medico.idMedico
inner join clinica
on medico.idClinica = clinica.idClinica;

-- Seleciona os dados dos pacientes, medicos a data da consulta, a area que o medico trabalha, situacao da consulta, horario que a clinica abre e fecha e o nome da mesma
select nomePaciente, nomeMedico, dataConsulta, nomeEspecialidade as especialidade, situacao, horarioAbertura [HorarioAberturaClinica], horarioFechamento[HorarioFechamentoClinica], nomeFantasia[nomeClinica] from consulta
inner join medico
on consulta.idMedico = medico.idMedico
inner join paciente
on consulta.idPaciente = paciente.idPaciente
inner join especialidade
on medico.idEspecialidade = especialidade.idEspecialidade
inner join situacao
on consulta.idSituacao = situacao.idSituacao
inner join clinica
on medico.idClinica = clinica.idClinica;

-- Seleciona o nome do medico e a especialidade
select nomeMedico, nomeEspecialidade from medico
inner join especialidade
on medico.idEspecialidade = especialidade.idEspecialidade;

-- Busca um usuário através do seu e-mail e senha
select tituloTipoUsuario[Permissao], email from Usuario
inner join tipoUsuario
on usuario.idTipoUsuario = tipoUsuario.idTipoUsuario
where email = 'adm@adm.com' and senha = 'adm123';

-- Converte a data de nascimento para PT-BR
SELECT nomePaciente AS Nomes, FORMAT (dataNascimento, 'd', 'pt-br') from paciente;

-- Seleciona os nomes dos pacientes e mostra a idade através da data de nascimento
SELECT paciente.nomePaciente, paciente.dataNascimento,DATEDIFF(YEAR, paciente.dataNascimento,GETDATE()) AS IdadeAtual FROM paciente;
