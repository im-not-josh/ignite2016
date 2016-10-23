using System;
using System.Collections.Generic;
using System.Text;

namespace Xtrade.Managers
{
    using System.Threading.Tasks;
    using Interfaces.Managers;
    using Xamarin.Forms;

    public class NavigationManager : INavigationManager
    {
        private bool isNavigating = false;

        public async Task PushAsync(INavigation navigation, Page page)
        {
            if (!isNavigating)
            {
                isNavigating = true;
                await navigation.PushAsync(page, true);
                isNavigating = false;
            }
        }
    }
}
