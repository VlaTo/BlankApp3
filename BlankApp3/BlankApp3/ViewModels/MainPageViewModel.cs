using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Essentials;

namespace BlankApp3.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IInitializeAsync
    {
        private string subtitle;
        private HttpClient httpClient;

        public string SubTitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            httpClient = new HttpClient();
            Title = "Main Page";
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var uri = new Uri("http://example.com", UriKind.Absolute);

            using (var response = await httpClient.GetAsync(uri))
            {
                var message = response.EnsureSuccessStatusCode();
                var stream = await message.Content.ReadAsStreamAsync();
                var text = $"example.com response {stream.Length} chars";

                Debug.WriteLine(text);

                await MainThread.InvokeOnMainThreadAsync(() => SubTitle = text);
            }
        }
    }
}
