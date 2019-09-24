using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    public enum CommentOnType
        {
        TeacherProfile,
        StudentProfile,
        Course
        }

    [Table(Name = TABLE_COMMENTS)]
    public class Comment
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "StudentId")]
        public Guid StudentId { get; set; }
        [Column(Name = "CommentOnId")]
        public Guid CommentOnId { get; set; }
        [Column(Name = "CommentOnType")]
        public CommentOnType CommentOnType { get; set; }
        [Column(Name = "Content")]
        public string Content { get; set; }
        [Column(Name = "Likes")]
        public int Likes { get; set; }
        [Column(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        }
    }
