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
            DataSourceStorage.Created += DataSourceStorageOnCreated;

            PrevalueSourceStorage.Saved += PrevalueSourceStorageOnSaved;
            PrevalueSourceStorage.Created += PrevalueSourceStorageOnCreated;

            WorkflowStorage.Saved += WorkflowStorageOnSaved;
            WorkflowStorage.Created += WorkflowStorageOnCreated;
        }

        #region Form
        
        private void FormStorageOnSaved(object sender, FormEventArgs formEventArgs)
        {
            DistributedCache.Instance.RefreshFormsCache(formEventArgs.Form);
        }

        #endregion

        #region Workflow

        private void WorkflowStorageOnCreated(object sender, WorkflowEventArgs workflowEventArgs)
        {
            DistributedCache.Instance.RefreshFormsWorkflowCache(workflowEventArgs.Workflow);
        }

        private void WorkflowStorageOnSaved(object sender, WorkflowEventArgs workflowEventArgs)
        {
            DistributedCache.Instance.RefreshFormsWorkflowCache(workflowEventArgs.Workflow);
        }

        #endregion

        #region PrevalueSource

        private void PrevalueSourceStorageOnCreated(object sender, FieldPreValueSourceEventArgs fieldPreValueSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsFieldPrevalueSourceCache(fieldPreValueSourceEventArgs.FieldPreValueSource);
        }

        private void PrevalueSourceStorageOnSaved(object sender, FieldPreValueSourceEventArgs fieldPreValueSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsFieldPrevalueSourceCache(fieldPreValueSourceEventArgs.FieldPreValueSource);
        }
        
        #endregion

        #region DataSource

        private void DataSourceStorageOnCreated(object sender, FormDataSourceEventArgs formDataSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsDataSourceCache(formDataSourceEventArgs.FormDataSource);
        }

        private void DataSourceStorageOnSaved(object sender, FormDataSourceEventArgs formDataSourceEventArgs)
        {
            DistributedCache.Instance.RefreshFormsDataSourceCache(formDataSourceEventArgs.FormDataSource);
        }
        
        #endregion
    }
}