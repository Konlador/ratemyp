using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/merch")]
    [ApiController]
    public class MerchandisesController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public MerchandisesController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Merchandise>>> GetMerchandises()
            {
            return await m_context.Merchandises.ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<Merchandise>> GetMerchandise(Guid id)
            {
            var merchandise = await m_context.Merchandises.FindAsync(id);

            if (merchandise == null)
                return NotFound();

            return merchandise;
            }

        [Authorize]
        [HttpPost]
        [Route("order")]
        public async Task<ActionResult<Merchandise>> PostMerchandiseOrder(MerchandiseOrder merchandiseOrder)
            {
            m_context.MerchandiseOrders.Add(merchandiseOrder);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetMerchandiseOrder", new { id = merchandiseOrder.Id }, merchandiseOrder);
            }

        private bool MerchandiseExists(Guid id)
            {
            return m_context.Merchandises.Any(e => e.Id == id);
            }
        }
    }
