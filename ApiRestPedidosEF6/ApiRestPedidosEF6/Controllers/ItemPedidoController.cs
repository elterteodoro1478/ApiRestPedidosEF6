using ApiRestPedidosEF6.Models;
using ApiRestPedidosEF6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestPedidosEF6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly Contexto _context;

        public ItemPedidoController(Contexto context)
        {
            _context = context;
        }

        [HttpGet("SelecionById")] 
        public async Task<ActionResult<ItemPedido>> SelecionById(int idPedido, int IdItem)
        {
            return await new ItemPedidoRep(_context).SelecionById(idPedido, IdItem);
        }

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<VW_ItensPedido>>> SelecionarTodos(int idPedido)
        {
            ItemPedidoRep rep = new ItemPedidoRep(_context);
            return (List<VW_ItensPedido>)await rep.SelecionarTodos(idPedido);
        }        

        [HttpPost("Incluir")]
        public async Task<ActionResult<Retorno>> Incluir(ItemPedido item)
        {
            return await new ItemPedidoRep(_context).Incluir(item);
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult<Retorno>> Alterar(ItemPedido item)
        {
            return await new ItemPedidoRep(_context).Alterar(item);
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult<Retorno>> Excluir(int idPedido, int IdItem)
        {
            return await new ItemPedidoRep(_context).Excluir(idPedido, IdItem);
        }

    }
}
