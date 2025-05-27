
using UnityEngine;

namespace Archero
{
    public class ShootAnimationEvent : MonoBehaviour
    {
        public void OnShoot()
        {
            EventBroker.Publish("OnShoot");
        }
    }
}