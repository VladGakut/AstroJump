using System.Collections.Generic;
using UnityEngine;

public class PlatformsController : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;

    private readonly List<IPlatform> _platforms = new();

    public void Add(Vector3 spawnPosition, Vector3 startPoint, Vector3 endPoint, float speed, PlatformType type)
    {
        AbstractPlatform platform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity, transform)
            .GetComponent<AbstractPlatform>();
        
        platform.SetStartPoint(startPoint);
        platform.SetEndPoint(endPoint);
        platform.SetSpeed(speed);
        platform.SetType(type);
        platform.OnDestroyEvent.AddListener(() => Remove(platform));
        
        _platforms.Add(platform);
    }

    public void MovePlatformToUp(AbstractPlatform platform)
    {
        
    }

    public void Remove(AbstractPlatform platform)
    {
        Destroy(platform.gameObject);
        _platforms.Remove(platform); // TODO: using Queue for optimization;
    }

    public void RemoveAll()
    {
        _platforms.Clear();
    }
}
