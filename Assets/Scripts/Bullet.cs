using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 10f;
    
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        // If bullet position is too high (for camera), destroy it
        if (transform.position.y >= 8.5)
        {
            Destroy(this.gameObject);
        }
    }
}
