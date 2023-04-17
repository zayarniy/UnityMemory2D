using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    //public int N, M;

    public Transform ThisTransform;   
    public Sprite[] sprites;
    public Image[] images;
    public Canvas canvas;
    public Sprite  Cover;


    Transform lastTrans,currentTrans;

    int clickCount = 0;
    Canvas copyCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        DOTween.defaultAutoKill = false;
        lastTrans = null;
        
        //ThisTransform = GetComponent<Transform>();
        for(int i=0;i<2*sprites.Length;i++)
        {
            GameObject go = new GameObject(sprites[i % sprites.Length].name);
            
            go.transform.SetParent(canvas.transform);
            go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprites[i% sprites.Length];
            go.AddComponent<Button>();

        }
        copyCanvas = Instantiate(canvas);
        foreach (Transform transform in copyCanvas.transform)

        {
            print(transform);
            transform.GetComponent<UnityEngine.UI.Image>().sprite = Cover;
            Button button=transform.GetComponent<Button>();
            button.onClick.AddListener(delegate { Flip(transform); });
            
            
        }

    }

    void RewindAnim()
    {
        print(lastTrans.name + ":" + currentTrans.name);
        lastTrans.DORotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetLoops(1, LoopType.Restart).SetEase(Ease.Linear);
        lastTrans.DOScaleX(1, 1);
        currentTrans.DORotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetLoops(1, LoopType.Restart).SetEase(Ease.Linear);
        currentTrans.DOScaleX(1, 1);
        lastTrans = null;
        clickCount = 0;
    }

    public void Flip(Transform trans)
    {
        if (clickCount<2)
        {
            clickCount++;
            print(lastTrans);
            //transform.DOFlip();
            //ThisTransform.DOMove(new Vector3(10, 0, 0), 1);
            trans.DORotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetLoops(1, LoopType.Restart).SetEase(Ease.Linear);
            trans.DOScaleX(0, 1);
            if (lastTrans == null)
                lastTrans = trans;
            else
            {
                print(lastTrans.name + ":" + trans.name);
                if (lastTrans.name != trans.name)
                {
                    currentTrans = trans;
                    print(lastTrans.name + ":" + currentTrans.name);
                    Invoke("RewindAnim", 1);
                }
                else

                {
                    clickCount = 0;
                    lastTrans = null;
                }

            }
        }
    }


    public void Flip()
    {
        print("flip");
        print(name);
        //transform.DOFlip();
        //ThisTransform.DOMove(new Vector3(10, 0, 0), 1);
        ThisTransform.DORotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
