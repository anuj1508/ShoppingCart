//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartDetail
    {
        public int Id { get; set; }
        public int Cart_id { get; set; }
        public int Map_id { get; set; }
        public int Quantity { get; set; }
    
        public virtual Cart Cart { get; set; }
        public virtual ProductMap ProductMap { get; set; }
    }
}