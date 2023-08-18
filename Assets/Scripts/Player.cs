using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 5f;
    
    [SerializeField] 
    private GameObject _bulletPrefab;
    
    [SerializeField] 
    private float _spawnRate = 0.3f;
    private float _bulletSpawn = -1f;

    [SerializeField] 
    private int _lives = 3;

    private SpawnManager _spawnManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        // player has initial position at (0,0,0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    private void Update()
    {
        MovementBehaviour();
        Bullet();
    }

    private void MovementBehaviour()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        // Movement on the player (Time.deltaTime smoothens the movement)
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        
        // Set movement boundaries for the player
        // Restrict movement on y axis
        if (transform.position.y >= 7.76f)
        {
            transform.position = new Vector3(transform.position.x, 7.76f, 0);
        }
        else if (transform.position.y <= -1.45f)
        {
            transform.position = new Vector3(transform.position.x, -1.45f, 0);
        }
        
        // Transition of position when moving out of boundaries on x axis
        if (transform.position.x >= 10.6f)
        {
            transform.position = new Vector3(-10.6f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.6f)
        {
            transform.position = new Vector3(10.6f, transform.position.y, 0);
        }
    }

    private void Bullet()
    {
        // Spawn bullet when space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _bulletSpawn)
        {
            _bulletSpawn = Time.time + _spawnRate;
            Instantiate(_bulletPrefab, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        // Reduce lives by one, when hit by a virus.
        _lives--;
        // If there are zero lives left, the player will die.
        if (_lives < 1)
        {
            Debug.Log("Oh no, you are dead :(");
            Destroy(this.gameObject);
            
            // Null reference check: If not null, access spawn manager method "OnPlayerDeath()".
            if (_spawnManager != null)
            {
                _spawnManager.OnPlayerDeath();
            } else { Debug.Log("Spawn manager is missing."); }
        }
        // Keep track of the lives left.
        Debug.Log(string.Format("You got hit! You have {0} lives left.", _lives));
    }
}
