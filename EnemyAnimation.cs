using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite; // Referenz auf das Sprite des Gegners

    public float speed; // Geschwindigkeit der Skalierungsanimation

    public float minSize, maxSize; // Minimale und maximale Skalierungsgröße

    private float activeSize; // Aktuelle Skalierungsgröße

    void Start()
    {
        activeSize = maxSize; // Setze die aktuelle Skalierungsgröße auf die maximale Größe

        speed = speed * Random.Range(.75f, 1.25f); // Passe die Geschwindigkeit basierend auf einem zufälligen Faktor an
    }

    void Update()
    {
        // Verändere die Skalierung des Sprites allmählich zur aktuellen Größe hin
        sprite.transform.localScale = Vector3.MoveTowards(sprite.transform.localScale, Vector3.one * activeSize, speed * Time.deltaTime);

        // Überprüfe, ob die Skalierung die aktuelle Größe erreicht hat
        if (sprite.transform.localScale.x == activeSize)
        {
            if (activeSize == maxSize) // Wenn die aktuelle Größe die maximale Größe ist
            {
                activeSize = minSize; // Setze die aktuelle Größe auf die minimale Größe
            }
            else
            {
                activeSize = maxSize; // Andernfalls setze die aktuelle Größe auf die maximale Größe
            }
        }
    }
}
