using AutoMapper;

using Core.DTOs;
using Core.Entities;

namespace Core.Mappers;

/// <summary>
/// An instance of <see cref="UserMappers"/>
/// </summary>
public sealed class UserMappers : Profile
{
    /// <summary>
    /// <see cref="UserMappers"/> contructor
    /// </summary>
    public UserMappers()
    {
        // queries
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, FlatUserDTO>().ReverseMap();

        // commands
        CreateMap<CreateUserDTO, User>();
    }
}
