using ApiRestPedidosEF6.Interfaces;
using ApiRestPedidosEF6.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;

namespace ApiRestPedidosEF6.Repository
{
    public class ItemPedidoRep : IItemPedidoRep
    {

        private readonly Contexto _contexto;

        public ItemPedidoRep(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Retorno> Incluir(ItemPedido itemPedido)
        {
            int IdPedido = itemPedido.IdPedido == null ? 0 : itemPedido.IdPedido;
            int IdItem = itemPedido.IdItem == null ? 0 : itemPedido.IdItem;
            string MsgErro = "Item não pode ser incluído, erro:";

            try
            {
                var pedido =   await _contexto.Pedido.Where(c => c.Id == IdPedido).FirstOrDefaultAsync();

                var item = await _contexto.Item.Where(c => c.Id == IdItem).FirstOrDefaultAsync();

                if (pedido == null)
                {
                    return await Getorno(0, $@"{MsgErro} Informe pedido correto", 0);
                }
                else if (item == null)
                {
                    return await Getorno(0, $@"{MsgErro} item não existente", 0);
                }
                else if (itemPedido.Quantidade == null)
                {
                    return await Getorno(0, $@"{MsgErro}  quantidade não informada", 0);
                }
                else if (itemPedido.VrUnitario == null)
                {
                    return await Getorno(0, $@"{MsgErro} valor não informado", 0);
                }
                else
                {
                    var novoVerifica = await _contexto.ItemPedido.Where(c => c.IdItem == IdItem && c.IdPedido == IdPedido).FirstOrDefaultAsync();

                    if (novoVerifica == null)
                    {
                        return await Getorno(0, $@"{MsgErro} item já existente", 0);
                    }
                    else
                    {
                        _contexto.ItemPedido.Add(itemPedido);
                        await _contexto.SaveChangesAsync();

                        var novoItem = await _contexto.Item.Where(c => c.Nome == item.Nome).FirstOrDefaultAsync();
                        return await Getorno(novoItem.Id, $@"{MsgErro} item não existente", 0);
                    }                      
                }
            }
            catch (Exception ex)
            {
                return await Getorno(0, $@"{MsgErro} {ex.Message}", 0);
            }
        }

        public async Task<Retorno> Alterar(ItemPedido itemPedido)
        {
            int IdItem = itemPedido.IdItem == null ? 0 : itemPedido.IdItem;

            try
            {

                var novo = await _contexto.ItemPedido.Where(c => c.IdItem == IdItem &&  c.IdPedido  == itemPedido.IdPedido ).FirstOrDefaultAsync();

                if (novo == null)
                {
                    return await Getorno(novo.Id, $@"Item do pedido foi não alterado,erro: Item não encontrado", 0);
                }
                else
                {   
                    novo.VrUnitario = itemPedido.VrUnitario;
                    novo.Quantidade = itemPedido.Quantidade;

                    _contexto.Entry(novo).State = EntityState.Modified;

                    await _contexto.SaveChangesAsync();
                    return await Getorno(novo.Id, "Item do pedido alterado com sucesso.", 1);
                }

            }
            catch (Exception ex)
            {
                return await Getorno(0, $@"Cliente não alterado,erro: {ex.Message}", 1);
            }
        }

        public async Task<Retorno> Excluir(int idPedido, int IdItem)
        {
            try
            {
                var itemPedido  = await _contexto.ItemPedido.Where(c => c.IdItem == IdItem && c.IdPedido == idPedido).FirstOrDefaultAsync();

                if (itemPedido == null)
                {
                    return await Getorno(0, $@"Item não pode ser excluido,erro: Item não encontrado", 0);
                }
                else
                {
                    _contexto.ItemPedido.Remove(itemPedido);
                    await _contexto.SaveChangesAsync();
                    return await Getorno(itemPedido.Id, "Item excluido com sucesso.", 1);
                }
            }
            catch (Exception ex)
            {
                return await Getorno(0, $@"Item não pode ser excluido,erro: {ex.Message}", 0);
            }
        }

        public async Task<ItemPedido> SelecionById(int idPedido, int IdItem)
        {
            try
            {
                return  await _contexto.ItemPedido.Where(c => c.IdItem == IdItem && c.IdPedido == idPedido).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                ItemPedido item = new ItemPedido();
                item.Id = 0;                
                return item;
            }
        }

        public async Task<IEnumerable<VW_ItensPedido>> SelecionarTodos(int idPedido)
        {
            try
            {
                return await _contexto.VW_ItensPedido.Where(c => c.IdPedido == idPedido).ToListAsync();
            }
            catch (Exception ex)
            {
                VW_ItensPedido item = new VW_ItensPedido();
                item.IdPedido = 0;
                item.Item  = $@"Erro na seleção do Item: {ex.Message}";
                List<VW_ItensPedido> Lista = new List<VW_ItensPedido>();

                Lista.Add(item);
                return Lista.ToList();
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

    }
}
