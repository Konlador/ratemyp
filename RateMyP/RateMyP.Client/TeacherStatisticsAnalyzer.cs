using RateMyP.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class TeacherStatisticsAnalyzer
        {
        public async Task<double> GetTeacherAverageMark(Guid teacherId)
            {
            var allRatings = await RateMyPClient.Client.Ratings.GetAll();
            var ratings = allRatings.Where((r) => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
            double sum = 0;
            foreach (var rating in ratings)
                sum += rating.OverallMark;
            return ratings.Count > 0 ? sum / ratings.Count : 0;
            }
        }
    }
