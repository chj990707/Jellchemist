using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Item
{
    public string getHoverText()
    {
        string Text = string.Empty;
        foreach (element e in gameObject.GetComponents<element>())
        {
            Text += "\n" + e.getEffect();
        }
        foreach (Transform child in transform)
        {
            Text += "\n" + child.GetComponent<Ingredient>().getHoverText();
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
}
