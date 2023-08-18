using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Access our virus prefab.
    [SerializeField] 
    private GameObject _virusPrefab;

    private bool _spawningOn = true;

    /// Here we start our coroutine.
    private void Start()
    {
        StartCoroutine(SpawnProcedure());
    }

    /// This method basically runs in the background to delay the instantiation of a new virus.
    private IEnumerator SpawnProcedure()
    {
        // Instantiate the virus and wait for a given time.
        while (_spawningOn)
        {
            Vector3 position = new Vector3(Random.Range(-10.6f, 10.6f), 8.9f, 0);
            Instantiate(_virusPrefab, position, Quaternion.identity).transform.SetParent(transform);
            yield return new WaitForSeconds(1);
        }
        Destroy(this.gameObject);
    }

    public void OnPlayerDeath()
    {
        _spawningOn = false;
    }
}
