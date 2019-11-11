using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateMyP.WebApp.Statistics;

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

        [HttpGet("teacher={teacherId}/timeStamps={timeStamps}")]
        public async Task<ActionResult<TeacherStatistics>> GetTeacherStatistics(Guid teacherId, int timeStamps)
            {
            var teacherStatistic = new TeacherStatistics
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = await m_analyzer.GetTeacherAverageMark(teacherId),
                AverageMarks = await m_analyzer.GetTeacherAverageMarks(teacherId, timeStamps),
                AverageLevelOfDifficulty = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId),
                WouldTakeAgainRatio = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId)
                };

            return teacherStatistic;
            }
        }
    }