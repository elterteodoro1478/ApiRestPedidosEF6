using ApiRestPedidosEF6.Models;
using ApiRestPedidosEF6.Repository;
using Microsoft.AspNetCore.Mvc;


namespace ApiRestPedidosEF6.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly Contexto _context;

        public ClienteController(Contexto context)
        {            
            _context = context;
        }

        [HttpGet("SelecionById")]
        public async Task<ActionResult<Cliente>> SelecionById(int  id)       
        {            
            return await  new ClienteRep(_context).SelecionById(id);
        }                

        [HttpGet("SelecionarTodos")]
        public async Task<ActionResult<IEnumerable<Cliente>>> SelecionarTodos()
        {
            ClienteRep rep = new ClienteRep(_context);
            return (List<Cliente>)await rep.SelecionarTodos();
        }

        [HttpPost("Incluir")]
        public async Task<ActionResult<Retorno>> Incluir(Cliente cliente)
        {            
            return await new ClienteRep(_context).Incluir(cliente);
        }

        [HttpPut("Alterar")]  
        public async Task<ActionResult<Retorno>> Alterar(Cliente cliente)
        {
            return await new ClienteRep(_context).Alterar(cliente);
        }

        [HttpDelete("Excluir")]
        public async Task<ActionResult<Retorno>> Excluir(int id)
        {
            return await new ClienteRep(_context).Excluir(id);
        }
    }
}
