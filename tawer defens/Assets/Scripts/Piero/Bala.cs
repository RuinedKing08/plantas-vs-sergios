using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Enemy"))
        {
            Destroy(colision.gameObject);
            Destroy(gameObject);           
        }
        else
        {
            Destroy(gameObject);         
        }
    }
}
