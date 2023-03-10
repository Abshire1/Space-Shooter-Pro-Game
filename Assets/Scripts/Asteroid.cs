using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 6.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    // Start is called before the first frame update
    private SpawnManager _spawnManager; 
   private void Start()
   {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
   }

    // Update is called once per frame
    void Update()
    {
        //ROTATE OBJECT ON THE ZED AXIS//
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    //Check for laser collision of type trigger 
    //Instatiate explostion at the postion of the Asteroid
    //Destroy the Explosion after 3 secs 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "LASER") 
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject); //Destroying Laser 
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.25f); //Destroying Asteroid
        }


    }


}
