using UnityEngine;

public class PlayerWrapAround : MonoBehaviour
{
    private PlayerEffects _playerEffects;

    private void Awake()
    {
        _playerEffects = GetComponent<PlayerEffects>();
    }
    private void LateUpdate()
    {
        HandlePlayerWrapUp();
    }
    private void HandlePlayerWrapUp()
    {
        Vector3 pos = transform.position;

        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        float leftEdge = Camera.main.transform.position.x - halfWidth;
        float rightEdge = Camera.main.transform.position.x + halfWidth;

        if (pos.x < leftEdge)
        {
            pos.x = rightEdge;

        }
        if (pos.x > rightEdge)
        {
            pos.x = leftEdge;

        }
        transform.position = pos;
    }
}
