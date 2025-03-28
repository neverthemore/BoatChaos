using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class UI_GameOver : MonoBehaviour
{
    //Запускает Game Over/ Victory
    //Подписка на событие 
    [SerializeField] GameOver _gameOver;

    [SerializeField] private Image _GameOverImage;
    [SerializeField] private Image _VictoryImage;
    [SerializeField] private string _mainMenuSceneName = "Menu";
    [SerializeField, Range(1, 10)] private float _showSpeed = 5f;


    private bool _isImageShow = false;



   
    private void OnEnable()
    {
        _gameOver.OnGameOver.AddListener(GameOver);
        _gameOver.OnGameVictory.AddListener(Victory);
    }

    private void OnDisable()
    {
        _gameOver.OnGameOver.RemoveListener(GameOver);
        _gameOver.OnGameVictory.RemoveListener(Victory);
    }

    private void GameOver()
    {

        StartCoroutine(ShowPanel(_GameOverImage));
    }

    private void Victory()
    {
        
        StartCoroutine(ShowPanel(_VictoryImage));
    }

    IEnumerator ShowPanel(Image img)
    {
       
        
        img.gameObject.SetActive(true);
        Color color = img.color;
        float elapsedTime = 0f;
        while(elapsedTime < _showSpeed)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime/_showSpeed);
            img.color = new Color(color.r, color.g, color.b, alpha);            
            yield return null;
        }
        img.color = new Color(color.r, color.g, color.b, 1);


        yield return new WaitForSeconds(3f);

     
        if (!string.IsNullOrEmpty(_mainMenuSceneName))
        {
            SceneManager.LoadScene(_mainMenuSceneName);
        }
    }
}
