using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    private ARTrackedImageManager _ARTrackedImageManager;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        _ARTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _ARTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    public void OnDisable()
    {
        _ARTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var trackedImage in args.added)
        {
            print(trackedImage.name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
