using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Element
{
    [SerializeField] private string name;
    [SerializeField] private string effect;
    [SerializeField] private uint solubility;

    public Element(string name, string effect, uint solubility)
    {
        this.name = name;
        this.effect = effect;
        this.solubility = solubility;
    }

    public Element(Element original)
    {
        name = original.name;
        effect = original.effect;
        solubility = original.solubility;
    }

    public string getName()
    {
        return name;
    }

    public string getEffect()
    {
        return effect;
    }

    public uint getSolubility()
    {
        return solubility;
    }
}
