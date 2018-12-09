using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.EditorUtilities;
using UnityEditor;
using Coffee.UIExtensions;

public class TMP_SDFShaderGUI_Dissolve : TMP_SDFShaderGUI
{
	static MaterialPanel dissolvePanel = new MaterialPanel ("Dissolve", true);
	static MaterialPanel spritePanel = new MaterialPanel ("Sprite", true);

	protected override void DoGUI ()
	{
		if(material.HasProperty("_FaceColor"))
		{
			base.DoGUI ();
		}
		else if(DoPanelHeader (spritePanel))
		{
			EditorGUI.indentLevel += 1;
			DoTexture2D ("_MainTex", "Texture");
			DoColor ("_Color", "Color");
			EditorGUI.indentLevel -= 1;
		}

		if (DoPanelHeader (dissolvePanel))
		{
			EditorGUI.indentLevel += 1;
			DoTexture2D ("_NoiseTex", "Texture");

			ColorMode color =
				material.IsKeywordEnabled ("ADD") ? ColorMode.Add
						: material.IsKeywordEnabled ("SUBTRACT") ? ColorMode.Subtract
						: material.IsKeywordEnabled ("FILL") ? ColorMode.Fill
						: ColorMode.Multiply;

			var newColor = (ColorMode)EditorGUILayout.EnumPopup ("Color Mode", color);
			if (color != newColor)
			{
				material.DisableKeyword (color.ToString ().ToUpper ());
				if (newColor != ColorMode.Multiply)
				{
					material.EnableKeyword (newColor.ToString ().ToUpper ());
				}
			}
			EditorGUI.indentLevel -= 1;
		}
	}
}
