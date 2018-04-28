using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace App.UnitTests
{
    internal class NavigationServiceMock : INavigationService
    {
        public async Task<bool> GoBackAsync(NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            await Task.Delay(0);
            return true;
        }

        public Task<bool> GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> GoBackAsync(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public async Task NavigateAsync(Uri uri, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            // TODO
            await Task.Delay(0);
        }

        public async Task NavigateAsync(string name, NavigationParameters parameters = null, bool? useModalNavigation = null, bool animated = true)
        {
            // TODO
            await Task.Delay(0);
        }

        public Task NavigateAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(Uri uri, NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task NavigateAsync(string name, NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}