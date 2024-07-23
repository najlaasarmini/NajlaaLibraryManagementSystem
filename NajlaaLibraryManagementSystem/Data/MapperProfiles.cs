using AutoMapper;
using NajlaaLibraryManagementSystem.Dtos.Author;
using NajlaaLibraryManagementSystem.Dtos.Book;
using NajlaaLibraryManagementSystem.Dtos.Country;
using NajlaaLibraryManagementSystem.Dtos.Language;
using NajlaaLibraryManagementSystem.Dtos.ParentCategory;
using NajlaaLibraryManagementSystem.Dtos.Publisher;
using NajlaaLibraryManagementSystem.Dtos.SubCategory;
using NajlaaLibraryManagementSystem.Models;

namespace NajlaaLibraryManagementSystem.Data
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            #region Author
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<AuthorDto, Author>().ReverseMap();
            #endregion

            #region Book
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<BookDto, Book>().ReverseMap();
            #endregion

            #region Country
            CreateMap<CreateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>();
            CreateMap<CountryDto, Country>().ReverseMap();
            #endregion

            #region Language
            CreateMap<CreateLanguageDto, Language>();
            CreateMap<UpdateLanguageDto, Language>();
            CreateMap<LanguageDto, Language>().ReverseMap();
            #endregion

            #region ParentCategory
            CreateMap<CreateParentCategoryDto, ParentCategory>();
            CreateMap<UpdateParentCategoryDto, ParentCategory>();
            CreateMap<ParentCategoryDto, ParentCategory>().ReverseMap();
            #endregion

            #region Publisher
            CreateMap<CreatePublisherDto, Publisher>();
            CreateMap<UpdatePublisherDto, Publisher>();
            CreateMap<PublisherDto, Publisher>().ReverseMap();
            #endregion

            #region SubCategory
            CreateMap<CreateSubCategoryDto, SubCategory>();
            CreateMap<UpdateSubCategoryDto, SubCategory>();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
            #endregion
        }
    }
}
