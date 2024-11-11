using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler,
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler,
        IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text quantityTxt;

    [SerializeField]
    private Image borderImage;
    [SerializeField]
    private InventoryDescription inventoryDescription;

    Modifier modifier;
    bool IsClickable;

    public event Action<InventoryItem> OnItemClicked,
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
        OnRightMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
    }
    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void SetData(Sprite sprite, int quantity, Modifier modifier)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityTxt.text = quantity + "";
        empty = false;
        this.modifier = modifier;
    }


    public void OnPointerClick(PointerEventData pointerData)
    {
        if (!IsClickable) 
            return;
        GameObject dealerObj = GameObject.Find("Dealer");
        Dealer dealer = dealerObj.GetComponent<Dealer>();
        Player player = dealer.GetCurrentPlayer();

        player.UseModifier(modifier);

        inventoryDescription.UpdateText(modifier);

        ResetData();

        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (modifier == null)
            return;
        inventoryDescription.UpdateText(modifier);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryDescription.ResetText();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    internal void SetClickable(bool clickable)
    {
        IsClickable = clickable;
    }
}
