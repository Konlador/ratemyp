using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VUDataScraper
    {
    class CSVWriter
        {
        public  string Directory { get; set; }
        public CSVWriter(string directory)
            {
            this.Directory = directory;
            }
        public void WriteTeachers(List<Teacher> teachers)
            {
            string newFileName = Directory + "teachers.csv";

            if (!File.Exists(newFileName))
                {
                string teacherHeader = "Id" + "," + "FirstName" + "," + "LastName" + "," + "Description" + "," + "Rank" + "," + "Faculty" + Environment.NewLine;

                File.WriteAllText(newFileName, teacherHeader);
                }

            foreach(var teacher in teachers)
                {
                string teacherDetails = teacher.Id + "," + teacher.FirstName + "," + teacher.LastName + "," + teacher.Description + "," + teacher.Rank + "," + teacher.Faculty + Environment.NewLine;
                File.AppendAllText(newFileName, teacherDetails, Encoding.Default);
                }
            }

        public void WriteCourses(List<Course> courses)
            {
            string newFileName = Directory + "courses.csv";

            if (!File.Exists(newFileName))
                {
                string teacherHeader = "Id" + "," + "Name" + "," + "CourseType" + "," + "Credits" + "," + "Faculty" + Environment.NewLine;

                File.WriteAllText(newFileName, teacherHeader, Encoding.Default);
                }

            foreach(var course in courses)
                {
                if (course.CourseType == "General university studies" || course.CourseType == "studies")
                    course.CourseType = "BUS";
                string teacherDetails = course.Id + "," + course.Name + "," + course.CourseType + "," + "5" + "," + course.Faculty + Environment.NewLine;

                File.AppendAllText(newFileName, teacherDetails);
                }
            }

        public void WriteTeacherActivities(List<TeacherActivity> activities)
            {
            string newFileName = Directory + "teacher_activities.csv";
            var dateStarted = DateTime.Parse("2019-9-1");
            if (!File.Exists(newFileName))
                {
                string teacherHeader = "TeacherId" + "," + "CourseId" + "," + "DateStarted" + "," + "LectureType" + Environment.NewLine;

                File.WriteAllText(newFileName, teacherHeader);
                }

            foreach (var activity in activities)
                {
                string teacherDetails = activity.TeacherId + "," + activity.CourseId + "," + dateStarted + "," + activity.LectureType + Environment.NewLine;

                File.AppendAllText(newFileName, teacherDetails, Encoding.UTF8);
                }
            }
        }
    }
