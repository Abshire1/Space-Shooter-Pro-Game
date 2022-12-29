using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
   //[SeiializeField] we are able to motifiy these Variables in the inspector in UNITY//
    [SerializeField]
    private float _speed = 12.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    //Variable for is triple shot active/ this will be fasle untill we create our power up and enable it. 
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
   
    //shield is set to false untill it is collected// 
    private bool _isShieldsActive = false;
    //Variable reference to the shiled visualizer
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    //Vaeriable to store Audio clip //
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource; 
    










    // Start is called before the first frame update
    void Start()
    {
        //take current position to a new position (take current position = new postion)(0,0,0,)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();//find the object and then get the componet.
        //Finding component for UI Manager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.Log("AudioSource is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
        if(_spawnManager == null)
         {
            Debug.Log("The Spawn Manager is NULL");

         }
        //FINDING UIManager correctly 
        if (_uiManager == null)
        {
            Debug.Log("The UI Manager is NULL");

        }


    }


    
    
    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        // if I hit the space key to make sure space bar works(AT the (&&) we are creating COOL DOWN SYSTEM)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Firelaser();
        }

    }





    void CalculateMovement()
    {
        //being able to control player horizontal(left & right) then(vertically up and down) 
        float HorizonalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        
        //moving player to the left  right at value speed and in  real time 
        //creating method telling our player to use Speed Boost/ IF , ELSE Statement
        transform.Translate(Vector3.right * HorizonalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * VerticalInput * _speed * Time.deltaTime);
        
        
        //making Player Bounds in Y (up) position
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        //making player Bounds for y (down) position
        if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //making PLAYER Bounds for left and right and reapearing from oppisite sides
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }

    }

    //CalculateMovement for laser calling it FireLaser 
    void Firelaser()
    {
        //CREATING COOL DOWN SYSTEM 
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

        }
        else
        {
            //Adding A cleaner look for laser when shot out starting with +//
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

        }
        //Play the laser audio clip 
        _audioSource.Play();
        
        

    }

    
    //THIS IS THE  ENEMY TAKING LIVES from the Player 
    public void Damage()
    {
        //If shield is active do nothing 
        //DEACTIVATE SHIELD after enemy hits once
        //RETURN
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            //Disabale visualizer
            _shieldVisualizer.SetActive(false);
            return; 
        }
        _lives--;
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);

        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }


        //IF lives is at 2 
        //Enable right wing 
        //If lives is 1 
        //Enable left wing

        _uiManager.UpdateLives(_lives);
        //other ways to type this/ _lives -= 1;/ _live--;/ _lives =  _lives - 1;
        
        //check if dead 
        //destroy us
       
        //this will Destory the player GAME OVER/<less then 0 is like having two ENEMIES HIT YOU AT ONCE//
        if(_lives <= 0)
        {
            //COMUNICATE WITH SPAWN MANAGER 
            _spawnManager.OnPlayerDeath();
           //LET THEM KNOW KNOW (ENEMIES) TO STOP SPAWNING
            
            Destroy(this.gameObject); 

        }


    }

   //This is colliding with the Power up Script in order to collect the triple shot power up
    public void TripleShotActive()
    {
        //TripleShotActive becomes true //FIRST STEP for triple shot power up collider 
        _isTripleShotActive = true;
        //FIFTH STEP 
        StartCoroutine(TripleShotPowerDownRoutine());
       
    }
    //Start power down coroutine for triple shot 
    //Second step (IEnumerator) 
    IEnumerator TripleShotPowerDownRoutine()
    {
       //THRID STEP 
        yield return new WaitForSeconds(5.0f);
       //FOURTH STEP 
        _isTripleShotActive = false; 

    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed*= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    //Creating IEnumerator Triple Shot Power down Routine 
    //wait 5 secs 
    //set tripe shot ot fasle 
    
    //Method for Shield active to be true when collected 
    public void ShieldsActive()
    {
        _isShieldsActive = true;
        //enable visualizer
        _shieldVisualizer.SetActive(true);
    }
    //Method to ADD TO THE SCORE//
    //COMUNICATE WITH THE UI TO UPDATE THE SCORE. 
    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);

    }




}


