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
  //ex: http://localhost:5000/api/medico
  [Route("api/[controller]")]

  //Define que é um controlador de API
  [ApiController]
  public class MedicoController : ControllerBase
  {
    private IMedicoRepository _medicoRepository { get; set; }

    public MedicoController()
    {
      _medicoRepository = new MedicoRepository();
    }

    /// <summary>
    /// Lista todos os médicos
    /// </summary>
    /// <returns>Uma lista de todos os médicos</returns>
    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_medicoRepository.Listar());
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    // [Authorize(Roles = "2")]
    [HttpGet("listarminhas")]
    public IActionResult GetMedico()
    {
      int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

      return Ok(_medicoRepository.ListarConsultas(idUsuario));
    }

    /// <summary>
    /// Lista um médico pelo ID buscado
    /// </summary>
    /// <param name="id">ID do médico buscado</param>
    /// <returns>Retorna um médico de acordo com o ID buscado</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      try
      {
        return Ok(_medicoRepository.BuscarPorId(id));
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Cadastra um novo médico
    /// </summary>
    /// <param name="novoMedico">Novo médico que será cadastrada</param>
    /// <returns>Retorna um status code 201- Created </returns>
    [Authorize(Roles = "1")]
    [HttpPost]
    public IActionResult Post(Medico novoMedico)
    {
      try
      {
        _medicoRepository.Cadastrar(novoMedico);

        return StatusCode(201);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Atualiza um médico passando apenas o id
    /// </summary>
    /// <param name="id">ID do médico buscado para ser atualizada</param>
    /// <param name="medicoAtualizado">Médico atualizada</param>
    /// <returns>Retorna um status code 204</returns>
    [HttpPut("{id}")]
    public IActionResult Put(int id, Medico medicoAtualizado)
    {
      try
      {
        _medicoRepository.AtualizarPorId(id, medicoAtualizado);

        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    /// <summary>
    /// Deleta um médico existente
    /// </summary>
    /// <param name="id">Id do médico que será deletada</param>
    /// <returns>Retorna um status code 204 - No Content</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      try
      {
        _medicoRepository.Deletar(id);

        return StatusCode(204);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}
