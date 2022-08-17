using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _health;



    public void SetHP(float hpNormalized)
    {
        _health.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    //Makes the health bar go down smoothly
    public IEnumerator SetHPSmooth(float newHp)
    {
        float curHp = _health.transform.localScale.x;
        float changeAmt = curHp - newHp;

        //runs until the diff of curHp and newHp is a very small value
        while (curHp - newHp > Mathf.Epsilon)
        {
            curHp -= changeAmt * Time.deltaTime;
            _health.transform.localScale = new Vector3(curHp, 1f);
            yield return null;
        }

        //sets the scale of the health bar to the new hp
        _health.transform.localScale = new Vector3(newHp, 1f);
    }
}
