using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.ViewModels;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class AccountSwitcher : UserControl
    {
        private AccountSwitcherViewModel _viewModel;

        public AccountSwitcher()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            _viewModel = Dependencies.Container.Resolve<AccountSwitcherViewModel>();
            DataContext = _viewModel;
        }

        private void AccountSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAccount = (sender as ListBox).SelectedItem as Account;
            _viewModel.OnAccountSelected(selectedAccount);
        }
    }
}
