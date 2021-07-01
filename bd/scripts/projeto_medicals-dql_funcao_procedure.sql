use projeto_medicals;

-- Funcao que mostra a quantidade de medicos que são da mesma especialidade
create function MedEsp(@idEspecialidade varchar (100))
returns int
as	
begin
declare @QuantMedicos as int
set @QuantMedicos = (select count (nomeMedico)
	from medico
	inner join especialidade
	on medico.idEspecialidade = especialidade.idEspecialidade
	where nomeEspecialidade = @idEspecialidade)
		return @quantMedicos
	end
go
select QuantMedicos = dbo.MedEsp('Pediatria');

-- Procedure para calcular a idade a partir da data de nascimento convertida em PT-BR
create procedure CalculadoraIdade @NomePaciente varchar (50)
as
select paciente.nomePaciente,  FORMAT (dataNascimento, 'd', 'pt-br') [dataNascimento], DATEDIFF(YEAR, paciente.dataNascimento,GETDATE()) AS IdadeAtual
FROM paciente where nomePaciente = @NomePaciente

exec CalculadoraIdade  @NomePaciente = 'Ligia'


