using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehaviour : MonoBehaviour
{
    [SerializeField] private float maxHitPoint;
    private float currHitPoint;
    [SerializeField] private EnvironmentHealthBar healthBarBehavior;
    // Start is called before the first frame update
    void Start()
    {
        currHitPoint = maxHitPoint;
        healthBarBehavior.SetHealth(currHitPoint, maxHitPoint);
    }

    private void TakeDamge(float _value) {
        currHitPoint = Mathf.Clamp(currHitPoint - _value, 0, maxHitPoint);
        healthBarBehavior.SetHealth(currHitPoint, maxHitPoint);
        if (currHitPoint <= 0) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P)) {
        //     TakeDamge(1);
        // }
    }
}
