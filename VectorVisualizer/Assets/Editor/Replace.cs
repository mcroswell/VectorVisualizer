using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

// Replace - by Michael L. Croswell for Colorado Game Coders, LLC
// (c) 2016, Colorado Game Coders, LLC
// Updated to use Destroy with 1 second delay. 2019 -mlc
public class Replace : ScriptableWizard
{
    public GameObject[] _selectedObjects;
    public GameObject _theNewObject;
    public bool _useOldScale = false;
    public bool _useOldName = false;
    public bool _deleteOldSelected = false;
    public bool _disableOldSelected = true;
    public bool _usePrefab = true;

    [MenuItem("Custom/Replace...")]

    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<Replace>("Replace", "Replace");
    }

    void OnWizardUpdate()
    {
        helpString = "Select the objects to replace.";
        _selectedObjects = Selection.gameObjects;
        if (_selectedObjects == null || _selectedObjects.Length == 0)
        {
            errorString = "Select objects in the scene to replace.";
            isValid = false;
        }
        else if (_theNewObject == null)
        {
            errorString = "Select the new object (which will replace others).";
            isValid = false;
        }
        else
        {
            errorString = "";
            isValid = true;
        }
    }

    void OnWizardCreate()
    {
        if (!isValid)
            return;
        string msg = "";
        msg += "Replaced the objects starting with  " + _selectedObjects[0].name + " with " + _theNewObject.name;
        Vector3 newPos = Vector3.zero;
        int i = 0;
        for (i = 0; i < _selectedObjects.Length; i++)
        {
            newPos = _selectedObjects[i].transform.position;
            //GameObject 
            GameObject go;
            if (_usePrefab)
            {
                go = (GameObject) PrefabUtility.InstantiatePrefab(_theNewObject);
                
                go.transform.position = newPos;
                go.transform.rotation = _selectedObjects[i].transform.rotation;
            }
            else
            {
                go = (GameObject)Instantiate(_theNewObject, newPos, _theNewObject.transform.rotation);
            }
            go.transform.parent = _selectedObjects[i].transform.parent;

            if (_useOldScale)
            {
                go.transform.localScale = _selectedObjects[i].transform.localScale;
            }
            if (_useOldName)
            {
                go.name = _selectedObjects[i].name;
            }
            else
            {
                go.name = _theNewObject.name + "_" + i;
            }
            if (_deleteOldSelected)
            {
                DestroyImmediate(_selectedObjects[i]);
            }
            else if (_disableOldSelected)
            {
                _selectedObjects[i].SetActive(false);
            }
        }
        msg += " " + i + " times. ";
        EditorUtility.DisplayDialog("What happened:", msg, "OK", "");
    }

}