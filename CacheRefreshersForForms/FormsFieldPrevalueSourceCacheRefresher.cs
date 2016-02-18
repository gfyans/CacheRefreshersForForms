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
    public class FormsFieldPrevalueSourceCacheRefresher : PayloadCacheRefresherBase<FormsFieldPrevalueSourceCacheRefresher>
    {
        public const string Id = "669d3733-b3d6-4e77-995d-07268ca26cbc";
        readonly BaseFileStorage<FieldPreValueSource> _fileStorage = new PrevalueSourceStorage();

        protected override FormsFieldPrevalueSourceCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(object payload)
        {
            FieldPreValueSource fieldPreValueSource = (FieldPreValueSource)payload;

            string path = this.Find(fieldPreValueSource.Id.ToString());

            if (path != null)
            {
                _fileStorage.SaveToSpecificPath(fieldPreValueSource, fieldPreValueSource.Id.ToString(), path);
            }
            else
            {
                _fileStorage.SaveToFile(fieldPreValueSource, fieldPreValueSource.Id, this.RootFolderPath());
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