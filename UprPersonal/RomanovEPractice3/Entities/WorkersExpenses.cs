//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RomanovEPractice3.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkersExpenses
    {
        public int Id_Expenses { get; set; }
        public int Id_Worker { get; set; }
        public int Id_Shop { get; set; }
        public decimal Summ { get; set; }
    
        public virtual ShopIdName ShopIdName { get; set; }
        public virtual Workers Workers { get; set; }
    }
}