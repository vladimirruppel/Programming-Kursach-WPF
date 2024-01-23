using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace prog3_kursach.ViewModel
{
    public class MyPlaylistsPageViewModel : ViewModelBase
    {
        private ApplicationContext db = new ApplicationContext();

        private ObservableCollection<PlaylistViewModel> playlists = new ObservableCollection<PlaylistViewModel>();
        public IEnumerable<PlaylistViewModel> Playlists => playlists;

        public MyPlaylistsPageViewModel()
        {
            db.Database.EnsureCreated();
            db.Playlists.Load();

            foreach (Playlist playlist in db.Playlists.ToList())
            {
                if (playlist.IsAdded)
                    playlists.Add(new PlaylistViewModel(playlist));
            }
        }
    }
}
