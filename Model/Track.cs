using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace prog3_kursach.Model
{
    public class Track : ICloneable
    {
        public int Id { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string TrackName { get; set; } = string.Empty;

        public int ReleaseYear;
        public string Path { get; set; } = string.Empty;
        public int Duration { get; set; } 
        public DateTime DateAdded { get; set; }
        public bool IsAdded { get; set; } = false;

        object ICloneable.Clone()
        {
            var clone = new Track
            {
                ArtistName = ArtistName,
                TrackName = TrackName,
                ReleaseYear = ReleaseYear,
                Path = Path,
                Duration = Duration,
                DateAdded = DateAdded,
                IsAdded = IsAdded
            };

            return clone;
        }
    }
}
