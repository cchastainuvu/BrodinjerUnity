using UnityEngine;
using UnityEngine.Events;
public class TestEvent : MonoBehaviour
{
    public UnityEvent eventCall;

    private void OnCollisionEnter(Collision other)
    {
        eventCall.Invoke();
    }
}
