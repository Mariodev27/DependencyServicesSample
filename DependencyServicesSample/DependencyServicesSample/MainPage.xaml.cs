using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyServicesSample.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DependencyServicesSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Item1.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new TxtMessage());
            };
            Item2.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new TextToSpeechDemo());
            };
            Item3.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new BatteryDemo());
            };
            Item4.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new QRCodeScanner());
            };
        }
    }
}