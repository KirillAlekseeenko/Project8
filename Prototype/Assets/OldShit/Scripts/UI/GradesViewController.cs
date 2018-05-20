using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradesViewController : MonoBehaviour {

    [SerializeField] private Image popularityGrade;
    [SerializeField] private Image revealGrade;

    public void SetPopularityGrade(float value)
    {
        CheckGradeValue(value);
        popularityGrade.GetComponent<RectTransform>().localScale = new Vector3(value, 1, 1);
    }

    public void SetRevealGrade(float value)
    {
        CheckGradeValue(value);
        revealGrade.GetComponent<RectTransform>().localScale = new Vector3(value, 1, 1);
    }

    private void CheckGradeValue(float value)
    {
        if (value > 1)
            throw new UnityException("Grade value more than 1");
    }

}
