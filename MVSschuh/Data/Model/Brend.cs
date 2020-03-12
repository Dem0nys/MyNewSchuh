using MVSschuh.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.Data.Model
{
    public class Brend
    {
        [Key]
        public int Id { get; set; }
        public string BrendName { get; set; }
    }
}
