using ClassRegister.BusinessLayer.Services;
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

            container.RegisterType<ICoursesService, CoursesService>();
            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<ICoachService, CoachService>();
            container.RegisterType<Func<IClassRegisterDbContext>>(
                new InjectionFactory(ctx => new Func<IClassRegisterDbContext>(() => new ClassRegisterDbContext())));

            return container;
        }
    }
}



