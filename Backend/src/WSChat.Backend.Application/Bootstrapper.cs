using Microsoft.Extensions.DependencyInjection;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Services;

namespace WSChat.Backend.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRoomService, RoomService>()
                .AddSingleton<IChatUserService, ChatUserService>()
                .AddTransient<IMessageService, MessageService>();
        }
    }
}
