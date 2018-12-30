﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.EditorData.Message;
using LuaSTGEditorSharp.EditorData.Document;
using LuaSTGEditorSharp.EditorData.Document.Meta;
using LuaSTGEditorSharp.EditorData.Node.NodeAttributes;
using Newtonsoft.Json;

namespace LuaSTGEditorSharp.EditorData.Node.Stage
{
    [Serializable, NodeIcon("/LuaSTGNodeLib;component/images/16x16/stagegroup.png")]
    [ClassNode]
    [RCInvoke(0)]
    public class StageGroup : TreeNode
    {
        [JsonConstructor]
        private StageGroup() : base() { }

        public StageGroup(DocumentData workSpaceData)
            : this(workSpaceData, "Spell Card", 7) { }

        public StageGroup(DocumentData workSpaceData, string name, int life)
            : base(workSpaceData)
        {
            attributes.Add(new AttrItem("Name", name, this, "stageGroup"));
            attributes.Add(new AttrItem("Start life", life.ToString(), this));
            attributes.Add(new AttrItem("Start power", "400", this));
            attributes.Add(new AttrItem("Start faith", "50000", this));
            attributes.Add(new AttrItem("Start bomb", "3", this));
            attributes.Add(new AttrItem("Allow practice", "true", this, "bool"));
            attributes.Add(new AttrItem("Difficulty value", "1", this, "difficulty"));
        }

        public override string ToString()
        {
            return "Stage Group \"" + attributes[0].AttrInput + "\"";
        }

        public override IEnumerable<string> ToLua(int spacing)
        {
            yield return "stage.group.New('menu',{},\"" + Lua.StringParser.ParseLua(NonMacrolize(0))
                + "\",{lifeleft=" + Macrolize(1) + ",power=" + Macrolize(2) + ",faith=" + Macrolize(3) 
                + ",bomb=" + Macrolize(4) + "}," + Macrolize(5) + "," + Macrolize(6) + ")\n";
            foreach (var a in base.ToLua(spacing))
            {
                yield return a;
            }
        }
        
        public override IEnumerable<Tuple<int, TreeNode>> GetLines()
        {
            yield return new Tuple<int, TreeNode>(1, this);
            foreach (Tuple<int, TreeNode> t in GetChildLines())
            {
                yield return t;
            }
        }

        public override object Clone()
        {
            var n = new StageGroup(parentWorkSpace)
            {
                attributes = new ObservableCollection<AttrItem>(from AttrItem a in attributes select (AttrItem)a.Clone()),
                Children = new ObservableCollection<TreeNode>(from TreeNode t in Children select (TreeNode)t.Clone()),
                _parent = _parent,
                isExpanded = isExpanded
            };
            n.FixAttrParent();
            n.FixChildrenParent();
            return n;
        }

        public override MetaInfo GetMeta()
        {
            return new StageGroupDefineMetaInfo(this);
        }

        public override string GetDifficulty()
        {
            return NonMacrolize(0) != "Easy" && NonMacrolize(0) != "Normal" 
                && NonMacrolize(0) != "Hard" && NonMacrolize(0) != "Lunatic" ? "" : NonMacrolize(0);
        }

        public override List<MessageBase> GetMessage()
        {
            List<MessageBase> messages = new List<MessageBase>();
            if (string.IsNullOrEmpty(NonMacrolize(0)))
                messages.Add(new ArgNotNullMessage(attributes[0].AttrCap, 0, this));
            return messages;
        }
    }
}