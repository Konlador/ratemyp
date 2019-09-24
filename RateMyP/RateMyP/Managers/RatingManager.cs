﻿using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface IRatingManager
        {
        List<Rating> GetAll();
        Rating GetById(Guid ratingId);
        void Add(Rating rating);
        }

    public class RatingManager : IRatingManager
        {
        public List<Rating> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<Rating>().ToList();
                }
            }

        public Rating GetById(Guid ratingId)
            {
            return GetAll().First(rating => rating.Id.Equals(ratingId));
            }

        public void Add(Rating rating)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var ratings = dataContext.GetTable<Rating>();
                ratings.InsertOnSubmit(rating);
                dataContext.SubmitChanges();
                }
            }
        }
    }
