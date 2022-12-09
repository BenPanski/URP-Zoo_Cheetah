using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeIn : MonoBehaviour
{
    [SerializeField] float fadeTime = 1f;
    Image image;
    // Start is called before the first frame update

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        var temp = image.color;
        temp.a = 0;
        image.color = temp;
    }

    private void Update()
    {
        if (image.color.a<255)
        {
            var temp = image.color;
            temp.a += Time.deltaTime / fadeTime;
            image.color = temp;
        }
        
    }

}
