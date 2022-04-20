using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AttackData {
    public enum BasicAttackType {
        LeftPunch,
        RightPunch,
        Kick,
        DoublePunch,
        Super
    };

    public static int getAttackDamage(BasicAttackType attackType) {
        switch (attackType) {
            default: {
                return 10;
            }
            case BasicAttackType.LeftPunch: {
                return 20;
            }
            case BasicAttackType.DoublePunch:
            {
                return 14;
            }
            case BasicAttackType.Super:
            {
                return 35;
            }
            
        }
    }

    public static Vector2 getAttackDirection(BasicAttackType attackType) {
        switch (attackType) {
            default: {
                return Vector2.right;
            }
        }
    }
}
