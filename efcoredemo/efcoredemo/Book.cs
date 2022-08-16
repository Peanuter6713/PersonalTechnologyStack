using System;
using System.Collections.Generic;
using System.Text;

namespace efcoredemo
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime PubTime { get; set; }
        public double Price { get; set; }

        public bool IsDeleted { get; set; }
    }
}
