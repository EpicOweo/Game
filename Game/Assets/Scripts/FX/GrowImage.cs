using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GrowImage : MonoBehaviour {
    Image img;

    void Start() {
        img = GetComponent<Image>();
        Reset();
        StartCoroutine(_Run());
    }

    public void Update() {
        
    }

    IEnumerator _Run() {     

        bool done = false;
        
        //while(true) {
            if(!done) {

                while(img.rectTransform.sizeDelta.x <= 1250) {
                    img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, img.rectTransform.sizeDelta.x+25);
                    img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, img.rectTransform.sizeDelta.y+25);
                    //img.rectTransform.localPosition -= new Vector3(1, 1, 0);

                    yield return new WaitForEndOfFrame();
                }

                done = true;

            } else {
                Reset();
                done = false;
            }   
        //}
    }

    void Reset() {
        img.rectTransform.sizeDelta = Vector2.zero;
        //img.rectTransform.localPosition = Vector3.zero;

    }
}