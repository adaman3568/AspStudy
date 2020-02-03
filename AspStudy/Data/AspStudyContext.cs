using AspStudy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspStudy.Data
{
    public class AspStudyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AspStudyContext() : base("name=AspStudyContext")
        {
        }

        /// <summary>
        /// 登録したいTableをModelで追加したら、下記の追加していく。
        /// </summary>
        public DbSet<Todo> Todoes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
