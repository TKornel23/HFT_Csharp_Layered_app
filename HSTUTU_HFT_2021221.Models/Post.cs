using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTUTU_HFT_2021221.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Blog BlogId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public bool IsDeleted { get; set; }


        public ICollection<PostTag> PostTags { get; set; }
    }
}
