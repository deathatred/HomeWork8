using UnityEngine;

public class MovingPlatform : Platform 
{
    public float speed = 1f;
    public float height = 1f;

    Vector3 _startPos;

    private float _randomOffset;
    private void Start()
    {
        _startPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();

        _randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }
    private void FixedUpdate()
    {
        if (_isAlive)
        {
            float yOffset = Mathf.Sin(Time.time * speed + _randomOffset) * height;
            Vector2 newPosition = _startPos + new Vector3(0f, yOffset, 0f);
            _rb.MovePosition(newPosition);
        }
    }
}
