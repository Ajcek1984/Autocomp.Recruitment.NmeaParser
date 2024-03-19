using Autocomp.Nmea.UnitTests.TestServices;
using Microsoft.Extensions.DependencyInjection;

namespace Autocomp.Nmea.UnitTests
{
    public abstract class TestBase
    {
        public void Test<TService>() where TService : TestServiceBase => Test<TService>(sc => { });

        public void Test<TService>(Action<IServiceCollection> additionalConiguration) where TService : TestServiceBase
        {
            var services = new ServiceCollection();
            services.AddScoped<TService>();
            ConfigureServices(services);
            additionalConiguration(services);
            using (var provider = services.BuildServiceProvider())
            {
                var testService = provider.GetRequiredService<TService>();
                testService.Test();
            }
        }

        public async Task TestAsync<TService>() where TService : AsyncTestServiceBase => await TestAsync<TService>(sc => { });

        public async Task TestAsync<TService>(Action<IServiceCollection> additionalConiguration) where TService : AsyncTestServiceBase
        {
            var services = new ServiceCollection();
            services.AddScoped<TService>();
            ConfigureServices(services);
            additionalConiguration(services);
            using (var provider = services.BuildServiceProvider())
            {
                var testService = provider.GetRequiredService<TService>();
                await testService.TestAsync();
            }
        }

        protected abstract void ConfigureServices(IServiceCollection services);
    }
}
