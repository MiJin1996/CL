using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ImageTracker : MonoBehaviour
{
    ARTrackedImageManager manager;
    [SerializeField] List<string> listName;            //배열 개수
    [SerializeField] List<GameObject> listAnimal;

    Dictionary<string, GameObject> dictPrefab = new(); // key값을 써서 value값을 불러내어 바로 사용할 수 있음 ||배열처럼 쓰는 것임
    Dictionary<string, GameObject> dictSpawn = new(); //=new는 필요할때마다 새로 만들어지기 위해 || 메모리를 적게 쓰기위해


    void Awake()                              //start함수보다 먼저 실행됨 || 사전 준비 || 컴퍼넌트의 초기화
    {
        manager = FindFirstObjectByType<ARTrackedImageManager>();

        for (int i = 0; i < listName.Count; i++)
        {
            dictPrefab[listName[i]] = listAnimal[i];
        }
    }


    void SpawnCharacter(ARTrackedImage img)
    {
        string name = img.referenceImage.name;
        if (!dictPrefab.ContainsKey(name)) return;

        var go = Instantiate(dictPrefab[name], img.transform);  //트랙킹 이미지가 위치가 이리저리 움직여도 트레킹되도록 하는 한 줄
        go.transform.localPosition = Vector3.zero;              //깔끔하게 하기위해 로콜 스케일을 초기화함  || 없애도 되는 줄

        dictSpawn[name] = go;                                  // 게임 오브젝트를 [name]안에 이름을 넣어서 삭제하도록 할 수 있음
    }

    void UpdateCharacter(ARTrackedImage img)
    {
        string name = img.referenceImage.name;

        if (!dictSpawn.ContainsKey(name)) return;


        bool active = img.trackingState == TrackingState.Tracking;

        dictSpawn[name].SetActive(active);



        void HideChatacter(ARTrackedImage img)
        {
            string name = img.referenceImage.name;

            if (dictSpawn.ContainsKey(name))

            {
                dictSpawn[name].SetActive(false);
            }

        }

        void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
        {
            foreach (var img in args.added)                //for문과 같음 || C#에서 사용함    var: 다목적으로 자동으로 타입을 추론하게함(int면 in변수, double이면 double로...?
                SpawnCharacter(img);

            foreach (var img in args.updated)
                UpdateCharacter(img);

            foreach (var img in args.removed)
                HideChatacter(img.Value);
        }


        void OnEnable()     //활성화    
        {
            manager.trackablesChanged.AddListener(OnChanged);
        }

        void OnDisable()    //비활성화
        {
            manager.trackablesChanged.RemoveListener(OnChanged);
        }

        void Start()
        {

        }


        void Update()
        {

        }
    }
}
