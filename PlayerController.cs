using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed; // Die Geschwindigkeit des Spielers
    public Animator anim; // Der Animator für die Animationen des Spielers
    public float pickupRange = 1.5f;
    public Weapon activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        // Diese Funktion wird zu Beginn des Spiels aufgerufen und initialisiert den Spieler
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f); // Der Eingabevektor für die Spielerbewegung
        moveInput.x = Input.GetAxisRaw("Horizontal"); // Erfasse die horizontale Eingabe (Achse "Horizontal")
        moveInput.y = Input.GetAxisRaw("Vertical"); // Erfasse die vertikale Eingabe (Achse "Vertical")

        Debug.Log(moveInput); // Gib den Bewegungsvektor in der Konsole aus
        moveInput.Normalize(); // Normalisiere den Bewegungsvektor, um eine gleichmäßige Geschwindigkeit in alle Richtungen zu gewährleisten
        transform.position += moveInput * moveSpeed * Time.deltaTime; // Bewege den Spieler entsprechend der Eingabe und der Geschwindigkeit

        if (moveInput != Vector3.zero) // Überprüfe, ob der Spieler sich bewegt
        {
            anim.SetBool("isMoving", true); // Setze die Animator-Variable "isMoving" auf "true", um die Bewegungsanimation abzuspielen
        }
        else
        {
            anim.SetBool("isMoving", false); // Setze die Animator-Variable "isMoving" auf "false", um die Bewegungsanimation zu stoppen
        }
    }
}
