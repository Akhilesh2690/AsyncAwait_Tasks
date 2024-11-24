using CloudServices.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Models.Support;

public class ManualAssistant : IAssistant
{
    private readonly ISupportService _supportService;

    public ManualAssistant(ISupportService supportService)
    {
        _supportService = supportService ?? throw new ArgumentNullException(nameof(supportService));
    }

    public async Task<string> RequestAssistanceAsync(string requestInfo)
    {
        try
        {
            //var t = _supportService.RegisterSupportRequestAsync(requestInfo); //this method needs to complete before proceeding to the next step,so it should be awaited.
            await _supportService.RegisterSupportRequestAsync(requestInfo);

            //Console.WriteLine(t.Status); // this is for debugging purposes
            //  Thread.Sleep(5000); // this is just to be sure that the request is registered
            //unessary blocks the current thread for 5 seconds,  which can degrade performance

            return await _supportService.GetSupportInfoAsync(requestInfo)
                .ConfigureAwait(false);
        }
        catch (HttpRequestException ex)
        {
            //return await Task.Run(async () =>
            //    await Task.FromResult($"Failed to register assistance request. Please try later. {ex.Message}"));

            //unnecessary use of Task.Run and Task.FromResult can be replaced with simple return statement, that return string.

            return $"Failed to register assistance request. Please try later. {ex.Message}";
        }
    }
}