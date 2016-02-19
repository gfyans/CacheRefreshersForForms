using System;
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
            FormStorage.Created += FormStorageOnCreated;
            FormStorage.Deleted += FormStorageOnDeleted;

            DataSourceStorage.Saved += DataSourceStorageOnSaved;
            DataSourceStorage.Created += DataSourceStorageOnCreated;
            DataSourceStorage.Deleted += DataSourceStorageOnDeleted;

            PrevalueSourceStorage.Saved += PrevalueSourceStorageOnSaved;
            PrevalueSourceStorage.Created += PrevalueSourceStorageOnCreated;
            PrevalueSourceStorage.Deleted += PrevalueSourceStorageOnDeleted;

            WorkflowStorage.Saved += WorkflowStorageOnSaved;
            WorkflowStorage.Created += WorkflowStorageOnCreated;
            WorkflowStorage.Deleted += WorkflowStorageOnDeleted;
        }

        #region Form

        private void FormStorageOnCreated(object sender, FormEventArgs formEventArgs)
        {
            DistributedCache.Instance.RefreshFormsCache(formEventArgs.Form);
        }

        private void FormStorageOnSaved(object sender, FormEventArgs formEventArgs)
        {
            DistributedCache.Instance.RefreshFormsCache(formEventArgs.Form);
        }

        private void FormStorageOnDeleted(object sender, FormEventArgs formEventArgs)
        {
            throw new NotImplementedException();
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

        private void WorkflowStorageOnDeleted(object sender, WorkflowEventArgs workflowEventArgs)
        {
            throw new NotImplementedException();
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

        private void PrevalueSourceStorageOnDeleted(object sender, FieldPreValueSourceEventArgs fieldPreValueSourceEventArgs)
        {
            throw new NotImplementedException();
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

        private void DataSourceStorageOnDeleted(object sender, FormDataSourceEventArgs formDataSourceEventArgs)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}