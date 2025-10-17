using UnityEngine;

public class Torre : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDisparo;  
    [SerializeField] private float rango = 15f;
    [SerializeField] private float tiempoEntreDisparos = 1f;

    private float temporizador = 0f;
    private Transform objetivo;

    void Update()
    {
        BuscarObjetivo();

        if (objetivo != null)
        {
         
            Vector3 direccion = objetivo.position - transform.position;
            direccion.y = 0;
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = rotacion;

            temporizador += Time.deltaTime;
            if (temporizador >= tiempoEntreDisparos)
            {
                Disparar();
                temporizador = 0f;
            }
        }
    }

    void BuscarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");

        float distanciaMinima = Mathf.Infinity;
        Transform enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMinima && distancia <= rango)
            {
                distanciaMinima = distancia;
                enemigoCercano = enemigo.transform;
            }
        }

        objetivo = enemigoCercano;
    }

    void Disparar()
    {
        if (prefabBala != null && puntoDisparo != null)
        {
            Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        }
    }
}
