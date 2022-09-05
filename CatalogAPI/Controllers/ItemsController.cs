using CatalogAPI.Contracts;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = await _repository.GetItemsAsync();
            var itemsDTOs = items.Select(item => item.AsDto());
            return itemsDTOs;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto itemDto)
        {
            Item item = new Item
            {
                Id          = Guid.NewGuid(),
                Name        = itemDto.Name,
                Price       = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem == null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name    = itemDto.Name,
                Price   = itemDto.Price
            };

            await _repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);

            return NoContent();

        }

    }
}
