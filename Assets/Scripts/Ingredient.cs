using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : Item, IPointerClickHandler
{
    private bool initialized = false;
    protected Dictionary<Element, uint> elements = new Dictionary<Element, uint>();

    [System.Serializable]
    public class ElementAmountPair
    {
        public string elementName;
        public uint amount;
    }
    [System.Serializable]
    public class IngredientJson
    {
        public string[] IngredientArray;
        public ElementAmountPair[] ElementArray;
    }

    public void Init(string name)
    {
        if (initialized) return;
        initialized = true;
        this.name = name;
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + name);
        GetComponent<SpriteRenderer>().sprite = sprite;
        TextAsset ingredientTextAsset = Resources.Load<TextAsset>("Ingredients/" + name);
        IngredientJson ingredientJson = JsonUtility.FromJson<IngredientJson>(ingredientTextAsset.text);

        GameObject prefab = Resources.Load<GameObject>("Prefabs/Ingredient");
        foreach (string ingredientName in ingredientJson.IngredientArray)
        {
            GameObject sub_ingredient = Instantiate(prefab);
            sub_ingredient.transform.SetParent(transform);
            sub_ingredient.transform.position = new Vector3(0, 0, 0);
            sub_ingredient.GetComponent<SpriteRenderer>().enabled = false;
            sub_ingredient.GetComponent<Collider2D>().enabled = false;
            sub_ingredient.GetComponent<Ingredient>().Init(ingredientName);
        }

        foreach (ElementAmountPair pair in ingredientJson.ElementArray)
        {
            TextAsset json_element = Resources.Load<TextAsset>("Elements/" + pair.elementName);
            Element element = JsonUtility.FromJson<Element>(json_element.text);
            elements.Add(element, pair.amount);
        }
    }
    public string getHoverText()
    {
        string Text = string.Empty;
        foreach (KeyValuePair<Element, uint> element in elements)
        {
            if (element.Value >= 100) Text += "강한 ";
            else if (element.Value >= 50) Text += "중간 정도의 ";
            else Text += "약한 ";
            Text +=element.Key.getEffect() + ", ";
        }
        foreach (Transform child in transform)
        {
            string childHoverText = child.GetComponent<Ingredient>().getHoverText();
            if(childHoverText != "효과 없음")
            { 
                Text += childHoverText; 
            }
        }
        if(Text.Length == 0)
        {
            Text = "효과 없음";
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
    
    public Dictionary<Element,uint> boil(uint time, Dictionary<Element, uint> water)
    {
        Dictionary<Element, uint> toRemove = new Dictionary<Element, uint>();
        foreach (KeyValuePair<Element, uint> element in elements)
        {
            uint amount = (uint)Mathf.Min(element.Key.getSolubility() * time, element.Value);
            if (amount == 0) continue;
            water.Add(new Element(element.Key), amount);
            toRemove.Add(element.Key, amount);
        }
        foreach(KeyValuePair<Element, uint> element in toRemove)
        {
            elements[element.Key] -= element.Value;
            if (elements[element.Key] == 0) elements.Remove(element.Key);
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Ingredient>().boil(time, water);
        }
        return water;
    }

    public void AddElements(Dictionary<Element, uint> elements_to_add)
    {
        foreach(KeyValuePair<Element, uint> element in elements_to_add)
        {
            uint prev_amount = 0;
            if(elements.TryGetValue(element.Key, out prev_amount))
            {
                elements[element.Key] = prev_amount + element.Value;
            }
            else
            {
                elements.Add(element.Key, element.Value);
            }
        }
    }



    public void Elements_Log()
    {
        Debug.Log(name + "의 성분 : ");
        foreach (KeyValuePair<Element, uint> element in elements)
        {
            Debug.Log(element.Key.getName() + " : " + element.Value);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Elements_Log();
            foreach (Transform child in transform)
            {
                child.GetComponent<Ingredient>().Elements_Log();
            }
        }
    }
}
