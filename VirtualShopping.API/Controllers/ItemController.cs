using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualShopping.BLL.Interface;
using VirtualShopping.Domain.Requests.ItemDTO;
using VirtualShopping.Domain.Responses.Item;

namespace VirtualShopping.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        /// <summary>
        /// Create a new Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>An object of new Item</returns>
        [HttpPost("create")]
        public async Task<ActionResult<ItemDTO>> Create([FromForm] ItemCreateDTO dto)
        {
            var item = await _itemService.Create(dto);
            return !string.IsNullOrEmpty(item.ErrorMessage)
                ? BadRequest(item.ErrorMessage)
                : Ok(item);
        }
        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>An object of new Item</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ItemUpdateDTO dto)
        {
            var item = await _itemService.Update(dto);

            return Ok(String.IsNullOrEmpty(item.ErrorMessage) ? item : item.ErrorMessage);
        }
        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>An object of Deleted Item</returns>
        [HttpDelete]
        public async Task<ActionResult<ItemDTO>> Delete(ItemDeleteDTO dto)
        {
            var item = await _itemService.Delete(dto);

            return !string.IsNullOrEmpty(item.ErrorMessage)
                ? BadRequest(item.ErrorMessage)
                : Ok(item);
        }

        /// <summary>
        /// Get Item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An object of Item</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemInfoById(string id)
        {
            var item = await _itemService.GetItemById(id);
            if (item != null)
            {
                return Ok(item);
            };
            return NotFound();            
        }

        /// <summary>
        /// Active Item
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response when shop active an item</returns>
        [HttpPut("active")]
        public async Task<IActionResult> ActiveItem(ActiveItemReqModel request)
        {
            var item = await _itemService.ActiveItem(request);

            return !string.IsNullOrEmpty(item.ErrorMessage)
                ? BadRequest(item.ErrorMessage)
                : Ok(item);
        }
    }
}
