using System;
using System.Web.Script.Serialization;
using Umbraco.Core.Cache;
using Umbraco.Forms.Core;
using Umbraco.Forms.Data.Storage;

namespace CacheRefreshersForForms
{
    public class FormsFieldPrevalueSourceCacheRefresher : PayloadCacheRefresherBase<FormsFieldPrevalueSourceCacheRefresher>
    {
        public const string Id = "669d3733-b3d6-4e77-995d-07268ca26cbc";

        readonly PrevalueSourceStorage _fileStorage = new PrevalueSourceStorage();

        protected override FormsFieldPrevalueSourceCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<FieldPreValueSource>(json);
        }

        public override void Refresh(object payload)
        {
            FieldPreValueSource fieldPreValueSource = (FieldPreValueSource)payload;

            _fileStorage.SaveToFile(fieldPreValueSource, fieldPreValueSource.Id);

            base.Refresh(payload);
        }
    }
}