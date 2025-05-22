using System;

public class EventBroker
{
    public static event Action OnChangeGameState;
    public static void InvokeOnChangeGameState()
    {
        OnChangeGameState?.Invoke();
    }

    public static event Action<int, int> OnPlay1;
    public static void InvokeOnPlay1(int t, int s)
    {
        OnPlay1?.Invoke(t, s);
    }    
    
    public static event Action OnPlay;
    public static void InvokeOnPlay()
    {
        OnPlay?.Invoke();
    } 
    
    public static event Action OnFirstTouch;
    public static void InvokeOnFirstTouch()
    {
        OnFirstTouch?.Invoke();
    }

    public static event Action<int> OnNeonColorChange;
    public static void InvokeOnNeonColorChange(int t)
    {
        OnNeonColorChange?.Invoke(t);
    }
}
