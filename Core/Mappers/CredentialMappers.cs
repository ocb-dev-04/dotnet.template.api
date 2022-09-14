using AutoMapper;

using Core.DTOs;
using Core.Entities;

namespace Core.Mappers;

/// <summary>
/// And instance of <see cref="CredentialMappers"/>
/// </summary>
public sealed class CredentialMappers : Profile
{
    /// <summary>
    /// <see cref="CredentialMappers"/> ctor
    /// </summary>
    public CredentialMappers()
    {
        CreateMap<Credential, CredentialDTO>();
    }
}
