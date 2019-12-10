using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace VUDataScraper
    {
    class Scraper
        {
        public FirefoxDriver Driver { get; set; }
        public  string BaseUrl { get; set; }

        public Scraper(FirefoxDriver driver)
            {
            this.Driver = driver;
            this.BaseUrl = "https://tvarkarasciai.vu.lt";
            }

        public static string ReplaceWhitespace(string input) 
            { 
            return Regex.Replace(input, @"\s*(?<capture><(?<markUp>\w+)>.*<\/\k<markUp>>)\s*", "${capture}", RegexOptions.Singleline);
            }

        public List<Teacher> GetTeachers(string faculty)
            {
            Driver.Navigate().GoToUrl(BaseUrl + "/" + faculty + "/employees/");
            var source = Driver.PageSource;

            var linksRegex = new Regex(@"(?<=href="")(.*?)(?=\"" data-lect)");
            var namesRegex = new Regex(@"(?<=data-lect="""">)(.*?)(?=</a>)");

            var namesMatches = namesRegex.Matches(source);
            var linksMatches = linksRegex.Matches(source);

            var teachers = new List<Teacher>();

            for (int i = 0; i < namesMatches.Count; i++)
                {
                var link = linksMatches[i].ToString();
                var fullName = namesMatches[i].ToString().SplitCommas()[0];
                var rank = String.Join<string>(String.Empty, namesMatches[i].ToString().SplitCommas().Skip(1).ToList());
                var firstName = fullName.SplitSpaces()[0];
                var lastName = String.Join<string>(" ", fullName.SplitSpaces().Skip(1).ToList());
                var description = "";
                var teacher = new Teacher
                    {
                    Id = GuidUtility.Create(GuidUtility.NameSpaceX500, fullName + rank + faculty),
                    Faculty = faculty.ToUpper(),
                    FirstName = firstName,
                    LastName = lastName,
                    Rank = rank,
                    Description = description,
                    Link = link
                    };

                teachers.Add(teacher);
                }
            return teachers;
            }

        public List<Course> GetCourses(string faculty)
            {
            Console.WriteLine("Scraping: " + faculty);
            Driver.Navigate().GoToUrl(BaseUrl + "/" + faculty + "/subjects/");
            var source = Driver.PageSource;

            var aRegex = new Regex(@"(?<=<td>)(.*?)(?=</td>)");
            var linksRegex = new Regex(@"(?<=<a href="")(.*?)(?=\"")");
            var namesRegex = new Regex(@"(?<="">)(.*?)(?=<small>)");
            var typeRegex = new Regex(@"(?<=<small>, )(.*?)(?=</small>)");

            var aMatches = (from Match m in aRegex.Matches(source) select m.Value).ToList();
            var aString = String.Join<string>(" ", aMatches);
            var typeMatches = typeRegex.Matches(aString);
            var linksMatches = linksRegex.Matches(aString);
            var namesMatches = namesRegex.Matches(aString);

            var courses = new List<Course>();

            for (int i = 0; i < namesMatches.Count; i++)
                {
                var link = linksMatches[i].ToString();
                var name = namesMatches[i].ToString();
                var courseType = typeMatches[i].ToString();
                var course = new Course
                    {
                    Id = GuidUtility.Create(GuidUtility.NameSpaceX500, name + " " + courseType),
                    Faculty = faculty.ToUpper(),
                    Name = name,
                    CourseType = courseType,
                    Link = link
                    };

                courses.Add(course);
                }
            return courses;
            }

        public List<TeacherActivity> GetTeacherActivities(List<Teacher> teachers, List<Course> courses, int maxIterations)
            {

            //maxIterations is for testing purposes. Set to low if few iterations are wanted.

            var fctitleRegex = new Regex(@"<a data-toggle(.*?)(?=<br>)");
            var dataeventtypeRegex = new Regex(@"(?<=data-eventtype="")(.*?)(?=\"")");
            var subjectLinkRegex = new Regex(@"(?<=href=&quot;)(.*?)(?=\&quot;)");
            var activityRegex = new Regex(@"(?<=>)(.*?)(?=<)");

            var teacherActivities = new List<TeacherActivity>();

            for (int i = 0; i < maxIterations && i < teachers.Count; i++)
                {
                Driver.Navigate().GoToUrl(BaseUrl + teachers[i].Link);
                var source = Driver.PageSource;

                var aMatches = (from Match m in fctitleRegex.Matches(source) select m.Value).ToList();

                foreach (var element in aMatches)
                    {
                    var dataeventtypeMatch = dataeventtypeRegex.Match(element).Value;
                    if (dataeventtypeMatch != "")
                        {
                        var subjectLinkMatch = subjectLinkRegex.Match(dataeventtypeMatch);
                        var activityMatch = activityRegex.Match(dataeventtypeMatch);
                        var course = courses.Find(x => x.Link == subjectLinkMatch.Value);
                        if (course == null && subjectLinkMatch.Value != "")
                            {
                            course = GetCourseInfo(subjectLinkMatch.Value);
                            courses.Add(course);
                            }

                        if(subjectLinkMatch != null && activityMatch != null && subjectLinkMatch.Value != "")
                            {
                            var activity = new TeacherActivity
                                {
                                CourseId = course.Id,
                                TeacherId = teachers[i].Id,
                                LectureType = activityMatch.Value
                                };

                            if(!teacherActivities.Contains(activity))
                                {
                                teacherActivities.Add(activity);
                                }
                            }
                        }
                    }
                }
            return teacherActivities;
            }

        public Course GetCourseInfo(string url)
            {
            Driver.Navigate().GoToUrl(BaseUrl + url);

            var element = Driver.FindElementByCssSelector(".intro-text > strong:nth-child(1)");
            var content = element.GetAttribute("innerHTML");

            var splitString = content.SplitSpaces();

            var courseType = splitString[splitString.Count() - 1].Trim(new Char[] { '(', ')' });
            var name = String.Join(" ", splitString.Take(splitString.Count() - 1).ToArray());
            
            var course = new Course
                {
                Id = GuidUtility.Create(GuidUtility.NameSpaceX500, name + " " + courseType),
                Faculty = url.Split("/")[1].ToUpper(),
                Name = name,
                CourseType = courseType,
                Link = url
                };

            return course;
            }

        public void SetLanguage(string language, FirefoxDriver driver)
            {
            driver.Navigate().GoToUrl(BaseUrl + "/" + "mif" + "/subjects/");
            driver.FindElement(By.XPath(".//a[@class='dropdown-toggle empty']")).Click();
            driver.FindElement(By.XPath($".//input[@value='{language}']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            driver.Navigate().Refresh();
            }
        }
    }
