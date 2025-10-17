using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    // cantidad de dinero
    [SerializeField] private int income = 10;

    private void Start()
    {
        // gana 1 dinero cada 5 segundos
        StartCoroutine(AddMoney());
    }

    // tiene que ser interface pero no se porque
    private IEnumerator AddMoney()
    {
        while (true)
        {
            // 5 segundos
            yield return new WaitForSeconds(5f);
            // 1 dinero
            Game.Instance.AddResources(income);
        }
    }
}

