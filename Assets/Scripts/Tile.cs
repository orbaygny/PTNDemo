using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor,offsetColor,hLightColor;
    [SerializeField] private SpriteRenderer _renderer;
    private Color selectedColor;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridY;

    [HideInInspector] public int gCost;
    [HideInInspector] public int hcost;
    [HideInInspector] public bool walkable;


    [HideInInspector] public List<GameObject> neighbours;
    public Node node;

    private void Start()
    {
        node = new Node(transform.position, walkable,gridX, gridY, gCost, hcost,gameObject);
    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
        selectedColor = _renderer.color;
    }

   
    private void OnMouseEnter()
    {
       /* if (Cursor.visible)
        {
            _renderer.color = hLightColor;
        }*/
        
        if(Snipping.Instance != null)
        {
            Snipping.Instance.snapCell = transform;
         
           
        }

    }

    private void OnMouseDown()
    {

        if (Cursor.visible)
        {
            _renderer.color = hLightColor;
          /*  if(PathFinder.Instance.character == null) { PathFinder.Instance.character = gameObject; }
            else
            {
                PathFinder.Instance.target = gameObject;
            }*/

           
            
        }
    }
    private void OnMouseExit()
    {
       // _renderer.color = selectedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grid"))
        {
            neighbours.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("nonWalk"))
        {
            walkable = false;
            node.walkable = walkable;
        }

        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("nonWalk"))
        {
            walkable = true;
            node.walkable = walkable;
        }

    }

    public int FCost
    {
        get { return gCost + hcost; }
    }
}
