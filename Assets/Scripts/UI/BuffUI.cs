using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    private void Start()
    {
        PlayerPowerupsPickup.Instance.OnPowerupPicked += Instance_OnPowerupPicked;
    }

    private void Instance_OnPowerupPicked(PowerupsSO obj)
    {
        StartCoroutine(ShowBuffIcon(obj));
    }
    private IEnumerator ShowBuffIcon(PowerupsSO powerupSO)
    {
        //float duration = powerupSO.PowerupPrefab.GetComponent<PowerupBase>().Duration;
        //Image powerupIcon = powerupSO.PowerupIcon;
        //Image buffIcon = Instantiate(powerupIcon, transform);
        //yield return new WaitForSeconds(duration);
        //Destroy(buffIcon);
        float duration = powerupSO.PowerupPrefab.GetComponent<PowerupBase>().Duration;
        Image powerupIcon = powerupSO.PowerupIcon;

        // Створюємо копію іконки
        Image buffIcon = Instantiate(powerupIcon, transform);

        float blinkStartTime = Time.time + duration * 0.8f; // почати мигати на останніх 20%
        float endTime = Time.time + duration;

        bool blinking = false;
        float blinkSpeed = 4f; // скільки разів в секунду буде мигати

        Color originalColor = buffIcon.color;

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
                buffIcon.color = newColor;
            }

            yield return null;
        }

        Destroy(buffIcon);

    }
}
