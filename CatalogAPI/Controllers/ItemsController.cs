using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Contracts;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Repositories;



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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = await _repository.GetAllItemsAsync();
            var itemsDTOs = items.Select(item => item.AsDto());
            return itemsDTOs;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new Item
            {
                Name        = itemDto.Name,
                Description = itemDto.Description,
                Price       = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemByIdAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = itemDto.Name;
            existingItem.Description = itemDto.Description;
            existingItem.Price = itemDto.Price;

            await _repository.UpdateItemAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);

            return NoContent();

        }

    }
}
