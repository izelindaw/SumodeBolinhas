using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Moeda")]
    public GameObject coinPrefab;

    [Header("Área de Spawn")]
    public float arenaWidth = 8f;
    public float arenaHeight = 8f;

    [Header("Tempo")]
    public float spawnTime = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), 2f, spawnTime);
    }

    void SpawnCoin()
    {
        float x = Random.Range(-arenaWidth, arenaWidth);
        float z = Random.Range(-arenaHeight, arenaHeight);

        Vector3 position = new Vector3(x, 0.5f, z);

        Instantiate(coinPrefab, position, Quaternion.identity);
    }
}