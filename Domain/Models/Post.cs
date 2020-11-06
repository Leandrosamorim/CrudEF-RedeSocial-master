using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public string OwnerEmail { get; set; }
        public string BlobUri { get; set; }
    }
}
