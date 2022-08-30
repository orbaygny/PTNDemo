using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrolling : MonoBehaviour
{

   private Vector3 pos;
   private ScrollRect rect;
    
   
   
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<ScrollRect>();
        pos = transform.GetChild(0).position;
      
    }

   
    public void OnValueChanged(Vector2 value)
    {
        if(value.y >1)
        {
            ObjPool.Instance.Spawner("Normal", true);
            ObjPool.Instance.AddObject("Normal", transform.GetChild(0).GetChild(5).gameObject, "first");
           
            transform.GetChild(0).position = pos;
        }

        if (value.y < 0.05)
        {
            
            ObjPool.Instance.Spawner("Normal", false);
            ObjPool.Instance.AddObject("Normal", transform.GetChild(0).GetChild(0).gameObject, "last");
           
           transform.GetChild(0).position = pos;
        }

    }
    
}
