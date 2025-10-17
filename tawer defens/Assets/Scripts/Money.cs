using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour
{
    // cantidad de dinero
    [SerializeField] private int amount = 0;

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
            amount += 1;
        }
    }

    // funciones para ganar y gastar
    public void Add(int value)
    {
        amount += value;
    }

    public bool Spend(int value)
    {
        if (amount >= value)
        {
            amount -= value;
            //previene gastar mas de lo que se tiene
            return true;
        }
        return false;
    }
}

