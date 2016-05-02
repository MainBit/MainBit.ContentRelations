using System.Linq;
using MainBit.ContentRelations.Services;
using MainBit.ContentRelations.Settings;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using MainBit.ContentRelations.Fields;

namespace MainBit.ContentRelations.Handlers
{
    public class ContentRelationFieldHandler : ContentHandler {
        private readonly IContentRelationItemService _contentRelationsService;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ContentRelationFieldHandler(
            IContentRelationItemService contentRelationsService, 
            IContentDefinitionManager contentDefinitionManager) {

            _contentRelationsService = contentRelationsService;
            _contentDefinitionManager = contentDefinitionManager;
        }

        protected override void Loading(LoadContentContext context) {
            base.Loading(context);

            var fields = context.ContentItem.Parts.SelectMany(x => x.Fields.Where(f => f.FieldDefinition.Name == typeof(ContentRelationField).Name)).Cast<ContentRelationField>();
            
            // define lazy initializer for ContentRelation.ContentItems
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null) {
                return;
            }
            
            foreach (var field in fields) {
                var settings = field.PartFieldDefinition.Settings.GetModel<ContentRelationFieldSettings>();
                field._contentItems.Loader(x => _contentRelationsService.Get(context.ContentItemRecord.Id, settings.ContentRelationRecord_Id));
            }
        }
    }
}