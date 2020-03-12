using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVSschuh.Data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVSschuh.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Sale { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Price { get; set; }
        public bool Available { get; set; }
        public int Count { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Trend")]
        public int TrendId { get; set; }
        [ForeignKey("Brend")]
        public int BrendId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Trend Trend { get; set; }
        public virtual Brend Brend { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
