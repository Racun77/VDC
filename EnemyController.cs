using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB; // Referenz auf das Rigidbody2D-Komponente des Gegners
    public float moveSpeed; // Bewegungsgeschwindigkeit des Gegners
    private Transform target; // Referenz auf das Transform des Spielers
    public float damage; // Schaden, den der Gegner verursacht
    public float hitWaitTime = 1f; // Wartezeit zwischen den Angriffen
    private float hitCounter; // Zähler für die Wartezeit zwischen den Angriffen
    public float health = 5f; // Gesundheit des Gegners
    public float knockBackTime = .5f; // Zeit, die der Gegner zurückschreckt, nachdem er getroffen wurde
    private float knockBackCounter; // Zähler für die Rückstoßzeit
    public int expToGive = 1;

    void Start()
    {
        // Finde den Spieler-Controller und setze das Ziel auf dessen Transform
        target = PlayerHealthController.instance.transform;
    }

    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f; // Ändere die Bewegungsgeschwindigkeit in die entgegengesetzte Richtung
            }
            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f); // Setze die Bewegungsgeschwindigkeit auf die Hälfte des ursprünglichen Werts
            }
        }

        theRB.velocity = (target.position - transform.position).normalized * moveSpeed; // Bewege den Gegner in Richtung des Spielers

        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime; // Verringere den Angriffs-Zähler basierend auf vergangener Zeit
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0f)
        {
            PlayerHealthController.instance.TakeDamage(damage); // Füge dem Spieler Schaden hinzu

            hitCounter = hitWaitTime; // Setze den Angriffs-Zähler auf die Wartezeit
        }
    }

    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake; // Verringere die Gesundheit des Gegners um den Schaden

        if (health <= 0)
        {
            Destroy(gameObject); // Zerstöre den Gegner, wenn die Gesundheit auf 0 oder darunter fällt

            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position); // Erzeuge eine Schadensnummer an der Position des Gegners
    }

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake); // Rufe die TakeDamage-Methode auf, um Schaden zu nehmen

        if (shouldKnockback == true)
        {
            knockBackCounter = knockBackTime; // Setze den Rückstoß-Zähler auf die Rückstoßzeit
        }
    }
}
