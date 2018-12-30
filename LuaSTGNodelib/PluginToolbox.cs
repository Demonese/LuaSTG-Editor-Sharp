﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuaSTGEditorSharp.Plugin;
using LuaSTGEditorSharp.Toolbox;
using LuaSTGEditorSharp.EditorData;
using LuaSTGEditorSharp.EditorData.Node.General;
using LuaSTGEditorSharp.EditorData.Node.Data;
using LuaSTGEditorSharp.EditorData.Node.Stage;
using LuaSTGEditorSharp.EditorData.Node.Task;
using LuaSTGEditorSharp.EditorData.Node.Boss;
using LuaSTGEditorSharp.EditorData.Node.Bullet;
using LuaSTGEditorSharp.EditorData.Node.Laser;
using LuaSTGEditorSharp.EditorData.Node.Object;
using LuaSTGEditorSharp.EditorData.Node.Graphics;
using LuaSTGEditorSharp.EditorData.Node.Audio;

namespace LuaSTGEditorSharp
{
    public class PluginToolbox : AbstractToolbox
    {
        public PluginToolbox(MainWindow mw) : base(mw) { }

        public override void InitFunc()
        {
            var data = new Dictionary<ToolboxItemData, AddNode>();
            #region data
            data.Add(new ToolboxItemData("localvar", "/LuaSTGNodeLib;component/images/variable.png", "Local Variable")
                , new AddNode(AddLocalVarNode));
            data.Add(new ToolboxItemData("assign", "/LuaSTGNodeLib;component/images/assignment.png", "Assignment")
                , new AddNode(AddAssignmentNode));
            data.Add(new ToolboxItemData("recordpos", "/LuaSTGNodeLib;component/images/positionVar.png", "Record Position")
                ,  new AddNode(AddRecordPosNode));
            #endregion
            ToolInfo.Add("Data", data);

            var stage = new Dictionary<ToolboxItemData, AddNode>();
            #region stage
            stage.Add(new ToolboxItemData("stagegroup", "/LuaSTGNodeLib;component/images/stagegroup.png", "Stage Group")
                , new AddNode(AddStageGroupNode));
            stage.Add(new ToolboxItemData("stage", "/LuaSTGNodeLib;component/images/stage.png", "Stage")
                , new AddNode(AddStageNode));
            stage.Add(new ToolboxItemData(true), null);
            stage.Add(new ToolboxItemData("setstagebg", "/LuaSTGNodeLib;component/images/bgstage.png", "Set Stage Background")
                , new AddNode(AddSetStageBGNode));
            #endregion
            ToolInfo.Add("Stage", stage);

            var task = new Dictionary<ToolboxItemData, AddNode>();
            #region task
            task.Add(new ToolboxItemData("task", "/LuaSTGNodeLib;component/images/task.png", "Task")
                , new AddNode(AddTaskNode));
            task.Add(new ToolboxItemData("wait", "/LuaSTGNodeLib;component/images/taskwait.png", "Wait")
                , new AddNode(AddTaskWaitNode));
            task.Add(new ToolboxItemData("moveto", "/LuaSTGNodeLib;component/images/taskmoveto.png", "Move To")
                , new AddNode(AddTaskMoveToNode));
            #endregion
            ToolInfo.Add("Task", task);
            
            var enemy = new Dictionary<ToolboxItemData, AddNode>();
            ToolInfo.Add("Enemy", enemy);

            var boss = new Dictionary<ToolboxItemData, AddNode>();
            #region boss
            boss.Add(new ToolboxItemData("defboss", "/LuaSTGNodeLib;component/images/bossdefine.png", "Define Boss")
                , new AddNode(AddDefineBossNode));
            boss.Add(new ToolboxItemData("bosssc", "/LuaSTGNodeLib;component/images/bossspellcard.png", "Define Spell Card")
                , new AddNode(AddBossSCNode));
            boss.Add(new ToolboxItemData(true), null);
            boss.Add(new ToolboxItemData("createboss", "/LuaSTGNodeLib;component/images/bosscreate.png", "Create Boss")
                , new AddNode(AddCreateBossNode));
            boss.Add(new ToolboxItemData(true), null);
            boss.Add(new ToolboxItemData("defbossbg", "/LuaSTGNodeLib;component/images/bgdefine.png", "Define Boss Background")
                , new AddNode(AddBossBGDefineNode));
            boss.Add(new ToolboxItemData("bossbglayer", "/LuaSTGNodeLib;component/images/bglayer.png", "Define Boss Background Layer")
                , new AddNode(AddBossBGLayerNode));
            #endregion
            ToolInfo.Add("Boss", boss);

            var bullet = new Dictionary<ToolboxItemData, AddNode>();
            #region bullet
            bullet.Add(new ToolboxItemData("defbullet", "/LuaSTGNodeLib;component/images/bulletdefine.png", "Define Bullet")
                , new AddNode(AddDefineBulletNode));
            bullet.Add(new ToolboxItemData("createbullet", "/LuaSTGNodeLib;component/images/bulletcreate.png", "Create Bullet")
                , new AddNode(AddCreateBulletNode));
            bullet.Add(new ToolboxItemData(true), null);
            bullet.Add(new ToolboxItemData("simplebullet", "/LuaSTGNodeLib;component/images/bulletcreatestraight.png", "Create Simple Bullet")
                , new AddNode(AddCreateSimpleBulletNode));
            bullet.Add(new ToolboxItemData("bulletgroup", "/LuaSTGNodeLib;component/images/bulletcreatestraightex.png", "Create Simple Bullet Group")
                , new AddNode(AddCreateBulletGroupNode));
            #endregion
            ToolInfo.Add("Bullet", bullet);

            var laser = new Dictionary<ToolboxItemData, AddNode>();
            #region laser
            laser.Add(new ToolboxItemData("deflaser", "/LuaSTGNodeLib;component/images/laserdefine.png", "Define Laser")
                , new AddNode(AddDefineLaserNode));
            laser.Add(new ToolboxItemData("createlaser", "/LuaSTGNodeLib;component/images/lasercreate.png", "Create Laser")
                , new AddNode(AddCreateLaserNode));
            laser.Add(new ToolboxItemData(true), null);
            laser.Add(new ToolboxItemData("defbentlaser", "/LuaSTGNodeLib;component/images/laserbentdefine.png", "Define Bent Laser")
                , new AddNode(AddDefineBentLaserNode));
            laser.Add(new ToolboxItemData("createbentlaser", "/LuaSTGNodeLib;component/images/laserbentcreate.png", "Create Bent Laser")
                , new AddNode(AddCreateBentLaserNode));
            laser.Add(new ToolboxItemData(true), null);
            laser.Add(new ToolboxItemData("laserturnhalfon", "/LuaSTGNodeLib;component/images/laserturnhalfon.png", "Turn Half On Laser")
                , new AddNode(AddLaserTurnHalfOnNode));
            laser.Add(new ToolboxItemData("laserturnon", "/LuaSTGNodeLib;component/images/laserturnon.png", "Turn On Laser")
                , new AddNode(AddLaserTurnOnNode));
            laser.Add(new ToolboxItemData("laserturnoff", "/LuaSTGNodeLib;component/images/laserturnoff.png", "Turn Off Laser")
                , new AddNode(AddLaserTurnOffNode));
            laser.Add(new ToolboxItemData(true), null);
            laser.Add(new ToolboxItemData("lasergrow", "/LuaSTGNodeLib;component/images/lasergrow.png", "Grow Laser")
                , new AddNode(AddLaserGrowNode));
            laser.Add(new ToolboxItemData("laserchangestyle", "/LuaSTGNodeLib;component/images/laserchangestyle.png", "Change Laser Style")
                , new AddNode(AddLaserChangeStyleNode));
            #endregion
            ToolInfo.Add("Laser", laser);

            var obj = new Dictionary<ToolboxItemData, AddNode>();
            #region object
            obj.Add(new ToolboxItemData("callbackfunc", "/LuaSTGNodeLib;component/images/callbackfunc.png", "Call Back Functions")
                , new AddNode(AddCallBackFuncNode));
            obj.Add(new ToolboxItemData("defaultaction", "/LuaSTGNodeLib;component/images/defaultaction.png", "Default Action")
                , new AddNode(AddDefaultActionNode));
            obj.Add(new ToolboxItemData(true), null);
            obj.Add(new ToolboxItemData("setv", "/LuaSTGNodeLib;component/images/setv.png", "Set Velocity")
                , new AddNode(AddSetVNode));
            obj.Add(new ToolboxItemData("seta", "/LuaSTGNodeLib;component/images/setaccel.png", "Set Acceleration")
                , new AddNode(AddSetANode));
            obj.Add(new ToolboxItemData("delete", "/LuaSTGNodeLib;component/images/unitdel.png", "Delete Unit")
                , new AddNode(AddDelNode));
            obj.Add(new ToolboxItemData("kill", "/LuaSTGNodeLib;component/images/unitkill.png", "Kill Unit")
                , new AddNode(AddKillNode));
            obj.Add(new ToolboxItemData(true), null);
            obj.Add(new ToolboxItemData("setblend", "/LuaSTGNodeLib;component/images/setcolor.png", "Set Color and Blend Mode")
                , new AddNode(AddSetBlendNode));
            #endregion
            ToolInfo.Add("Object", obj);

            var graphics = new Dictionary<ToolboxItemData, AddNode>();
            #region graphics
            graphics.Add(new ToolboxItemData("loadimage", "/LuaSTGNodeLib;component/images/loadimage.png", "Load Image")
                , new AddNode(AddLoadImageNode));
            #endregion
            ToolInfo.Add("Graphics", graphics);

            var audio = new Dictionary<ToolboxItemData, AddNode>();
            #region audio
            audio.Add(new ToolboxItemData("loadbgm", "/LuaSTGNodeLib;component/images/loadbgm.png", "Load Background Music")
                , new AddNode(AddLoadBGMNode));
            audio.Add(new ToolboxItemData("playbgm", "/LuaSTGNodeLib;component/images/playbgm.png", "Play Background Music")
                , new AddNode(AddPlayBGMNode));
            #endregion
            ToolInfo.Add("Audio", audio);

            var inherit = new Dictionary<ToolboxItemData, AddNode>();
            ToolInfo.Add("Inheritance", inherit);

            var render = new Dictionary<ToolboxItemData, AddNode>();
            ToolInfo.Add("Render", render);

            var background = new Dictionary<ToolboxItemData, AddNode>();
            ToolInfo.Add("Background", background);

            var player = new Dictionary<ToolboxItemData, AddNode>();
            ToolInfo.Add("Player", player);
        }

