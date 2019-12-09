using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateMyP.WebApp.Helpers;

namespace RateMyP.WebApp.Controllers
    {
    public interface IBadgeController
        {
        Task<ActionResult<IEnumerable<Badge>>> GetBadges();
        Task<ActionResult<Badge>> GetBadge(Guid id);
        Task<ActionResult<IEnumerable<TeacherBadge>>> GetTeacherBadges(Guid teacherId);
        ActionResult GetBadgeImage(Guid id);

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

        [HttpGet("badge={id}")]
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

        public byte[] GetImageMetadata(Guid id, out string type)
            {
            byte[] imageData = null;
            string imageType = null;
            Badge badge = GetBadgeImageFromDb(id).Result.Value;
            if (badge != null)
                {
                imageType = badge.Type;
                imageData = badge.Data;
                }

            type = imageType;
            return imageData;
            }

        public async Task<ActionResult<Badge>> GetBadgeImageFromDb(Guid id)
            {
            await using (m_context)
                {
                var badge = await m_context.Badges.FirstAsync(i => i.Id == id);
                if (badge != null)
                    return badge;
                }

            return null;
            }

        [HttpGet("image={id}")]
        public ActionResult GetBadgeImage(Guid id)
            {
            string mime;
            byte[] bytes = GetImageMetadata(id, out mime);
            return File(bytes, "image/" + mime);
            }
        }
    }