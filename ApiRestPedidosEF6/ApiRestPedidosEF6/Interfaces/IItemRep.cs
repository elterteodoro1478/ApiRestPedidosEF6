using ApiRestPedidosEF6.Models;

namespace ApiRestPedidosEF6.Interfaces
{
    public interface IItemRep
    {
        Task<Retorno> Incluir(Item item);
        Task<Retorno> Alterar(Item item);
        Task<Retorno> Excluir(int id);
        Task<Item> SelecionById(int id);
        Task<IEnumerable<Item>> ListarByName(string Nome );
        Task<IEnumerable<Item>> SelecionarTodos();
    }
}
