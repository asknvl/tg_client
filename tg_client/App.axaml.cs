using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using tg_client.ViewModels;
using tg_client.Views;

namespace tg_client
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new mainWnd
                {
                    DataContext = new mainVM(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
