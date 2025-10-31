﻿using AutoMapper;
using e_commerce_project.DTOs;
using e_commerce_project.Modles;
using e_commerce_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace e_commerce_project.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        const int maxPageSize = 40;
        private readonly IProducts context;
        public ProductsController(IProducts _context)
        {
            context = _context;
        }


        [HttpGet]
        public async Task<IActionResult> Get_All_Products(string? name, string? description, int pagenumber=1, int pagesize=20)
        {
            if (pagesize > maxPageSize)
                pagesize = maxPageSize;

            var pros = await context.Get_All_Products(name, description, pagenumber, pagesize);
            return Ok(pros);

        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get_Product_By_Id(int Id)
        {
            try
            {
                var pros = await context.Get_Product_By_Id(Id);
                return Ok(pros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       [HttpGet("{productId:int}/skus/{skuId:int}")]
        public async Task<IActionResult> GetSkuForProduct(int productId, int skuId)
        {
            try
            {
                ProductSkuDTO sku = await context.GetSkuByProductAsync(productId, skuId);
                return Ok(sku);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create_New_Product(CreateProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await context.Create_New_Product(product);
            return Ok("Product added successfully!");
        }

        [HttpPut("{Id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Product_By_Id(int Id, UpdateProductDTO UPro)
        {
            try
            {
                await context.Update_Product_By_Id(Id, UPro);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpDelete("{Id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Product_By_Id(int Id)
        {
            try
            {
                await context.Delete_Product_By_Id(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
