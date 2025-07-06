using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int index;
    public int nextLevel;
    [SerializeField] RoShape[] equation; // each 3 shape combines a equation


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsEqual()
    {
        for (int i = 0; i < equation.Length; i += 3)
        {
            if (equation[i].value + equation[i + 1].value != equation[i + 2].value)
            {
                Debug.Log($"Check {equation[i].value} + {equation[i + 1].value} ?= {equation[i + 2].value}");
                return false;
            }
        }
        return true;
    }
}
