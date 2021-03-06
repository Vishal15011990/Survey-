//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Survey.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questionarie
    {
        public int Id { get; set; }
        public string Company_Name { get; set; }
        public string User_Name { get; set; }
        public System.Guid Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> State { get; set; }
        public string Address_1 { get; set; }
        public string Pincode { get; set; }
        public string EmailId { get; set; }
        public string Phone { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.Guid CreatedBy { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Country_Info Country_Info { get; set; }
        public virtual State_Info State_Info { get; set; }
    }
}
