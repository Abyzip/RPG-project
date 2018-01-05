using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageUIScript : MonoBehaviour
{
    public int maxRage = 100;
    public int currentRage;
    public RectTransform rageBar;

    void Start()
    {
        currentRage = 0;
    }

    public void gainRage(int amount)
    {
        currentRage += amount;
        if (currentRage > maxRage)
            currentRage = maxRage;
    }


    public bool hasEnoughRage(int cost)
    {
        if (currentRage < cost)
            return false;
        else
        {
            loseRage(cost);
            return true;
        }
    }

    public void loseRage(int amount)
    {
       
        currentRage -= amount;
        if (currentRage < 0)
            currentRage = 0;
    }


    // Update is called once per frame
    void Update()
    {

        rageBar.sizeDelta = new Vector2(currentRage, rageBar.sizeDelta.y);
    }
}


