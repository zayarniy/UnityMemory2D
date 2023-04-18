using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    //public int N, M;

    public Transform ThisTransform;   
    public Sprite[] sprites;
    //public Image[] images;
    public Canvas canvas;
    public GameObject panel;
    public Sprite  Cover;
    public TMPro.TextMeshProUGUI textCounter, textTimer;
    //public AudioClip soundClickDefault;
    Canvas copyCanvas;
    int counter;

    Transform lastTrans,currentTrans;
    AudioSource asClickDefault;
    int clickCount = 0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        counter = 0;
        startGame = System.DateTime.Now;
        DOTween.defaultAutoKill = false;
        
        lastTrans = null;
        asClickDefault=GetComponent<AudioSource>();
        //ThisTransform = GetComponent<Transform>();
        Shuffle(20);
        List<Sprite> list = new List<Sprite>();
        for (int i = 0; i < sprites.Length; i++)
            list.Add(sprites[i]);

        for (int i=0;i<sprites.Length;i++)
        {
            GameObject go = new GameObject(sprites[i].name);            
            go.transform.SetParent(panel.transform);
            go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<UnityEngine.UI.Image>().sprite = sprites[i];
            go.AddComponent<Button>();
        }

        while (list.Count != 0)
        {
            int index = Random.Range(0, list.Count);
            GameObject go = new GameObject(list[index].name);
            go.transform.SetParent(panel.transform);
            go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<UnityEngine.UI.Image>().sprite = list[index];
            go.AddComponent<Button>();
            list.RemoveAt(index);
        }

        copyCanvas = Instantiate(canvas);
        var pan = copyCanvas.transform.GetChild(0);
        foreach (Transform transform in pan.transform)

        {
            print(transform);
            if (transform.name != "Panel")
            {
                transform.GetComponent<UnityEngine.UI.Image>().sprite = Cover;
                Button button = transform.GetComponent<Button>();
                button.onClick.AddListener(delegate { Flip(transform); });
            }
            
        }

    }

    void Shuffle(int n)
    {

        for(int i=0;i<n;i++)
        {
            int index1 = Random.Range(0, sprites.Length);
            int index2 = Random.Range(0, sprites.Length);
            Object temp = sprites[index1];
            sprites[index1] = sprites[index2];
            sprites[index2] = temp as Sprite;
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
            asClickDefault.Play();
            //textCounter.text = (++counter).ToString();
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

    float spentTime = 0;
    System.DateTime startGame=System.DateTime.Now;

    void Update()
    {
        spentTime += Time.deltaTime;
        if (spentTime>=1)
        {
          //  textTimer.text = (System.DateTime.Now - startGame).TotalSeconds.ToString();
            spentTime = 0;
        }
    }


}
