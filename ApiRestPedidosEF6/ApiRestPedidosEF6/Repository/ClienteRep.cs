using ApiRestPedidosEF6.Controllers;
using ApiRestPedidosEF6.Interfaces;
using ApiRestPedidosEF6.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace ApiRestPedidosEF6.Repository
{
    public class ClienteRep : IClienteRep
    {

        private readonly Contexto _contexto;

        public ClienteRep(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Retorno> Alterar(Cliente cliente)
        {
            int id = cliente.Id == null ? 0 : cliente.Id;
            
            try
            {
                var novo = await _contexto.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (novo == null)
                {
                    return await Getorno(id, $@"Cliente não alterado,erro: cliente não encontrado", 0);
                }
                else
                {
                    novo.Nome  =  cliente.Nome;
                    novo.Email  =  cliente.Email;
                    novo.Documento =  cliente.Documento;

                    _contexto.Entry(novo).State = EntityState.Modified;  

                    await _contexto.SaveChangesAsync();
                    return await Getorno(id, "Cliente alterado com sucesso.", 1);
                }
                 
            }
            catch(Exception ex)
            {
                return await Getorno(id, $@"Cliente não alterado,erro: {ex.Message}", 1);
            }
        }

        public async Task<Retorno> Excluir(int id)
        { 
            try
            {
                var cliente = await _contexto.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return await Getorno(id, $@"Cliente não pode ser excluido,erro: cliente não encontrado", 0);
                }
                else
                {
                    _contexto.Cliente.Remove(cliente);
                    await _contexto.SaveChangesAsync();
                    return await Getorno(id, "Cliente excluido com sucesso.", 1);
                }                   
            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"Cliente não pode ser excluido,erro: {ex.Message}", 0);
            }
        }

        public async Task<Retorno> Incluir(Cliente cliente)
        {
            int id = 0;
            string MsgErro = "Cliente não pode ser incluído, erro:";
            string Documento = String.IsNullOrEmpty(cliente.Documento) ? "" : cliente.Documento;

            try
            {  
                if (String.IsNullOrEmpty(cliente.Nome))
                {
                    return await Getorno(id, $@"{MsgErro} Informe o nome do cliente", 0);
                }
                else
                {
                    var novo = await _contexto.Cliente.Where(c => c.Nome == cliente.Nome && c.Documento.Trim() == Documento).FirstOrDefaultAsync();

                    if (novo != null)
                    {
                        return await Getorno(id, $@"{MsgErro} já cadastrado", 0);
                    }
                    else
                    {
                        _contexto.Cliente.Add(cliente);
                        await _contexto.SaveChangesAsync();

                        var novocliente = await _contexto.Cliente.Where(c => c.Nome == cliente.Nome && c.Documento.Trim() == Documento).FirstOrDefaultAsync();

                        return await Getorno(novocliente.Id , "Cliente Incluído com sucesso.", 1);
                    }                   
                }
                
            }
            catch (Exception ex)
            {
                return await Getorno(id, $@"{MsgErro} {ex.Message}", 0);
            }
        }

        public async Task<IEnumerable<Cliente>> SelecionarTodos()
        {
            try
            {
                return await _contexto.Cliente.ToListAsync();
            }
            catch (Exception ex)
            {
                Cliente cliente = new Cliente();
                cliente.Id = 0;
                cliente.Nome = $@"Erro na seleção de cliente: {ex.Message}";
                List<Cliente> Lista = new List<Cliente>();

                Lista.Add(cliente);
                return  Lista.ToList();
            }
        }

        public async Task<Cliente> SelecionById(int id)
        {      

            try
            {
                return  await _contexto.Cliente.Where(c => c.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Cliente cliente = new Cliente();
                cliente.Id = 0;
                cliente.Nome = $@"Erro na seleção de cliente: {ex.Message}";
;               return  cliente;
            }
        }

        private async Task<Retorno> Getorno(int Id, String Mensagem, int Sucesso)
        {
            Retorno retorno = new Retorno();

            retorno.Id = Id;
            retorno.Mensagem = Mensagem;
            retorno.Sucesso =  Sucesso > 0 ? "SIM" :"NÃO";
            return retorno;
        }            

    }
}
