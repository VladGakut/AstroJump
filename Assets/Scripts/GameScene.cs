using UnityEngine;
using Random = UnityEngine.Random;

public class GameScene : MonoBehaviour
{
    [SerializeField] private CharacterController _character;
    
    [SerializeField] private PlatformsController _platformsController;
    
    [Header("Platform Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    
    [SerializeField] private float _minSpeed = 3f;
    [SerializeField] private float _maxSpeed = 5f;

    [SerializeField] private float _minDistanceBetweenPlatforms = 3f;
    [SerializeField] private float _maxDistanceBetweenPlatforms = 5f;
    
    private Vector3 _platformSpawnPosition;
    
    private void Awake()
    {
        InitializeLevel();
    }

    private void OnDestroy()
    {
        _platformsController.RemoveAll();
    }

    private void Update()
    {
        if (IsNewPlatformNeeded())
        {
            AddPlatform();
        }
    }

    private void InitializeLevel()
    {
        for (int i = 0; i < 10; i++)
        {
            AddPlatform();
        }
    }
    
    private void AddPlatform()
    {
        float speed = Random.Range(_minSpeed, _maxSpeed);
        
        _platformSpawnPosition.x = Random.Range(_startPoint.position.x, _endPoint.position.x);
        _platformSpawnPosition.y += Random.Range(_minDistanceBetweenPlatforms, _maxDistanceBetweenPlatforms);

        PlatformType type = (PlatformType)Random.Range((int)PlatformType.Default, (int)PlatformType.Count - 1);
        
        _platformsController.Add(_platformSpawnPosition, _startPoint.position, _endPoint.position, speed, type);
    }

    private bool IsNewPlatformNeeded()
    {
        float distance = Mathf.Abs(_character.transform.position.y - _platformSpawnPosition.y);

        return distance < _maxDistanceBetweenPlatforms * 2;
    }
}
