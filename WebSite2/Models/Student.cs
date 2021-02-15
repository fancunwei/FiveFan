using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite2.Models
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; set; }
    }
}
