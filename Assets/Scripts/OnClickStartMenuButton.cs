using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickStartMenuButton : MonoBehaviour {


    public void LoadOnClickScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
