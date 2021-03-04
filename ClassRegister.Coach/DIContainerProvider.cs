using System;
using Unity;
using Unity.Injection;

namespace ClassRegister.CoachApp
{
    public class DIContainerProvider
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IIoHelper, IoHelper>();

            return container;
        }
    }
}



