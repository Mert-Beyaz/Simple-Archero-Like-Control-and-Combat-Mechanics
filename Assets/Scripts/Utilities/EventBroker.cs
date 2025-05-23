using System;

public class EventBroker
{
    public static event Action OnShoot;
    public static void InvokeOnShoot()
    {
        OnShoot?.Invoke();
    } 
    
    public static event Action OnFirstTouch;
    public static void InvokeOnFirstTouch()
    {
        OnFirstTouch?.Invoke();
    }

    public static event Action<Type, bool> OnChangeSkill;
    public static void InvokeOnOnChangeSkill(Type t, bool b)
    {
        OnChangeSkill?.Invoke(t, b);
    }

    //public static event Action OnChangeGameState;
    //public static void InvokeOnChangeGameState()
    //{
    //    OnChangeGameState?.Invoke();
    //}

    //public static event Action<int, int> OnPlay1;
    //public static void InvokeOnPlay1(int t, int s)
    //{
    //    OnPlay1?.Invoke(t, s);
    //}

    //public static event Action<Type> On;
    //public static void InvokeOn(Type t)
    //{
    //    On?.Invoke(t);
    //}
}
