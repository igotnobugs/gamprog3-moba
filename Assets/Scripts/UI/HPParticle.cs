using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPParticle : MonoBehaviour
{
	[SerializeField]
	private float floatSpeed = 2.0f;

	[SerializeField]
	private float fadeSpeed = 1f;


	private TextMeshPro textDamage;
	private float alpha = 1f;

    private void Awake()
    {
		textDamage = GetComponent<TextMeshPro>();
	}

	private void FixedUpdate()
	{
		alpha = Mathf.Lerp(alpha, 0f, fadeSpeed * Time.deltaTime);
		transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

		Color CurrentColor = textDamage.color;
		textDamage.color = new Color(CurrentColor.r, CurrentColor.g, CurrentColor.b, alpha);

		if (alpha < 0.005f)
		{
			Destroy(gameObject);
		}
	}

	public void SetDamageAmount(float damage)
    {
		textDamage.text = $"- {Mathf.Floor(damage)}";
	}

	public void SetText(string displayText)
	{
		textDamage.text = displayText;
	}
}
