using UnityEngine;

public class Platform : AbstractPlatform
{
    private void Update()
    {
        if (IsDirectionToStart && transform.localPosition.x < _startPoint.x)
        {
            _movementDirection = MovementDirection.End;
        } 
        else if (!IsDirectionToStart && transform.localPosition.x > _endPoint.x)
        {
            _movementDirection = MovementDirection.Start;
        }

        Vector3 direction = IsDirectionToStart ? Vector3.left : Vector3.right;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + direction,
            _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(TagManager.DestroyZone))
        {
            OnDestroyEvent?.Invoke();
        }
    }
}
