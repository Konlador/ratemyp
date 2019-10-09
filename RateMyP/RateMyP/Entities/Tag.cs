using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
{
    class Tag
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
