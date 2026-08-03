using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Common
{
    public record Result(bool success, string? error = null, ResultKind kind = ResultKind.ok)
    {
        public static Result Ok() => new(true);
        public static Result Fail(string error, ResultKind kind = ResultKind.Conflict) => new(false, error);
        public static Result NotFound(string error = "Not Found") => new(false,error,ResultKind.NotFound);
        public static Result Validation(string error)=> new(false, error, ResultKind.ValidationFailed);
    }


    public record Result<T>(bool success,T? Value, string? error = null, ResultKind kind = ResultKind.ok)
    {
        public static Result<T> Ok(T Value) => new(true, Value);
        public static Result<T> Fail(string error, ResultKind kind = ResultKind.Conflict) => new(false, default,error);
        public static Result<T> NotFound(string error = "Not Found") => new(false, default, error, ResultKind.NotFound);
        public static Result<T>Validation(string error) => new(false, default, error, ResultKind.ValidationFailed);
    }



}
