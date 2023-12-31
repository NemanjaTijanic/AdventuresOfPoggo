using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();

        // Equip
        EquipmentManager.instance.Equip(this);

        // Remove from INV
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Ring, Bracelet, Necklace}
