using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerFactory : MonoBehaviour
{
    [SerializeField] protected GameObject prefabIngredient;
    public void CreateFlower()
    {
        prefabIngredient.name = "whitebell";
        GameObject newFlower = Instantiate(prefabIngredient);
        newFlower.transform.position = new Vector3(0, 0, 0);
    }
}
