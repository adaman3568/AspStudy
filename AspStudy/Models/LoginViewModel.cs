using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspStudy.Models
{
    /// <summary>
    /// ログイン時にViewとControllerに値を渡す役割のMODEL
    /// </summary>
    public class LoginViewModel
    {

        [Required]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("パスワード")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}