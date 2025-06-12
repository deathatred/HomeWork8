using System;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static Water Instance { get; private set; }

    public event EventHandler OnPlayerTouchedWater;
    private float startSpeed = 1.0f;
    private float acceleration = 0.1f;
    private float maxSpeed = 6.5f;

    private float currentSpeed;
    private float timeElapsed;

    private void Awake()
    {
        Instance = this;
        currentSpeed = startSpeed;
    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        currentSpeed = Mathf.Min(startSpeed+ acceleration* timeElapsed, maxSpeed);
        transform.position += Vector3.up * currentSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("ENTER?");
        if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerTouchedWater?.Invoke(this, EventArgs.Empty);
        }
    }

}
