using Microsoft.AspNetCore.Mvc;
using RateMyP.WebApp.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface ITeacherStatisticsController
        {
        Task<ActionResult<TeacherStatistics>> GetTeacherStatistics(Guid teacherId, int timeStamps);
        }

    [Route("api/statistics")]
    [ApiController]
    public class TeacherStatisticsController : ControllerBase, ITeacherStatisticsController
        {
        private readonly ITeacherStatisticsAnalyzer m_analyzer;

        public TeacherStatisticsController(ITeacherStatisticsAnalyzer analyzer)
            {
            m_analyzer = analyzer;
            }

        [HttpGet("teacher={teacherId}/timeStamps={timeStamps}")]
        public async Task<ActionResult<TeacherStatistics>> GetTeacherStatistics(Guid teacherId, int timeStamps)
            {
            double averageMark;
            try
                {
                averageMark = await m_analyzer.GetTeacherAverageMark(teacherId);
                }
            catch (InvalidDataException)
                {
                averageMark = 0;
                }

            List<DateMark> averageMarks;
            try
                {
                averageMarks = await m_analyzer.GetTeacherAverageMarks(teacherId, timeStamps);
                }
            catch (InvalidDataException)
                {
                averageMarks = new List<DateMark>();
                }

            double levelOfDifficulty;
            try
                {
                levelOfDifficulty = await m_analyzer.GetTeacherAverageLevelOfDifficulty(teacherId);
                }
            catch (InvalidDataException)
                {
                levelOfDifficulty = 0;
                }

            double wouldTakeAgainRatio;
            try
                {
                wouldTakeAgainRatio = await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(teacherId);
                }
            catch (InvalidDataException)
                {
                wouldTakeAgainRatio = 0;
                }

            var teacherStatistic = new TeacherStatistics
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = averageMark,
                AverageMarks = averageMarks,
                AverageLevelOfDifficulty = levelOfDifficulty,
                WouldTakeAgainRatio = wouldTakeAgainRatio
                };

            return teacherStatistic;
            }
        }
    }