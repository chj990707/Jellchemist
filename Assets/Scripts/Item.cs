using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    protected CursorUI cursorUI;
    protected SpriteRenderer spriteRenderer;
    new protected Collider2D collider;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        cursorUI = GameObject.Find("Canvas").transform.Find("Panel").GetComponent<CursorUI>();
    }
    public void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
    public void OnMouseDown()
    {
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Selected Item");
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
    }
    virtual public void OnMouseUp()
    {
        spriteRenderer.sortingLayerID = SortingLayer.NameToID("Item");
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, new ContactFilter2D().NoFilter(), results);
        int maxOrder = -1;
        foreach(Collider2D col in results)
        {
            SpriteRenderer colSpriteRenderer = col.GetComponent<SpriteRenderer>();
            if (colSpriteRenderer != null && colSpriteRenderer.sortingOrder > maxOrder)
            {
                maxOrder = colSpriteRenderer.sortingOrder;
            }
        }
        spriteRenderer.sortingOrder = maxOrder + 1;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
