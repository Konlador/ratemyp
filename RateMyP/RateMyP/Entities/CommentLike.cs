using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public class CommentLike
        {
        public Comment Comment { get; set; }

        public Student Student { get; set; }

        [Key]
        [Column(Order = 0)]
        [ForeignKey("Comment")]
        public Guid CommentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        }
    }
