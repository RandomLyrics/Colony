//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HParser
{
    using System;
    using System.Collections.Generic;
    
    public partial class File
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public File()
        {
            this.Records = new HashSet<Record>();
            this.Files = new HashSet<File>();
        }
    
        public long FileId { get; set; }
        public int GatewayId { get; set; }
        public int FileTypeId { get; set; }
        public Nullable<long> MatchedFileId { get; set; }
        public string Name { get; set; }
        public string InsertDate { get; set; }
        public string Metadata { get; set; }
    
        public virtual FileType FileType { get; set; }
        public virtual Gateway Gateway { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Record> Records { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }
        public virtual File File1 { get; set; }
    }
}
