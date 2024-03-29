using CorePackage.StateMachine.ScriptableObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CorePackage.StateMachine.Editor
{
    internal class TransitionTableEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset _visualTree;
        [SerializeField]
        private StyleSheet _styleSheet;

        public static string test = "";
        private static TransitionTableEditorWindow _window;
        private bool _doRefresh;

        private UnityEditor.Editor _transitionTableEditor;

        [MenuItem("State Machine Editor", menuItem = "Tools/State Machine Editor")]
        internal static void Display()
        {
            if (_window == null)
            { _window = GetWindow<TransitionTableEditorWindow>("State Machine Editor"); }

            _window.Show();
        }

        private void OnEnable()
        {
            rootVisualElement.Add(_visualTree.CloneTree());

            string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
            rootVisualElement.Query<Label>().Build().ForEach(label => label.AddToClassList(labelClass));

            rootVisualElement.styleSheets.Add(_styleSheet);

            minSize = new Vector2(480, 360);

            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
            { _doRefresh = true; }
        }

        /// <summary>
        /// Update list every time we gain focus
        /// </summary>
        private void OnFocus()
        {
            // Calling CreateListView() from here when the window is docked
            // throws a NullReferenceException in UnityEditor.DockArea:OnEnable().
            if (!_doRefresh)
            { _doRefresh = true; }
        }

        private void OnLostFocus()
        {
            ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
#if UNITY_2022_2_OR_NEWER
            listView.selectionChanged -= OnListSelectionChanged;
#else
			listView.onSelectionChange -= OnListSelectionChanged;
#endif
        }

        private void Update()
        {
            if (!_doRefresh)
            { return; }

            CreateListView();
            _doRefresh = false;
        }

        private void CreateListView()
        {
            TransitionTableSO[] assets = FindAssets();
            ListView listView = rootVisualElement.Q<ListView>(className: "table-list");

            listView.makeItem = null;
            listView.bindItem = null;

            listView.itemsSource = assets;
            listView.fixedItemHeight = 16;
            string labelClass = $"label-{(EditorGUIUtility.isProSkin ? "pro" : "personal")}";
            listView.makeItem = () =>
            {
                Label label = new();
                label.AddToClassList(labelClass);
                return label;
            };
            listView.bindItem = (element, i) => ((Label)element).text = assets[i].name;
            listView.selectionType = SelectionType.Single;

#if UNITY_2022_2_OR_NEWER
            listView.selectionChanged -= OnListSelectionChanged;
            listView.selectionChanged += OnListSelectionChanged;
#else
			listView.onSelectionChange -= OnListSelectionChanged;
            listView.onSelectionChange += OnListSelectionChanged;
#endif

            if (_transitionTableEditor && _transitionTableEditor.target)
            { listView.selectedIndex = System.Array.IndexOf(assets, _transitionTableEditor.target); }
        }

        private void OnListSelectionChanged(IEnumerable<object> enumerable)
        {
            IMGUIContainer editor = rootVisualElement.Q<IMGUIContainer>(className: "table-editor");
            editor.onGUIHandler = null;

            List<object> list = (List<object>)enumerable;

            if (list.Count == 0)
            { return; }

            TransitionTableSO table = (TransitionTableSO)list[0];
            if (table == null)
            { return; }

            if (_transitionTableEditor == null)
            { _transitionTableEditor = UnityEditor.Editor.CreateEditor(table, typeof(TransitionTableEditor)); }
            else if (_transitionTableEditor.target != table)
            { UnityEditor.Editor.CreateCachedEditor(table, typeof(TransitionTableEditor), ref _transitionTableEditor); }

            editor.onGUIHandler = () =>
            {
                if (!_transitionTableEditor.target)
                {
                    editor.onGUIHandler = null;
                    return;
                }

                ListView listView = rootVisualElement.Q<ListView>(className: "table-list");
                if ((Object)listView.selectedItem != _transitionTableEditor.target)
                {
                    int i = listView.itemsSource.IndexOf(_transitionTableEditor.target);
                    listView.selectedIndex = i;
                    if (i < 0)
                    {
                        editor.onGUIHandler = null;
                        return;
                    }
                }

                _transitionTableEditor.OnInspectorGUI();
            };
        }


        private TransitionTableSO[] FindAssets()
        {
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(TransitionTableSO)}");
            TransitionTableSO[] assets = new TransitionTableSO[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            { assets[i] = AssetDatabase.LoadAssetAtPath<TransitionTableSO>(AssetDatabase.GUIDToAssetPath(guids[i])); }

            return assets;
        }
    }
}
