using Autofac;
using EBook.Services.Books;

namespace EBook.Services.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册
            builder.RegisterType<BookService>().As<IBookService>();
        }
    }
}
