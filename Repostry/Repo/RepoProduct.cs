using Base;
using Base.Models;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostry.Repo
{
    public class RepoProduct : BaseRepostry<Product>, IRepoProduct
    {
        private readonly HundredPlusDbContext _context;
        public RepoProduct(HundredPlusDbContext context) : base(context)
        {
            context = _context;
        }
        public async Task< Product> AddProduct(ProductDtoModel productDto)
        {
            Product product = new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description ??""
            };
            var  result =await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();  

            return product;
        }
        public async Task<Product> UpdateProduct(ProductDtoUpAndDelt productDto)
        {
          var product = await _context.Products.Where(p =>p.Id==productDto.Id).SingleOrDefaultAsync();
            if (product != null) 
            {
                if (product != null)
                {
                    if (productDto.Price != null)
                    {
                        product.Price = productDto.Price;
                    }
                    if (productDto.Description != null)
                    {
                        product.Description = productDto.Description;
                    }
                    if (productDto.Name != null)
                    {
                        product.Name = productDto.Name;
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
            }

            return product;
        }
        public  bool DeletProduct(int Id)
        {
            var product =  _context.Products.Where(p => p.Id ==Id).SingleOrDefault();
            if (product != null)
            {
                Product newPrpduct = new Product()
                {
                    Id = Id,
                    IsDeleted = true,
                    DatetimeDeleted = DateTime.Now,
                };
                _context.Update(product);
                 _context.SaveChanges();
                return true;

            }

            return false;
        }
    }
}
