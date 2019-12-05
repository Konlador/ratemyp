using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace RateMyP.Tests.Controllers
    {
    public class TagsControllerTests : TestsBase
        {
        private ITagsController m_controller;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new TagsController(Context);
            }

        [Test]
        public async Task GetTags_ReturnsAllTags()
            {
            var tagsResult = await m_controller.GetTags();

            Assert.IsNull(tagsResult.Result);
            Assert.AreEqual(3, tagsResult.Value.Count());
            }

        private void Seed(RateMyPDbContext context)
            {
            var tag1 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "A TAG",
                Type = TagTypes.Teacher
                };

            var tag2 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "B TAG",
                Type = TagTypes.None
                };

            var tag3 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "C TAG",
                Type = TagTypes.Course
                };

            context.Tags.AddRange(tag1, tag2, tag3);
            context.SaveChanges();
            }
        }
    }
