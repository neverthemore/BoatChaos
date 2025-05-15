using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ShipGame.Events;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] private Image _GameOverImage;
    [SerializeField] private Image _VictoryImage;
    [SerializeField] private string _mainMenuSceneName = "Menu";
    [SerializeField, Range(1, 10)] private float _showSpeed = 5f;


    private bool _isImageShow = false;



   
    private void OnEnable()
    {
        GameStartEventBus.SubscribeToGameOver(GameOver);
        GameStartEventBus.SubscribeToGameVictory(Victory);
    }

    private void OnDisable()
    {
        GameStartEventBus.UnsubscribeFromGameOver(GameOver);
        GameStartEventBus.UnsubscribeFromGameVictory(Victory);
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
