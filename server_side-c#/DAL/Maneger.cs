//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Maneger
    {
        public string Gmail { get; set; }
        public double Salary { get; set; }
        public string Password { get; set; }
        public string Id { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
