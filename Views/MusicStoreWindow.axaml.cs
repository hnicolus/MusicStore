using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MusicStore.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;


namespace MusicStore.Views
{
    public class MusicStoreWindow :  ReactiveWindow<MusicStoreViewModel>
    {
        public MusicStoreWindow()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel.BuyMusicCommand.Subscribe(Close)));
#if DEBUG
            this.AttachDevTools();
#endif
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}