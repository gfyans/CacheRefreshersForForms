using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Umbraco.Core.Cache;
using Umbraco.Core.IO;
using Umbraco.Forms.Core;
using Umbraco.Forms.Data.Storage;

namespace CacheRefreshersForForms
{
    public class FormsFormCacheRefresher : PayloadCacheRefresherBase<FormsFormCacheRefresher>
    {
        public const string Id = "74318b85-f97d-49af-ba15-caf9e0ba4d5a";
        readonly FormStorage _formStorage = new FormStorage();

        protected override FormsFormCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            return new JavaScriptSerializer().Deserialize<Form>(json);
        }

        public override void Refresh(object payload)
        {
            Form form = (Form)payload;

            string path = Find(form.Id.ToString(), _formStorage);

            if (path != null)
            {
                _formStorage.SaveToSpecificPath(form, form.Id.ToString(), path);
            }
            else
            {
                _formStorage.SaveToFile(form, form.Id, RootFolderPath(_formStorage));
            }

            base.Refresh(payload);
        }

        public static string Find(string id, FormStorage fileStorage)
        {
            return Directory.GetFiles(RootFolderPath(fileStorage), id + "." + fileStorage.Extension, SearchOption.AllDirectories).FirstOrDefault();
        }

        public static string RootFolderPath(FormStorage fileStorage)
        {
            return IOHelper.MapPath(Path.Combine(fileStorage.Path, fileStorage.Folder));
        }
    }
}