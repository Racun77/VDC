using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageText; // Referenz auf das TextMeshPro-Objekt, das den Schadenstext anzeigt
    public float lifetime; // Lebensdauer der Schadensnummer
    private float lifeCounter; // Zähler für die verbleibende Lebensdauer
    public float floatSpeed = 1f; // Geschwindigkeit, mit der die Schadensnummer aufsteigt

    void Update()
    {
        if (lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime; // Verringere den Lebensdauer-Zähler basierend auf vergangener Zeit

            if (lifeCounter <= 0)
            {
                DamageNumberController.instance.PlaceInPool(this); // Platziere die Schadensnummer im Pool, wenn die Lebensdauer abgelaufen ist
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime; // Lasse die Schadensnummer nach oben "schweben"
    }

    // Methode zum Einrichten der Schadensnummer
    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime; // Setze den Lebensdauer-Zähler auf den angegebenen Wert
        damageText.text = damageDisplay.ToString(); // Aktualisiere den Schadenstext mit dem angegebenen Schaden
    }
}
