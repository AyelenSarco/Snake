using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private Time timedelta;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("Game");
        }
    }
}
