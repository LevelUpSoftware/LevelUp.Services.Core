﻿using AutoMapper;
using LevelUp.Services.Tests.ServiceTests.ReadServiceTests.Models;
using LevelUp.Services.Tests.TestAssets.Models;

namespace LevelUp.Services.Tests.ServiceTests.ReadServiceTests.Mappers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<DB_Customer, DisplayCustomer>();
    }
}