        #region data
        private void AddLocalVarNode()
        {
            parent.Insert(new LocalVar(parent.ActivatedWorkSpaceData));
        }

        private void AddAssignmentNode()
        {
            parent.Insert(new Assignment(parent.ActivatedWorkSpaceData));
        }

        private void AddRecordPosNode()
        {
            parent.Insert(new RecordPos(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region stage
        private void AddStageGroupNode()
        {
            TreeNode newStG = new StageGroup(parent.ActivatedWorkSpaceData);
            TreeNode newSt = new Stage(parent.ActivatedWorkSpaceData);
            TreeNode newTask = new TaskNode(parent.ActivatedWorkSpaceData);
            TreeNode newFolder = new Folder(parent.ActivatedWorkSpaceData, "Initialize");
            newFolder.AddChild(new Code(parent.ActivatedWorkSpaceData,
                "LoadMusic(\'spellcard\',\'THlib\\\\music\\\\spellcard.ogg\',75,0xc36e80/44100/4)"));
            newFolder.AddChild(new StageBG(parent.ActivatedWorkSpaceData, "bamboo_background"));
            newTask.AddChild(newFolder);
            newTask.AddChild(new TaskWait(parent.ActivatedWorkSpaceData, "60"));
            newTask.AddChild(new PlayBGM(parent.ActivatedWorkSpaceData, "\"spellcard\"", "", "false"));
            newTask.AddChild(new TaskWait(parent.ActivatedWorkSpaceData, "180"));
            newSt.AddChild(newTask);
            newStG.AddChild(newSt);
            parent.Insert(newStG);
        }

        private void AddStageNode()
        {
            TreeNode newSt = new Stage(parent.ActivatedWorkSpaceData);
            TreeNode newTask = new TaskNode(parent.ActivatedWorkSpaceData);
            TreeNode newFolder = new Folder(parent.ActivatedWorkSpaceData, "Initialize");
            newFolder.AddChild(new Code(parent.ActivatedWorkSpaceData,
                "LoadMusic(\'spellcard\',\'THlib\\\\music\\\\spellcard.ogg\',75,0xc36e80/44100/4)"));
            newFolder.AddChild(new StageBG(parent.ActivatedWorkSpaceData, "bamboo_background"));
            newTask.AddChild(newFolder);
            newTask.AddChild(new TaskWait(parent.ActivatedWorkSpaceData, "60"));
            newTask.AddChild(new PlayBGM(parent.ActivatedWorkSpaceData, "\"spellcard\"", "", "false"));
            newTask.AddChild(new TaskWait(parent.ActivatedWorkSpaceData, "180"));
            newSt.AddChild(newTask);
            parent.Insert(newSt);
        }

        private void AddSetStageBGNode()
        {
            TreeNode newStBG = new StageBG(parent.ActivatedWorkSpaceData);
            parent.Insert(newStBG);
        }
        #endregion
        #region task
        private void AddTaskNode()
        {
            parent.Insert(new TaskNode(parent.ActivatedWorkSpaceData));
        }

        private void AddTaskWaitNode()
        {
            parent.Insert(new TaskWait(parent.ActivatedWorkSpaceData));
        }

        private void AddTaskMoveToNode()
        {
            parent.Insert(new TaskMoveTo(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region boss
        private void AddDefineBossNode()
        {
            TreeNode newDef = new BossDefine(parent.ActivatedWorkSpaceData);
            TreeNode newSC = new BossSpellCard(parent.ActivatedWorkSpaceData);
            TreeNode newSCStart = new BossSCStart(parent.ActivatedWorkSpaceData);
            TreeNode newTask = new TaskNode(parent.ActivatedWorkSpaceData);
            newSCStart.AddChild(newTask);
            newTask.AddChild(new TaskMoveTo(parent.ActivatedWorkSpaceData, "0,120", "60", "MOVE_NORMAL"));
            newSC.AddChild(newSCStart);
            newSC.AddChild(new BossSCFinish(parent.ActivatedWorkSpaceData));
            newDef.AddChild(newSC);
            parent.Insert(newDef);
        }

        private void AddBossSCNode()
        {
            TreeNode newSC = new BossSpellCard(parent.ActivatedWorkSpaceData);
            TreeNode newSCStart = new BossSCStart(parent.ActivatedWorkSpaceData);
            TreeNode newTask = new TaskNode(parent.ActivatedWorkSpaceData);
            newSCStart.AddChild(newTask);
            newTask.AddChild(new TaskMoveTo(parent.ActivatedWorkSpaceData, "0,120", "60", "MOVE_NORMAL"));
            newSC.AddChild(newSCStart);
            newSC.AddChild(new BossSCFinish(parent.ActivatedWorkSpaceData));
            parent.Insert(newSC);
        }

        private void AddCreateBossNode()
        {
            parent.Insert(new CreateBoss(parent.ActivatedWorkSpaceData));
        }

        private void AddBossBGDefineNode()
        {
            parent.Insert(new BossBGDefine(parent.ActivatedWorkSpaceData));
        }

        private void AddBossBGLayerNode()
        {
            TreeNode newDef = new BossBGLayer(parent.ActivatedWorkSpaceData);
            newDef.AddChild(new BossBGLayerInit(parent.ActivatedWorkSpaceData));
            newDef.AddChild(new BossBGLayerFrame(parent.ActivatedWorkSpaceData));
            newDef.AddChild(new BossBGLayerRender(parent.ActivatedWorkSpaceData));
            parent.Insert(newDef);
        }
        #endregion
        #region bullet
        private void AddDefineBulletNode()
        {
            TreeNode newDef = new BulletDefine(parent.ActivatedWorkSpaceData);
            newDef.AddChild(new BulletInit(parent.ActivatedWorkSpaceData));
            parent.Insert(newDef);
        }

        private void AddCreateBulletNode()
        {
            parent.Insert(new CreateBullet(parent.ActivatedWorkSpaceData));
        }

        private void AddCreateSimpleBulletNode()
        {
            parent.Insert(new CreateSimpleBullet(parent.ActivatedWorkSpaceData));
        }

        private void AddCreateBulletGroupNode()
        {
            parent.Insert(new CreateBulletGroup(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region laser
        private void AddDefineLaserNode()
        {
            TreeNode newDef = new LaserDefine(parent.ActivatedWorkSpaceData);
            TreeNode newInit = new LaserInit(parent.ActivatedWorkSpaceData);
            TreeNode newTask = new TaskNode(parent.ActivatedWorkSpaceData);
            TreeNode newTurnOn = new LaserTurnOn(parent.ActivatedWorkSpaceData);
            newTask.AddChild(newTurnOn);
            newInit.AddChild(newTask);
            newDef.AddChild(newInit);
            parent.Insert(newDef);
        }

        private void AddCreateLaserNode()
        {
            parent.Insert(new CreateLaser(parent.ActivatedWorkSpaceData));
        }

        private void AddDefineBentLaserNode()
        {
            TreeNode newDef = new BentLaserDefine(parent.ActivatedWorkSpaceData);
            newDef.AddChild(new BentLaserInit(parent.ActivatedWorkSpaceData));
            parent.Insert(newDef);
        }

        private void AddCreateBentLaserNode()
        {
            parent.Insert(new CreateBentLaser(parent.ActivatedWorkSpaceData));
        }

        private void AddLaserTurnHalfOnNode()
        {
            parent.Insert(new LaserTurnHalfOn(parent.ActivatedWorkSpaceData));
        }

        private void AddLaserTurnOnNode()
        {
            parent.Insert(new LaserTurnOn(parent.ActivatedWorkSpaceData));
        }

        private void AddLaserTurnOffNode()
        {
            parent.Insert(new LaserTurnOff(parent.ActivatedWorkSpaceData));
        }

        private void AddLaserGrowNode()
        {
            parent.Insert(new LaserGrow(parent.ActivatedWorkSpaceData));
        }

        private void AddLaserChangeStyleNode()
        {
            parent.Insert(new LaserChangeStyle(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region object
        private void AddCallBackFuncNode()
        {
            TreeNode newCBF = new CallBackFunc(parent.ActivatedWorkSpaceData);
            newCBF.AddChild(new DefaultAction(parent.ActivatedWorkSpaceData));
            parent.Insert(newCBF);
        }

        private void AddDefaultActionNode()
        {
            parent.Insert(new DefaultAction(parent.ActivatedWorkSpaceData));
        }

        private void AddSetVNode()
        {
            parent.Insert(new SetVelocity(parent.ActivatedWorkSpaceData));
        }

        private void AddSetANode()
        {
            parent.Insert(new SetAccel(parent.ActivatedWorkSpaceData));
        }

        private void AddDelNode()
        {
            parent.Insert(new Del(parent.ActivatedWorkSpaceData));
        }

        private void AddKillNode()
        {
            parent.Insert(new Kill(parent.ActivatedWorkSpaceData));
        }

        private void AddSetBlendNode()
        {
            parent.Insert(new SetBlend(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region graphics
        private void AddLoadImageNode()
        {
            parent.Insert(new LoadImage(parent.ActivatedWorkSpaceData));
        }
        #endregion
        #region audio
        private void AddLoadBGMNode()
        {
            parent.Insert(new LoadBGM(parent.ActivatedWorkSpaceData));
        }

        private void AddPlayBGMNode()
        {
            parent.Insert(new PlayBGM(parent.ActivatedWorkSpaceData));
        }
        #endregion
    }
}