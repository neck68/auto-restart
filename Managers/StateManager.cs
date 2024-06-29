using AutoRestarter.Util;

namespace AutoRestarter.Managers;

public class StateManager
{
    public static Dictionary<int, AppState> accountStates = new Dictionary<int, AppState>();
    public static AppState currentState = AppState.Idle;
    public static void InitializeAccountStates(int numberOfAccounts)
    {
        for (int i = 0; i < numberOfAccounts; i++)
        {
            accountStates[i] = AppState.Idle;
        }
    }

}
