using System.Collections.Generic;
using System.Globalization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace MainBit.ContentRelations.Settings
{
    public class ContentRelationFieldEditorEvents : ContentDefinitionEditorEventsBase
    {

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "ContentRelationField")
            {
                var model = definition.GetContentRelationFieldSettings();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "ContentRelationField")
            {
                yield break;
            }

            var model = new ContentRelationFieldSettings();
            if (updateModel.TryUpdateModel(model, "ContentRelationFieldSettings", null, null))
            {
                builder.WithSetting("ContentRelationFieldSettings.Hint", model.Hint);
                builder.WithSetting("ContentRelationFieldSettings.Required", model.Required.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentRelationFieldSettings.Multiple", model.Multiple.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentRelationFieldSettings.ContentRelationRecord_Id", model.ContentRelationRecord_Id.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentRelationFieldSettings.HideEditor", model.HideEditor.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentRelationFieldSettings.DisplayedContentTypes", model.DisplayedContentTypes);
            }

            yield return DefinitionTemplate(model);
        }
    }
}