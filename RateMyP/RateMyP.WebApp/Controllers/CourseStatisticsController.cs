using Microsoft.AspNetCore.Mvc;
using RateMyP.WebApp.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/statistics")]
    [ApiController]
    public class CourseStatisticsController : ControllerBase
        {
        private readonly ICourseStatisticsAnalyzer m_analyzer;

        public CourseStatisticsController(ICourseStatisticsAnalyzer analyzer)
            {
            m_analyzer = analyzer;
            }

        [HttpGet("course={courseId}/timeStamps={timeStamps}")]
        public async Task<ActionResult<CourseStatistics>> GetCourseStatistics(Guid courseId, int timeStamps)
            {
            double averageMark;
            try
                {
                averageMark = await m_analyzer.GetCourseAverageMark(courseId);
                }
            catch (InvalidDataException)
                {
                averageMark = 0;
                }

            List<DateMark> averageMarks;
            try
                {
                averageMarks = await m_analyzer.GetCourseAverageMarks(courseId, timeStamps);
                }
            catch (InvalidDataException)
                {
                averageMarks = new List<DateMark>();
                }

            double levelOfDifficulty;
            try
                {
                levelOfDifficulty = await m_analyzer.GetCourseAverageLevelOfDifficulty(courseId);
                }
            catch (InvalidDataException)
                {
                levelOfDifficulty = 0;
                }

            double wouldTakeAgainRatio;
            try
                {
                wouldTakeAgainRatio = await m_analyzer.GetCourseWouldTakeTeacherAgainRatio(courseId);
                }
            catch (InvalidDataException)
                {
                wouldTakeAgainRatio = 0;
                }

            var courseStatistic = new CourseStatistics
                {
                Id = Guid.NewGuid(),
                CourseId = courseId,
                AverageMark = averageMark,
                AverageMarks = averageMarks,
                AverageLevelOfDifficulty = levelOfDifficulty,
                WouldTakeAgainRatio = wouldTakeAgainRatio
                };

            return courseStatistic;
            }
        }
    }