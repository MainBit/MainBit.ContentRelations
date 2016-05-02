using System;
using System.Collections.Generic;
using System.Linq;
using MainBit.ContentRelations.Fields;
using MainBit.ContentRelations.Settings;
using MainBit.ContentRelations.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using Orchard.Utility.Extensions;
using MainBit.ContentRelations.Services;

namespace MainBit.ContentRelations.Drivers
{
    public class ContentRelationFieldDriver : ContentFieldDriver<ContentRelationField>
    {
        private readonly IContentRelationItemService _contentRelationsService;

        // EditorTemplates/Fields/ContentRelation.cshtml
        //private const string TemplateName = "Fields/ContentRelation";

        public ContentRelationFieldDriver(IContentRelationItemService contentRelationsService)
        {
            _contentRelationsService = contentRelationsService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        private static string GetPrefix(ContentRelationField field, ContentPart part)
        {
            return part.PartDefinition.Name + "." + field.Name;
        }

        private static string GetDifferentiator(ContentRelationField field, ContentPart part)
        {
            return field.Name;
        }

        protected override DriverResult Display(ContentPart part, ContentRelationField field, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Fields_ContentRelation", GetDifferentiator(field, part), () => shapeHelper.Fields_ContentRelation()),
                ContentShape("Fields_ContentRelation_SummaryAdmin", GetDifferentiator(field, part), () => shapeHelper.Fields_ContentRelation_SummaryAdmin())
            );
        }

        protected override DriverResult Editor(ContentPart part, ContentRelationField field, dynamic shapeHelper)
        {
            var settings = field.PartFieldDefinition.GetContentRelationFieldSettings();
            if (settings.HideEditor) { return null; }


            var model = new ContentRelationFieldViewModel
            {
                Field = field,
                Part = part,
                ContentItems = _contentRelationsService.Get(part.ContentItem.Id, settings.ContentRelationRecord_Id).ToList(),
            };
            model.SelectedIds = string.Concat(",", model.ContentItems.Select(r => r.Id));

            return ContentShape("Fields_ContentRelation_Edit", GetDifferentiator(field, part),
                () => shapeHelper.EditorTemplate(TemplateName: "Fields/ContentRelation", Model: model, Prefix: GetPrefix(field, part)));
                    
        }

        protected override DriverResult Editor(ContentPart part, ContentRelationField field, IUpdateModel updater, dynamic shapeHelper)
        {
            var settings = field.PartFieldDefinition.GetContentRelationFieldSettings();
            if (settings.HideEditor) { return null; }

            var model = new ContentRelationFieldViewModel();
            updater.TryUpdateModel(model, GetPrefix(field, part), null, null);

            

            int[] ids;
            if (String.IsNullOrEmpty(model.SelectedIds))
            {
                ids = new int[0];
            }
            else
            {
                ids = model.SelectedIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }


            if (settings.Required && ids.Length == 0)
            {
                updater.AddModelError("Id", T("The field {0} is mandatory", field.Name.CamelFriendly()));
            }
            else {
                _contentRelationsService.Update(part.ContentItem.Id, ids, settings.ContentRelationRecord_Id);
            }

            return Editor(part, field, shapeHelper);
        }
    }
}