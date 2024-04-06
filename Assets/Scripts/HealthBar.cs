using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject playerState;

    private float currentHealth, maxHealth;

    private void Awake()
    {
        slider = GetComponent<Slider>();

    }
    private void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;//playerstate'den de�erler �ekildi
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth;//result 1-0 aras�nda olursa slider daha rahat ayarlanabilir
        slider.value = fillValue;
    }
}
