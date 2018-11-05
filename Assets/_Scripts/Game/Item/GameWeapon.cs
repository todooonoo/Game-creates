using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWeapon : GameItem {

    [SerializeField]
    protected WeaponType weaponType;

	public void OnEquip(Player3D player)
    {
        player.AnimationController.SetWeaponType(weaponType);

        // TODO: Update equipment canvas
    }
}
