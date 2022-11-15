using ApiRestPedidosEF6.Models;
using ApiRestPedidosEF6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestPedidosEF6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {        
        private readonly Contexto _context;

        public PedidoController(Contexto context)
        {
            _context = context;
        }

        [HttpGet("SelecionById")]
        public async Task<ActionResult<VW_Pedido>> SelecionById(int id)
        {
            return await new PedidoRep(_context).SelecionById(id);
        }

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<VW_Pedido>>> SelecionarTodos()
        {
            PedidoRep rep = new PedidoRep(_context);
            return (List<VW_Pedido>)await rep.SelecionarTodos();
        }

        [HttpPut("Incluir")]
        public async Task<ActionResult<Retorno>> Incluir(Pedido pedido)
        {
            return await new PedidoRep(_context).Incluir(pedido);
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult<Retorno>> Excluir(int id)
        {
            return await new PedidoRep(_context).Excluir(id);
        }   
    }
}
