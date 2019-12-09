using System;

namespace RateMyP.WebApp.Models
    {
    public class Badge
        {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }
        }
    }
