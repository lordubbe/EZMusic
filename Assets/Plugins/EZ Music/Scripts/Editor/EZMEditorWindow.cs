using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace EZMusic
{
    public class EZMEditorWindow : EditorWindow
    {
        private EZMTreeView treeView;
        [SerializeField]
        private TreeViewState treeViewState;
        private SearchField treeViewSearchField;

        private List<EZMTreeViewItem> selectedItems = new List<EZMTreeViewItem>();

        [SerializeField]
        private float treeViewSize = 100f;
        private bool resizingTreeView = false;

        [MenuItem("Tools/EZ Music Editor")]
        public static void OpenWindow()
        {
            var editor = GetWindow<EZMEditorWindow>("EZ Music Editor");
            editor.Init();
            editor.Show();
        }

        public void Init()
        {
            treeViewState = treeViewState ?? new TreeViewState();
            treeView = treeView ?? new EZMTreeView(treeViewState);
            treeView.Reload();

            treeViewSearchField = treeViewSearchField ?? new SearchField();
        }

        private void OnEnable()
        {
            Init();
            treeView.OnSelectionChanged += TreeViewSelectionChanged;
        }

        private void OnDisable()
        {
            treeView.OnSelectionChanged -= TreeViewSelectionChanged;
        }

        private void TreeViewSelectionChanged(List<EZMTreeViewItem> selectedItems)
        {
            this.selectedItems = selectedItems;
        }

        public void OnGUI()
        {
            if (treeView == null)
            {
                Init();
                return;
            }


            Rect space = new Rect(Vector2.zero, position.size);

            Rect headerSpace = space.WithHeight(25f);
            HeaderGUI(headerSpace);

            Rect treeViewSpace = space.WithWidth(treeViewSize).WithYMin(headerSpace.yMax);
            TreeViewGUI(treeViewSpace);

            Rect editSpace = treeViewSpace.WithXMin(treeViewSpace.xMax).WithXMax(space.xMax);
            EditGUI(editSpace);

        }

        private void HeaderGUI(Rect space)
        {
            GUI.BeginGroup(space, "Header", GUI.skin.box);

            GUI.EndGroup();
        }

        private void TreeViewGUI(Rect space)
        {
            Event e = Event.current;
            space = space.WithPadding(2.5f);

            //Set up resize zone
            float resizeWidth = 10f;
            Rect cursorRect = space.WithX(space.xMax - resizeWidth / 2).WithWidth(resizeWidth);
            EditorGUIUtility.AddCursorRect(cursorRect, MouseCursor.ResizeHorizontal);

            if (cursorRect.IsClicked())
            {
                resizingTreeView = true;
                e.Use();
            }

            if (e.type == EventType.MouseUp)
            {
                if (resizingTreeView)
                {
                    resizingTreeView = false;
                }
            }

            if (resizingTreeView)
            {
                float sizeLimit = 30f;
                if (e.mousePosition.x > sizeLimit && e.mousePosition.x < position.xMax - sizeLimit)
                {
                    treeViewSize = e.mousePosition.x;
                    Repaint();
                }
            }

            Rect searchBarRect = space.WithHeight(20f);
            treeView.searchString = treeViewSearchField.OnGUI(searchBarRect, treeView.searchString);

            treeViewSearchField = treeViewSearchField ?? new SearchField();
            treeView.OnGUI(space.WithYMin(searchBarRect.yMax));

        }

        private void EditGUI(Rect space)
        {
            string message = "";

            if (selectedItems.Count > 1)
            {
                message = "Multi-Editing not supported :(";
            }
            else
            {
                if (selectedItems.Count == 1)
                {
                    System.Type type = selectedItems[0].GetType();

                    if (type == typeof(EZMFolderTreeViewItem))
                    {
                        //Don't change current view
                    }
                    else if (type == typeof(EZMMusicPieceTreeViewItem))
                    {
                        message = "Editing a Music Piece";
                    }
                    else if (type == typeof(EZMMusicSegmentTreeViewItem))
                    {
                        message = "Editing a Music Segment";
                    }
                    else if (type == typeof(EZMMusicTrackTreeViewItem))
                    {
                        message = "Editing a Music Track";
                    }
                }
                else
                {
                    message = "Nothing selected...";
                }
            }
			GUI.BeginGroup(space, "Editor", GUI.skin.box);
			
			GUI.EndGroup();

            EditorGUI.HelpBox(space.WithPadding(15f), message, MessageType.Info);

        }
    }
}