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

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherStatistic>>> GetTeacherStatistics(Guid teacherId)
        {
            var teacherStatistic = new TeacherStatistic()
            {
                Id = Guid.NewGuid(),
                TeacherId = teacherId,
                AverageMark = await m_analyzer.GetTeacherAverageMark(teacherId),
                AverageLevelOfDifficulty = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId),
                AverageWouldTakeAgainRatio = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId)
            };
            List<TeacherStatistic> list = new List<TeacherStatistic>();
            list.Add(teacherStatistic);
            return list;
        }
    }
}