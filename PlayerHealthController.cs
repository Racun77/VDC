using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; // Instanz des Spieler-Gesundheitscontrollers

    public float currentHealth; // Aktuelle Gesundheit des Spielers
    public float maxHealth; // Maximale Gesundheit des Spielers

    public Slider healthSlider; // Slider zur Anzeige der Gesundheit des Spielers

    private void Awake()
    {
        instance = this; // Setze die Instanz auf diese Klasse
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Setze die aktuelle Gesundheit auf die maximale Gesundheit

        healthSlider.maxValue = maxHealth; // Setze den Maximalwert des Sliders auf die maximale Gesundheit
        healthSlider.value = currentHealth; // Setze den aktuellen Wert des Sliders auf die aktuelle Gesundheit
    }

    // Update is called once per frame
    void Update()
    {
        // Hier können weitere Aktualisierungen des Spieler-Gesundheitscontrollers vorgenommen werden, falls erforderlich
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake; // Ziehe den Schaden von der aktuellen Gesundheit ab

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); // Deaktiviere das Spielobjekt (Spieler), wenn die Gesundheit auf oder unter 0 fällt
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Aktualisiere den Wert des Sliders entsprechend der aktuellen Gesundheit
        }
    }
}
