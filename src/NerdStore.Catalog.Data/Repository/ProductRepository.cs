﻿using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int code)
        {
            return await _context.Products.AsNoTracking().Include(x => x.Category).Where(y => y.Category.Code == code).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
