using Autofac;
using EBook.Core.Data;
using EBook.Core.Infrastructure;
using EBook.Core.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;

namespace EBook.Core.Autofac
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<DbConnection>(x =>
            {
                var connectionString = AppSettingConfig.EBookConnectionString;

                var connection = new SqlConnection(connectionString);

                connection.Open();

                return connection;
            })
            .AsSelf()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

            builder.RegisterType<EBookDbContext>().AsSelf().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
