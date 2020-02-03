using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspStudy.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        /// <summary>
        /// 他のTableとリレーションをするときはVirtualを付ける。
        /// </summary>
        public virtual ICollection<User> users { get; set; }
    }
}