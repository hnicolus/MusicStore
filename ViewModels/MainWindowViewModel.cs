using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive.Linq;
namespace MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        private bool _collectionEmpty;

        public bool CollectionEmpty
        {
            get => _collectionEmpty;
            set => this.RaiseAndSetIfChanged(ref _collectionEmpty, value);
        }
        
        public ObservableCollection<AlbumViewModel> Albums { get; } = new();
        public ICommand BuyMusicCommand { get; set; }
public Interaction<MusicStoreViewModel,AlbumViewModel?> ShowDialog { get;  }

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
            
            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();
                var result = await ShowDialog.Handle(store);
                if (result != null)
                {
                    Albums.Add(result);
                    await result.SaveToDiskAsync();
                }
            });
            
            this.WhenAnyValue(x => x.Albums.Count)
                .Subscribe(x => CollectionEmpty = x == 0);
        }
        
    }
}
