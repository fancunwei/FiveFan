using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite2.Infrastructure.Queue
{
    /// <summary>
    /// BlockingCollection演示消息队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DemoQueueBlock<T> 
    {
        private static readonly BlockingCollection<T> Colls = new BlockingCollection<T>();
        

        public static bool Add(T msg)
        {
            Colls.Add(msg);
            return true;
        }
        public static T Take()
        {
            return Colls.Take();
        }

        public static void Consume(Action<T> func)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var item in Colls.GetConsumingEnumerable())
                {
                    func(item);
                    Console.WriteLine(string.Format("---------------: {0}", item));
                }
            });
        }

    }

}
