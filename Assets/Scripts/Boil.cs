using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boil : Item, IDropHandler, IPointerClickHandler
{
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject result;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inputField.SetActive(true);
        }
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag == "Ingredient")
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<SpriteRenderer>().enabled = false;
            eventData.pointerDrag.GetComponent<Collider2D>().enabled = false;
        }
    }
    public virtual void boil(string time)
    {
        int timeInt;
        if(int.TryParse(time, out timeInt))
        {
            if (timeInt <= 0) return;
            Debug.Log(time + "분 동안 끓이기");
            result.name = "potion";
            GameObject water = Instantiate(result);
            Dictionary<Element, uint> dissolved = new Dictionary<Element, uint>();
            foreach (Transform child in transform)
            {
                if (child.tag == "Ingredient")
                {
                    dissolved = child.GetComponent<Ingredient>().boil((uint)timeInt, dissolved);
                }
            }
            water.GetComponent<Ingredient>().AddElements(dissolved);
        }
    }
}
