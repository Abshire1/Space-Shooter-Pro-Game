using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY : MonoBehaviour
{
    //speed for enemy going down 
    private float _speed = 3.0f;
    private PLAYER _player;
    //Handle to animator component//
    private Animator _anim;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canfire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PLAYER>();
        _audioSource = GetComponent<AudioSource>();
        //Null check for player 
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");

        }
        //Assign the Component to Anim
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canfire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canfire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            LASER[] lasers = enemyLaser.GetComponentsInChildren<LASER>();
            lasers[0].AssignEnemyLaser();
            lasers[1].AssignEnemyLaser();
            


        }

    }
    void CalculateMovement ()
    {
        //Move Enemy Down 4 Meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //when Enemy is at the bottom of the screen 
        //Respwan Enemy at the top with a Random  X Position(Meaning it will respawn randomly from the top going to bottom 

        if (transform.position.y < -5f)
        {
            //CODE Clean up for Enemy Ramdom Values
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
         
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
       
        //THIS IS THE LOGIC FOR THE PLAYER COLLISION//
        if (other.tag =="Player")
        {
            //WE have PLAYER in PLAYER Script Acessing The Player In Enemy//
            PLAYER player = other.GetComponent<PLAYER>();
           
            //IF Player is not Null/Null is checking for componets making sure its not Empty 
            if(player != null)
            {
                player.Damage();
                //This will Damage the player//
            }

            //Before enemy is destroyed, trigger the animation

            //DAMAGE ENEMY with PLAYER 
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
            
        }
        
        
       //THIS IS THE LOGIC FOR THE LASER COLLISION//
        //HERE WE ARE DESTROYING THE LASER AND ENEMY
        if(other.tag =="LASER")
        {
            Destroy(other.gameObject);
            _speed = 0;
            //ADD 10 TO SCORE 
            if (_player != null)
            {
                _player.AddScore();

            }
            //Before laser is destroyed, trigger Animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
            

        }
    }


    
}   













     



     
