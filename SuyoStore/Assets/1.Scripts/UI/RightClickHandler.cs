using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
public class RightClickHandler : MonoBehaviour, IPointerClickHandler 
{
    UIManager ui;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ui = FindObjectOfType<UIManager>();
            ui.ChangeWeaponState(gameObject);
            SoundManager.SM.PlaySfxSound(SfxSoundName.ButtonClick);
        }
    }
}
