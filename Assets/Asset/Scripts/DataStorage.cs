using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage
{
    private float titleScreenSpeed = 0.05f;
    public void setTitleScreenSpeed(float num)
    {
        titleScreenSpeed = num;
    }
    public float getTitleScreenSpeed()
    {
        return titleScreenSpeed;
    }
}
