using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZMusic
{
    public delegate void TreeViewSelectionChangedEvent(List<EZMTreeViewItem> selectedItems);
    public class EZMTreeView : TreeView
    {
        public TreeViewSelectionChangedEvent OnSelectionChanged;

        public EZMTreeView(TreeViewState treeViewState) : base(treeViewState)
        {
            rowHeight = 20f;
            showAlternatingRowBackgrounds = true;
            showBorder = true;

            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };

            //var musicPieces 

            var folder = new EZMFolderTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Folder" };
            var musicPieceTest = new EZMMusicPieceTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Music Piece 1" };
            var musicPieceTest2 = new EZMMusicPieceTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Music Piece 2" };
            var segmentTest = new EZMMusicSegmentTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Music Segment" };
            var trackTest = new EZMMusicTrackTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Music Track 1" };
            var trackTest2 = new EZMMusicTrackTreeViewItem() { id = Random.Range(-1000, 1000), displayName = "Music Track 2" };

            root.AddChild(folder);
            folder.AddChild(musicPieceTest);
            folder.AddChild(musicPieceTest2);
            musicPieceTest.AddChild(segmentTest);
            segmentTest.AddChild(trackTest);
            segmentTest.AddChild(trackTest2);

            SetupDepthsFromParentsAndChildren(root);

            return root;
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            base.SelectionChanged(selectedIds);

            List<EZMTreeViewItem> selectedItems = new List<EZMTreeViewItem>();
            for (int i = 0; i < selectedIds.Count; i++)
            {
                int currentSelectedID = selectedIds[i];
                EZMTreeViewItem selectedItem = FindItem(currentSelectedID, rootItem) as EZMTreeViewItem;
                selectedItems.Add(selectedItem);
            }

            OnSelectionChanged?.Invoke(selectedItems);
        }

        protected override void RowGUI(TreeView.RowGUIArgs args)
        {
            Rect rowSpace = args.rowRect;
            EZMTreeViewItem entityTreeItem = args.item as EZMTreeViewItem;
            var childOptions = entityTreeItem.GetChildOptions();

            if (childOptions != null)
            {

                if (GUI.Button(rowSpace.WithSize(new Vector2(17.5f, 17.5f)).WithX(rowSpace.xMax - 20), "+", EditorStyles.centeredGreyMiniLabel))
                {
                    GenericMenu gm = new GenericMenu();

                    for (int i = 0; i < childOptions.Length; i++)
                    {
                        var type = childOptions[i];
                        gm.AddItem(new GUIContent("Add " + childOptions[i].Name), false, () =>
                        {
                            AddElement(entityTreeItem, type);
                            Reload();
                        });
                    }

                    gm.ShowAsContext();

                }
            }

            base.RowGUI(args);
        }

        private void AddElement(EZMTreeViewItem parentItem, System.Type type)
        {

        }
    }
}