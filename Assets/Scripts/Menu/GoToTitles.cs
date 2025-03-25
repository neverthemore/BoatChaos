using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GoToTitles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoTo()
    {
        SceneManager.LoadScene("Titles");
    }

    public void GoToBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
