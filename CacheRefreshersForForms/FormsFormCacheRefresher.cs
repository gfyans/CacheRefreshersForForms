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
    public class FormsFormCacheRefresher : PayloadCacheRefresherBase<FormsFormCacheRefresher>
    {
        public const string Id = "74318b85-f97d-49af-ba15-caf9e0ba4d5a";
        readonly BaseFileStorage<Form> _fileStorage = new FormStorage();

        protected override FormsFormCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(object payload)
        {
            Form form = (Form)payload;

            string path = this.Find(form.Id.ToString());

            if (path != null)
            {
                _fileStorage.SaveToSpecificPath(form, form.Id.ToString(), path);
            }
            else
            {
                _fileStorage.SaveToFile(form, form.Id, this.RootFolderPath());
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