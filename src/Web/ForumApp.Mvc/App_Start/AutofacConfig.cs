﻿namespace ForumApp.Mvc
{
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using ForumApp.Data;
    using ForumApp.Data.Repositories;
    using ForumApp.Services.Forum;

    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Register context
            builder.RegisterTypes(typeof(ForumAppDbContext)).As(typeof(IForumAppDbContext)).InstancePerRequest();

            // Register repositories
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            // Register all services
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IForumService)))
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}