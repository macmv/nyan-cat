using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMeter : MonoBehaviour {
  private Texture2D primaryTex;
  private Texture2D backgroundTex;
  public PlayerController player;

  public void Awake() {
    primaryTex = new Texture2D(1, 1);
    primaryTex.SetPixel(0, 0, new Color(0, 255, 0));
    primaryTex.wrapMode = TextureWrapMode.Repeat;
    primaryTex.Apply();
    backgroundTex = new Texture2D(1, 1);
    backgroundTex.SetPixel(0, 0, new Color(255, 0, 0));
    backgroundTex.wrapMode = TextureWrapMode.Repeat;
    backgroundTex.Apply();
  }

  void OnGUI() {
    var rect = new Rect(50, Screen.height - 200 - 50, 50, 200);
    Debug.Log(rect);
    GUI.skin.box.normal.background = backgroundTex;
    GUI.Box(rect, GUIContent.none);
    var boost = player.getBoost();
    if (boost > 0) {
      rect.y = rect.y - player.getBoost() * 2 + 200;
      rect.height = player.getBoost() * 2;
      GUI.skin.box.normal.background = primaryTex;
      GUI.Box(rect, GUIContent.none);
    }
  }
}
