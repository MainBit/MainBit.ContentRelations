using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MainBit.ContentRelations.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;

namespace MainBit.ContentRelations.Services
{
    public interface IContentRelationItemService : IDependency
    {
        IEnumerable<ContentItem> Get(int contentItemId, int contentRelationRecordId);
        bool Update(int contentItemId, int[] relContentItemId, int contentRelationRecordId);
    }

    public class ContentRelationItemService : IContentRelationItemService
    {
        private readonly IRepository<ContentRelationItemRecord> _contentRelationRepository;
        private readonly IContentManager _contentManager;

        public ContentRelationItemService(
            IRepository<ContentRelationItemRecord> contentRelationRepository,
            IContentManager contentManager)
        {
            _contentRelationRepository = contentRelationRepository;
            _contentManager = contentManager;
        }

        public IEnumerable<ContentItem> Get(int contentItemId, int contentRelationRecordId) {
            var contentItemIds = _contentRelationRepository
                .Fetch(r => r.ContentRelationRecord_Id == contentRelationRecordId 
                    && (r.ContentItemRecord1_Id == contentItemId || r.ContentItemRecord2_Id == contentItemId))
                .Select(p => p.ContentItemRecord1_Id == contentItemId ? p.ContentItemRecord2_Id : p.ContentItemRecord1_Id);
            return _contentManager.GetMany<ContentItem>(contentItemIds, VersionOptions.Published, QueryHints.Empty);
        }


        public bool Update(int contentItemId, int[] relContentItemIds, int contentRelationRecordId) {
            var contentRelations = _contentRelationRepository
                .Fetch(r => r.ContentRelationRecord_Id == contentRelationRecordId
                            && (r.ContentItemRecord1_Id == contentItemId || r.ContentItemRecord2_Id == contentItemId)).ToList();

            for (var i = 0; i < contentRelations.Count(); i++ ) {
                var contentRelationItemRecord = contentRelations[i];
                var relContentItemId = contentRelationItemRecord.ContentItemRecord1_Id == contentItemId ?
                    contentRelationItemRecord.ContentItemRecord2_Id : contentRelationItemRecord.ContentItemRecord1_Id;

                if (!relContentItemIds.Any(r => r == relContentItemId)) {
                    _contentRelationRepository.Delete(contentRelationItemRecord);
                    contentRelations.Remove(contentRelationItemRecord);
                    i--;
                }
            }

            for(var i = 0; i < relContentItemIds.Count(); i++) {
                var relContentItemId = relContentItemIds[i];
                var contentRelationItemRecord = contentRelations.FirstOrDefault(r =>
                    r.ContentItemRecord1_Id == relContentItemId || r.ContentItemRecord2_Id == relContentItemId);
                if(contentRelationItemRecord == null) {
                    contentRelationItemRecord = new ContentRelationItemRecord() {
                        ContentItemRecord1_Id = contentItemId,
                        ContentItemRecord2_Id = relContentItemId,
                        ContentRelationRecord_Id = contentRelationRecordId
                    };
                    _contentRelationRepository.Create(contentRelationItemRecord);
                }
            }

            return true;
        }
    }
}