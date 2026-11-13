using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab; // Tarik prefab musuh ke sini
    public float spawnInterval = 3f; // Waktu jeda antar spawn (dalam detik)
    
    [Header("Spawn Locations")]
    public Transform[] spawnPoints; // Bikin beberapa titik lokasi spawn di ujung-ujung ruangan

    void Start()
    {
        // Memulai siklus auto-generate saat game dimulai
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        // Looping tak terbatas selama spawner ini aktif
        while (true)
        {
            // Tunggu selama waktu interval
            yield return new WaitForSeconds(spawnInterval);

            // Pastikan ada prefab dan titik spawn yang didaftarkan
            if (enemyPrefab != null && spawnPoints.Length > 0)
            {
                // Pilih satu titik lokasi secara acak
                int randomIndex = Random.Range(0, spawnPoints.Length);
                
                // Munculkan musuh di lokasi tersebut
                Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
            }
        }
    }
}