using Microsoft.Win32;
using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;

namespace Common
{
    public static class WindowsServiceHelper
    {
        #region 封装Window服务

        /// <summary>  
        /// 安装服务  
        /// </summary>  
        /// <param name="NameService">Windows服务显示名称</param>  
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool InstallService(string serviceName)
        {
            bool flag = true;
            if (!IsServiceIsExisted(serviceName))
            {
                try
                {

                    string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string assemblyDirPath = Path.GetDirectoryName(location);
                    string serviceFileName = $" {assemblyDirPath}\\{serviceName}.exe";
                    InstallMyService(null, serviceFileName);
                }
                catch
                {
                    flag = false;
                }

            }
            return flag;
        }

        /// <summary>  
        /// 卸载服务  
        /// </summary>  
        /// <param name="NameService">Windows服务显示名称</param>  
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool UninstallService(string serviceName)
        {
            bool flag = true;
            if (IsServiceIsExisted(serviceName))
            {
                try
                {
                    string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string assemblyDirPath = Path.GetDirectoryName(location);
                    string serviceFileName = $" {assemblyDirPath}\\{serviceName}.exe";
                    UnInstallMyService(serviceFileName);
                }
                catch
                {
                    flag = false;
                }
            }
            return flag;
        }


        /// <summary>  
        /// 检查Windows服务是否存在  
        /// </summary>  
        /// <param name="NameService">Windows服务显示名称</param>  
        /// <returns>存在返回 true,否则返回 false;</returns>  
        public static bool IsServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>  
        /// 安装Windows服务  
        /// </summary>  
        /// <param name="stateSaver">集合</param>  
        /// <param name="filepath">Windows服务程序文件路径</param>  
        private static void InstallMyService(IDictionary stateSaver, string filePath)
        {
            AssemblyInstaller installer = new AssemblyInstaller
            {
                UseNewContext = true,
                Path = filePath
            };
            installer.Install(stateSaver);
            installer.Commit(stateSaver);
            installer.Dispose();
        }

        /// <summary>
        /// 卸载Windows服务  
        /// </summary>
        /// <param name="filePath">Windows服务程序文件路径</param>
        private static void UnInstallMyService(string filePath)
        {
            AssemblyInstaller installer = new AssemblyInstaller
            {
                UseNewContext = true,
                Path = filePath
            };
            installer.Uninstall(null);
            installer.Dispose();
        }

        /// <summary>
        /// 判断某个Windows服务是否启动  
        /// </summary>
        /// <param name="serviceName">Windows服务显示名称</param>
        /// <returns>bool</returns>
        public static bool IsServiceStart(string serviceName)
        {
            ServiceController psc = new ServiceController(serviceName);
            bool bStartStatus = false;
            try
            {
                if (!psc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    bStartStatus = true;
                }

                return bStartStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>    
        /// 修改服务的启动项 2为自动,3为手动    
        /// </summary>    
        /// <param name="startType">2为自动,3为手动</param>    
        /// <param name="serviceName">Windows服务显示名称</param>    
        /// <returns>bool</returns>    
        public static bool ChangeServiceStartType(int startType, string serviceName)
        {
            try
            {
                RegistryKey regist = Registry.LocalMachine;
                RegistryKey sysReg = regist.OpenSubKey("SYSTEM");
                RegistryKey currentControlSet = sysReg.OpenSubKey("CurrentControlSet");
                RegistryKey services = currentControlSet.OpenSubKey("Services");
                RegistryKey servicesName = services.OpenSubKey(serviceName, true);
                servicesName.SetValue("Start", startType);
            }
            catch (Exception)
            {
                return false;
            }
            return true;


        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName">Windows服务显示名称</param>
        /// <returns>bool</returns>
        public static bool StartService(string serviceName)
        {
            bool flag = true;
            if (IsServiceIsExisted(serviceName))
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running && service.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                {
                    service.Start();
                    for (int i = 0; i < 60; i++)
                    {
                        service.Refresh();
                        System.Threading.Thread.Sleep(1000);
                        if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                        {
                            break;
                        }
                        if (i == 59)
                        {
                            flag = false;
                        }
                    }
                }
            }
            else
            {
                flag = false;
                throw new Exception("Service is not installed.");
            }
            return flag;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName">Windows服务显示名称</param>
        /// <returns>bool</returns>
        public static bool StopService(string serviceName)
        {
            bool flag = true;
            if (IsServiceIsExisted(serviceName))
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                if (service.Status == System.ServiceProcess.ServiceControllerStatus.Running)
                {
                    service.Stop();
                    for (int i = 0; i < 60; i++)
                    {
                        service.Refresh();
                        System.Threading.Thread.Sleep(1000);
                        if (service.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
                        {
                            break;
                        }
                        if (i == 59)
                        {
                            flag = false;
                        }
                    }
                }
            }
            else
            {
                flag = false;
                throw new Exception("Service is not installed.");
            }
            return flag;
        }
        /// <summary>
        /// 重启Windows服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool RestartService(string serviceName)
        {
            if (StopService(serviceName))
            {
                return StartService(serviceName);
            }
            return false;
        }
        #endregion
    }
}

