using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void LateUpdate()
    {
        if (_target != null && _target.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
        }
    }
}
