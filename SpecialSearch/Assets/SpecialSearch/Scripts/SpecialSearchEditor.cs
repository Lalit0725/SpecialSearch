using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RedLabsGames.SpecialSearch
{
    public class SpecialSearchEditor : EditorWindow
    {
        [MenuItem("Red Labs Games/Special Search/Special Search")]
        static void Init()
        {
            GetWindowWithRect<SpecialSearchEditor>(new Rect(0, 100, 200, 75), false, "Special Search").Show();
            CustomLayout.LoadSpecialSearchLayout("Assets/SpecialSearch/Layout/SpecialSearchLayout.wtl");
        }

        [SerializeField]
        SpecialSearch specialSearch;

        private void OnEnable()
        {
            if (specialSearch == null)
            {
                specialSearch = new SpecialSearch();
            }
            specialSearch.onInputChanged = onInputChanged;

        }

        private void OnGUI()
        {
            GUILayout.Label("Special Search", EditorStyles.boldLabel);
            specialSearch.OnGUI();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Find GameObjects")))
            {
                specialSearch.SearchForTheWord();
                specialSearch.SelectMatchedGameObjects();
            }

            if (GUILayout.Button(new GUIContent("Inverse Select")))
            {
                specialSearch.SelectUnmatchedGameObjects();
                specialSearch.ClearSearch();
            }

            if (GUILayout.Button(new GUIContent("Clear Selection")))
            {
                Selection.objects = new Object[0];
            }

            GUILayout.EndHorizontal();
        }

        void onInputChanged(string _Word)
        {
            if (!string.IsNullOrEmpty(_Word))
            {
                specialSearch.ClearSearch();
                specialSearch.WordToBeSearched(_Word);
            }
        }
    }
}