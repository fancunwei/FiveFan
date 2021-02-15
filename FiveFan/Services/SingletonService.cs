using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveFan.Services
{
    /// <summary>
    /// 单例
    /// </summary>
    public class SingletonService:ISingletonService
    {
        public string GetInfo()
        {
            return $"this is singleton service";
        }
    }
}
