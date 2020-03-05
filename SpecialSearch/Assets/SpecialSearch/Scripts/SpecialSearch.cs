using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace RedLabsGames.SpecialSearch
{
    public class SpecialSearch
    {
        SearchField searchField;

        private string searchedWord;

        private string gameObjectName;
        private string gameObjectComponent;
        private string gameObjectComponentVariableName;
        private string gameObjectComponentVariableValue;

        private int wordCount = 0;


        private List<GameObject> matchedObjects = new List<GameObject>();
        private List<GameObject> unMatchedName = new List<GameObject>();
        private List<GameObject> unMatchedComponent = new List<GameObject>();
        private List<GameObject> unMatchedComponentValue = new List<GameObject>();

        public Action<string> onInputChanged;

        public void OnToolbarGUI()
        {
            DrawSearchBar(true);
            if (GUILayout.Button(new GUIContent("Find GameObjects")))
            {
                ClearSearch();
            }
        }
        public void OnGUI()
        {
            DrawSearchBar(false);
        }

        private void DrawSearchBar(bool asToolBar)
        {
            Rect rect = GUILayoutUtility.GetRect(1, 1, 25, 15, GUILayout.ExpandWidth(true));

            CreateSearchBar(rect, asToolBar);
        }
        private void CreateSearchBar(Rect rect, bool asToolBar)
        {
            if (searchField == null)
            {
                searchField = new SearchField();
            }

            string result = asToolBar ? searchField.OnToolbarGUI(rect, searchedWord) : searchField.OnGUI(rect, searchedWord);

            if (result != searchedWord && onInputChanged != null)
            {
                onInputChanged(result);
            }

            searchedWord = result;
        }

        public void WordToBeSearched(string _Word)
        {
            searchedWord = _Word;
        }

        public void SearchForTheWord()
        {
            WordSplit(searchedWord);

            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                unMatchedName.Add(obj);
                switch (wordCount)
                {
                    case 1:
                        if (obj.name.ToLower() == gameObjectName.ToLower())
                        {
                            matchedObjects.Add(obj);
                            unMatchedName.Remove(obj);
                        }
                        break;

                    case 2:
                        if (obj.name.ToLower() == gameObjectName.ToLower())
                        {
                            unMatchedComponent.Add(obj);
                            if (obj.GetComponent(gameObjectComponent) != null)
                            {
                                matchedObjects.Add(obj);
                                unMatchedComponent.Remove(obj);
                            }
                        }
                        break;
                    case 3:
                        Debug.LogError("Type The value");
                        break;
                    case 4:
                        if (obj.name.ToLower() == gameObjectName.ToLower())
                        {
                            unMatchedComponentValue.Add(obj);
                            if (obj.GetComponent(gameObjectComponent) == null)
                            {
                                unMatchedComponentValue.Remove(obj);
                            }
                            if (obj.GetComponent(gameObjectComponent) != null)
                            {
                                unMatchedComponent.Add(obj);
                                if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName) != null)
                                {

                                    if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).PropertyType == typeof(string))
                                    {

                                        if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent), null).ToString().Contains(gameObjectComponentVariableValue.ToLower()))
                                        {
                                            matchedObjects.Add(obj);
                                            unMatchedComponentValue.Remove(obj);
                                        }
                                    }
                                    else if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).PropertyType == typeof(bool))
                                    {
                                        if ((bool)obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent), null) == bool.Parse(gameObjectComponentVariableValue))
                                        {
                                            matchedObjects.Add(obj);
                                            unMatchedComponentValue.Remove(obj);
                                        }
                                    }
                                    else if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).PropertyType == typeof(int))
                                    {
                                        if ((int)obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent), null) == int.Parse(gameObjectComponentVariableValue))
                                        {
                                            matchedObjects.Add(obj);
                                            unMatchedComponentValue.Remove(obj);
                                        }
                                    }
                                    else if (obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).PropertyType == typeof(float))
                                    {
                                        if ((float)obj.GetComponent(gameObjectComponent).GetType().GetProperty(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent), null) == float.Parse(gameObjectComponentVariableValue))
                                        {
                                            matchedObjects.Add(obj);
                                            unMatchedComponentValue.Remove(obj);
                                        }
                                    }
                                }
                                else
                                {
                                    if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName) != null)
                                    {
                                        if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).FieldType == typeof(string))
                                        {
                                            if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent)).ToString().Contains(gameObjectComponentVariableValue))
                                            {
                                                matchedObjects.Add(obj);
                                                unMatchedComponentValue.Remove(obj);
                                            }
                                        }
                                        else if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).FieldType == typeof(bool))
                                        {
                                            if ((bool)obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent)) == bool.Parse(gameObjectComponentVariableValue))
                                            {
                                                matchedObjects.Add(obj);
                                                unMatchedComponentValue.Remove(obj);
                                            }
                                        }
                                        else if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).FieldType == typeof(int))
                                        {
                                            if ((int)obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent)) == int.Parse(gameObjectComponentVariableValue))
                                            {
                                                matchedObjects.Add(obj);
                                                unMatchedComponentValue.Remove(obj);
                                            }
                                        }
                                        else if (obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).FieldType == typeof(float))
                                        {
                                            if ((float)obj.GetComponent(gameObjectComponent).GetType().GetField(gameObjectComponentVariableName).GetValue(obj.GetComponent(gameObjectComponent)) == float.Parse(gameObjectComponentVariableValue))
                                            {
                                                matchedObjects.Add(obj);
                                                unMatchedComponentValue.Remove(obj);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            wordCount = 0;
        }

        private void WordSplit(string _word)
        {
            char[] separators = new char[] { '>' };

            foreach (string word in _word.Split(separators, StringSplitOptions.RemoveEmptyEntries))
            {
                wordCount++;
                if (wordCount == 1)
                {
                    gameObjectName = word;
                }
                if (wordCount == 2)
                {
                    gameObjectComponent = word;
                }
                if (wordCount == 3)
                {
                    gameObjectComponentVariableName = word;
                }
                if (wordCount == 4)
                {
                    gameObjectComponentVariableValue = word;
                }
            }
        }

        public void SelectMatchedGameObjects()
        {
            if (matchedObjects.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < matchedObjects.Count; i++)
            {
                Selection.objects = matchedObjects.ToArray();
            }
        }

        public void SelectUnmatchedGameObjects()
        {
            if (unMatchedName.Count <= 0 && unMatchedComponent.Count <= 0 && unMatchedComponentValue.Count <= 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < unMatchedName.Count; i++)
                {
                    Selection.objects = unMatchedName.ToArray();
                }
                for (int i = 0; i < unMatchedComponent.Count; i++)
                {
                    Selection.objects = unMatchedComponent.ToArray();
                }
                for (int i = 0; i < unMatchedComponentValue.Count; i++)
                {
                    Selection.objects = unMatchedComponentValue.ToArray();
                }
            }
        }

        public void ClearSearch()
        {
            matchedObjects.Clear();
            unMatchedName.Clear();
            unMatchedComponent.Clear();
            unMatchedComponentValue.Clear();
        }
    }
}
