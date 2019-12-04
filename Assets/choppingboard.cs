using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choppingboard : MonoBehaviour
{
    public List<string> salad;

    public void addchoppedvegie(string veg)
    {
        if (salad.Count < 3)
        {
            salad.Add(veg);
        }
    }
}
