using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite2.Models
{
    /// <summary>
    /// 日志
    /// </summary>
    public class RequestResponseLog
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string RequestJson { get; set; }
        public string ResponseJson { get; set; }
    }
}
