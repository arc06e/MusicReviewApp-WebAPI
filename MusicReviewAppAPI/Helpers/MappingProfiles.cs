using AutoMapper;
using MusicReviewAppAPI.DTO;
using MusicReviewAppAPI.Models;

namespace MusicReviewAppAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //mapping style for get requests
            CreateMap<Album, AlbumDto>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();

            //mapping style for post/put requests
            CreateMap<AlbumDto, Album>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<CountryDto, Country>();
            CreateMap<GenreDto, Genre>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewerDto, Reviewer>();
        }

    }
}
