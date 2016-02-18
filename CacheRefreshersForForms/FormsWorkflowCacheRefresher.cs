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
    public class FormsWorkflowCacheRefresher : PayloadCacheRefresherBase<FormsWorkflowCacheRefresher>
    {
        public const string Id = "61596c60-e2eb-4ea8-8282-5de564a395b8";
        readonly BaseFileStorage<Workflow> _fileStorage = new WorkflowStorage();

        protected override FormsWorkflowCacheRefresher Instance => this;
        public override Guid UniqueIdentifier => new Guid(Id);
        public override string Name => "Umbraco Forms cache refresher";

        protected override object Deserialize(string json)
        {
            throw new NotImplementedException();
        }

        public override void Refresh(object payload)
        {
            Workflow workflow = (Workflow)payload;

            string path = this.Find(workflow.Id.ToString());

            if (path != null)
            {
                _fileStorage.SaveToSpecificPath(workflow, workflow.Id.ToString(), path);
            }
            else
            {
                _fileStorage.SaveToFile(workflow, workflow.Id, this.RootFolderPath());
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