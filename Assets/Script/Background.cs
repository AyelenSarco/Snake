using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer background_sr = this.GetComponent<SpriteRenderer>();
        Color randomColor = Color.HSVToRGB(Random.Range(0f,01),0.17f,0.79f);
        background_sr.color = randomColor;
    }

    
}
