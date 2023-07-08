using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractPlatform : MonoBehaviour, IPlatform
{
    protected Vector3 _startPoint;
    protected Vector3 _endPoint;
    protected float _speed;
    protected PlatformType _platformType;

    protected MovementDirection _movementDirection = MovementDirection.End;

    public UnityEvent OnDestroyEvent { get; } = new();

    public void SetStartPoint(Vector3 point)
    {
        _startPoint = point;
    }

    public void SetEndPoint(Vector3 point)
    {
        _endPoint = point;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetType(PlatformType platformType)
    {
        _platformType = platformType;
    }

    protected bool IsDirectionToStart => _movementDirection == MovementDirection.Start;
}