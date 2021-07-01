using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.sp_medicals.webApi.Domains;
using senai.sp_medicals.webApi.Repositories;
using System;

namespace senai.sp_medicals.webApi.Controllers
{
    //Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Define que a rota da requisição será no formato dominió/api/nomeController
    //ex: http://localhost:5000/api/clinica
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        private ISituacaoRepository _situacaoRepository { get; set; }

           
        private IClinicaRepository _clinicaRepository { get; set; }

        public ClinicaController()
        {
            _clinicaRepository = new ClinicaRepository();
        }

        /// <summary>
        /// Lista todas as clinicas
        /// </summary>
        /// <returns>Uma lista de clinicas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clinicaRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lista uma clinica pelo ID buscado
        /// </summary>
        /// <param name="id">ID da clinica buscada</param>
        /// <returns>Retorna uma clinica de acordo com o ID buscado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_clinicaRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastra uma nova clinica
        /// </summary>
        /// <param name="novaClinica">Nova clinica que será cadastrada</param>
        /// <returns>Retorna um status code 201- Created </returns>
        [Authorize (Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(Clinica novaClinica)
        {
            try
            {
                _clinicaRepository.Cadastrar(novaClinica);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Atualiza uma clinica passando apenas o id
        /// </summary>
        /// <param name="id">ID da clinica buscado para ser atualizada</param>
        /// <param name="clinicaAtualizada">clinica atualizada</param>
        /// <returns>Retorna um status code 204</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Clinica clinicaAtualizada)
        {
            try
            {
                _clinicaRepository.AtualizarPorId(id, clinicaAtualizada);

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Deleta uma clinica existente
        /// </summary>
        /// <param name="id">Id da clinica que será deletada</param>
        /// <returns>Retorna um status code 204 - No Content</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _clinicaRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}