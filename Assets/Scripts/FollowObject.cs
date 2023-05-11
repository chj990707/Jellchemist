using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowObject : MonoBehaviour
{
    [SerializeField] GameObject toFollow;
    [SerializeField] Boil boil;
    // Update is called once per frame
    private void Awake()
    {
        GetComponent<InputField>().onEndEdit.AddListener(boil.boil);
    }
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(toFollow.transform.position);
    }
}
