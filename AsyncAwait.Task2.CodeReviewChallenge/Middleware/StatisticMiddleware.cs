using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        //var staticRegTask = Task.Run(
        //    () => _statisticService.RegisterVisitAsync(path)
        //        .ConfigureAwait(false)
        //        .GetAwaiter().OnCompleted(UpdateHeaders));
        //Console.WriteLine(staticRegTask.Status); // just for debugging purposes

        //Task create new  a thread  async on threadpool but async methods are already designed to run asynchronously without blocking a thread.

        //void UpdateHeaders()
        //{
        //    context.Response.Headers.Add(
        //        CustomHttpHeaders.TotalPageVisits,
        //        _statisticService.GetVisitsCountAsync(path).GetAwaiter().GetResult().ToString()); //GetVisitsCountAsync should be await as,it blocks the execution of the thread until the result is returned.
        //}

        //Thread.Sleep(3000); // without this the statistic counter does not work ,(It blocks the thread ,wait for 3 sec.)

        //await _next(context);

        await _statisticService.RegisterVisitAsync(path);
        var visitsCount = await _statisticService.GetVisitsCountAsync(path);

        context.Response.Headers.Add(
            CustomHttpHeaders.TotalPageVisits,
            visitsCount.ToString());

        await _next(context);
    }
}