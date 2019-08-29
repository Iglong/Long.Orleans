using App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services
{
    /// <summary>
    /// 注入谷粒服务
    /// </summary>
    public static class ServerInjection
    {
        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void InjectionServices(this IServiceCollection services)
        {
            _services = services;
            _serviceProvider = services.BuildServiceProvider();
        }
        private static void AfterBuildServiceProvider<T>(this T instance) where T : class
        {
            _services.AddSingleton(instance);
            _serviceProvider = _services.BuildServiceProvider();
        }
        private static ServiceProvider _serviceProvider;
        private static IServiceCollection _services;

        public static T GetServer<T>(this BaseViewModel controller, Guid guid) where T : class, IGrainWithGuidKey
        {
            var TService = _serviceProvider.GetService<T>();
            if (TService == null)
            {
                var client = _serviceProvider.GetService<IClusterClient>();
                if (client == null)
                {
                    throw new Exception("客户端没有启动 或者没有被注入");
                }
                TService = client.GetGrain<T>(guid);
                if (TService == null)
                {
                    throw new Exception("没有找到该类型" + TService.GetType());
                }
                TService.AfterBuildServiceProvider();
                return TService;
            }
            return TService;
        }
        public static T GetServer<T>(this BaseViewModel controller, Guid guid, string extexdkey) where T : class, IGrainWithGuidCompoundKey
        {
            var TService = _serviceProvider.GetService<T>();
            if (TService == null)
            {
                var client = _serviceProvider.GetService<IClusterClient>();
                if (client == null)
                {
                    throw new Exception("客户端没有启动 或者没有被注入");
                }
                TService = client.GetGrain<T>(guid, extexdkey);
                if (TService == null)
                {
                    throw new Exception("没有找到该类型" + TService.GetType());
                }
                TService.AfterBuildServiceProvider();
                return TService;
            }
            return TService;
        }
        public static T GetServer<T>(this BaseViewModel controller, long id) where T : class, IGrainWithIntegerKey
        {
            var TService = _serviceProvider.GetService<T>();
            if (TService == null)
            {
                var client = _serviceProvider.GetService<IClusterClient>();
                if (client == null)
                {
                    throw new Exception("客户端没有启动 或者没有被注入");
                }
                TService = client.GetGrain<T>(id);
                if (TService == null)
                {
                    throw new Exception("没有找到该类型" + TService.GetType());
                }
                TService.AfterBuildServiceProvider();
                return TService;
            }
            return TService;
        }
        public static T GetServer<T>(this BaseViewModel controller, long id, string extedkey) where T : class, IGrainWithIntegerCompoundKey
        {
            var TService = _serviceProvider.GetService<T>();
            if (TService == null)
            {
                var client = _serviceProvider.GetService<IClusterClient>();
                if (client == null)
                {
                    throw new Exception("客户端没有启动 或者没有被注入");
                }
                TService = client.GetGrain<T>(id, extedkey);
                if (TService == null)
                {
                    throw new Exception("没有找到该类型" + TService.GetType());
                }
                TService.AfterBuildServiceProvider();
                return TService;
            }
            return TService;
        }
        public static T GetServer<T>(this BaseViewModel controller, string key) where T : class, IGrainWithStringKey
        {
            var TService = _serviceProvider.GetService<T>();
            if (TService == null)
            {
                var client = _serviceProvider.GetService<IClusterClient>();
                if (client == null)
                {
                    throw new Exception("客户端没有启动 或者没有被注入");
                }
                TService = client.GetGrain<T>(key);
                if (TService == null)
                {
                    throw new Exception("没有找到该类型" + TService.GetType());
                }
                TService.AfterBuildServiceProvider();
                return TService;
            }
            return TService;
        }
    }
}
