using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snipping : MonoBehaviour
{   
    public static Snipping Instance { get; private set; }

    public Transform snapCell;
    private Vector3 pos;


    private float min_X, max_X, min_Y, max_Y;

    public bool canPlace;
    private SpriteRenderer _renderer;
    private Vector3 mousePos;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        canPlace = true;
        pos.z = 0;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
        Cursor.visible = false;
        mousePos = Input.mousePosition;


        min_X = (transform.localScale.x/2)-4;
        min_Y = (transform.localScale.y / 2)-4;

        //Cell Count -2 * 8 -4
        max_X = ((WorldCreator.Instance.ReturnCellCize("x")- transform.localScale.x / 16) *8)-4;
        max_Y = ((WorldCreator.Instance.ReturnCellCize("y") - transform.localScale.y / 16) * 8)-4;

       
        if ((transform.localScale.x/8) % 2 != 0)
        {
            max_X += 4;
        }
        if ((transform.localScale.y/8)%2 != 0)
        {
            max_Y += 4;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.color = canPlace ? _renderer.color = new Color32(0, 200, 255, 100) : _renderer.color = new Color32(255, 0, 0, 100);
        

        
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Cursor.visible = true;
            CanvasControl.Instance.OpenProdictionButton();
            _renderer.color = Color.white;
           
            gameObject.SendMessage("CreatedBuilding", SendMessageOptions.DontRequireReceiver);
            Destroy(GetComponent<Snipping>());    
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = true;
            CanvasControl.Instance.OpenProdictionButton();
            Destroy(gameObject);
        }


        // This part to make clamp the object in game boundaries
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

           pos.z = 0;

        if (snapCell != null && Input.mousePosition == mousePos && Input.GetButton("Snap")) 
           {
               if((transform.localScale.x / 8)%2 == 0)
               {
                   pos.x = snapCell.transform.position.x - 4;
               }
               else
               {
                   pos.x = snapCell.transform.position.x ;
               }
               if((transform.localScale.y / 8) % 2 == 0)
               {
                   pos.y = snapCell.transform.position.y - 4;
               }
               else
               {
                   pos.y = snapCell.transform.position.y;
               }
               pos.x = Mathf.Clamp(pos.x, min_X, max_X);
               pos.y = Mathf.Clamp(pos.y, min_Y, max_Y);
               transform.position = pos;
           }

        if (!Input.GetButton("Snap"))
        {
             pos.x = Mathf.Clamp(pos.x, min_X, max_X);
             pos.y = Mathf.Clamp(pos.y, min_Y, max_Y);
             transform.position = pos;
        }

        mousePos = Input.mousePosition;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Building") ||collision.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            canPlace = false;
        }

       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Building") || collision.gameObject.layer == LayerMask.NameToLayer("Unit"))
        {
            canPlace = true;
            
        }
    }

   
}
