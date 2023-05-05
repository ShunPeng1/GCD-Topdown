using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentHealthBar : MonoBehaviour
{
    [SerializeField] private Slider heathBar;
    [SerializeField] private Color low;
    [SerializeField] private Color high;
    [SerializeField] private Vector3 _offset;

    // Update is called once per frame
    void Update()
    {
        heathBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public void SetHealth(float currHealth, float maxHealth) {
        Debug.Log(currHealth < maxHealth);
        heathBar.gameObject.SetActive(currHealth < maxHealth);
        heathBar.value = currHealth;
        heathBar.maxValue = maxHealth;

        heathBar.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, heathBar.normalizedValue);
    }
}
