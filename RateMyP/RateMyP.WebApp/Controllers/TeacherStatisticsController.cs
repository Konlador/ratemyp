using Microsoft.AspNetCore.Mvc;
using RateMyP.WebApp.Statistics;
using System;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface ITeacherStatisticsController
        {
        Task<ActionResult<TeacherStatistics>> GetTeacherStatistics(Guid teacherId, int timeStamps);
        }

    [Route("api/statistics")]
    [ApiController]
    public class TeacherStatisticsController : ControllerBase
        {
        private readonly ITeacherStatisticsAnalyzer m_analyzer;

        public TeacherStatisticsController(ITeacherStatisticsAnalyzer analyzer)
            {
            m_analyzer = analyzer;
            }

        [HttpGet("teacher={teacherId}/timeStamps={timeStamps}")]
        public async Task<ActionResult<TeacherStatistics>> GetTeacherStatistics(Guid teacherId, int timeStamps)
            {
            var teacherStatistic = new TeacherStatistics
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = await m_analyzer.GetTeacherAverageMark(teacherId),
                AverageMarks = await m_analyzer.GetTeacherAverageMarks(teacherId, timeStamps),
                AverageLevelOfDifficulty = await m_analyzer.GetTeacherAverageLevelOfDifficulty(teacherId),
                WouldTakeAgainRatio = await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(teacherId)
                };

            return teacherStatistic;
            }
        }
    }