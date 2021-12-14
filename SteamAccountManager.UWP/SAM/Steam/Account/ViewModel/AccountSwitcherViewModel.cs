using SteamAccountManager.UWP.SAM.Common.ViewModel;
using SteamAccountManager.UWP.SAM.Steam.Account.Model;
using System.Collections.ObjectModel;

namespace SteamAccountManager.UWP.SAM.Steam.Account.ViewModel
{
    internal class AccountSwitcherViewModel : BaseViewModel
    {
        public ObservableCollection<SteamAccount> Accounts { get; set; }

        public AccountSwitcherViewModel()
        {
            Accounts = new ObservableCollection<SteamAccount>() 
            { 
                new SteamAccount() 
                {
                    SteamId = "450237405214",
                    Name = "Sahin",
                    ProfilePicture = "Assets//LockScreenLogo.scale-200.png"
                } 
            };
        }
    }
}
