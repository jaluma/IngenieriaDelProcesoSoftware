using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using MahApps.Metro.Controls;
using Ui.Main.Pages.Competition.Times;
using Ui.Main.Pages.MenuInitial;

namespace Ui.Main {
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow {
    //public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            //_mainFrame.NavigationService.Navigate(new MainMenu());

            AppearanceManager.Current.AccentColor = Colors.BlueViolet;
            ContentSource = MenuLinkGroups.First().Links.First().Source;
        }
    }
}
