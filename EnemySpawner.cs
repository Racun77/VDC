using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn; // Das zu spawnende Gegnerobjekt
    public float timeToSpawn; // Zeit zwischen den Spawns
    private float spawnCounter; // Zähler für den Spawn-Timer
    public Transform minSpawn, maxSpawn; // Bereich, in dem der Spawn stattfinden kann
    private Transform target; // Das Ziel des Spawners
    private float despawnDistance; // Entfernung, ab der die Gegner despawnen sollen
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // Liste der gespawnten Gegner
    public int checkPerFrame; // Anzahl der zu überprüfenden Gegner pro Frame
    private int enemyToCheck; // Index des Gegners, der überprüft wird
    public List<WaveInfo> waves; // Liste der Welleninformationen für die Gegner
    private int currentWave; // Index der aktuellen Welle
    private float waveCounter; // Zähler für den Timer der aktuellen Welle

    void Start()
    {
        // spawnCounter = timeToSpawn; // Setze den Spawn-Zähler auf die Ausgangszeit

        target = PlayerHealthController.instance.transform; // Setze das Ziel auf den Spieler
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f; // Berechne die Entfernung, ab der die Gegner despawnen sollen
        currentWave = -1; // Setzt den Wert von currentWave auf -1 als Startwert
        GoToNextWave(); //Ruft die Methode auf, um zur nächsten Welle zu wechseln
    
    }

    void Update()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf) // Überprüft, ob das Game-Objekt des PlayerHealthControllers aktiv ist
        {
            if (currentWave < waves.Count) // Überprüft, ob der Index currentWave innerhalb der Liste der Wellen liegt
            {
                waveCounter -= Time.deltaTime; // Verringert den Counter der aktuellen Welle basierend auf die vergangene Zeit
                if (waveCounter <= 0)
                {
                    GoToNextWave(); // Wechselt zur nächsten Welle
                }
            }

            spawnCounter -= Time.deltaTime; // Verringert den Spawn Counter basierend auf die vergangene Zeit
            if (spawnCounter <= 0) // Überprüft, ob der Spawn Counter kleiner oder gleich 0 ist
            {
                spawnCounter = waves[currentWave].timeBetweenSpawns; // Setzt den Spawn Counter auf die Zeit zwischen den Spawns der aktuellen Welle

                GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity); // Erzeugt einen neuen Gegner basierend auf der aktuellen Welle
                spawnedEnemies.Add(newEnemy); // Fügt den neu erzeugten Gegner zur Liste der gespawnten Gegner hinzu
            }
        }

        transform.position = target.position; // Positioniere den Spawner auf die Position des Ziels (Spieler)

        int checkTarget = enemyToCheck + checkPerFrame; // Setzt das Überprüfungsziel für die Anzahl der zu überprüfenden Gegner pro Frame

        while (enemyToCheck < checkTarget) // Führt die Schleife aus, solange der Wert von enemyToCheck kleiner als checkTarget ist
        {
            if (enemyToCheck < spawnedEnemies.Count) // Überprüft, ob der Index enemyToCheck innerhalb der Liste der gespawnten Gegner liegt
            {
                if (spawnedEnemies[enemyToCheck] == null) // Überprüft, ob der Gegner null ist, d.h. bereits zerstört wurde
                {
                    spawnedEnemies.RemoveAt(enemyToCheck); // Entfernt den zerstörten Gegner aus der Liste der gespawnten Gegner
                    checkTarget--; // Verringert das Überprüfungsziel, da ein Gegner entfernt wurde
                }
                else if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance) // Überprüft, ob der Gegner die Entfernung überschritten hat, ab der er despawnen soll
                {
                    Destroy(spawnedEnemies[enemyToCheck]); // Zerstört den Gegner
                    spawnedEnemies.RemoveAt(enemyToCheck); // Entfernt den zerstörten Gegner aus der Liste der gespawnten Gegner
                    checkTarget--; // Verringert das Überprüfungsziel, da ein Gegner entfernt wurde
                }
                else
                {
                    enemyToCheck++; // Inkrementiert den Index enemyToCheck, um den nächsten Gegner zu überprüfen
                }
            }
            else
            {
                enemyToCheck = 0; // Setzt enemyToCheck auf 0, um von vorne zu beginnen
                checkTarget = 0; // Setzt das Überprüfungsziel auf 0, um die Schleife zu beenden
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = (Random.Range(0f, 1f) > .5f); // Entscheide zufällig, ob der Spawn-Punkt an einem vertikalen Rand liegt

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) < .5f)
            {
                spawnPoint.x = minSpawn.position.x; // Verschiebe den Spawn-Punkt nach links
            }
            else
            {
                spawnPoint.x = maxSpawn.position.x; // Verschiebe den Spawn-Punkt nach rechts
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) < .5f)
            {
                spawnPoint.y = minSpawn.position.y; // Verschiebe den Spawn-Punkt nach unten
            }
            else
            {
                spawnPoint.y = maxSpawn.position.y; // Verschiebe den Spawn-Punkt nach oben
            }
        }

        return spawnPoint; // Gib den ausgewählten Spawn-Punkt zurück
    }

    public void GoToNextWave()
    {
        currentWave++; // Inkrementiert den Wert von currentWave, um zur nächsten Welle zu wechseln
        if (currentWave >= waves.Count) // Überprüft, ob der Wert von currentWave größer oder gleich der Anzahl der Wellen in der Liste ist
    {
        {
            currentWave = waves.Count - 1; // Setzt den Wert von currentWave auf das letzte Element der Liste, um sicherzustellen, dass er innerhalb des gültigen Bereichs bleibt
        }
        waveCounter = waves[currentWave].wavelength; // Setzt den Welle-Zähler auf die Dauer der aktuellen Welle
        spawnCounter = waves[currentWave].timeBetweenSpawns; // Setzt den Spawn-Zähler auf die Zeit zwischen den Spawns der aktuellen Welle
    }

    [System.Serializable] // Kennzeichnet die Klasse als serialisierbar, um ihre Daten im Editor anpassen zu können
    public class WaveInfo // Definition der Klasse WaveInfo, die serialisierbar ist
    {
        public GameObject enemyToSpawn; // Das zu spawnende Gegnerobjekt für die Welle
        public float wavelength = 10f; // Dauer der Welle
        public float timeBetweenSpawns = 1f; // Zeit zwischen den Spawns innerhalb der Welle
    }
}
