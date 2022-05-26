using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using static IRepository.IGenericRepository;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> Complete();
        int SaveChanges();
    }
}