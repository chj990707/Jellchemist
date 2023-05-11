using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class element : MonoBehaviour
{
    [SerializeField] protected int amount;
    [SerializeField] protected string effect;
    public int getAmount()
    {
        return amount;
    }
    public string getEffect()
    {
        return effect;
    }
}
