using Microsoft.Win32;
using NAudio.Wave;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace prog3_kursach.ViewModel
{
    public class EditPageViewModel : ViewModelBase
    {
        private const string FILE_PATH_FIELD_LABEL = "Путь к музыкальному файлу";
        private const string TRACK_NAME_FIELD_LABEL = "Название трека";
        private const string ARTIST_NAME_FIELD_LABEL = "Имя артиста";
        private const string RELEASE_YEAR_FIELD_LABEL = "Год выпуска";
        private const string ALBUM_NAME_FIELD_LABEL = "Название альбома";

        public enum FieldType
        {
            Text,
            Number,
            File,
        }
        public class LabelFieldStruct
        {
            public string Label { get; set; }

            private string text = string.Empty;
            public string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    ValidateField();
                }
            }
            public FieldType Type { get; set; }
            public bool IsValidated { get; set; }
            public bool IsReadOnly { get; set; }
            public LabelFieldStruct(string label, FieldType type, bool isReadOnly = false)
            {
                Label = label;
                Type = type;
                IsReadOnly = isReadOnly;
            }
            public LabelFieldStruct(string label, string text, FieldType type, bool isReadOnly = false)
            {
                Label = label;
                Text = text;
                Type = type;
                IsReadOnly = isReadOnly;
            }

            private void ValidateField()
            {
                switch (Type)
                {
                    case FieldType.Text:
                        IsValidated = true;
                        break;
                    case FieldType.Number:
                        IsValidated = IsStringNumber(Text);
                        break;
                    case FieldType.File:
                        IsValidated = true;
                        break;
                }
            }
        }

        ApplicationContext db = new ApplicationContext();

        private LabelFieldStruct filePathField 
            = new LabelFieldStruct(FILE_PATH_FIELD_LABEL, FieldType.File, true);
        public LabelFieldStruct FilePathField
        {
            get { return filePathField; }
            set 
            { 
                filePathField = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Duration));
            }
        }

        private LabelFieldStruct trackNameField 
            = new LabelFieldStruct(TRACK_NAME_FIELD_LABEL, FieldType.Text);
        public LabelFieldStruct TrackNameField
        {
            get { return trackNameField; }
            set 
            { 
                trackNameField = value;
                OnPropertyChanged();
            }
        }

        private LabelFieldStruct artistNameField
            = new LabelFieldStruct(ARTIST_NAME_FIELD_LABEL, FieldType.Text);
        public LabelFieldStruct ArtistNameField
        {
            get { return artistNameField; }
            set 
            { 
                artistNameField = value;
                OnPropertyChanged();
            }
        }

        private LabelFieldStruct releaseYearField
            = new LabelFieldStruct(RELEASE_YEAR_FIELD_LABEL, FieldType.Number);
        public LabelFieldStruct ReleaseYearField
        {
            get { return releaseYearField; }
            set
            {
                releaseYearField = value;
                OnPropertyChanged();
            }
        }

        private int duration;

        private LabelFieldStruct albumNameField
            = new LabelFieldStruct(ALBUM_NAME_FIELD_LABEL, FieldType.Text);
        public LabelFieldStruct AlbumNameField
        {
            get { return albumNameField; }
            set
            {
                albumNameField = value;
                OnPropertyChanged();
            }
        }

        private TrackListChooseViewModel trackListChooseViewModel = new TrackListChooseViewModel();
        public TrackListChooseViewModel TrackListChooseViewModel
        {
            get { return trackListChooseViewModel; }
            set
            {
                trackListChooseViewModel = value;
                OnPropertyChanged();
            }
        }

        public EditPageViewModel()
        {
            ChooseFileCommand = new RelayCommand(execute => ChooseFile());
            CreateTrackCommand = new RelayCommand(execute => CreateTrack(), canExecute => CanCreateTrack());
            CreateAlbumCommand = new RelayCommand(execute => CreateAlbum(), canExecute => CanCreateAlbum());
        }

        public RelayCommand ChooseFileCommand { get; }
        public RelayCommand CreateTrackCommand { get; }
        public RelayCommand CreateAlbumCommand { get; }

        private void ChooseFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Audio Files | *.mp3";

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                string path = fileDialog.FileName;
                FilePathField
                    = new LabelFieldStruct(FILE_PATH_FIELD_LABEL, path, FieldType.File, true);

                duration = (int)new AudioFileReader(path).TotalTime.TotalSeconds;
            }
        }

        private void CreateTrack()
        {
            Track track = new Track
            {
                ArtistName = ArtistNameField.Text,
                TrackName = TrackNameField.Text,
                ReleaseYear = Convert.ToInt32(ReleaseYearField.Text),
                Path = FilePathField.Text,
                Duration = duration,
                IsAdded = false,
                AlbumId = -1,
                DateAdded = DateTime.Now,
            };

            db.Tracks.Add(track);
            db.SaveChanges();

            // очистка полей
            ArtistNameField = new LabelFieldStruct(ARTIST_NAME_FIELD_LABEL, FieldType.Text);
            TrackNameField = new LabelFieldStruct(TRACK_NAME_FIELD_LABEL, FieldType.Text);
            ReleaseYearField = new LabelFieldStruct(RELEASE_YEAR_FIELD_LABEL, FieldType.Text);
            FilePathField = new LabelFieldStruct(FILE_PATH_FIELD_LABEL, FieldType.File, true);
            // вывод диалогового окна "Трек создан"
            MessageBox.Show($"Трек создан: {track.ArtistName} - {track.TrackName}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanCreateTrack()
        {
            if (ArtistNameField.Text.Length == 0)
                return false;
            if (TrackNameField.Text.Length == 0)
                return false;
            if (ReleaseYearField.Text.Length != 4 || !ReleaseYearField.IsValidated)
                return false;
            if (FilePathField.Text.Length == 0)
                return false;
            return true;
        }

        private void CreateAlbum()
        {
            Album album = new Album
            {
                ArtistName = ArtistNameField.Text,
                Name = AlbumNameField.Text,
                Tracks = GetListOfTracksFromTrackListChooseViewModel(TrackListChooseViewModel),
                ReleaseYear = Convert.ToInt32(ReleaseYearField.Text),
                DateAdded = DateTime.Now,
                IsAdded = false
            };

            db.Albums.Add(album);
            db.SaveChanges();

            // очистка полей
            ArtistNameField = new LabelFieldStruct(ARTIST_NAME_FIELD_LABEL, FieldType.Text);
            AlbumNameField = new LabelFieldStruct(ALBUM_NAME_FIELD_LABEL, FieldType.Text);
            ReleaseYearField = new LabelFieldStruct(RELEASE_YEAR_FIELD_LABEL, FieldType.Text);
            TrackListChooseViewModel = new TrackListChooseViewModel();
            // вывод диалогового окна "Альбом создан"
            MessageBox.Show($"Альбом создан: {album.ArtistName} - {album.Name}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanCreateAlbum()
        {
            if (ArtistNameField.Text.Length == 0)
                return false;
            if (AlbumNameField.Text.Length == 0)
                return false;
            if (ReleaseYearField.Text.Length != 4 || !ReleaseYearField.IsValidated)
                return false;
            if (TrackListChooseViewModel.AddedTracks.Count() == 0)
                return false;
            return true;
        }

        private static bool IsStringNumber(string str)
        {
            foreach (var ch in str)
            {
                if (!char.IsDigit(ch))
                    return false;
            }
            return true;
        }

        private static List<Track> GetListOfTracksFromTrackListChooseViewModel(TrackListChooseViewModel trackListChooseViewModel)
        {
            List<Track> tracks = new List<Track>();
            foreach (ChooseTrackViewModel chooseTrackViewModel in trackListChooseViewModel.AddedTracks)
            {
                TrackViewModel trackViewModel = chooseTrackViewModel.TrackViewModel;
                Track track = trackViewModel.Track;
                tracks.Add(track);
            }
            return tracks;
        }
    }
}
