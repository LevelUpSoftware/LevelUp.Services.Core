using System.Collections.Generic;

namespace LevelUp.Services.Core.FluentValidation;

public class ServiceActionResult<T>
{
    public bool Success { get; set; }

    public T? ResultObject { get; set; }

    public List<string> Errors { get; set; }

    public ServiceActionResult()
    {
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

    public ServiceActionResult(object? resultObject)
    {
        this.Success = true;
        if (resultObject != null)
        {
            this.ResultObject = (T)resultObject;
        }
        this.Errors = new List<string>();
    }

    public ServiceActionResult(bool success, object? resultObject = null, List<string>? errors = null)
    {
        this.Success = success;
        if (resultObject != null)
        {
            this.ResultObject = (T)resultObject;
        }
        this.Errors = errors ?? new List<string>();
    }
}