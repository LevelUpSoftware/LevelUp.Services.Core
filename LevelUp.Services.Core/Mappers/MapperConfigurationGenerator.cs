using System;
using System.Collections.Generic;
using AutoMapper;

namespace LevelUp.Services.Core.Mappers;

public static class MapperConfigurationGenerator
{
    private static readonly List<Profile> _profiles = new List<Profile>();

    public static void AddProfile(Profile newProfile)
    {
        _profiles.Add(newProfile);
    }

    public static Action<IMapperConfigurationExpression> Invoke()
    {
        return (x => x.AddProfiles(_profiles));
    }

    public static MapperConfiguration Create()
    {
        return new MapperConfiguration(Invoke());
    }
}