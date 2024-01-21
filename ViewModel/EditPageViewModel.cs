using Microsoft.Win32;
using NAudio.Wave;
using prog3_kursach.Model;
using prog3_kursach.MVVM;
using System;
using System.Windows;

namespace prog3_kursach.ViewModel
{
    public class EditPageViewModel : ViewModelBase
    {
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
            = new LabelFieldStruct("Путь к музыкальному файлу", FieldType.File, true);
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
            = new LabelFieldStruct("Название трека", FieldType.Text);
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
            = new LabelFieldStruct("Имя артиста", FieldType.Text);
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
            = new LabelFieldStruct("Год выпуска", FieldType.Number);
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

        public EditPageViewModel()
        {
            ChooseFileCommand = new RelayCommand(execute => ChooseFile());
            CreateTrackCommand = new RelayCommand(execute => CreateTrack(), canExecute => CanCreateTrack());
        }

        public RelayCommand ChooseFileCommand { get; }
        public RelayCommand CreateTrackCommand { get; }

        private void ChooseFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Audio Files | *.mp3";

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                string path = fileDialog.FileName;
                FilePathField
                    = new LabelFieldStruct("Путь к музыкальному файлу", path, FieldType.File, true);

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
            ArtistNameField = new LabelFieldStruct("Имя артиста", FieldType.Text);
            TrackNameField = new LabelFieldStruct("Название трека", FieldType.Text);
            ReleaseYearField = new LabelFieldStruct("Год выпуска", FieldType.Text);
            FilePathField = new LabelFieldStruct("Путь к музыкальному файлу", FieldType.File, true);
            // вывод диалогового окна "Трек создан"
            MessageBox.Show("Успешно!", "Трек создан", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private static bool IsStringNumber(string str)
        {
            foreach (var ch in str)
            {
                if (!char.IsDigit(ch))
                    return false;
            }
            return true;
        }
    }
}
