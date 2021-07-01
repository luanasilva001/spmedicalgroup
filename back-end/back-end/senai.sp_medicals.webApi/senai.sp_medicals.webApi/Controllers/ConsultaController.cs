using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.sp_medicals.webApi.Domains;
using senai.sp_medicals.webApi.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace senai.sp_medicals.webApi.Controllers
{
  //Define que o tipo de resposta da API será no formato JSON
  [Produces("application/json")]

  //Define que a rota da requisição será no formato dominió/api/nomeController
  //ex: http://localhost:5000/api/consulta
  [Route("api/[controller]")]

  //Define que é um controlador de API
  [ApiController]
  public class ConsultaController : ControllerBase
  {
    private IConsultaRepository _consultaRepository { get; set; }

    public ConsultaController()
    {
      _consultaRepository = new ConsultaRepository();
    }

    /// <summary>
    /// Lista todos as consultas mostrando o médico, paciente e situação (se foi realizada ou não)
    /// </summary>
    /// <returns>Uma lista de consultas</returns>
    [HttpGet("lista")]
    public IActionResult GetConsultas()
    {
      try
      {
        return Ok(_consultaRepository.ListarTudo());
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("medico-consulta")]
    public IActionResult GetMedico(int id)
    {
      int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
      var idMedico = _consultaRepository.BuscarIdMedico(idUsuario);

      return Ok(_consultaRepository.ListarConsultasMedico(idMedico));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("paciente-consulta")]
    public IActionResult GetPaciente(int id)
    {
      int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
      var idPaciente = _consultaRepository.BuscarIdPaciente(idUsuario);

      return Ok(_consultaRepository.ListarConsultasPaciente(idPaciente));
    }

    /// <summary>
    /// Lista uma consulta pelo ID buscado
    /// </summary>
    /// <param name="id">ID da consulta buscada</param>
    /// <returns>Retorna uma consulta de acordo com o ID buscado</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      try
      {
        return Ok(_consultaRepository.BuscarPorId(id));
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Cadastra uma nova consulta
    /// </summary>
    /// <param name="novaConsulta">Nova consulta que será cadastrada</param>
    /// <returns>Retorna um status code 201- Created </returns>
    [Authorize(Roles = "1, 2")]
    [HttpPost]
    public IActionResult Post(Consultum novaConsulta)
    {
      try
      {
        _consultaRepository.Cadastrar(novaConsulta);

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Atualiza uma consulta passando apenas o id
    /// </summary>
    /// <param name="id">ID da consulta buscado para ser atualizada</param>
    /// <param name="consultaAtualizada">Consulta atualizada</param>
    /// <returns>Retorna um status code 204</returns>
    [Authorize(Roles = "1")]
    [HttpPut("{id}")]
    public IActionResult Put(int id, Consultum consultaAtualizada)
    {
      try
      {
        _consultaRepository.AtualizarPorId(id, consultaAtualizada);

        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Troca a descrição de uma consulta
    /// </summary>
    /// <param name="id">Id da descrição que será mudada</param>
    /// <param name="Descricao">Nova descrição que será trocada</param>
    /// <returns>Retorna uma nova descrição</returns>
    // [Authorize(Roles = "2")]
    [HttpPatch("medico/{id}")]
    public IActionResult PatchDescricao(int id, Consultum Descricao)
    {
      try
      {
        _consultaRepository.MudarDescricao(id, Descricao);
        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Troca o status de uma situação
    /// </summary>
    /// <param name="id">Id da consulta que será trocado</param>
    /// <param name="status">Status da situação que será trocado</param>
    /// <returns>Retorna uma situação mudada</returns>
    // [Authorize(Roles = "1")]
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, Situacao status)
    {
      try
      {
        _consultaRepository.AprovarRecusar(id, status.Situacao1);

        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }


    /// <summary>
    /// Deleta uma consulta existente
    /// </summary>
    /// <param name="id">Id da consulta que será deletada</param>
    /// <returns>Retorna um status code 204 - No Content</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      try
      {
        _consultaRepository.Deletar(id);

        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}