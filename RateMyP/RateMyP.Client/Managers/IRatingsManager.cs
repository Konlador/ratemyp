using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateMyP.Entities;

namespace RateMyP.Client.Managers
    {
    public interface IRatingsManager
        {
        Task<List<Rating>> GetAll();
        }
    }
