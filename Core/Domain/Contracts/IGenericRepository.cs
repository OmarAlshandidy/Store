﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity , Tkey>  where TEntity :BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,Tkey> spec,bool trackChanges = false);
        Task<TEntity?> GetAsync(ISpecifications<TEntity, Tkey> spec);
        Task<TEntity?> GetAsync(Tkey id);

        Task AddAsync(TEntity entity);
         void Update(TEntity entity);   
         void Delete(TEntity entity);
    }
}
