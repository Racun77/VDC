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
    public List<WaveInfo> waves;
    private int currentWave;
    private float waveCounter;

    void Start()
    {
        // spawnCounter = timeToSpawn; // Setze den Spawn-Zähler auf die Ausgangszeit

        target = PlayerHealthController.instance.transform; // Setze das Ziel auf den Spieler
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f; // Berechne die Entfernung, ab der die Gegner despawnen sollen
        currentWave = -1;
        GoToNextWave();
    
    }

    void Update()
    {
        if (PlayerHealthController.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GoToNextWave();
                }
            }

            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = waves[currentWave].timeBetweenSpawns;

                GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
            }
        }

        transform.position = target.position; // Positioniere den Spawner auf die Position des Ziels (Spieler)

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] == null)
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
                else if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                {
                    Destroy(spawnedEnemies[enemyToCheck]);
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
                else
                {
                    enemyToCheck++;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
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
        currentWave++;
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }
        waveCounter = waves[currentWave].wavelength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }

    [System.Serializable]
    public class WaveInfo
    {
        public GameObject enemyToSpawn;
        public float wavelength = 10f;
        public float timeBetweenSpawns = 1f;
    }
}

