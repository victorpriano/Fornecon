using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fornecon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produto;
        public ProdutoController(IProdutoRepository produto)
        {
            _produto = produto;
        }

        [HttpGet]
        public async Task<ActionResult> GetProdutos()
        {
            try
            {
                var produtos = await _produto.GetProdutos();

                return Ok(produtos);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter produtos." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProdutoById(int id)
        {
            try
            {
                var produto = await _produto.GetProdutoById(id);

                if(produto == null || produto.Count() == 0)
                {
                    return NotFound("O produto não existe!");
                }

                return Ok(produto);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível obter o produto." });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto produto)
        {
            try
            {
                await _produto.AddProduto(produto);

                return Ok(produto);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível incluir o produto." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            try
            {
                var exists = await _produto.GetProdutoById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("O produto não existe!");
                }

                await _produto.DeleteProduto(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível deletar produto." });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> Put(int id, [FromBody] Produto produto)
        {
            try
            {
                var exists = await _produto.GetProdutoById(id);

                if(exists == null || exists.Count() == 0)
                {
                    return NotFound("O produto não existe!");
                }

                await _produto.UpdateProduto(id, produto);

                return Ok(produto);
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Não foi possível modificar o produto" });
            }
        }

    }
}
