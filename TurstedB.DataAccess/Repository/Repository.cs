﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustedB.DataAccess.Repository.IRepository;

using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using TrustedB.DataAccess.Data;
using TrustedB.Models;

namespace TrustedB.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet
            //_db.Products.Include(u => u.Country).Include(u => u.CountryId);
            
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked) {
                 query= dbSet;
                
            }
            else {
                 query = dbSet.AsNoTracking();
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties)) {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) {
                query = query.Where(filter);
            }
			if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        //public IEnumerable<T> GetAllPagination(int recSkip, int recTak, Expression<Func<T, bool>>? filter, string? includeProperties = null)
        //{

        //    IQueryable<T> query = dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Skip(recSkip).Take(recTak).Where(filter);
        //    }

        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var includeProp in includeProperties
        //            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProp);
        //        }
        //    }

        //    return query.ToList();

        //}

        public IEnumerable<T> GetAllPagination(int recSkip, int recTak, Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {

            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //query = query.Where(filter);
                query = query.Where(filter).Skip(recSkip).Take(recTak);
            }
            else
            {
                query = query.Skip(recSkip).Take(recTak);
            }
            //query = query.Skip(recSkip).Take(recTak);


            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();

        }


        public IEnumerable<T> GetAllPaginationAudio(int recSkip, int recTak, Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {

            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);

            }
            query = query.Skip(recSkip).Take(recTak);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();

        }
        //___________________________________________________________
        public IEnumerable<Topics> GetAllPaginationDB(int recSkip, int recTak, int subCategory, string? includeProperties = null)
        {
           
            var query = _db.Topics.Where(u => (u.SubCategoryID == subCategory)&&(u.Active == "نعم")).OrderByDescending(u => u.CreationDate).Skip(recSkip).Take(recTak);
  
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();

        }
//___________________________________________________
    }
}
