using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play(){
        SceneManager.LoadScene("Imated Scene");
    }
    public void Options(){
        SceneManager.LoadScene("Options");
    }
}
