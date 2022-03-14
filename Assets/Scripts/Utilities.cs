using UnityEngine;
using System.Collections;

public class Utilities {

	public static bool IsObjectInLayer(GameObject gameObject, LayerMask mask) {
        //  Bithack found thanks to https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
        int maskValue = mask.value;
        return maskValue == (maskValue | (1 << gameObject.layer));
    }

    public static float outOfWorldY = -50f;
}
