using System;
using System.Web.Script.Serialization;
using Umbraco.Core.Cache;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data;
using Umbraco.Forms.Data.Storage;

namespace CacheRefreshersForForms
{
    public class DataSourceCacheRefresher : PayloadCacheRefresherBase<DataSourceCacheRefresher>
    {
        public const string Id = "bff62a97-801b-4e87-8304-5d354c1e0f7c";
        readonly DataSourceStorage _fileStorage = new DataSourceStorage();

        protected override DataSourceCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<FormDataSource>(json);
        }

        public override void Refresh(object payload)
        {
            FormDataSource formDataSource = (FormDataSource)payload;

            _fileStorage.SaveToFile(formDataSource, formDataSource.Id);

            base.Refresh(payload);
        }
    }
}