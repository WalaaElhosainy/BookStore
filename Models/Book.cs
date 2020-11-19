using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int id { get; set; }
        public string  title { get; set; }
        public string descreption { get; set; }
        public string imgUrl { get; set; }
        public Author author { get; set; }
    }
}
