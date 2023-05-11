using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boil : Item, IDropHandler, IPointerClickHandler
{
    [SerializeField] GameObject inputField;
    [SerializeField] GameObject boiled;
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
        int.TryParse(time, out timeInt);
        Debug.Log(time + "�� ���� ���̱�");
        GameObject water = Instantiate(boiled);
        foreach(Transform child in transform)
        {
            if(child.tag == "Ingredient") child.GetComponent<Ingredient>().boil(timeInt, water);
        }
    }
}
