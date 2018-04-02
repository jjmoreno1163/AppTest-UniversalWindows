using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppTest_UniversalWindows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // initialisations
        public MainPage()
        {
            this.InitializeComponent();

            countryList.Add(new Country
            {
                Name = "Switzerland",
                CurrencyCode = "CHF",
                ToEuroRate = 0.8,
                UriSource = new Uri(img_CurrencyFlag.BaseUri, "ms-appx:///Assets/flag-of-Switzerland.png")
            });
            countryList.Add(new Country
            {
                Name = "United States",
                CurrencyCode = "USD",
                ToEuroRate = 0.9,
                UriSource = new Uri(img_CurrencyFlag.BaseUri, "ms-appx:///Assets/flag-of-United-States-of-America.png")
            });
            foreach (Country country in countryList)
            {
                cboBx_CurrencyCode.Items.Add(country.CurrencyCode);
            }
            cboBx_CurrencyCode.SelectedIndex = 0;
            txBx_CurrencyAmount.Text = "";

            // pour que le clavier soit numérique - FIX ME - ne marche pas
            //InputScope scope = new InputScope();
            //InputScopeName scopeName = new InputScopeName();
            //scopeName.NameValue = InputScopeNameValue.CurrencyAmount;
            //scope.Names.Add(scopeName);
            //txBx_CurrencyAmount.InputScope = scope;

            // 2e option - FIX ME - ne marche pas non plus
            //txBx_CurrencyAmount.InputScope = new InputScope()
            //{
            //    Names = { new InputScopeName(InputScopeNameValue.Number)}
            //};

            // Pour ne pas que le focus soit sur la Text box au démarrage - FIX ME - ne marche pas
            var foc = FocusManager.GetFocusedElement();
            FocusManager.TryMoveFocus(FocusNavigationDirection.Down);
        }

        public class Country
        {
            public string Name { get; set; }
            public string CurrencyCode { get; set; }
            public double ToEuroRate { get; set; }
            public Uri UriSource { get; set; }
        }

        List<Country> countryList = new List<Country>();

        private string ConvertCurrencyTxt(string inputValue, double rate)
        {
            double convertedValue;
            try
            {
                convertedValue = Convert.ToDouble(inputValue) * rate;
            }
            catch (Exception)
            {
                return "Valeur incorrecte";
            }
            return convertedValue.ToString();
        }

        private void cboBx_CurrencyCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = countryList[cboBx_CurrencyCode.SelectedIndex].UriSource;
            img_CurrencyFlag.Source = bitmapImage;
            txBx_ConversionResult.Text = ConvertCurrencyTxt(txBx_CurrencyAmount.Text, countryList[cboBx_CurrencyCode.SelectedIndex].ToEuroRate);

        }

        private void bt_ConvertCurrency_Click(object sender, RoutedEventArgs e)
        {
            txBx_ConversionResult.Text = ConvertCurrencyTxt(txBx_CurrencyAmount.Text, countryList[cboBx_CurrencyCode.SelectedIndex].ToEuroRate);
        }

        private void txBx_CurrencyAmount_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                txBx_ConversionResult.Text = ConvertCurrencyTxt(txBx_CurrencyAmount.Text, countryList[cboBx_CurrencyCode.SelectedIndex].ToEuroRate);
            }
        }
    }
}
