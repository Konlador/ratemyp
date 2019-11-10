using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class TeacherStatisticsController : ControllerBase
    {
        private readonly RateMyPDbContext m_context;
        private TeacherStatisticsAnalyzer m_analyzer;

        public TeacherStatisticsController(RateMyPDbContext context)
        {
            m_context = context;
            m_analyzer = new TeacherStatisticsAnalyzer(m_context);
        }
        
        [HttpGet("teacher={teacherId}/date={startDate}-{endDate}/parts={parts}")]
        public async Task<ActionResult<TeacherStatistic>> GetTeacherStatistics(Guid teacherId, long startDate, long endDate, int parts)
            {
            var startDateTime = new DateTime(startDate);
            var endDateTime = new DateTime(endDate);
            var teacherStatisticByDate = new TeacherStatistic()
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = await m_analyzer.GetTeacherAverageMark(teacherId),
                AverageMarkByDate = await m_analyzer.GetTeacherAverageMark(teacherId, startDateTime, endDateTime),
                AverageMarkList = await m_analyzer.GetTeacherAverageMarkList(teacherId, startDateTime, endDateTime, parts),
                AverageLevelOfDifficulty = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId),
                AverageWouldTakeAgainRatio = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId)
                };

            return teacherStatisticByDate;
            }
        }
}