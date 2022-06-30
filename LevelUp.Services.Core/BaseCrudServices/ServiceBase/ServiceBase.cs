using AutoMapper;
using LevelUp.Services.Core.Exceptions;
using Microsoft.Extensions.Configuration;

namespace LevelUp.Services.Core.BaseCrudServices.ServiceBase;

public abstract class ServiceBase
{
    private readonly IConfiguration _configuration;
    protected IConfiguration Configuration
    {
        get
        {
            if (_configuration == null)
            {
                throw new NullConfigurationException();
            }

            return _configuration;
        }
    }

    protected ServiceBase()
    {

    }

    protected ServiceBase(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}