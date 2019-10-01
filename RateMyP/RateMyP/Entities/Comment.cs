using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public enum CommentOnType
        {
        TeacherProfile,
        StudentProfile,
        Course
        }

    public class Comment
        {
        [Key]
        public Guid Id { get; set; }
        public Student Student { get; set; }
        public Guid CommentOnId { get; set; }
        public CommentOnType CommentOnType { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public DateTime DateCreated { get; set; }
        }
    }
