using System;
using Unity;
using Unity.Injection;

namespace ClassRegister.Coach
{
    public class DIContainerProvider
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}



