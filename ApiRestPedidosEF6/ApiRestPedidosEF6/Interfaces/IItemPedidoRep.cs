using ApiRestPedidosEF6.Models;

namespace ApiRestPedidosEF6.Interfaces
{
    public interface IItemPedidoRep
    {
        Task<Retorno> Incluir(ItemPedido itemPedido);
        Task<Retorno> Alterar(ItemPedido itemPedido);
        Task<Retorno> Excluir(int idPedido, int Item);
        Task<ItemPedido> SelecionById(int idPedido, int Item);
        Task<IEnumerable<VW_ItensPedido>> SelecionarTodos(int idPedido);
    }
}
