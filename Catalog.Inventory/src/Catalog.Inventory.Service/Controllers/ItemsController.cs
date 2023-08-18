using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Common;
using Catalog.Inventory.Service.DTOs;
using Catalog.Inventory.Service.Entities;
using System.Linq;

namespace Catalog.Inventory.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> itemsRepository;

        public ItemsController(IRepository<InventoryItem> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
                return BadRequest($"{nameof(userId)} is null");
            
            var items = (await itemsRepository.GetAllAsync(item => item.UserId == userId))
                        .Select(items => items.AsDto());   

            return Ok(items);
        }
    }   
}