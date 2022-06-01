using Application.Commands;
using Application.Commands.Enums.Difference;
using Application.Implementation;
using Application.Requests;
using Application.Response;
using Common.Extensions;
using DiffingApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void RegisterRoutesForDiffer(this WebApplication app)
        {
            app.MapPut("/v1/diff/{id}/right", (int id, [FromBody] WordSetRequest request, [FromServices] ISetWordCommand command) =>
                CommandExecutor(command.Execute, new WordSetModel { CheckSide = HorziontalCheckSide.Right, Id = id, Value = request.Value.DecodeBase64() }))
            .WithName("AddRight");

            app.MapPut("/v1/diff/{id}/left", (int id, [FromBody] WordSetRequest request, [FromServices] ISetWordCommand command) =>
                CommandExecutor(command.Execute, new WordSetModel { CheckSide = HorziontalCheckSide.Left, Id = id, Value = request.Value.DecodeBase64()}))
            .WithName("AddLeft");

            app.MapGet("/v1/diff/{id}", (int id, [FromServices] IDifferenceCheckerCommand command) =>
                CommandExecutor(command.Execute, new DiffCheckModel{ Id = id}))
            .WithName("CheckDiff");

            app.UseMiddleware<GlobalExceptionMiddleware>();
        }
        

        private static IResult CommandExecutor<TReq, TRes>(Func<TReq, TRes> func, TReq model)
            where TReq : BaseModel
            where TRes : class
        {
            TRes response = func(model);

            if (response?.GetType() != typeof(EmptyResponse))
            {
                return Results.Json(response, new System.Text.Json.JsonSerializerOptions { IncludeFields = true } , contentType: "application/json", statusCode: 200);
            }
            else
            {
                return Results.StatusCode(201);
            }
        }
    }
}
