using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColliders : MonoBehaviour
{
    float heigth, width;

	void Start ()
    {
        heigth = Screen.height;
        width = Screen.width;

        float coeff = width / heigth;

        if (coeff > 1.7f)
        {
            transform.localScale = Vector3.one;
        } 
        else
        {
            transform.localScale = new Vector3(.75f, 1, 1);
        }
    }
}
