//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARM_Apteka.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        public int user_group { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    
        public virtual Groups Groups { get; set; }
    }
}