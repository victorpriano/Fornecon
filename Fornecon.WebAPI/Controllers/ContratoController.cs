using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fornecon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoRepository _contrato;
        public ContratoController(IContratoRepository contrato)
        {
            _contrato = contrato;
        }

        [HttpGet]
        public async Task<ActionResult> GetContratos()
        {
            try
            {
                var contratos = await _contrato.GetContratos();
                
                return Ok(contratos);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter Contratos" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetContratoById(int id)
        {
            try
            {
                var contratos = await _contrato.GetContratoById(id);

                if(contratos == null || contratos.Count() == 0)
                {
                    return NotFound("Esse contrato não existe!");
                }

                return Ok(contratos);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter o contrato" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> Post([FromBody] Contrato contrato)
        {
            try
            {
                await _contrato.AddContrato(contrato);

                return Ok(contrato);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível criar o contrato." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contrato>> Put(int id, [FromBody] Contrato contrato)
        {
            try
            {
                var exists = await _contrato.GetContratoById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("Este contrato não existe!");
                }

                await _contrato.UpdateContrato(id, contrato);

                return Ok(contrato);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível atualizar o contrato." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contrato>> Delete(int id)
        {
            try
            {
                var exists = await _contrato.GetContratoById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("Este contrato não existe!");
                }

                await _contrato.DeleteContrato(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível excluir o contrato." });
            }
        }

    }
}
