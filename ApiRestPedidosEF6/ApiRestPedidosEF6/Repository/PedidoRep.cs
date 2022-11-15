using ApiRestPedidosEF6.Interfaces;
using ApiRestPedidosEF6.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Repository
{
    public class PedidoRep : IPedidoRep
    {

        private readonly Contexto _contexto;

        public PedidoRep(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Retorno> Alterar(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public async Task<Retorno> Excluir(int id)
        {
            var transaction = _contexto.Database.BeginTransaction();

            try
            {               

                var pedido = await _contexto.Pedido.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (pedido == null)
                {
                    transaction.Rollback();
                    return await Getorno(id, $@"O Pedido não pode ser excluido,erro: Item não encontrado", 0);
                }
                else
                { 
                    pedido.ItemPedido = await _contexto.ItemPedido.Where(c => c.IdPedido == id).ToListAsync();                    

                    _contexto.Pedido.Remove(pedido);
                    await _contexto.SaveChangesAsync();

                    transaction.Commit();

                    return await Getorno(id, "O Pedido excluido com sucesso.", 1);
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback() ;

                return await Getorno(id, $@"O Pedido não pode ser excluido,erro: {ex.Message}", 0);
            }
        }

        public async Task<Retorno> Incluir(Pedido pedido)
        {
            int id = 0;
            string MsgErro = "Item não pode ser incluído, erro:";


            try
            {
                if (String.IsNullOrEmpty(pedido.NumeroPedido))
                {
                    return await Getorno(id, $@"{MsgErro} Informe o nº do pedido", 0);
                }
                else
                {
                    var novo = await _contexto.Pedido.Where(c => c.NumeroPedido == pedido.NumeroPedido).FirstOrDefaultAsync();

                    if (novo != null)
                    {
                        return await Getorno(id, $@"{MsgErro} já cadastrado", 0);
                    }
                    else if (pedido.ItemPedido == null)
                    {
                        return await Getorno(id, $@"{MsgErro} informe os items do pedido", 0);
                    }
                    else
                    {
                        _contexto.Pedido.Add(pedido);
                        await _contexto.SaveChangesAsync();

                        var novoItem = await _contexto.Pedido.Where(c => c.NumeroPedido == pedido.NumeroPedido).FirstOrDefaultAsync();



                        return await Getorno(novoItem.Id, "Pedido Incluído com sucesso.", 1);
                    }
                }

            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"{MsgErro} {ex.Message}", 0);
            }
        }

        public async Task<IEnumerable<VW_Pedido>> SelecionarTodos()
        {
            try
            {
                return await _contexto.VW_Pedido.ToListAsync();
            }
            catch (Exception ex)
            {
                VW_Pedido pedido = new VW_Pedido();
                pedido.IdPedido = 0;
                pedido.Cliente = $@"Erro na seleção do Item: {ex.Message}";
                List<VW_Pedido> Lista = new List<VW_Pedido>();

                Lista.Add(pedido);
                return Lista.ToList();
            }
        }

        public async Task<VW_Pedido> SelecionById(int id)
        {
            try
            {
                return await _contexto.VW_Pedido.Where(c=> c.IdPedido == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                VW_Pedido pedido = new VW_Pedido();
                pedido.IdPedido = 0;
                pedido.Cliente = $@"Erro na seleção do pedido: {ex.Message}";
               
                return pedido;
            }
        }


        private async Task<Retorno> Getorno(int Id, String Mensagem, int Sucesso)
        {
            Retorno retorno = new Retorno();

            retorno.Id = Id;
            retorno.Mensagem = Mensagem;
            retorno.Sucesso = Sucesso > 0 ? "SIM" : "NÃO";
            return retorno;
        }

        Task<Pedido> IPedidoRep.SelecionById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
