namespace AsyncAwait.Task2.CodeReviewChallenge.Services;

public class PrivacyDataService : IPrivacyDataService
{
    //public Task<string> GetPrivacyDataAsync()
    //{
    //    return new ValueTask<string>("This Policy describes how async/await processes your personal data," +
    //                                 "but it may not address all possible data processing scenarios.").AsTask();
    //}

    //there is no any asynchronous work happening and we can ,simply return the string synchronously.

    public string GetPrivacyData()
    {
        return "This Policy describes how async/await processes your personal data," +
               "but it may not address all possible data processing scenarios.";
    }
}