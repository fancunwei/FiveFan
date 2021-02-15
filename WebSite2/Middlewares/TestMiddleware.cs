using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite2.Middlewares
{
    public class TestMiddleware : IMiddleware
    {
        public async  Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("TestMiddleware");
          await  next(context);
           // throw new NotImplementedException();
        }
    }
}
