//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace asc_general.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public blog()
        {
            this.likes = new HashSet<like>();
        }
    
        public int id { get; set; }
        public string title { get; set; }
        public string photo { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> category_id { get; set; }
        public string text { get; set; }
        public Nullable<int> mylikes { get; set; }
    
        public virtual blog_category blog_category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<like> likes { get; set; }
    }
}
