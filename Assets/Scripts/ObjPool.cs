using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
       
    }

    public Dictionary<string,LinkedList<GameObject>> buttonPool;

   [SerializeField] private Transform _buttonPool;
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public List<GameObject> prefabs;
        public int size; 
    }
    public List<Pool> pools;

    // Start is called before the first frame update
    void Start()
    {
        buttonPool = new Dictionary<string, LinkedList<GameObject>>();
        foreach (Pool pool in pools)
        {
            LinkedList<GameObject> objectPool = new LinkedList<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                
                    GameObject obj = Instantiate(pool.prefabs[i % pool.prefabs.Count],_buttonPool);
                    
                    obj.SetActive(false);

                    objectPool.AddLast(obj);
            }

            buttonPool.Add(pool.tag, objectPool);
        }

        for (int j = 0; j <4 + 1; j++)
        {
            GameObject obj = buttonPool["Normal"].First.Value.gameObject;
            buttonPool["Normal"].RemoveFirst();
            obj.transform.SetParent(transform);
            obj.SetActive(true);
        }

    }

    public void AddObject(string tag, GameObject obj, string place)
    {
        if (place.CompareTo("first") == 0)
        {
            buttonPool[tag].AddFirst(obj);
            obj.transform.SetParent(_buttonPool);
            obj.SetActive(false);
        }

       else if (place.CompareTo("last") == 0)
        {
            buttonPool[tag].AddLast(obj);
            obj.transform.SetParent(_buttonPool);
            obj.SetActive(false);
        }
    }
   public void Spawner(string tag, bool normal)
    {

        switch (normal)
        {
            case true:
                 var obj = buttonPool[tag].Last.Value.gameObject;
                buttonPool[tag].RemoveLast();
                obj.transform.SetParent(transform);
                obj.transform.SetAsFirstSibling();
                //buttonPool[tag].AddLast(obj);
                obj.SetActive(true);
                break;


            case false:
                
                var _obj = buttonPool[tag].First.Value.gameObject;
                buttonPool[tag].RemoveFirst();
                _obj.transform.SetParent(transform);
                _obj.transform.SetAsLastSibling();
                //buttonPool[tag].AddLast(obj);
                _obj.SetActive(true);

                break;
                
        }
       
     


    }
}
