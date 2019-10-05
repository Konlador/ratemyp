﻿using CsvHelper;
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
            var assembly = typeof(Program).Assembly;
            var path = Path.Combine(Path.GetFullPath(assembly.Location + @"..\..\..\..\..\.."), @"DbData\teachers.csv");

            var reader = new StreamReader(path);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;

            return csvReader.GetRecords<Teacher>().ToList();
            }
        }
    }