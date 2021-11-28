using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    [Table("PostTagTable")]
    [DataContract(IsReference = true)]
    public class PostTag
    {
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Post Post { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Tag Tag { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Blog Blog { get; set; }
        public int BlogId { get; set; }
    }
}
