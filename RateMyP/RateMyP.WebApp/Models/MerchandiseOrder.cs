using System;

namespace RateMyP.WebApp.Models
    {
    public class MerchandiseOrder
        {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        }
    }
