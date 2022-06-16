using System.Collections.Generic;

namespace LevelUp.Services.Core.FluentValidation;

/// <summary>
/// CRUD service response object.
/// </summary>
/// <typeparam name="TDisplayModel">Display DTO model type.</typeparam>
public class ServiceActionResult<TDisplayModel> : ServiceActionResult
{

    public TDisplayModel? ResultObject { get; set; }

    public ServiceActionResult(bool success, object? resultObject = null, List<string>? errors = null)
    {
        this.Success = success;
        if (resultObject != null)
        {
            this.ResultObject = (TDisplayModel)resultObject;
        }
        this.Errors = errors ?? new List<string>();
    }

    public ServiceActionResult(object? resultObject)
    {
        this.Success = true;
        if (resultObject != null)
        {
            this.ResultObject = (TDisplayModel)resultObject;
        }
        this.Errors = new List<string>();
    }


    public ServiceActionResult(List<string> errors) : base(errors)
    {
    }

    public ServiceActionResult(string error) : base(error)
    {
    }

    

   
}

public class ServiceActionResult
{
    public bool Success { get; set; }
    public List<string> Errors { get; set; }

    public ServiceActionResult()
    {
        Success = true;
        Errors = new List<string>();
    }

    public ServiceActionResult(List<string> errors)
    {
        Success = false;
        Errors = errors;
    }

    public ServiceActionResult(string error)
    {
        Success = false;
        Errors = new List<string> { error };
    }

}