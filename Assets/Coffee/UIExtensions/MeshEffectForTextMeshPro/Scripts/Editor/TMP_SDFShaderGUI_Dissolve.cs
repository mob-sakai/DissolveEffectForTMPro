using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.EditorUtilities;
using UnityEditor;
using Coffee.UIExtensions;

public class TMP_SDFShaderGUI_Dissolve : TMP_SDFShaderGUI
{
	GUIStyle panelTitle;
	Material currentMaterial;

	public override void OnGUI (MaterialEditor materialEditor, MaterialProperty [] properties)
	{
		currentMaterial = materialEditor.target as Material;
		base.OnGUI (materialEditor, properties);
	}

	protected override void DoGUI ()
	{
		if (currentMaterial.HasProperty ("_FaceColor"))
		{
			base.DoGUI ();
		}
		else
		{
			if (BeginCommonPanel ("Sprite", true))
			{
				EditorGUI.indentLevel++;
				DoTexture2D ("_MainTex", "Texture");
				DoColor ("_Color", "Color");
				EditorGUI.indentLevel--;
			}
			EndCommonPanel ();
		}

		if (BeginCommonPanel ("Dissolve", true))
		{
			EditorGUI.indentLevel++;
			DoTexture2D ("_NoiseTex", "Texture");

			ColorMode color =
				currentMaterial.IsKeywordEnabled ("ADD") ? ColorMode.Add
						: currentMaterial.IsKeywordEnabled ("SUBTRACT") ? ColorMode.Subtract
						: currentMaterial.IsKeywordEnabled ("FILL") ? ColorMode.Fill
						: ColorMode.Multiply;

			var newColor = (ColorMode)EditorGUILayout.EnumPopup ("Color Mode", color);
			if (color != newColor)
			{
				currentMaterial.DisableKeyword (color.ToString ().ToUpper ());
				if (newColor != ColorMode.Multiply)
				{
					currentMaterial.EnableKeyword (newColor.ToString ().ToUpper ());
				}
			}
			EditorGUI.indentLevel--;
		}
		EndCommonPanel ();
	}

	bool BeginCommonPanel (string panel, bool expanded)
	{
		if (panelTitle == null)
		{
			panelTitle = new GUIStyle (EditorStyles.label) { fontStyle = FontStyle.Bold };
		}

		EditorGUILayout.BeginVertical (EditorStyles.helpBox);
		Rect position = EditorGUI.IndentedRect (GUILayoutUtility.GetRect (20f, 18f));
		position.x += 20;
		position.width += 6f;
		expanded = GUI.Toggle (position, expanded, panel, panelTitle);
		EditorGUI.indentLevel++;
		EditorGUI.BeginDisabledGroup (false);
		return expanded;
	}

	void EndCommonPanel ()
	{
		EditorGUI.EndDisabledGroup ();
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical ();
	}
}
