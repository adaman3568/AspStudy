using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspStudy.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Sammary { get; set; }
        public string Detail { get; set; }
        public DateTime Limit { get; set; }
        public bool Done { get; set; }

        /// <summary>
        /// リレーション時に対１なら単体のクラスで良い
        /// </summary>
        public virtual User User { get; set; }
    }
}