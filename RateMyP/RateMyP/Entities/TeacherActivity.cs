using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public enum LectureTypes
        {
        paskaita = 1,
        pratybos = 2,
        seminaras = 3
        }

    public class TeacherActivity
        {
        [Key]
        public Guid Id { get; set; }
        
        public Guid TeacherId { get; set; }   
        public Guid CourseId { get; set; }
        public DateTime DateStarted { get; set; }
        [Required]
        public virtual int Type
        {
            get
            {
                return (int)this.LectureType;
            }
            set
            {
                LectureType = (LectureTypes)value;
            }
        }
        [EnumDataType(typeof(LectureTypes))]
        public LectureTypes LectureType { get; set; }

        
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
    }
