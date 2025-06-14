using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Проста «структура даних» для активної анімації
public class ActiveBuffData
{
    public Coroutine Routine;
    public Image Icon;
}

public class BuffUI : MonoBehaviour
{
    private readonly Dictionary<PowerupsSO, ActiveBuffData> _activeBuffs= new Dictionary<PowerupsSO, ActiveBuffData>();
    private void Instance_OnPowerupPicked(PowerupsSO obj)
    {
        if (_activeBuffs.TryGetValue(obj, out var oldData))
        {
            if (oldData.Routine != null) StopCoroutine(oldData.Routine);
            if (oldData.Icon != null) Destroy(oldData.Icon.gameObject);
        }
        Image newIcon = Instantiate(obj.PowerupIcon, transform);
        float duration = obj.PowerupPrefab.GetComponent<PowerupBase>().Duration;
        Coroutine routine = StartCoroutine(ShowBuffIcon(newIcon, duration));
        _activeBuffs[obj] = new ActiveBuffData
        {
            Routine = routine,
            Icon = newIcon
        };
    }

    private void Start()
    {
        PlayerPowerupsPickup.Instance.OnPowerupPicked += Instance_OnPowerupPicked;
    }


    private IEnumerator ShowBuffIcon(Image powerupIcon, float duration)
    {

        float blinkStartTime = Time.time + duration * 0.8f;
        float endTime = Time.time + duration;

        bool blinking = false;
        float blinkSpeed = 4f;

        Color originalColor = powerupIcon.color;

        while (Time.time < endTime)
        {
            if (Time.time >= blinkStartTime)
            {
                blinking = true;
            }

            if (blinking)
            {
                float alpha = Mathf.Lerp(0.2f, 1f, Mathf.PingPong(Time.time * blinkSpeed, 1));
                Color newColor = originalColor;
                newColor.a = alpha;
                powerupIcon.color = newColor;
            }

            yield return null;
        }

        Destroy(powerupIcon.gameObject);

    }
}
