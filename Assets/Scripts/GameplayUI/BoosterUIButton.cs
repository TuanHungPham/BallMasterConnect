using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class BoosterUIButton : MonoBehaviour, IPointerClickHandler
{
    #region public
    public Action<BoosterUIButton> OnItemClicked;
    #endregion

    #region private
    private const string SELECTED_PARAM = "Booster_Selected";
    [SerializeField] private bool isSelected;
    [SerializeField] private bool isEmpty;
    [SerializeField] private BoosterID boosterID;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text boosterQuantity;
    [SerializeField] private Image adsLogo;
    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        animator = GetComponent<Animator>();
        boosterQuantity = transform.Find("Plus").GetChild(0).GetComponent<TMP_Text>();
        adsLogo = transform.Find("AdsLogo").GetComponent<Image>();

        isSelected = false;
        isEmpty = true;
    }

    public void Select()
    {
        isSelected = true;
        SetSelectedAnimation(isSelected);
    }

    public void Deselect()
    {
        isSelected = false;
        SetSelectedAnimation(isSelected);
    }

    public void SetDataUI(int itemQuantity)
    {
        if (itemQuantity != 0)
        {
            adsLogo.enabled = false;
            isEmpty = false;
            boosterQuantity.text = itemQuantity.ToString();
        }
        else
        {
            adsLogo.enabled = true;
            isEmpty = true;
            boosterQuantity.text = "+";
        }
    }

    private void SetSelectedAnimation(bool selected)
    {
        animator.SetBool(SELECTED_PARAM, selected);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right) return;

        OnItemClicked?.Invoke(this);
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public BoosterID GetButtonBoosterID()
    {
        return boosterID;
    }
}
