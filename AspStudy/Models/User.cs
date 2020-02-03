using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspStudy.Models
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [DisplayName("ユーザー名")]
        [Index(IsUnique = true)]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [DisplayName("パスワード")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 他のTableとリレーションがある時は、Icollection<Role>という形にする。
        /// またこの時virtual装飾子をつける
        /// </summary>
        public virtual ICollection<Role> roles { get; set; }
        public virtual ICollection<Todo> todo { get; set; }

        /// <summary>
        ///  DBMigrationを掛けたくないときは、NotMappedアノテーションをセット
        /// </summary>
        [NotMapped]
        [DisplayName("ロール")]
        public List <int> RoleIds { get; set; }
    }
}