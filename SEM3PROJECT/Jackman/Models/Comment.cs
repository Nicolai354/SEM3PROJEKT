using System;
using System.Runtime.Serialization;

namespace Jackman.Models
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Person Person { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }
    }
}
