using UnityEngine;

public class FibonacciSpawner : MonoBehaviour
{
    [Header("Prefab y punto de spawn")]
    public GameObject unidadPrefab; 
    public GameObject puntoSpawn;

    [Header("Configuración")]
    public float intervalo = 10f;   

    private int indice = 0;         
    private float tiempoSiguiente;   
    private int[] fibCache = new int[100];

    void Start()
    {
        tiempoSiguiente = intervalo;
        fibCache[0] = 0;
        fibCache[1] = 1;
    }

    void Update()
    {
        if (Time.time >= tiempoSiguiente)
        {
            int cantidad = Fibonacci(indice);
            CrearUnidades(cantidad);
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

    void CrearUnidades(int cantidad)
    {
        if (unidadPrefab == null || puntoSpawn == null)
        {
            Debug.LogWarning("Asigna el prefab y el punto de spawn en el inspector.");
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            Instantiate(unidadPrefab, puntoSpawn.transform.position, Quaternion.identity);
        }

        Debug.Log($"[FibonacciSpawner] Segundo {(int)tiempoSiguiente}: creadas {cantidad} unidades (índice {indice}).");
    }
}
