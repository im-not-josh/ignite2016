using System;
using System.Collections.Generic;
using System.Text;

namespace Xtrade.Interfaces.Managers
{
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public interface INavigationManager
    {
        Task PushAsync(INavigation navigation, Page page);
    }
}
