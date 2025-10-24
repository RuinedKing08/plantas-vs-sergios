using System.Collections;
using UnityEngine;

public class FibonacciSpawner : MonoBehaviour
{
    [Header("Prefab y punto de spawn")]
    public GameObject[] unidadPrefab;   // Prefab que se va a instanciar
    public GameObject puntoSpawn;     // Cualquier objeto de la jerarquía o prefab

    [Header("Configuración")]
    public float intervalo = 10f;     // Cada cuántos segundos se genera una nueva cantidad
    [SerializeField] private float spawnDelay = 0.5f;

    private int indice = 0;           // Posición en la serie de Fibonacci
    private float tiempoSiguiente;    // Próximo instante de spawn
    private int[] fibCache = new int[100];
    private bool spawningWave = false;

    void Start()
    {
        tiempoSiguiente = intervalo;
        fibCache[0] = 0;
        fibCache[1] = 1;
    }


    private void Update()
    {
        if (!spawningWave && Time.time >= tiempoSiguiente)
        {
            int cantidad = Fibonacci(indice);
            StartCoroutine(SpawnWave(cantidad));

            indice++;
            tiempoSiguiente += intervalo;
        }
    }

    int Fibonacci(int n)
    {
        if (n < 2) return fibCache[n];
        if (fibCache[n] != 0) return fibCache[n];
        fibCache[n] = Fibonacci(n - 1) + Fibonacci(n - 2);
        return fibCache[n];
    }

    private IEnumerator SpawnWave(int cantidad)
    {
        if (unidadPrefab == null || unidadPrefab.Length == 0 || puntoSpawn == null)
        {
            yield break;
        }

        spawningWave = true;
        Debug.Log($"[FibonacciSpawner] Iniciando oleada {indice} ({cantidad} enemigos).");

        for (int i = 0; i < cantidad; i++)
        {
            int r = Random.Range(0, unidadPrefab.Length);
            GameObject prefab = unidadPrefab[r];

            GameObject clone = Instantiate(prefab, puntoSpawn.transform.position, Quaternion.identity);

            if (clone.TryGetComponent(out BaseEnemy be))
            {
                be.Initialize();
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        spawningWave = false;
        Debug.Log($"[FibonacciSpawner] Oleada {indice} completada ({cantidad} enemigos generados).");
    }
}
