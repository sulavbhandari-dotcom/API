using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Mappers;
using API.Dtos.Stock;
using API.Interfaces;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StocksController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StocksController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        // GET: Stocks
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepo.GetALlAsync();
            var StockDto =  stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }


        // GET: Stocks/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
            
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id } , stockModel);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if(stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
