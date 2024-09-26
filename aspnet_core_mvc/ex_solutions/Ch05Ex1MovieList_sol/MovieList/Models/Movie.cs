using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieList.Models
{
    public class Movie
    {
        // EF will instruct the database to automatically generate this value
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")] 
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a year.")]
        [Range(1889, 2050, ErrorMessage = "Year must be between 1889 and now.")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter a rating.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "Please enter a genre.")]
        public string GenreId { get; set; } = string.Empty;

        [ValidateNever]
        public Genre Genre { get; set; } = null!;

        public string Slug =>
            Name?.Replace(' ', '-').ToLower() + '-' + Year?.ToString();
    }
}