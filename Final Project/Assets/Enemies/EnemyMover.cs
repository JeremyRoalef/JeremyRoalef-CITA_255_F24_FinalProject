using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class EnemyMover : MonoBehaviour
{
    //Serialized Fields
    [SerializeField]
    [Min(0)]
    float fltVelocity = 3f;

    //Cashe References
    Rigidbody rb;
    GameObject player;

    //Attributes

    private void Start()
    {
        PlayerMover.OnPlayerDeath += EnemyDies;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerMover>().gameObject;
    }

    void Update()
    {
        //player pos - enemy pos normalized = direction of velocity
        Vector3 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * fltVelocity;
    }

    void EnemyDies()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlayerMover.OnPlayerDeath -= EnemyDies;
    }
}
