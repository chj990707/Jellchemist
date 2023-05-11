using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : Item
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, new ContactFilter2D().NoFilter(), results);
        foreach(Collider2D col in results)
        {
            if(col.tag == "Ingredient")
            {
                col.GetComponent<Ingredient>().cut();
            }
        }
    }
}
