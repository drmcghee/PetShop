using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Diagnostics.Management;

namespace PetShopWebRole
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            String wadConnectionString = "Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString";

            CloudStorageAccount cloudStorageAccount=null;
            try
            {
                cloudStorageAccount =
                          CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue
                          (wadConnectionString));
            }
            catch (Exception exc)
            {
                // todo: deal with the already exists problem better
            }

            RoleInstanceDiagnosticManager roleInstanceDiagnosticManager =
                     cloudStorageAccount.CreateRoleInstanceDiagnosticManager(
                     RoleEnvironment.DeploymentId,
                     RoleEnvironment.CurrentRoleInstance.Role.Name,
                     RoleEnvironment.CurrentRoleInstance.Id);
            DiagnosticMonitorConfiguration diagnosticMonitorConfiguration =
                roleInstanceDiagnosticManager.GetCurrentConfiguration();

            PerformanceCounterConfiguration performanceCounterConfiguration = new
                PerformanceCounterConfiguration();
             performanceCounterConfiguration.CounterSpecifier =
                @"\Processor(_Total)\% Processor Time";
             performanceCounterConfiguration.SampleRate =
                System.TimeSpan.FromSeconds(10d);
             diagnosticMonitorConfiguration.PerformanceCounters.DataSources.Add
                (performanceCounterConfiguration);
             diagnosticMonitorConfiguration.PerformanceCounters.ScheduledTransferPeriod =
                TimeSpan.FromMinutes(1d);
 
            roleInstanceDiagnosticManager.SetCurrentConfiguration
                   (diagnosticMonitorConfiguration);

            return base.OnStart();






        }
    }
}
