using System;
using System.Net.Http;
using System.Threading.Tasks;
using Hestify;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace Wd3w.Localizer.Test.Utils
{
    public abstract class ApiTestBase : IClassFixture<LocalizerApplicationFactory>
    {
        private readonly LocalizerApplicationFactory _factory;

        private IServiceProvider _serviceProvider;

        private delegate void ConfigureTestServiceHandler(IServiceCollection services);

        private delegate Task SetupFixtureHandler(IServiceProvider provider);

        private event SetupFixtureHandler OnSetupFixtures;

        private event ConfigureTestServiceHandler OnConfigureTestServices;

        protected ApiTestBase(LocalizerApplicationFactory factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateClient()
        {
            return _factory.WithWebHostBuilder(builder => builder
                    .ConfigureTestServices(services =>
                    {
                        OnConfigureTestServices?.Invoke(services);
                        _serviceProvider = services.BuildServiceProvider();

                        using (_serviceProvider.CreateScope())
                        {
                            OnSetupFixtures?.Invoke(_serviceProvider).Wait();
                        }
                    }))
                .CreateClient();
        }

        protected HestifyClient CreateHestify(string resourceUri)
        {
            return CreateClient().Resource(resourceUri);
        } 
        
        protected void ReplaceService<TService, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            CheckClientIsNotCreated(nameof(ReplaceService));
            OnConfigureTestServices += services => services.Replace(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
        }
        
        protected void ReplaceService<TService>(ServiceLifetime lifetime = ServiceLifetime.Scoped) where TService : class
        {
            CheckClientIsNotCreated(nameof(ReplaceService));
            OnConfigureTestServices += services => services.Replace(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
        }
        
        protected void ReplaceService<TService>(TService obj)
        {
            CheckClientIsNotCreated(nameof(ReplaceService));
            OnConfigureTestServices += services => services.Replace(new ServiceDescriptor(typeof(TService), _ => obj, ServiceLifetime.Singleton));
        }

        protected Mock<TService> MockService<TService>() where TService : class
        {
            CheckClientIsNotCreated(nameof(MockService));
            var mock = new Mock<TService>();
            ReplaceService(mock.Object);
            return mock;
        }

        protected async Task SetupFixture<TService>(Func<TService, Task> action)
        {
            OnSetupFixtures += provider => action.Invoke(provider.GetService<TService>());
        }
        
        protected async Task UsingService<TService>(Action<TService> action)
        {
            CheckClientIsCreated(nameof(UsingService));
            using var scope = _serviceProvider.CreateScope();
        }

        private void CheckClientIsNotCreated(string methodName)
        {
            if (_serviceProvider != default)
                throw new InvalidOperationException($"{methodName}는 CreateClient/CreateHestify CreateClient/CreateHestify 호출 이전에만 사용할 수 있습니다."); 
        }
        
        private void CheckClientIsCreated(string methodName)
        {
            if (_serviceProvider == default)
                throw new InvalidOperationException($"{methodName}는 CreateClient/CreateHestify 생성 이후에만 사용할 수 있습니다."); 
        }
    }
}