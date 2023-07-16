using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    public void Awake()
    {
        instance = this; // Stellt sicher, dass auf die Instanz dieses Skripts von überall aus zugegriffen werden kann
    }

    public DamageNumber numberToSpawn; // Referenz auf das Schadensnummer-Prefab
    public Transform numberCanvas; // Referenz auf das Eltern-Canvas, auf dem die Schadensnummern platziert werden sollen

    private List<DamageNumber> numberPool = new List<DamageNumber>(); // Pool für Schadensnummern

    void Start()
    {
        // Start-Methode ist leer, hier können weitere Initialisierungen vorgenommen werden
    }

    // Methode zum Erzeugen einer Schadensnummer
    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damageAmount); // Rundet den Schaden auf die nächste ganze Zahl

        DamageNumber newDamage = GetFromPool(); // Holt eine Schadensnummer aus dem Pool

        newDamage.Setup(rounded); // Ruft die Setup-Methode des DamageNumber-Skripts auf und übergibt die gerundete Schadensmenge
        newDamage.gameObject.SetActive(true); // Aktiviert das DamageNumber-Objekt, um es sichtbar zu machen
        newDamage.transform.position = location; // Setzt die Position der Schadensnummer auf die gegebene Position
    }

    // Methode zum Holen einer Schadensnummer aus dem Pool oder Erzeugen einer neuen Schadensnummer, falls der Pool leer ist
    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutput = null;

        if (numberPool.Count == 0)
        {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas); // Instanziert eine neue Schadensnummer, falls der Pool leer ist
        }
        else
        {
            numberToOutput = numberPool[0]; // Holt eine Schadensnummer aus dem Pool
            numberPool.RemoveAt(0); // Entfernt die Schadensnummer aus dem Pool
        }

        return numberToOutput;
    }

    // Methode zum Platzieren einer Schadensnummer zurück in den Pool
    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false); // Deaktiviert die Schadensnummer, um sie unsichtbar zu machen
        numberPool.Add(numberToPlace); // Fügt die Schadensnummer dem Pool hinzu
    }
}
