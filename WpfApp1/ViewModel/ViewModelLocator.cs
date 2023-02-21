using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1.ViewModel
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
        public DBConnectionTestViewModel DBConnectionTestViewModel => App.Host.Services.GetRequiredService<DBConnectionTestViewModel>();

    }
}
