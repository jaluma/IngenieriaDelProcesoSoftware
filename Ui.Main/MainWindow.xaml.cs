using System.Linq;
using System.Windows;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace Ui.Main
{
    /// <summary>
    ///     Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        //public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            AppearanceManager.Current.AccentColor = Colors.BlueViolet;
            ContentSource = MenuLinkGroups.First().Links.First().Source;
        }
    }
}
