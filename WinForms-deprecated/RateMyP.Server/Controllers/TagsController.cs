using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
    {
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public TagsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
            {
            return await m_context.Tags.ToListAsync();
            }
        }
    }
