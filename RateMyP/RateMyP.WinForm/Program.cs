using CsvHelper;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RateMyP.WinForm
    {
    static class Program
        {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            }

        private static void LoadTeachersToDb()
            {
            using (var context = new RateMyPDbContext())
                {
                var teachers = ParseTeachersFromCsv();
                context.Teachers.AddRange(teachers);
                context.SaveChanges();
                }
            }

        private static List<Teacher> ParseTeachersFromCsv()
            {
            const string teachersFile = "teachers.csv";
            var assembly = typeof(Program).Assembly;
            var teachersFileStream = assembly.GetManifestResourceStream($"RateMyP.Db.Data.{teachersFile}");

            var reader = new StreamReader(teachersFileStream);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;

            return csvReader.GetRecords<Teacher>().ToList();
            }
        }
    }
