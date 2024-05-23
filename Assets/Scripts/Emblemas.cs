using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;



public class Emblemas : MonoBehaviour
{
    public Texture[] estados = new Texture[2];

    private void Awake()
    {
        estados[0] = gameObject.GetComponent<RawImage>().texture;
    }
    public void ResetTexture()
    {
        gameObject.GetComponent<RawImage>().texture = estados[0];
    }
    public void ChangeState(bool state)
    {
        if (state)
        {
            gameObject.GetComponent<RawImage>().texture = estados[1];
        } else
        {
            gameObject.GetComponent<RawImage>().texture = estados[0];
        }
    }
    
}
