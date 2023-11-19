using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public Image ringImage;
    public Image braceletImage;
    public Image necklaceImage;

    public Text atkText;
    public Text defText;

    public GameObject infoPanel;
    public Transform player;
    PlayerStats stats;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        stats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Info"))
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            if((int)newItem.equipSlot == 0) // ring
            {
                ringImage.enabled = true;
                ringImage.sprite = newItem.icon;
            }
            else if((int)newItem.equipSlot == 1) // bracelet
            {
                braceletImage.enabled = true;
                braceletImage.sprite = newItem.icon;
            }
            else if((int)newItem.equipSlot == 2) // necklace
            {
                necklaceImage.enabled = true;
                necklaceImage.sprite = newItem.icon;
            }
        }
        else
        {
            if ((int)oldItem.equipSlot == 0) // ring
            {
                ringImage.enabled = false;
                ringImage.sprite = null;
            }
            else if ((int)oldItem.equipSlot == 1) // bracelet
            {
                braceletImage.enabled = false;
                braceletImage.sprite = null;
            }
            else if ((int)oldItem.equipSlot == 2) // necklace
            {
                necklaceImage.enabled = false;
                necklaceImage.sprite = null;
            }
        }

        StartCoroutine(UpdateInfo(0.6f));
    }

    IEnumerator UpdateInfo(float delay)
    {
        yield return new WaitForSeconds(delay);

        int dmg = stats.damage.getValue();
        int armor = stats.armor.getValue();

        atkText.text = dmg.ToString();
        defText.text = armor.ToString();
    }
}
