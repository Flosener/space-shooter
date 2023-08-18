using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Virus : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 3f;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -3f)
        {
            transform.position = new Vector3(Random.Range(-10.6f, 10.6f), 8.9f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Virus hit the " + other.transform.name);
        // If the other object is the player, we want to damage the player and destroy virus object
        // Else: destroy the pill and destroy the virus
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Damage();
            // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
            Destroy(this.gameObject);
        }
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
