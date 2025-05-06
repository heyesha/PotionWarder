using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureText : MonoBehaviour
{
   public void ChangeText(string temperature)
    {
        GetComponent<Text>().text = temperature;
    }
}
