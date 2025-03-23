using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class GMTools : MonoBehaviour {
    [LabelText("Ellen 无敌")]
    public bool EllenInvinciable;

    private bool mHideAllChomper;
    private GameObject[] mChompers;
    private List<GameObject> mCreatedObjects = new List<GameObject>();
    [Button("隐藏所有 Chomper")]
    public void HideAllChomper() {
        if (mChompers == null) {
            mChompers = GameObject.FindGameObjectsWithTag("Chomper");    
        }
        
        foreach (var chomper in mChompers) {
            chomper.SetActive(mHideAllChomper);
        }
        mHideAllChomper = !mHideAllChomper;
    }

    [Button("创建 Chomper")]
    public void CreateChomper() {
        InstantiatePrefab("Assets\\3DGamekit\\Prefabs\\Characters\\Enemies\\Chomper\\Chomper.prefab");
    }
    
    [Button("创建 Grenadia")]
    public void CreateGrenadia() {
        InstantiatePrefab("Assets\\3DGamekit\\Prefabs\\Characters\\Enemies\\Grenadier\\Grenadier.prefab");
    }
    
    [Button("删除GM创建的所有对象")]
    public void DestroyAllCreatedObjects() {
        if (!Application.isPlaying) {
            return;
        }
        foreach (var obj in mCreatedObjects) {
            Destroy(obj);
        }
        mCreatedObjects.Clear();
    }
    
    void Start() {
        DontDestroyOnLoad(this);
    }
    
    public static GMTools Instance { get; private set; }

    GMTools() {
        Instance = this;
    }

    private void InstantiatePrefab(string prefabPath) {
        if (!Application.isPlaying) {
            return;
        }
        var sample = GameObject.Find("Ellen");
        if (sample == null) {
            sample = this.gameObject;
        }
        
        // 加载 Prefab
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab != null) {
            // 计算实例化位置
            Vector3 instantiatePosition = sample.transform.position + sample.transform.forward * 2;

            // 实例化 Prefab
            var inst = Instantiate(prefab, instantiatePosition, Quaternion.identity);
            mCreatedObjects.Add(inst);
        }
        else {
            Debug.LogError("Prefab not found at path: " + prefabPath);
        }
    }
    
    void Update() {
    }
}