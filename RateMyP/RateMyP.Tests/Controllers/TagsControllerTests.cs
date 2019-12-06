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

        private Tag m_tag1;
        private Tag m_tag2;
        private Tag m_tag3;

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
            Assert.Contains(m_tag1, tagsResult.Value.ToList());
            Assert.Contains(m_tag2, tagsResult.Value.ToList());
            Assert.Contains(m_tag3, tagsResult.Value.ToList());
            }

        private void Seed(RateMyPDbContext context)
            {
            m_tag1 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "A TAG",
                Type = TagTypes.Teacher
                };

            m_tag2 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "B TAG",
                Type = TagTypes.None
                };

            m_tag3 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "C TAG",
                Type = TagTypes.Course
                };

            context.Tags.AddRange(m_tag1, m_tag2, m_tag3);
            context.SaveChanges();
            }
        }
    }
