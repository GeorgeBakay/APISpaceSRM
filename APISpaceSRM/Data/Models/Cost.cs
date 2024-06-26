﻿using System.ComponentModel.DataAnnotations;

namespace APISpaceSRM.Data.Models
{
    public class Cost
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
    }
}
