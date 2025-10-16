using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 20f;
    [SerializeField] private float launchForce = 5f;

    private Rigidbody cachedRigidbody;

    private void Awake()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cachedRigidbody.linearVelocity = transform.forward * initialSpeed;
        cachedRigidbody.AddForce(transform.forward * launchForce, ForceMode.Impulse);

        Destroy(gameObject, 3f);
    }
}
