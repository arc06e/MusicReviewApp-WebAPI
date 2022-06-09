using MusicReviewAppAPI.Data;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI
{
    public class Seed
    {

        private readonly DataContext _dataContext;
        public Seed(DataContext context)
        {
            _dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!_dataContext.AlbumArtists.Any())
            {
                var artistAlbums = new List<AlbumArtist>()
                {
                    new AlbumArtist()
                    {
                        Artist = new Artist()
                        {
                            ArtistName = "Dolly Parton",

                            
                            Country = new Country()
                            {
                                CountryName = "USA"
                            }

                        },
                        Album = new Album()
                        {
                            AlbumName = "Jolene",
                            YearReleased = 1974,

                            AlbumGenres = new List<AlbumGenre>()
                            {
                                new AlbumGenre { Genre = new Genre() { MusicGenre = "Country"}}
                            },

                            Reviews = new List<Review>()
                            {
                                new Review { Title="Jolene",Text = "Jolene is one of Dolly's Best albums", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Van", LastName = "Johnson" } },
                                new Review { Title="Jolene", Text = "I love every song on this album", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "John", LastName = "Donahue" } },
                                new Review { Title="Jolene",Text = "I just can't get into country music", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jennifer", LastName = "Parson" } },
                            }
                        }
                    },


                    new AlbumArtist()
                    {
                        Artist = new Artist()
                        {
                            ArtistName = "Elton John",

                            Country = new Country()
                            {
                                CountryName = "UK"
                            }
                        },
                        Album = new Album()
                        {
                            AlbumName = "Goodbye Yellow Brick Road",
                            YearReleased = 1973,

                            AlbumGenres = new List<AlbumGenre>()
                            {
                                new AlbumGenre { Genre = new Genre() { MusicGenre = "Pop Rock"}}
                            },

                            Reviews = new List<Review>()
                            {
                                new Review { Title="Goodbye Yellow Brick Road",Text = "GBYBR is an all-time classic", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Audrey", LastName = "Taylor" } },
                                new Review { Title="Goodbye Yellow Brick Road", Text = "Elton John has never been better", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Heather", LastName = "Benton" } },
                                new Review { Title="Goodbye Yellow Brick Road",Text = "over-rated and underwhelming", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "George", LastName = "Calahan" } },
                            }
                        }
                    },

                    new AlbumArtist()
                    {
                        Artist = new Artist()
                        {
                            ArtistName = "Scorpions",

                            Country = new Country()
                            {
                                CountryName = "Germany"
                            }
                        },
                        Album = new Album()
                        {
                            AlbumName = "In Trance",
                            YearReleased = 1975,

                            AlbumGenres = new List<AlbumGenre>()
                            {
                                new AlbumGenre { Genre = new Genre() { MusicGenre = "Hard Rock"}}
                            },

                            Reviews = new List<Review>()
                            {
                                new Review { Title="In Trance",Text = "This is a solid album from start to finish", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Bill", LastName = "Steward" } },
                                new Review { Title="In Trance", Text = "One of my personal favorites from their 70s era", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Molly", LastName = "Peterson" } },
                                new Review { Title="In Trance",Text = "they should have stuck to their krautrock roots", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Andrew", LastName = "Wellers" } },
                            }
                        }
                    },                    

                    new AlbumArtist()
                    {
                        Artist = new Artist()
                        {
                            ArtistName = "Otis Redding",

                            Country = new Country()
                            {
                                CountryName = "USA"
                            }
                        },
                        Album = new Album()
                        {
                            AlbumName = "Dictionary of Soul",
                            YearReleased = 1966,

                            AlbumGenres = new List<AlbumGenre>()
                            {
                                new AlbumGenre { Genre = new Genre() { MusicGenre = "Soul"}}
                            },

                            Reviews = new List<Review>()
                            {
                                new Review { Title="Dictionary of Soul",Text = "All hail King Otis", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Connor", LastName = "Davis" } },
                                new Review { Title="Dictionary of Soul", Text = "Nobody sings like Otis and these are some of his very best songs", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Stephanie", LastName = "Gallagher" } },
                                new Review { Title="Dictionary of Soul",Text = "I guess soul just isn't my thing", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Sarah", LastName = "Scott" } },
                            }
                        }
                    }

                };
                _dataContext.AlbumArtists.AddRange(artistAlbums);
                _dataContext.SaveChanges();
            }
        }
    }
}
