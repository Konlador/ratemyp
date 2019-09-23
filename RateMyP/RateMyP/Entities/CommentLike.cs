using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    [Table(Name = TABLE_COMMENT_LIKES)]
    public class CommentLike
        {
        [Column(IsPrimaryKey = true, Name = "CommentId")]
        public Guid CommentId { get; set; }
        [Column(IsPrimaryKey = true, Name = "IdStudentId")]
        public Guid StudentId { get; set; }
        }
    }
