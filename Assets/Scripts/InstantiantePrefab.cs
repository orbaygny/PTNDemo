using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiantePrefab : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;
    private Button button;
    // Start is called before the first frame update
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(CreatePrefab);
    }

    void CreatePrefab()
    {
        Instantiate(buildingPrefab);
        CanvasControl.Instance.CloseProductionMenu();
    }
}
