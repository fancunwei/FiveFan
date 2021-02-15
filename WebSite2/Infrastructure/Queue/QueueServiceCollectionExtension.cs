using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite2.Infrastructure.Queue
{
    public static class QueueServiceCollectionExtension
    {
        /// <summary>
        /// 添加队列
        /// </summary>
        /// <returns></returns>
        public static void AddConsume(this IServiceCollection services)
        {

            services.AddScoped<DemoConsume, DemoConsume>();
            var provider = services.BuildServiceProvider();
            var consume=provider.GetService<DemoConsume>();
            consume.Consume();
        }
    }
}
