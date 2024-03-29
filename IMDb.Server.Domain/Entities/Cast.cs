﻿using IMDb.Server.Domain.Entities.Abstract;

namespace IMDb.Server.Domain.Entities
{
    public class Cast : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<CastActMovies> ActedMovies { get; set; } = default!;
        public IEnumerable<CastDirectMovies> DirectedMovies { get; set; } = default!;
        public DateTime DateBirth { get; set; }
    }
}
