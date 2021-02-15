using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveFan.Services
{
    /// <summary>
    /// 瞬时
    /// </summary>
    public class TransientService : ITransientService
    {
        public string GetInfo()
        {
            return $"this is transient service";
        }
    }
}
