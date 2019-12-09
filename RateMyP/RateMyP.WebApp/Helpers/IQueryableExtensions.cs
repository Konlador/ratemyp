using RateMyP.WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models.Reports;

namespace RateMyP.WebApp.Helpers
    {
    public static class QueryableExtensions
        {
        public static RatingDto SelectRatingDto(this Rating r)
            {
            return new RatingDto
                {
                Id = r.Id,
                TeacherId = r.TeacherId,
                CourseId = r.CourseId,
                OverallMark = r.OverallMark,
                LevelOfDifficulty = r.LevelOfDifficulty,
                WouldTakeTeacherAgain = r.WouldTakeTeacherAgain,
                DateCreated = r.DateCreated,
                Comment = r.Comment,
                RatingType = r.RatingType,
                ThumbUps = r.ThumbUps,
                ThumbDowns = r.ThumbDowns,
                Tags = r.Tags?.Select(rt => rt.Tag).ToList()
                };
            }

        public static RatingReportDto SelectRatingReportDto(this RatingReport rr)
            {
            return new RatingReportDto
                {
                Id = rr.Id,
                StudentId = rr.StudentId,
                DateCreated = rr.DateCreated,
                Reason = rr.Reason,
                RatingId = rr.Rating.Id,
                Rating = rr.Rating.SelectRatingDto()
                };
            }

        public static IQueryable<RatingDto> SelectRatingDto(this IQueryable<Rating> ratings)
            {
            return ratings
                   .Include(r => r.Tags)
                   .ThenInclude(rt => rt.Tag)
                   .Select(r => r.SelectRatingDto());
            }

        public static IQueryable<RatingReportDto> SelectRatingReportDto(this IQueryable<RatingReport> ratingReports)
            {
            return ratingReports
                   .Include(rr => rr.Rating)
                   .ThenInclude(r => r.Tags)
                   .ThenInclude(rt => rt.Tag)
                   .Select(rr => rr.SelectRatingReportDto());
            }
        }
    }
