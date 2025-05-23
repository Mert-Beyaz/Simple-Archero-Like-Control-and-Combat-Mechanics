
using UnityEngine;

public class ShootAnimationEvent : MonoBehaviour
{
    public void OnShoot()
    {
        EventBroker.InvokeOnShoot();
    }
}
