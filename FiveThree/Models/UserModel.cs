using System;
using System.Collections.Generic;
using System.Text;

namespace FiveFan.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }
    }

    public class UserVm
    {

        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }
    }
}
