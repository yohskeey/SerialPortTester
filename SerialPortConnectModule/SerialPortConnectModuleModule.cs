using SerialPortConnectModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace SerialPortConnectModule
{
    public class SerialPortConnectModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}