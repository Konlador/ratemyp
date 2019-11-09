using System;
using System.Collections.Generic;

namespace RateMyP.WebApp.Models
    {
    [Flags]
    public enum TagTypes { None = 0, Teacher = 1, Course = 2 }
    public class Tag : IEquatable<Tag>
        {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public TagTypes TagType { get; set; }

        public bool Equals(Tag other) =>
            other != null &&
            other.Text.Equals(Text) &&
            other.TagType.Equals(TagType);
        public override string ToString()
            {
            return this.Text;
            }
        }
    }
