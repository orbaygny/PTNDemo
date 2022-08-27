using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor,offsetColor,hLightColor;
    [SerializeField] private SpriteRenderer _renderer;
    private Color selectedColor;
    
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? offsetColor : baseColor;
        selectedColor = _renderer.color;
    }

    private void OnMouseEnter()
    {
        if (Cursor.visible)
        {
            _renderer.color = hLightColor;
        }
        
        if(Snipping.Instance != null)
        {
            Snipping.Instance.snapCell = transform;
        }
        
    }
    private void OnMouseExit()
    {
        _renderer.color = selectedColor;
    }
}
