using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Mortal.Core;
using UnityEngine;

namespace LegendOfMortalPlugin
{
    [BepInPlugin("yuki.BepinEx.LegendOfMortalPlugin", "Yuki LegendOfMortalPlugin", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<int> speed;
        public static ConfigEntry<bool> speedLock;
        public static ConfigEntry<bool> resource;
        public static ConfigEntry<bool> resourceFlag;
        public static ConfigEntry<bool> day;
        public static ConfigEntry<bool> winLose;
        public static ConfigEntry<bool> anim;
        public static ConfigEntry<bool> dice;
        public static ConfigEntry<int> diceNumber;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony.CreateAndPatchAll(typeof(RollDice));
        }

        private void Start()
        {
            speed = Config.Bind<int>("加速", "", 1, "");
            speedLock = Config.Bind<bool>("加速鎖定", "沒有鎖定切換場景會變回原速度", false, "");
            resource = Config.Bind<bool>("資源", "", false, "");
            resourceFlag = Config.Bind<bool>("資源旗幟", "", false, "");
            day = Config.Bind<bool>("測試白天晚上", "", false, "");
            winLose = Config.Bind<bool>("直接勝利失敗", "", false, "");
            anim = Config.Bind<bool>("測試動畫", "", false, "");
            dice = Config.Bind<bool>("開啟控制骰子", "", false, "");
            diceNumber = Config.Bind<int>("骰子數字", "", 50, "");

            speed.Value = 1;
            speedLock.Value = false;
            resource.Value = false;
            resourceFlag.Value = false;
            day.Value = false;
            winLose.Value = false;
            anim.Value = false;
            dice.Value = false;
            diceNumber.Value = 50;

            speed.SettingChanged += Speed_SettingChanged;
            resource.SettingChanged += Resource_SettingChanged;
            resourceFlag.SettingChanged += ResourceFlag_SettingChanged;
            day.SettingChanged += Day_SettingChanged;
            winLose.SettingChanged += WinLose_SettingChanged;
            anim.SettingChanged += Anim_SettingChanged;
        }

        private void Speed_SettingChanged(object sender, System.EventArgs e)
        {
            setTimeScale();
        }

        private void setTimeScale()
        {
            if (speed.Value > 0)
                Time.timeScale = speed.Value;
        }

        private void ResourceFlag_SettingChanged(object sender, System.EventArgs e)
        {
            if (GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel/Flags") != null)
                GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel/Flags").gameObject.SetActive(true);

            if (GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel/Flags (1)") != null)
                GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel/Flags (1)").gameObject.SetActive(true);
        }

        private void Anim_SettingChanged(object sender, System.EventArgs e)
        {
            if (GameObject.Find("[UI]/MainUI/Layer_5/TestAnimationPanel") != null)
                GameObject.Find("[UI]/MainUI/Layer_5/TestAnimationPanel").gameObject.SetActive(anim.Value);
        }

        private void WinLose_SettingChanged(object sender, System.EventArgs e)
        {
            if(GameObject.Find("[UI]/TopPanel/MenuPanel/TestWin") != null)
                GameObject.Find("[UI]/TopPanel/MenuPanel/TestWin").gameObject.SetActive(winLose.Value);

            if(GameObject.Find("[UI]/TopPanel/MenuPanel/TestLose") != null)
                GameObject.Find("[UI]/TopPanel/MenuPanel/TestLose").gameObject.SetActive(winLose.Value);
        }

        private void Resource_SettingChanged(object sender, System.EventArgs e)
        {
            if (GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel") != null)
                GameObject.Find("[UI]/TopPanel/StatusPanel/TestPanel").gameObject.SetActive(resource.Value);
        }

        private void Day_SettingChanged(object sender, System.EventArgs e)
        {
            if(GameObject.Find("[UI]/MainUI/Layer_1/TestDayButton") != null)
                GameObject.Find("[UI]/MainUI/Layer_1/TestDayButton").gameObject.SetActive(day.Value);

            if(GameObject.Find("[UI]/MainUI/Layer_1/TestNightButton") != null)
                GameObject.Find("[UI]/MainUI/Layer_1/TestNightButton").gameObject.SetActive(day.Value);
        }

        private void Update()
        {
            if (speedLock.Value)
            {
                setTimeScale();
            }
        }
    }
}
