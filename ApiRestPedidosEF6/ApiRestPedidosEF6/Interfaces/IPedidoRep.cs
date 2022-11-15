using ApiRestPedidosEF6.Models;

namespace ApiRestPedidosEF6.Interfaces
{
    public interface IPedidoRep
    {  
        Task<Retorno> Incluir(Pedido pedido);
        Task<Retorno> Alterar(Pedido pedido);
        Task<Retorno> Excluir(int id);
        Task<Pedido> SelecionById(int id); 
        Task<IEnumerable<VW_Pedido>> SelecionarTodos();        
    }
}
