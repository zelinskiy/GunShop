using GunShop.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShop.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public SearchService(
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<SearchService>();
        }
    }
}
