using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyP
    {
    /* 
     *Temporary class for imitating a database
     */
    public class DatabaseConnection
        {
        private readonly string m_ratingsFileName;
        private readonly string m_teachersFileName;

        public DatabaseConnection (string ratingsFileName, string teachersFileName)
            {
            m_ratingsFileName = ratingsFileName;
            m_teachersFileName = teachersFileName;
            if (!Directory.Exists (Path.GetDirectoryName (m_ratingsFileName)))
                Directory.CreateDirectory (Path.GetDirectoryName (m_ratingsFileName));
            if (!Directory.Exists(Path.GetDirectoryName(m_teachersFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(m_teachersFileName));
            if (!File.Exists(m_ratingsFileName))
                File.Create(m_ratingsFileName).Close();
            if (!File.Exists(m_teachersFileName))
                File.Create(m_teachersFileName).Close();
            }

        public void Clear ()
            {
            if (File.Exists (m_ratingsFileName))
                {
                File.Delete(m_ratingsFileName);
                File.Create(m_ratingsFileName).Close();
                }

            if (File.Exists (m_teachersFileName))
                {
                File.Delete(m_teachersFileName);
                File.Create(m_teachersFileName).Close();
                }
            }

        public List<Rating> GetRatings ()
            {
            var records = GetRecords (m_ratingsFileName);
            var ratings = new List<Rating> ();

            foreach (var record in records)
                {
                var values = record.Split(',');

                ratings.Add(new Rating
                    {
                    TeacherGuid = new Guid(values[0]),
                    TeacherMark = int.Parse(values[1]),
                    LevelOfDifficulty = int.Parse(values[2]),
                    WouldTakeTeacherAgain = bool.Parse(values[3]),
                    Tags = values[4].Split('|').ToList(),
                    Comment = values[5]
                    });
                }
            return ratings;
            }

        public void SaveRating (Rating rating)
            {
            string record = $"{rating.TeacherGuid},{rating.TeacherMark},{rating.LevelOfDifficulty},{rating.WouldTakeTeacherAgain},{string.Join("|", rating.Tags.ToArray ())},{rating.Comment}";
            File.WriteAllText(m_ratingsFileName, record);
            }

        public List<Teacher> GetTeachers()
            {
            var records = GetRecords(m_teachersFileName);
            var teachers = new List<Teacher>();

            foreach (var record in records)
                {
                var values = record.Split(',');

                teachers.Add(new Teacher
                    {
                    Id = new Guid(values[0]),
                    Name = values[1],
                    Surname = values[2],
                    Rank = (AcademicRank)Enum.Parse(typeof(AcademicRank), values[3])
                    });
                }
            return teachers;
            }

        public void SaveTeacher(Teacher teacher)
            {
            string record = $"{teacher.Id.ToString()},{teacher.Name},{teacher.Surname},{teacher.Rank.ToString()}";
            File.WriteAllText(m_teachersFileName, record);
            }

        private List<string> GetRecords(string filePath)
            {
            var records = new List<string>();
            using (var reader = new StreamReader(filePath))
                {
                while (!reader.EndOfStream)
                    records.Add(reader.ReadLine());
                }

            return records;
            }
        }
    }
