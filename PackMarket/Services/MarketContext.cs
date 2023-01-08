using Microsoft.EntityFrameworkCore;

using PackMarket.Data;

namespace PackMarket.Services
{
    public class MarketContext
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public ApplicationDbContext DbContext { get; set; }
        public MarketContext(IDbContextFactory<ApplicationDbContext> dbContextFactory) 
        { 
            _dbContextFactory= dbContextFactory;
            DbContext= _dbContextFactory.CreateDbContext();
        }
    }
}
