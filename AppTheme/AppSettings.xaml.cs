using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace AppTheme
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AppSettings : Page
    {
        public const ElementTheme DEFAULTTHEME = ElementTheme.Light;
        public const ElementTheme NONDEFAULTHEME = ElementTheme.Dark;

        const string KEY_THEME = "appColourMode";
        static ApplicationDataContainer LOCALSETTINGS = ApplicationData.Current.LocalSettings;

        /// <summary>
        /// Gets or sets the current app colour setting from memory (light or dark mode).
        /// </summary>
        public static ElementTheme Theme
        {
            get
            {
                // Never set: default theme
                if (LOCALSETTINGS.Values[KEY_THEME] == null)
                {
                    LOCALSETTINGS.Values[KEY_THEME] = (int)DEFAULTTHEME;
                    return DEFAULTTHEME;
                }
                // Previously set to default theme
                else if ((int)LOCALSETTINGS.Values[KEY_THEME] == (int)DEFAULTTHEME)
                    return DEFAULTTHEME;
                // Previously set to non-default theme
                else
                    return NONDEFAULTHEME;
            }
            set
            {
                // Error check
                if (value == ElementTheme.Default)
                    throw new System.Exception("Only set the theme to light or dark mode!");
                // Never set
                else if (LOCALSETTINGS.Values[KEY_THEME] == null)
                    LOCALSETTINGS.Values[KEY_THEME] = (int)value;
                // No change
                else if ((int)value == (int)LOCALSETTINGS.Values[KEY_THEME])
                    return;
                // Change
                else
                    LOCALSETTINGS.Values[KEY_THEME] = (int)value;
            }
        }

        public AppSettings()
        {
            this.InitializeComponent();

            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            SetThemeToggle(AppSettings.Theme);
        }

        private void SetThemeToggle(ElementTheme theme)
        {
            if (theme == AppSettings.DEFAULTTHEME)
                TglDarkMode.IsOn = false;
            else
                TglDarkMode.IsOn = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // MainPage.Current.BackButton.Visibility = Visibility.Visible;
            //  MainPage.Current.BackButton.IsEnabled = true;

            //   MainPage.mAppBarButtonSettings.Visibility = Visibility.Collapsed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //  MainPage.mAppBarButtonSettings.Visibility = Visibility.Visible;
        }
        public static async Task SetRequestedThemeAsync()
        {
            foreach (var view in CoreApplication.Views)
            {
                await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (Window.Current.Content is FrameworkElement frameworkElement)
                    {
                        frameworkElement.RequestedTheme = Theme;
                    }
                });
            }
        }

        private void TglDarkMode_Toggled(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = (FrameworkElement)Window.Current.Content;

            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    AppSettings.Theme = AppSettings.NONDEFAULTHEME;
                    window.RequestedTheme = AppSettings.NONDEFAULTHEME;
                }
                else
                {
                    AppSettings.Theme = AppSettings.DEFAULTTHEME;
                    window.RequestedTheme = AppSettings.DEFAULTTHEME;
                }
            }
        }
    }
}
