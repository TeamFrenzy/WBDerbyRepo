using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoScript : MonoBehaviour
{
    Color alpha;
    public float speed;
    public float alphaView;
    public float maxAlpha;
    void Awake()
    {
        alpha = GetComponent<Image>().color;
        alpha.a = 0;
        GetComponent<Image>().color = alpha;
    }

    private void Update()
    {
        if (alpha.a < maxAlpha)
        {
            alpha.a = alpha.a + Time.deltaTime * speed;
        }

        alphaView = alpha.a;
        GetComponent<Image>().color = alpha;
    }
}
