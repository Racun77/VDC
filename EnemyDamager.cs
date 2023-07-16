using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount; // Schaden, den dieser Schadensverursacher verursacht
    public float lifeTime, growSpeed = 5f; // Lebensdauer und Wachstumsgeschwindigkeit des Schadensverursachers
    private Vector3 targetSize; // Zielgröße für das Wachstum des Schadensverursachers
    public bool shouldKnockBack; // Gibt an, ob ein Rückstoß angewendet werden soll
    public bool destroyParent;


    void Start()
    {
        // Destroy(gameObject, lifeTime);
        targetSize = transform.localScale; // Setze die Zielgröße auf die aktuelle Skalierung des Schadensverursachers
        transform.localScale = Vector3.zero; // Setze die Skalierung auf Null, um den Schadensverursacher zu verstecken
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime); // Verändere die Skalierung allmählich zur Zielgröße hin
        lifeTime -= Time.deltaTime; // Verringere die Lebensdauer basierend auf vergangener Zeit

        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero; // Setze die Zielgröße auf Null, um den Schadensverursacher schrumpfen zu lassen

            if (transform.localScale.x == 0f) // Überprüfe, ob der Schadensverursacher vollständig geschrumpft ist
            {
                Destroy(gameObject); // Zerstöre den Schadensverursacher

                if (destroyParent)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") // Überprüfe, ob der Kollisionsgegenstand das Tag "Enemy" hat
        {
            collision.GetComponent<EnemyController>().TakeDamage(damageAmount, shouldKnockBack); // Verursache Schaden am Kollisionsgegenstand (Gegner)
        }
    }
}
