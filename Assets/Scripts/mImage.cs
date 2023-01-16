using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class mImage
{
    public static Sprite GetSprite(byte[] imageSource, int width, int height)
    {
        var tex = new Texture2D(1, 1);
        tex.LoadImage(imageSource);
        var sprite = Sprite.Create(tex,
            new Rect(0f, 0f, tex.width, tex.height),
            new Vector2(0,0),
            100f);

        return sprite;
    }
}
