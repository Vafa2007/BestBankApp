using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using BestBankApp.Repository.Context;

namespace BestBankApp.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _entity;

        public Repository(AppDbContext context)
        {
            _dbContext = context;
            _entity = _dbContext.Set<TEntity>();
        }

        public IDbContextTransaction BeginTransaction()
        {
           return _dbContext.Database.BeginTransaction();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
        //public IDbContextTransaction UseTransaction(IDbContextTransaction transaction)
        //{
        //    return _dbContext.Database.UseTransaction(transaction.GetDbTransaction());
        //}
        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }
        public void RollBackTransaction(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }
        public IEnumerable<TEntity> All
        {
            get { return _entity; }
        }
        public IQueryable<TEntity> AllQuery
        {
            get { return _entity; }
        }
        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _entity.ToListAsync();
        }

        public void Insert(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return ((IQueryable<TEntity>)_entity).Where(predicate);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }        
    }
}
