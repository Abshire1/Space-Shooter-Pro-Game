using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   //STORE THE OBJECT WE WANT TO SPAWN
   [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject [] powerups;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void StartSpawning()

    {
        StartCoroutine("SpawnEnemyRoutine");
        StartCoroutine(SpawnPowerupRoutine());

    }


    // Update is called once per frame
    void Update()
    {


    }   
        //Spwan Game Objects every 5 secs//
     //CREATE A COROUTINE OF TYPE IEnumerator WHICH WILL ALLOWS EVENTS TO YIELD(WAIT/SPAWN TIME)
     //DEFINE A COROUTINE OF TYPE IEnumerator/ CALLING IT SPWAN/ 
     IEnumerator SpawnEnemyRoutine()
     {
        yield return new WaitForSeconds(3.0f);
        
        //THIS WILL COMINICATE WITH THE SPAWN MANAGER SAYING STOP SPAWNING IS TRUE
        while(_stopSpawning == false)
        {
            //DO NOT DO THIS UNTILL YOU HAVE A YIELD STATEMENT A CREATED A VAERIABLE private GameObject _enemyPrefab;
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
         
            //Adding newEnemy is part of the process of the Enemy Container//
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            
            //The method below is the have enemy clones Inside the Enemy Container//
            newEnemy.transform.parent = _enemyContainer.transform;
           
            yield return new WaitForSeconds(5.0f);
            //THIS WILL MAKE IT WHERE WE WILL NEVER EXIT THE while loop//
            //THIS IS AN INFINITE LOOP//
            //WHILE WE ARE IN THIS LOOP WE WILL BE BALE TO WAIT 5 SECS AND START AGAIN(REPEATIVLY)
            //BECAUSE IT IS AN INFINITE LOOP//
            //AFTER THIS WE MUST START A COROUNTINE OTHER WISE THIS CODE WILL NOT WORK 
            //WE NEED TO CALL A SPECIAL METHOD CALLED START COROUNTINE

        }
        
     }
    //Staring IEnumerator for Triple Shot Power up to Spawn every 3-7 secs for random spawning
    private IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        //every 3-7 secs, Spawn in Power up
        while (_stopSpawning == false) //This will make the triple shot power up stop Spawning if and when the player dies 
         {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
           //Creating random Spawning for triple shot and speed power up//
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
      

    }




    //ONCE this happpens we will be out of the loop we created for our ENEMY// WE WILL THEN BE DONE WITH THE COROUTINE
    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }





}
