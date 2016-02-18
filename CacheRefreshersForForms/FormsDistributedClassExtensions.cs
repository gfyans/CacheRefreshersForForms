using System;
using Umbraco.Forms.Core;
using Umbraco.Web.Cache;

namespace CacheRefreshersForForms
{
    public static class FormsDistributedClassExtensions
    {
        /// <summary>
        /// Refreshes the cache amongst servers for Forms
        /// </summary>
        /// <param name="dc">The DistributedCache.</param>
        /// <param name="form">The form.</param>
        public static void RefreshFormsCache(this DistributedCache dc, Form form)
        {
            dc.RefreshByPayload(new Guid(FormsFormCacheRefresher.Id), form);
        }

        /// <summary>
        /// Refreshes the cache amongst servers for Forms
        /// </summary>
        /// <param name="dc">The DistributedCache.</param>
        /// <param name="workflow">The workflow.</param>
        public static void RefreshFormsWorkflowCache(this DistributedCache dc, Workflow workflow)
        {
            dc.RefreshByPayload(new Guid(FormsWorkflowCacheRefresher.Id), workflow);
        }

        /// <summary>
        /// Refreshes the cache amongst servers for Forms
        /// </summary>
        /// <param name="dc">The DistributedCache.</param>
        /// <param name="fieldPreValueSource">The field pre value source.</param>
        public static void RefreshFormsFieldPrevalueSourceCache(this DistributedCache dc, FieldPreValueSource fieldPreValueSource)
        {
            dc.RefreshByPayload(new Guid(FormsFieldPrevalueSourceCacheRefresher.Id), fieldPreValueSource);
        }

        /// <summary>
        /// Refreshes the cache amongst servers for Forms
        /// </summary>
        /// <param name="dc">The DistributedCache.</param>
        /// <param name="dataSource">The data source.</param>
        public static void RefreshFormsDataSourceCache(this DistributedCache dc, FormDataSource dataSource)
        {
            dc.RefreshByPayload(new Guid(DataSourceCacheRefresher.Id), dataSource);
        }
    }
}