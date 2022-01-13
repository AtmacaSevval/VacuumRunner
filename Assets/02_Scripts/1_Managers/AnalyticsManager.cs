using Twenty.Managers;
//using ElephantSDK;

public static class AnalyticsManager
{
    public static void InitiateThisClass()
    {
        SubscribeNecessaryMethods();
    }

    private static void SubscribeNecessaryMethods()
    {
        GameManager.onLevelStart += Started;
        GameManager.onLevelSuccess += Completed;
        GameManager.onLevelFail += Failed;
    }

    private static void Started()
    {
        //Elephant.LevelStarted(SceneController.uiLevelIndex + 1);
    }

    private static void Completed()
    {
        //Elephant.LevelCompleted(SceneController.uiLevelIndex + 1);
    }

    private static void Failed()
    {
        //Elephant.LevelFailed(SceneController.uiLevelIndex + 1);
    }
}
