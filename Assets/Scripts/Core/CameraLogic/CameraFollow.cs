using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothness;
    [Space]
    [SerializeField] private bool _followByX;
    [SerializeField] private bool _followByY;
    [SerializeField] private bool _followByZ;

    private void LateUpdate()
    {
        HandleCameraFollow();
    }
    private void HandleCameraFollow()
    {
        var targetPosition = new Vector3(
            _followByX ? _target.position.x : transform.position.x,
            _followByY ? _target.position.y : transform.position.y,
            _followByZ ? _target.position.z : transform.position.z
            );

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothness);
    }
}
