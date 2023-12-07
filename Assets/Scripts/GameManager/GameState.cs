using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneManager.LoadScene(1);
    }
}
