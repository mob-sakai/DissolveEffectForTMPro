using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Coffee.UIExtensions
{
	public class DissolveEffectForTMPro_Demo : MonoBehaviour
	{
		public void Play ()
		{
			foreach (var d in GetComponentsInChildren<UIDissolve> ())
			{
				d.Play ();
			}
		}

		public void SetEffectFactor (float value)
		{
			foreach (var d in GetComponentsInChildren<UIDissolve> ())
			{
				d.effectFactor = value;
			}
		}

		public void SetWidth (float value)
		{
			foreach (var d in GetComponentsInChildren<UIDissolve> ())
			{
				d.width = value;
			}
		}

		public void SetSoftness (float value)
		{
			foreach (var d in GetComponentsInChildren<UIDissolve> ())
			{
				d.softness = value;
			}
		}

		public void SplitByCharacter (bool flag)
		{
			foreach (var d in GetComponentsInChildren<UIDissolve> ())
			{
				d.effectArea = flag ? EffectArea.Character : EffectArea.RectTransform;
			}
		}
	}
}