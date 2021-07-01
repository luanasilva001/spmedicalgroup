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
    //ex: http://localhost:5000/api/situacao
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]
    public class SituacaoController : ControllerBase
    {
        private ISituacaoRepository _situacaoRepository { get; set; }

        public SituacaoController()
        {
            _situacaoRepository = new SituacaoRepository();
        }

        /// <summary>
        /// Lista todas as situaçõs
        /// </summary>
        /// <returns>Uma lista de situações</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_situacaoRepository.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Lista uma situação pelo ID buscado
        /// </summary>
        /// <param name="id">ID da situação buscada</param>
        /// <returns>Retorna uma situação de acordo com o ID buscado</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_situacaoRepository.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Cadastra uma nova situação
        /// </summary>
        /// <param name="novaSituacao">Nova situação que será buscada</param>
        /// <returns>Retorna um status code 201- Created </returns>
        [Authorize (Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post(Situacao novaSituacao)
        {
            try
            {
                _situacaoRepository.Cadastrar(novaSituacao);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Atualiza uma situação passando apenas o id
        /// </summary>
        /// <param name="id">ID da situação buscada para ser atualizada</param>
        /// <param name="situacaoAtualizada">Situação atualizada</param>
        /// <returns>Retorna um status coe 204</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Situacao situacaoAtualizada)
        {
            try
            {
                _situacaoRepository.AtualizarPorId(id, situacaoAtualizada);

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Deleta uma situação existente
        /// </summary>
        /// <param name="id">Id da situação que será deletada</param>
        /// <returns>Retorna um status code 204 - No Content</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _situacaoRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
