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
        
        [HttpGet("teacher={teacherId}/history/parts={parts}")]
        public async Task<ActionResult<TeacherStatistic>> GetTeacherStatistics(Guid teacherId, int parts)
            {
            var teacherStatistic = new TeacherStatistic()
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = await m_analyzer.GetTeacherAverageMark(teacherId),
                AverageMarkList = await m_analyzer.GetTeacherAverageMarkList(teacherId, parts),
                AverageLevelOfDifficulty = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId),
                WouldTakeAgainRatio = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId)
                };
            return teacherStatistic;
            }
        }
}