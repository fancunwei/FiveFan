using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite2.Models;

namespace WebSite2.Infrastructure.Queue
{
    public class DemoConsume
    {
        private readonly MysqlDbContext _dbContext;
        public DemoConsume(MysqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Consume()
        {
            DemoQueueBlock<RequestResponseLog>.Consume(async (log)=> {
                Console.WriteLine(JsonConvert.SerializeObject(log));
               await _dbContext.AddAsync(log);
               await _dbContext.SaveChangesAsync();
            });
            return true;
        }
    }
}
