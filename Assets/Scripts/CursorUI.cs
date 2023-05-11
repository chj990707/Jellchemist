using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject tooltip;
    GameObject Holding;
    Image panel;
    private Text tooltipText;

    void Awake()
    {
        tooltipText = tooltip.GetComponent<Text>();
        panel = GetComponent<Image>();
        hideText();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Input.mousePosition + new Vector2(20,-20);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
        if (Holding != null) Holding.transform.position = pos;
    }

    public void showHoverText(string text)
    {
        panel.enabled = true;
        tooltip.SetActive(true);
        tooltipText.text = text;
    }
    public void hideText()
    {
        panel.enabled = false;
        tooltip.SetActive(false);
    }

    public void Hold(GameObject gameObject)
    {
        Release();
        Holding = gameObject;
    }
    public void Release()
    {
        Holding = null;
    }
}
