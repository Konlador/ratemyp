using System;
using System.Collections.Generic;

namespace RateMyP.WebApp.Models
    {
    public class Tag : IEquatable<Tag>
        {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool Equals(Tag other) =>
            other != null &&
            other.Text.Equals(Text);
        public override string ToString()
            {
            return this.Text;
            }
        }
    }
