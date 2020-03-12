using MVSschuh.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVSschuh.Data.Model
{
    public class Trend
    {
        [Key]
        public int Id { get; set; }
        public string TrendName { get; set; }

    }
}
