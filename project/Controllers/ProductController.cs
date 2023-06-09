﻿using EfCoreProject.Context;
using EfCoreProject.Models;
using EfCoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Dtos.Product;
using RepositoryComponent.Page;

namespace EfCoreProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("PageList")]
        public async Task<PaginatedList<Product>> PageList(PaginatedOptions<PageProductDto> query)
        {
            return await _productService.PageList(query);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetList()
        {
            var products = await _productService.GetList(); 
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductDto product)
        {
           await _productService.Add(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productService.Update(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.Delete(product);           
            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return (await _productService.GetById(id)) != null;
        }
    }


}
