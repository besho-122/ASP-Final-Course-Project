using Besho.BLL.Services.Interfaces;
using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Besho.DAL.Models;
using Besho.DAL.Repositories.Classes;
using Besho.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Classes
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> Repository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            Repository = genericRepository;
        }

     

        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return Repository.Add(entity);

        
        }

        public int Delete(int id)
        {
            var entity = Repository.GetById(id);
            if (entity is null) return 0;
            return Repository.Remove(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive=false)
        {
            var entities = Repository.GetAll();
            if (onlyActive) {
                entities= entities.Where(e=>e.Status==Status.Active);   


            }
            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id)
        {
            var entity = Repository.GetById(id);
            return entity is null ? default: entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = Repository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            Repository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = Repository.GetById(id);
            if (entity is null) return 0;

           var updatedEntity =request.Adapt(entity);   
            return Repository.Update(updatedEntity);
        }
    }
}
