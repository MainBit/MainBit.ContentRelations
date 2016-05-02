using Orchard.ContentManagement.MetaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.ContentRelations.Settings
{
    public class ContentRelationFieldSettings
    {
        public string Hint { get; set; }
        public bool Required { get; set; }
        public bool Multiple { get; set; }
        public int ContentRelationRecord_Id { get; set; }
        public bool HideEditor { get; set; }
        public string DisplayedContentTypes { get; set; }
    }

    public static class ContentRelationFieldSettingsExtensions
    {
        public static ContentRelationFieldSettings GetContentRelationFieldSettings(this ContentPartFieldDefinition definition)
        {
            var settings = definition.Settings.GetModel<ContentRelationFieldSettings>();
            return settings;
        }
    }
}