using ApiRestPedidosEF6.Models;

namespace ApiRestPedidosEF6.Interfaces
{
    public interface IClienteRep
    {
        Task<Retorno> Incluir(Cliente cliente);      
         Task<Retorno> Alterar(Cliente cliente);
        Task<Retorno> Excluir(int id);
        Task<Cliente> SelecionById(int id);
        Task<IEnumerable<Cliente>> SelecionarTodos(); 
    }
}
