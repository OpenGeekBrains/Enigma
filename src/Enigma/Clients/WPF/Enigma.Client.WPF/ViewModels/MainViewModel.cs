using System;
using System.Collections.Generic;
using System.Text;
using Enigma.Client.WPF.Services;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Options;

namespace Enigma.Client.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title { get; set; }

        public MainViewModel(IOptions<AppSettings> options)
        {
            Title = options.Value.MainWindowTitle;
        }
    }
}
