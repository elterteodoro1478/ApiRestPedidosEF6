using ApiRestPedidosEF6.Models;
using ApiRestPedidosEF6.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestPedidosEF6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        
        private readonly Contexto _context;

        public ItemController(Contexto context)
        {
            _context = context;
        }

        [HttpGet("SelecionById")]
        public async Task<ActionResult<Item>> SelecionById(int id)
        {
            return await new ItemRep(_context).SelecionById(id);
        }

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<Item>>> SelecionarTodos()
        {
            ItemRep rep = new ItemRep(_context);
            return (List<Item>)await rep.SelecionarTodos();
        }

        [HttpGet("ListarByName")]
        public async Task<ActionResult<IEnumerable<Item>>> ListarByName(string Nome)
        {
            ItemRep rep = new ItemRep(_context);
            return (List<Item>)await rep.ListarByName(Nome);
        }

        [HttpPost("Incluir")]
        public async Task<ActionResult<Retorno>> Incluir(Item item)
        {
            return await new ItemRep(_context).Incluir(item);
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult<Retorno>> Alterar(Item item)
        {
            return await new ItemRep(_context).Alterar(item);
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult<Retorno>> Excluir(int id)
        {
            return await new ItemRep(_context).Excluir(id);
        }
    }
}
