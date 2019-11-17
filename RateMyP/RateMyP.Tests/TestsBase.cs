using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RateMyP.WebApp;
using System;

namespace RateMyP.Tests
    {
    public class TestsBase : IDisposable
        {
        protected RateMyPDbContext Context;

        [SetUp]
        public void SetUp()
            {
            var options = new DbContextOptionsBuilder<RateMyPDbContext>()
                          .UseInMemoryDatabase(GetType().Name)
                          .Options;
            Context = new RateMyPDbContext(options);
            }

        [TearDown]
        public void TearDown()
            {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            }

        public void Dispose()
            {
            Context.Dispose();
            }
        }
    }
