using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    public static CanvasControl Instance { get; private set; }

    [Header("Production Menu Buttons")]
    [SerializeField] private GameObject productionMenu;
    [SerializeField] private GameObject openProductMenu_Btn;
    [SerializeField] private GameObject closeProductMenu_Btn;
   



    #region Production Menu Button Functions

    private void Awake()
    {
        Instance = this;
    }
    public void OpenProductionMenu()
    {
        productionMenu.SetActive(true);
        openProductMenu_Btn.SetActive(false);
        closeProductMenu_Btn.SetActive(true);
    }

    public void CloseProductionMenu()
    {
        productionMenu.SetActive(false);
        openProductMenu_Btn.SetActive(true);
        closeProductMenu_Btn.SetActive(false);

    }

    public void OpenProdictionButton()
    {
        openProductMenu_Btn.SetActive(true);
    }

    public void HideProductionButton()
    {
        productionMenu.SetActive(false);
        closeProductMenu_Btn.SetActive(false);
    }
    #endregion



}
