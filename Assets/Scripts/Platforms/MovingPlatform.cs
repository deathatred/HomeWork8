using UnityEngine;

public class MovingPlatform : Platform 
{
    public float speed = 1f;
    public float height = 1f;

    Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }
    private void Update()
    {
        if (_isAlive) 
        {
            float yOffset = Mathf.Sin(Time.time * speed);
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }
    }
}
