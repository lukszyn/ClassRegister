using ClassRegister.DataLayer;
using System;
using Unity;
using Unity.Injection;

namespace ClassRegister.WebApi
{
    public class DIContainerProvider
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<Func<IClassRegisterDbContext>>(
                new InjectionFactory(ctx => new Func<IClassRegisterDbContext>(() => new ClassRegisterDbContext())));

            return container;
        }
    }
}



