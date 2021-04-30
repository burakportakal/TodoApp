using Autofac;
using TodoApp.CommandHandlers;

namespace TodoApp
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            foreach (var item in typeof(CommandHandler<,>).Assembly.GetTypes())
            {
                if (item.IsAssignableTo<ICommandHandler>() && !item.IsAbstract && !item.IsInterface)
                {
                    builder.RegisterType(item).Named<ICommandHandler>(item.BaseType.GetGenericArguments()[0].FullName);
                }
            }
        }
    }
}
