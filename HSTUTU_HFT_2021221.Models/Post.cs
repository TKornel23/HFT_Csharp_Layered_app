﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    [Table("PostTable")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        [ForeignKey(nameof(Blog))]
        public int BlogId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Blog Blog { get; set; }
        public string PostContent { get; set; }
        public int Likes { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
