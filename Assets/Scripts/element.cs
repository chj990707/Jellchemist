using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] protected bool initialized = false;
    [SerializeField] protected int amount;
    [SerializeField] protected string effect;
    [SerializeField] protected int solubility;

    public void Init(int amount, string effect, int solubility)
    {
        if (!initialized)
        {
            initialized = true;
            this.amount = amount;
            this.effect = effect;
            this.solubility = solubility;
        }
    }

    public int getAmount()
    {
        return amount;
    }
    public string getEffect()
    {
        return effect;
    }
    public int getSolubility()
    {
        return solubility;
    }

    public bool TryAddAmount(string effect, int amount)
    {
        Debug.Log(effect + ", " + this.effect);
        if (effect.Equals(this.effect))
        {
            this.amount += amount;
            return true;
        }
        else return false;
    }

    public int boil(int time)
    {
        int dissolved = Mathf.Min(amount, solubility * time);
        amount -= dissolved;
        return dissolved;
    }
}
