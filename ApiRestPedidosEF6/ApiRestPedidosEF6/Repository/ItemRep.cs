using ApiRestPedidosEF6.Interfaces;
using ApiRestPedidosEF6.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Repository
{
    public class ItemRep  : IItemRep
    {

        private readonly Contexto _contexto;

        public ItemRep(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Retorno> Alterar(Item item)
        {
            int id = item.Id == null ? 0 : item.Id;

            try
            {

                var novo = await _contexto.Item.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (novo == null)
                {
                    return await Getorno(id, $@"Item não alterado,erro: Item não encontrado", 0);
                }
                else
                {

                    novo.Nome = item.Nome;
                    novo.VrUnitario = item.VrUnitario;
                    novo.Quantidade = item.Quantidade ;


                    _contexto.Entry(novo).State = EntityState.Modified;

                    await _contexto.SaveChangesAsync();
                    return await Getorno(id, "Item alterado com sucesso.", 1);
                }

            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"Cliente não alterado,erro: {ex.Message}", 1);
            }
        }

        public async Task<Retorno> Excluir(int id)
        {
            try
            {
                var item = await _contexto.Item.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (item == null)
                {
                    return await Getorno(id, $@"Item não pode ser excluido,erro: Item não encontrado", 0);
                }
                else
                {
                    _contexto.Item.Remove(item);
                    await _contexto.SaveChangesAsync();
                    return await Getorno(id, "Item excluido com sucesso.", 1);
                }
            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"Item não pode ser excluido,erro: {ex.Message}", 0);
            }
        }

        public async Task<Retorno> Incluir(Item item)
        {
            int id = 0;
            string MsgErro = "Item não pode ser incluído, erro:";
            

            try
            {
                if (String.IsNullOrEmpty(item.Nome))
                {
                    return await Getorno(id, $@"{MsgErro} Informe o nome do item", 0);
                }
                else
                {
                    var novo = await _contexto.Item.Where(c => c.Nome == item.Nome ).FirstOrDefaultAsync();

                    if (novo != null)
                    {
                        return await Getorno(id, $@"{MsgErro} já cadastrado", 0);
                    }
                    else
                    {
                        _contexto.Item.Add(item);
                        await _contexto.SaveChangesAsync();

                        var novoItem = await _contexto.Item.Where(c => c.Nome == item.Nome ).FirstOrDefaultAsync();

                        return await Getorno(novoItem.Id, "Cliente Incluído com sucesso.", 1);
                    }
                }

            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"{MsgErro} {ex.Message}", 0);
            }
        }
          
        public async Task<IEnumerable<Item>> SelecionarTodos()
        {
            try
            {
                return await _contexto.Item.ToListAsync();
            }
            catch (Exception ex)
            {
                Item item = new Item();
                item.Id = 0;
                item.Nome = $@"Erro na seleção do Item: {ex.Message}";
                List<Item> Lista = new List<Item>();

                Lista.Add(item);
                return Lista.ToList();
            }
        }

        public async Task<IEnumerable<Item>> ListarByName(string Nome)
        {
            try
            {
                return await _contexto.Item.Where(c => c.Nome.ToUpper().Contains(Nome.ToUpper()) ).ToListAsync();
            }
            catch (Exception ex)
            {
                Item item = new Item();
                item.Id = 0;
                item.Nome = $@"Erro na seleção do Item: {ex.Message}";
                List<Item> Lista = new List<Item>();

                Lista.Add(item);
                return Lista.ToList();
            }
        }

        public async Task<Item> SelecionById(int id)
        {
            try
            {
                return await _contexto.Item.Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Item item = new Item();
                item.Id = 0;
                item.Nome = $@"Erro na seleção do Item: {ex.Message}";
                return item;
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
