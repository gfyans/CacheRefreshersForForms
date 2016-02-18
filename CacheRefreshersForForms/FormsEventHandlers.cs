using Umbraco.Core;
using Umbraco.Forms.Core;
using Umbraco.Forms.Data.Storage;
using Umbraco.Web.Cache;

namespace CacheRefreshersForForms
{
    public class FormsEventHandlers : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            FormStorage.Saved += FormStorageOnSaved;
            DataSourceStorage.Saved += DataSourceStorageOnSaved;
            PrevalueSourceStorage.Saved += PrevalueSourceStorageOnSaved;
            WorkflowStorage.Saved += WorkflowStorageOnSaved;

            FormStorage.Created += FormStorageOnSaved;
            DataSourceStorage.Created += DataSourceStorageOnSaved;
            PrevalueSourceStorage.Created += PrevalueSourceStorageOnSaved;
            WorkflowStorage.Created += WorkflowStorageOnSaved;
        }

        private void WorkflowStorageOnSaved(object sender, WorkflowEventArgs workflowEventArgs)
        {
            DistributedCache.Instance.RefreshFormsWorkflowCache(workflowEventArgs.Workflow);
        }

        private void PrevalueSourceStorageOnSaved(object sender, FieldPreValueSourceEventArgs fieldPreValueSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsFieldPrevalueSourceCache(fieldPreValueSourceEventArgs.FieldPreValueSource);
        }

        private void DataSourceStorageOnSaved(object sender, FormDataSourceEventArgs formDataSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsDataSourceCache(formDataSourceEventArgs.FormDataSource);
        }

        private void FormStorageOnSaved(object sender, FormEventArgs formEventArgs)
        {
            DistributedCache.Instance.RefreshFormsCache(formEventArgs.Form);
        }
    }
}