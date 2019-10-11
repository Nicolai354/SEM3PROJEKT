using System;
using System.Runtime.Serialization;

namespace Jackman.Models
{
    [DataContract]
    public class Case
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Status Status { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public int Priority { get; set; }

        [DataMember]
        public Subcategory Subcategory { get; set; }

        [DataMember]
        public Category Category { get; set; }

        [DataMember]
        public string OperatingSystem { get; set; }

        [DataMember]
        public Supporter Supporter { get; set; }

        [DataMember]
        public Customer Customer { get; set; }
    }
}
