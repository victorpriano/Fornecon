using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fornecon.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedor;

        public FornecedorController(IFornecedorRepository fornecedor)
        {
            _fornecedor = fornecedor;
        }

        [HttpGet]
        public async Task<ActionResult> GetFornecedores()
        {
            try
            {
                var fornecedores = await _fornecedor.GetFornecedores();

                return Ok(fornecedores);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível listar os fornecedores" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetFornecedoresById(int id)
        {
            try
            {
                var fornecedores = await _fornecedor.GetFornecedorById(id);

                if(fornecedores == null || fornecedores.Count() == 0)
                {
                    return NotFound("O fornecedor procurado não existe!");
                }

                return Ok(fornecedores);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter o fornecedor.." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Fornecedor>> Post([FromBody] Fornecedor fornecedor)
        {
            try
            {
                await _fornecedor.AddFornecedor(fornecedor);

                return Ok(fornecedor);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível incluir o fornecedor." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Fornecedor>> Put(int id, [FromBody] Fornecedor fornecedor)
        {
            try
            {
                var exists = await _fornecedor.GetFornecedorById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("O fornecedor não existe!");
                }

                await _fornecedor.UpdateFornecedor(id, fornecedor);

                return Ok(fornecedor);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível atualizar os dados do fornecedor" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Fornecedor>> Delete(int id)
        {
            try
            {
                var exists = await _fornecedor.GetFornecedorById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("O fornecedor não existe!");
                }

                await _fornecedor.DeleteFornecedor(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível deletar o fornecedor" });
            }
        }
    }
}
