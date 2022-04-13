using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AttackData {
    public enum BasicAttackType {
        LeftPunch,
        RightPunch,
        Kick
    };

    public static int getAttackDamage(BasicAttackType attackType) {
        switch (attackType) {
            default: {
                return 10;
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
