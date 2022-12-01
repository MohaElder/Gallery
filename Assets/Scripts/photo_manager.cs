using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;

public class photo_manager : MonoBehaviour
{
    public Animator light_animator;

    Image[] photos;
    int idx = 0;
    Material mat;
    bool changed = false;


    //the class that serves as a deserialization template
    //for retreiving image array json
    class PhotoList
    {
        public string[] tags;
        public Dictionary<string, Image> images;
    }

    class Image
    {
        public string url;
        public string DateTime;
        public string Camera;
        public string[] Tags;
    }


    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        StartCoroutine("GetPhotoJson");

    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            light_animator.SetTrigger("go_dark");
            changed = false;
        }
        //portion of if statement referenced from http://answers.unity.com/answers/779549/view.html
        if (light_animator.GetCurrentAnimatorStateInfo(0).IsName("go through day") && 
            light_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && 
            !light_animator.IsInTransition(0) && 
            !changed)
        {
            StartCoroutine("ChangePic");
        }
    }


    IEnumerator GetPhotoJson()
    {
        UnityWebRequest req = UnityWebRequest.Get("https://raw.githubusercontent.com/MohaElder/me/main/src/utils/imageLink.json");
        yield return req.SendWebRequest();
        var res = req.downloadHandler.text;
        var ret = JsonConvert.DeserializeObject<PhotoList>(res);
        photos = ret.images.Values.ToArray();
        Debug.Log(photos[0]);
    }

    //code derived from Self-Reliance https://github.com/MohaElder/SelfReliance
    IEnumerator ChangePic()
    {
        changed = true;
        idx = idx + 1 < photos.Length ? idx + 1 : 0;
        UnityWebRequest req = new UnityWebRequest(photos[idx].url);
        DownloadHandlerTexture texD = new DownloadHandlerTexture(true);
        req.downloadHandler = texD;
        yield return req.SendWebRequest();
        Debug.Log("sending");
        if (string.IsNullOrEmpty(req.error))
        {
            mat.SetTexture("_MainTex", texD.texture); //_MainTex is equivalent to Albedo
        }
        light_animator.SetTrigger("go_bright");
    }
}
