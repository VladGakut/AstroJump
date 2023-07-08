using UnityEngine;

public class Jetpack : MonoBehaviour
{
    [SerializeField] private float _force = 0.05f;

    [SerializeField] private Vector3 _direction;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0f)
        {
            Vector3 direction = _rigidbody.transform.up + _rigidbody.transform.forward;
            
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        }
    }
}
