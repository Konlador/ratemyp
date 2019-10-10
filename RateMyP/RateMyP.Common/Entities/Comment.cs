using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
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
        public Guid Id { get; set; }
        public Student Student { get; set; }
        public Guid CommentOnId { get; set; }
        public CommentOnType CommentOnType { get; set; }
        public string Content { get; set; }
        public List<CommentLike> Likes { get; set; }
        public DateTime DateCreated { get; set; }
        }
    }
