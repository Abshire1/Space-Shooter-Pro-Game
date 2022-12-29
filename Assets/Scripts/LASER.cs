using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASER : MonoBehaviour
{
    //create speed variable OF  meters per second  for laser 
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private bool _isEnemyLaser = false;





    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //IF laser position is greater then 8 on the y 
        //destroy the object(destroying laser after its been fired and off screen)
        if (transform.position.y > 8f)
        {
            
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);

        }

    }
    
    
    
    
    void MoveDown()
    {
        
        //translate laser up 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //IF laser position is greater then 8 on the y 
        //destroy the object(destroying laser after its been fired and off screen)
        if (transform.position.y < -8f)
        {
            
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);

        }


    }
    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true)
        {
            PLAYER player = other.GetComponent<PLAYER>();
            if (player != null)
            {
                player.Damage();
            }
        
        }

    }

}
