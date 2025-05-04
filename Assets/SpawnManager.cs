using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject BulletPrefab;


    private void Start()
    {
        InvokeRepeating("Spawn", 2, 1);
    }

    void Spawn()
    {
        int idx = Random.Range(0, spawnPoints.Length);
        Instantiate(BulletPrefab, spawnPoints[idx].position, Quaternion.identity);
    }

   
}
