using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [Serializable]
    public struct AugmentedObject
    {
        public string name;
        public GameObject prefab;
    }
    [SerializeField]
    private XRReferenceImageLibrary m_ImageLibrary;
    [SerializeField]
    [Tooltip("Prefab to spawn on\n Use similiar name with images at Image Library")]
    /*private Dictionary<string, GameObject> objectToPlace = new Dictionary<string, GameObject>();*/
    private List<AugmentedObject> objectToPlace = new List<AugmentedObject> { };
    private Guid[] imageGUID;
    private List<AugmentedObject> spawnedObjects = new List<AugmentedObject> { };
    private ARTrackedImageManager m_ImageManager;
    public TMP_Text t;



    private void Awake()
    {
        m_ImageManager=FindObjectOfType<ARTrackedImageManager>();
        spawnedObjects = new List<AugmentedObject> { };
        //t = FindObjectOfType<TextMeshPro>();
        /*        foreach(GameObject prefabs in placeablePrefabs)
                {
                    GameObject newPrefabs = Instantiate(prefabs, Vector3.zero, Quaternion.identity);
                    newPrefabs.name = prefabs.name;
                    spawnedObjects.Add(prefabs.name, newPrefabs);
                }*/
    }
    private void OnEnable()
    {
        imageGUID= new Guid[0];
        foreach(var image in m_ImageLibrary)
        {
            imageGUID.Append(image.guid);
            t.text = t.text + "\n onEnable "+ image.guid;
        }
        m_ImageManager.trackedImagesChanged += OnImageChanged;
        
    }

    private void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= OnImageChanged;
        t.text = t.text + "\n onDisable ";
    }
    private void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage image in eventArgs.added)
        {
            string name = image.referenceImage.name;
            AugmentedObject obj = new AugmentedObject();
            obj.name = name;
            int objIndex = objectToPlace.FindIndex(obj => obj.name == name);
            obj.prefab = Instantiate(objectToPlace[objIndex].prefab, image.transform.position, image.transform.rotation);
            spawnedObjects.Append(obj);
            t.text = t.text + "\n added "+name;
        }
        foreach (ARTrackedImage image in eventArgs.updated)
        {
            if(image.trackingState == TrackingState.Tracking)
            {
                for(int i = 0; i < spawnedObjects.Count; i++)
                {
                    t.text = t.text +" "+ spawnedObjects[i].name +" "+ image.referenceImage.name;
                    if (spawnedObjects[i].name == image.referenceImage.name)
                    {
                        spawnedObjects[i].prefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    }
                }
            }
            //t.text = t.text + "\n update " + image.trackingState.ToString();
        }
        foreach (ARTrackedImage image in eventArgs.removed)
        {
            t.text = t.text + "\n removed " + image.referenceImage.name;
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                if (spawnedObjects[i].name == image.referenceImage.name)
                {
                    Destroy(spawnedObjects[i].prefab);
                    spawnedObjects.RemoveAt(i);
                }
            }
        }
    }
    /*private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedObjects[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach(GameObject obj in spawnedObjects.Values)
        {
            if(obj.name != name)
            {
                obj.SetActive(false);
            }
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
