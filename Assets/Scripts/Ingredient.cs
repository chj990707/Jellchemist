using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    public string getHoverText()
    {
        string Text = string.Empty;
        foreach (Element e in gameObject.GetComponents<Element>())
        {
            if (e.getAmount() >= 100) Text += "���� ";
            else if (e.getAmount() >= 50) Text += "�߰� ������ ";
            else Text += "���� ";
            Text +=e.getEffect() + ", ";
        }
        foreach (Transform child in transform)
        {
            string childHoverText = child.GetComponent<Ingredient>().getHoverText();
            if(childHoverText != "ȿ�� ����")
            { 
                Text += childHoverText; 
            }
        }
        if(Text.Length == 0)
        {
            Text = "ȿ�� ����";
        }
        return Text;
    }

    public void OnMouseEnter()
    {
        cursorUI.showHoverText(getHoverText());
    }
    public void OnMouseExit()
    {
        cursorUI.hideText();
    }

    public void cut()
    {
        Debug.Log("Cutting " + name);
        if (transform.childCount == 0) return;
        int sortingOrder = spriteRenderer.sortingOrder;
        Vector3 offsetVector = new Vector3(0, 0, 0);
        foreach(Transform child in transform)
        {
            child.transform.position += offsetVector;
            child.GetComponent<SpriteRenderer>().sortingOrder = ++sortingOrder;
            child.GetComponent<SpriteRenderer>().enabled = true;
            child.GetComponent<Collider2D>().enabled = true;
            offsetVector += new Vector3(0.25f, 0.25f, 0);
        }
        gameObject.transform.DetachChildren();
        Destroy(gameObject);
    }
    
    public void boil(uint time, GameObject water)
    {
        foreach (Element e in gameObject.GetComponents<Element>())
        {
            uint amount = e.boil(time);
            if (amount == 0) continue;
            bool added = false;
            Element[] water_elements = water.GetComponents<Element>();
            foreach(Element ew in water_elements)
            {
                if(ew.TryAddAmount(e.getEffect(), amount))
                {
                    added = true;
                    break;
                }
            }
            if (added) continue;
            Element newElement = water.AddComponent<Element>();
            newElement.Init(amount, e.getEffect(), e.getSolubility());
            if (e.getAmount() == 0) Destroy(e);
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Ingredient>().boil(time, water);
        }
    }
}
