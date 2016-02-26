using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
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
            return new JavaScriptSerializer().Deserialize<Workflow>(json);
        }

        public override void Refresh(object payload)
        {
            Workflow workflow = (Workflow)payload;

            workflow.Active = true;

            _fileStorage.SaveToFile(workflow, workflow.Id, "");
            
            using (FormStorage formStorage = new FormStorage())
            {
                Form form = formStorage.GetForm(workflow.Form);

                form.WorkflowIds.Add(workflow.Id);

                formStorage.UpdateForm(form, "");
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