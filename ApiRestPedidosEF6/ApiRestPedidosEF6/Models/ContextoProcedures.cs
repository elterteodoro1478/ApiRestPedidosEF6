using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    public partial class Contexto
    {
        private IContextoProcedures _procedures;

        public virtual IContextoProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new ContextoProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IContextoProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
        }
    }

    public partial class ContextoProcedures : IContextoProcedures
    {
        private readonly Contexto _context;

        public ContextoProcedures(Contexto context)
        {
            _context = context;
        }
    }
}
