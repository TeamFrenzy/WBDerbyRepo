using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTimerScript : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > 3f)
        {
            SceneManager.LoadScene(2);
        }

    }
}
