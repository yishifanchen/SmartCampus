using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {
    public static void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
