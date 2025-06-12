using UnityEngine;

public class Water : MonoBehaviour
{
    private float startSpeed = 1.0f;
    private float acceleration = 0.1f;
    private float maxSpeed = 6.5f;

    private float currentSpeed;
    private float timeElapsed;

    private void Awake()
    {
        currentSpeed = startSpeed;
    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        currentSpeed = Mathf.Min(startSpeed+ acceleration* timeElapsed, maxSpeed);
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }

}
