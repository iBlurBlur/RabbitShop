using Application.Commom.Interfaces;
using Application.Features.Products;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace Infrastructure;

public class DefaultInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
          .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
          .Where(t => t.Name.EndsWith("API"))
          .AsImplementedInterfaces()
          .InstancePerLifetimeScope();

        builder.RegisterType<ProductService>()
              .As<IProductService>()
              .InstancePerLifetimeScope();
    }
}