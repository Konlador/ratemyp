using System;

namespace RateMyP.WebApp.Models
    {
    [Flags]
    public enum TagTypes
        {
        None,
        Teacher,
        Course
        }

    public class Tag : IEquatable<Tag>
        {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public TagTypes Type { get; set; }

        public bool Equals(Tag other) =>
            other != null &&
            other.Text.Equals(Text) &&
            other.Type.Equals(Type);

        public override string ToString()
            {
            return Text;
            }
        }
    }
