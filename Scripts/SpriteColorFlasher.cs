using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorFlasher : MonoBehaviour
{
    public void FlashColor(SpriteRenderer spriteRend, float duration, Color color) {
        //TODO: Safety check that are sprite isn't already in use?
        StartCoroutine(DoColorFlash(spriteRend, duration, color));
    }

    private IEnumerator DoColorFlash(SpriteRenderer spriteRend, float duration, Color newColor) {
        Color oldColor = spriteRend.color;
        spriteRend.color = newColor;
        yield return new WaitForSeconds(duration);
        //Check to ensure the sprite's game object hasn't been destroyed
        if(spriteRend != null) {
            spriteRend.color = oldColor;
        }
    }
}
