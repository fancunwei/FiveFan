using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveFan.Services
{
    /// <summary>
    /// 范围
    /// </summary>
    public class ScopedService : IScopedService
    {
        public string GetInfo()
        {
            return $"this is scoped service ";
        }
    }
}
