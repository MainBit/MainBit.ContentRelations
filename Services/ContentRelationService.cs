using MainBit.ContentRelations.Models;
using Orchard;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.ContentRelations.Services
{
    public interface IContentRelationService : IDependency
    {
        List<ContentRelationRecord> Get();
        ContentRelationRecord Get(int id);
        void Create(ContentRelationRecord model);
        void Update(ContentRelationRecord model);
        void Delete(int id);
    }

    public class ContentRelationService : IContentRelationService
    {
        private readonly IRepository<ContentRelationRecord> _contentRelationRepository;

        public ContentRelationService(
            IRepository<ContentRelationRecord> contentRelationRepository)
        {
            _contentRelationRepository = contentRelationRepository;
        }

        public List<ContentRelationRecord> Get()
        {
            return _contentRelationRepository.Table.ToList();
        }

        public ContentRelationRecord Get(int id)
        {
            return _contentRelationRepository.Get(id);
        }
            
        public void Create(ContentRelationRecord model) {
            _contentRelationRepository.Create(model);
        }

        public void Update(ContentRelationRecord model)
        {
            _contentRelationRepository.Update(model);
        }

        public void Delete(int id)
        {
            _contentRelationRepository.Delete(Get(id));
        }
    }
}