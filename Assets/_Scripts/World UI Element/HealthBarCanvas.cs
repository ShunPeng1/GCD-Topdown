using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class HealthBarCanvas : MonoBehaviour
{
    
    [SerializeField] private Slider _heathBar;
    [SerializeField] private float _showTime = 5f, _hideSpeed = 0.5f;
    
    private CanvasGroup _canvasGroup;
    
    // Update is called once per frame
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(true);
        _heathBar.value = 1;
        _heathBar.maxValue = 1;
        _canvasGroup.alpha = 0;
    }

    public void SetHealth(float currHealth, float maxHealth, bool isShow = true)
    {
        StopAllCoroutines();
        StartCoroutine(ShowUntilHide(currHealth, maxHealth, isShow));
    }

    IEnumerator ShowUntilHide(float currHealth, float maxHealth, bool isShow) 
    {
        _heathBar.value = currHealth;
        _heathBar.maxValue = maxHealth;
        _canvasGroup.alpha = isShow? 1 : 0;
        yield return new WaitForSeconds(_showTime);
        
        for (float t = 0f; t < _hideSpeed; t += Time.deltaTime) {
            _canvasGroup.alpha = Mathf.Lerp( 1, 0, t / _hideSpeed);
            yield return null;
        }

        _canvasGroup.alpha = 0;
    }
    
}
