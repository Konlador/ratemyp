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

        public void Dispose()
            {
            Context.Dispose();
            }
        }
    }
