using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Play.Catalog.Service.Controllers
{
    //https://localhost:5001/items
    [ApiController]
    [Route("items")]
    public class ItemController
      : ControllerBase
    {

        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antido", "Cures position ", 7, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronmde sword", "deals a small amount of HP", 20, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        //Get /items/12345
        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(i => i.Id == id).SingleOrDefault();
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = items.Where(d => d.Id == id).SingleOrDefault();
            var updateItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items[index] = updateItem;
            return NoContent();
        }

        //DELETE  /items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id){
           var index = items.FindIndex(index=>index.Id== id);
           items.RemoveAt(index);

           return NoContent();
        }



    }

}