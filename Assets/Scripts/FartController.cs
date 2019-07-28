using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartController : MonoBehaviour {

  public SpriteRenderer spriteRender;
  public Color[] colors;
  public int colorHeight;
  public int colorWidth;

  public void Awake() {
    var singleTex = addTexture();
    int numRepetitions = 40;
    var fullTex = new Texture2D(singleTex.width * numRepetitions, singleTex.height + 1, TextureFormat.ARGB32, false);
    fillTex(fullTex, new Color(0, 0, 0, 0));
    for (int i = 0; i < numRepetitions; i++) {
      copyTex(singleTex, fullTex, i * singleTex.width, i % 2);
    }
    fullTex.filterMode = FilterMode.Point;
    float pixelPercent = 1f / (singleTex.height + 1);
    Sprite sprite = Sprite.Create(fullTex, new Rect(0, 0, fullTex.width, fullTex.height), new Vector2(0, 0.5f - pixelPercent / 2 - pixelPercent));
    spriteRender.sprite = sprite;
    spriteRender.flipX = true;
  }

  private void fillTex(Texture2D tex, Color color) {
    for (int y = 0; y < tex.height; y++) {
      for (int x = 0; x < tex.width; x++) {
        tex.SetPixel(x, y, color);
      }
    }
    tex.Apply();
  }

  private void copyTex(Texture2D from, Texture2D to, int x, int y) {
    for (int fromY = 0; fromY < from.height; fromY++) {
      for (int fromX = 0; fromX < from.width; fromX++) {
        Color col = from.GetPixel(fromX, fromY);
        to.SetPixel(fromX + x, fromY + y, col);
      }
    }
    to.Apply();
  }

  private Texture2D addTexture() {
    var tex = new Texture2D(colorWidth, colors.Length * colorHeight, TextureFormat.ARGB32, false);

    // set the pixel values
    for (int i = 0; i < colors.Length; i++) {
      var color = colors[i];
      color.a = 1;
      for (int j = i * colorHeight; j < i * colorHeight + colorHeight; j++) {
        for (int k = 0; k < colorWidth; k++) {
          tex.SetPixel(k, j, color);
        }
      }
    }

    // Apply all SetPixel calls
    tex.Apply();
    tex.filterMode = FilterMode.Point;
    return tex;
  }
}
