using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //IF the R key is Pressed 
        //Restart the Current Scene
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current game Scene 

        }
       //if the ESC is pressed 
       //quit application 
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            Application.Quit();

       }
    }
    public void GameOver()
    {

        _isGameOver = true;
    
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
