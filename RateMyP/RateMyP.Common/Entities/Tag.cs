using System;

namespace RateMyP.Entities
    {
    public class Tag : IEquatable<Tag>
        {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool Equals(Tag other) =>
            other != null &&
            other.Text.Equals(Text);
        }
    }
