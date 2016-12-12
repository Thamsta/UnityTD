using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float maxHealth;
    public float currentHealth;
    private float originalScale;

	// Use this for initialization
	void Start () {
        originalScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update () {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = (currentHealth / maxHealth) * originalScale;
        gameObject.transform.localScale = tmpScale;
    }

    /// <summary>
    /// 
    /// </summary>
     /// <returns>Relative life in percent</returns>
    public float RelativeLife()
    {
        return currentHealth / maxHealth * 100;
    }
}
