using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animation;
    [SerializeField] private float _thrustForce = 5f;
    [SerializeField] private float _jumpHeight = 2f;

    [Header("Ground")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private Vector3 _groundCheckSize = new Vector3(1,0.2f, 1);
    
    [Header("Jetpack Particles")]
    [SerializeField] private ParticleSystem _jetPackThrustEffectLeft;
    [SerializeField] private ParticleSystem _jetPackThrustEffectRights;

    private Vector3 _velocity;

    private bool _isGrounded;

    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Update()
    {
        _isGrounded = Physics.CheckBox(_groundCheckTransform.position, _groundCheckSize, Quaternion.identity,
            _groundLayer, QueryTriggerInteraction.Ignore);

        if (Input.GetButton("Jump") && _isGrounded)
        {
            _animation.SetTrigger(Jump);
            if (!_jetPackThrustEffectLeft.isPlaying)
                _jetPackThrustEffectLeft.Play();
            if (!_jetPackThrustEffectRights.isPlaying)
                _jetPackThrustEffectRights.Play();
        }
        else
        {
            _jetPackThrustEffectLeft.Stop();
            _jetPackThrustEffectRights.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && _isGrounded)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = _jumpHeight;
            _rigidbody.velocity = velocity;
        }
    }

    // TODO: do we need to delete the object itself?
    // private void OnTriggerEnter(Collider collider)
    // {
    //     if (collider.CompareTag(TagManager.DestroyZone))
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_groundCheckTransform.position, _groundCheckSize);
    }
}
 