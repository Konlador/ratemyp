using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public class CommentLike
        {
        public Guid CommentId { get; set; }
        public Guid StudentId { get; set; }

        [ForeignKey("CommentId")]
        public Comment Comment { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        }
    }
