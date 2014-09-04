using System.ComponentModel.Design;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Library.API.Mappers;
using Library.Data;
using Library.Data.Infrastructure;
using Library.Data.Repository;
using Library.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Library.API
{
    public static class Bootstrapper
    {
        public static void Configure()
        {
            ConfigureAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void ConfigureAutofacContainer()
        {
            var webApiContainerBuilder = new ContainerBuilder();
            ConfigureWebApiContainer(webApiContainerBuilder);
        }

        private static void ConfigureWebApiContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().AsImplementedInterfaces().InstancePerRequest();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerRequest();

            containerBuilder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerRequest();
            containerBuilder.RegisterType<BookAuthorRepository>().As<IBookAuthorRepository>().InstancePerRequest();
            containerBuilder.RegisterType<BookAmountRepository>().As<IBookAmountRepository>().InstancePerRequest();
            containerBuilder.RegisterType<BookQrCodeRepository>().As<IBookQrCodeRepository>().InstancePerRequest();
            containerBuilder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerRequest();
            containerBuilder.RegisterType<UserBookRepository>().As<IUserBookRepository>().InstancePerRequest();
            containerBuilder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>().InstancePerRequest();

            containerBuilder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerRequest();
            containerBuilder.RegisterType<BookAmountService>().As<IBookAmountService>().InstancePerRequest();
            containerBuilder.RegisterType<BookAuthorService>().As<IBookAuthorService>().InstancePerRequest();
            containerBuilder.RegisterType<BookQrCodeService>().As<IBookQrCodeService>().InstancePerRequest();
            containerBuilder.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            containerBuilder.RegisterType<UserBookService>().As<IUserBookService>().InstancePerRequest();
            containerBuilder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerRequest();

            containerBuilder.Register(c => new UserManager<IdentityUser>(new UserStore<IdentityUser>(new LibraryEntities())
            {
                /*Avoids UserStore invoking SaveChanges on every actions.*/
                //AutoSaveChanges = false
            })).As<UserManager<IdentityUser>>().InstancePerRequest();

            containerBuilder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());
            IContainer container = containerBuilder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

    }
}