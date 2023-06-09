﻿using AeC.Datas.Data;
using AeC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AeC.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly AeCAPIContext _context;

        public RepositoryBase(AeCAPIContext context)
        {
            _context = context;
        }

        public async Task Delete(T obj)
        {
            _context.Entry(obj).State = EntityState.Deleted;
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Save(T obj)
        {
            _context.Set<T>().Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Set<T>().Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
