using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface IBadgeController
        {
        Task<ActionResult<IEnumerable<Badge>>> GetBadges();
        Task<ActionResult<Badge>> GetBadge(Guid id);
        Task<ActionResult<IEnumerable<TeacherBadge>>> GetTeacherBadges(Guid teacherId);
        Task<ActionResult<byte[]>> GetBadgeImage(Guid id);

        }

    [Route("api/badges")]
    [ApiController]
    public class BadgeController : ControllerBase, IBadgeController
        {
        private readonly RateMyPDbContext m_context;

        public BadgeController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges()
            {
            return await m_context.Badges.ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(Guid id)
            {
            var badge = await m_context.Badges.FindAsync(id);

            if (badge == null)
                return NotFound();

            return badge;
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherBadge>>> GetTeacherBadges(Guid teacherId)
            {
            var badges = await m_context.TeacherBadges
                            .Where(x => x.TeacherId.Equals(teacherId))
                            .ToListAsync();

            return badges;
            }

        [HttpGet("image/{id}")]
        public async Task<ActionResult<byte[]>> GetBadgeImage(Guid id)
            {
            byte[] imageData = null;
            await using (m_context)
                {
                var image = await m_context.Badges.FirstAsync(i => i.Id == id);
                if (image != null)
                    {
                    imageData = image.Data;
                    }
                }

            return imageData;
            }

        public void Upload(Stream fileStream, string name, string description, long size)
            {
            try
                {
                var imageData = new byte[fileStream.Length];
                fileStream.Read(imageData, 0, imageData.Length);

                var badge = new Badge
                    {
                    Id = new Guid(),
                    Data = imageData,
                    Description = description,
                    Image = name,
                    Size = size,
                    };

                using (m_context)
                    {
                    m_context.Badges.Add(badge);
                    m_context.SaveChanges();
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine("Shit's fucked: " + ex.Message);
                }
            }
        }
    }