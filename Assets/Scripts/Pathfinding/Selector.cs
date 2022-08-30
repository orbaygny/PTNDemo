using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public static Selector Instance;
    public GameObject target;
    public GameObject unit;

    private void Awake()
    {
        Instance = this;
    }

    public void CallForPath()
    {
        unit.GetComponent<Unit>().target = target;
        unit.GetComponent<Unit>().RequestPathForUnit();
    }

    public void NewSelect(GameObject selectedObj)
    {
        if(unit != null)
        {
            unit.GetComponent<SpriteRenderer>().color = Color.white;
        }
        unit = selectedObj;
        unit.GetComponent<SpriteRenderer>().color = Color.yellow;

    }
    public void DeSelect()
    {
        if(unit != null)
        {
            unit.GetComponent<SpriteRenderer>().color = Color.white;
            unit = null;
        }
        
        target = null;
    }
}

