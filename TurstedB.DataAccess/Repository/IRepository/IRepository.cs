﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrustedB.Models;

namespace TrustedB.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        IEnumerable<T> GetAllPagination(int recSkip, int pageSize, Expression<Func<T, bool>>? filter, string? includeProperties = null);
        IEnumerable<T> GetAllPaginationAudio(int recSkip, int pageSize, Expression<Func<T, bool>>? filter, string? includeProperties = null);
        public IEnumerable<Topics> GetAllPaginationDB(int recSkip, int recTak, int subCategory, string? includeProperties = null);


    }
}
