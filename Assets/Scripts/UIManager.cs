using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Need to add UnityEngine UI When working with UI'S 
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Handle to text 
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _LivesSprites;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gameManager; 

    
    // Start is called before the first frame update
    void Start()
    {
       
        //assign text component to the handle 
        _scoreText.text = "Score:" + 0;
        _gameOverText.gameObject.SetActive(false);
       

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL!");
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }


    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;

    }
   public void UpdateLives(int currentLives)
   {
        
        //display Image sprite 
        _LivesImg.sprite = _LivesSprites[currentLives];
    //give it a new one based  on the current lives index
    
        //this is will make game over text appear after all 3 lives have been taken 
        if (currentLives == 0)
        {
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());

        }


   }
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);

        }



    }
}
