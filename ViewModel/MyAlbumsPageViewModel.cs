using Microsoft.EntityFrameworkCore;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace prog3_kursach.ViewModel
{
    public class MyAlbumsPageViewModel : ViewModelBase
    {
        private ApplicationContext db = new ApplicationContext();

        private ObservableCollection<AlbumViewModel> albums = new ObservableCollection<AlbumViewModel>();
        public IEnumerable<AlbumViewModel> Albums => albums;

        public MyAlbumsPageViewModel()
        {
            db.Database.EnsureCreated();
            db.Albums.Load();

            foreach (Album album in db.Albums.ToList()) {
                albums.Add(new AlbumViewModel(album));
            }
        }
    }
}
