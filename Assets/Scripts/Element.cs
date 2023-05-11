using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] protected bool initialized = false;
    [SerializeField] protected uint amount;
    [SerializeField] protected string effect;
    [SerializeField] protected int solubility;

    public void Init(uint amount, string effect, int solubility)
    {
        if (!initialized)
        {
            initialized = true;
            this.amount = amount;
            this.effect = effect;
            this.solubility = solubility;
        }
    }

    public uint getAmount()
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

    public bool TryAddAmount(string effect, uint amount)
    {
        Debug.Log(effect + ", " + this.effect);
        if (effect.Equals(this.effect))
        {
            this.amount += amount;
            return true;
        }
        else return false;
    }

    public uint boil(uint time)
    {
        uint dissolved = (uint)Mathf.Min(amount, solubility * time);
        amount -= dissolved;
        return dissolved;
    }
}
