using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 3.0f;
    [SerializeField] // 0 = TRIPLE SHOT/ 1 = SPEED/ 2 = SHIELD//ID'S FOR POWERUPS 
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at the speed of 3 (Adjust in the inspector so our vareiable needs to be [SerializeField]
        //when we leave the screen, destroy this  object 
        //Checking to see if power up is gone off the screen which is -4.5
       
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);

        }

    }





   //OnTriggerCollision 
    //only be collected by the Player (Hint use tags) 
    //On collected, Destor


    //this is what will happne after the player collids with the triple shot power up
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //communicate with player script( this is for collection power up and also a method where we are colliding with our player Script//
            //Create a handle to the component I want 
            //Assign the Handle to the Component
            PLAYER player = other.transform.GetComponent<PLAYER>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
               switch (powerupID) //"Break" is telling each case when it ends//
               {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;

                    //default case will be called if its niether 0,1, or 2. 
                    default:
                        Debug.Log("Default Value");
                        break;
               }
        
            }
        
                     Destroy(this.gameObject);
        }

        
    }

    
}



