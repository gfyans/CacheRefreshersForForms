using System;
using System.IO;
using System.Linq;
using Umbraco.Core.Cache;
using Umbraco.Core.IO;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Data;
using Umbraco.Forms.Data.Storage;

namespace CacheRefreshersForForms
{
    public class DataSourceCacheRefresher : PayloadCacheRefresherBase<DataSourceCacheRefresher>
    {
        public const string Id = "bff62a97-801b-4e87-8304-5d354c1e0f7c";
        readonly BaseFileStorage<FormDataSource> _fileStorage = new DataSourceStorage();

        protected override DataSourceCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(object payload)
        {
            FormDataSource formDataSource = (FormDataSource)payload;

            string path = this.Find(formDataSource.Id.ToString());

            if (path != null)
            {
                _fileStorage.SaveToSpecificPath(formDataSource, formDataSource.Id.ToString(), path);
            }
            else
            {
                _fileStorage.SaveToFile(formDataSource, formDataSource.Id, this.RootFolderPath());
            }

            base.Refresh(payload);
        }

        public string Find(string id)
        {
            return Directory.GetFiles(this.RootFolderPath(), id + "." + _fileStorage.Extension, SearchOption.AllDirectories).FirstOrDefault();
        }

        public string RootFolderPath()
        {
            return IOHelper.MapPath(Path.Combine(_fileStorage.Path, _fileStorage.Folder));
        }
    }
}