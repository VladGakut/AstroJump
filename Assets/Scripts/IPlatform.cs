using UnityEngine;
using UnityEngine.Events;

public interface IPlatform
{
    UnityEvent OnDestroyEvent { get; }
    
    void SetStartPoint(Vector3 point);
    void SetEndPoint(Vector3 point);
    void SetSpeed(float speed);
    void SetType(PlatformType platformType);
}