using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _heathBar;
    [SerializeField] private Color _low;
    [SerializeField] private Color _high;
    [SerializeField] private Vector3 _offset;

    // Update is called once per frame
    void Update()
    {
        // heathBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + _offset);
    }

    public void SetHealth(float currHealth, float maxHealth) {
        _heathBar.gameObject.SetActive(currHealth < maxHealth);
        _heathBar.value = currHealth;
        _heathBar.maxValue = maxHealth;

        _heathBar.fillRect.GetComponent<Image>().color = Color.Lerp(_low, _high, _heathBar.normalizedValue);
    }
}
