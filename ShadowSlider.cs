using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ShadowSlider
{
    public class ShadowSliderMod : LoadingExtensionBase, IUserMod
    {
        private const string CONFIG_PATH = "ShadowSlider.txt";

        private bool loaded = false;

        private float _sStrength = 2.0f;

        public string Name
        {
            get { return "ShadowSlider"; }
        }
        public string Description
        {
            get { return "Changes the shadow strength."; }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            loaded = true;
            ChangeShadow();
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            loaded = false;
        }

        private float sStrength
        {
            get
            {
                if (_sStrength > 1.0f)
                {
                    try
                    {
                        _sStrength = Convert.ToSingle(System.IO.File.ReadAllText(CONFIG_PATH));
                    }
                    catch
                    {
                        _sStrength = 0.8f;
                    }
                }
                return _sStrength;
            }
            set
            {
                if (_sStrength == value) return;

                _sStrength = value;
                System.IO.File.WriteAllText(CONFIG_PATH, value.ToString());
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group = helper.AddGroup("ShadowSlider");
            group.AddSlider("Shadow Strength", 0.0f, 1.0f, 0.05f, 0.8f, (sel) =>
            {
                sStrength = sel;
                if (loaded) ChangeShadow();
            });
        }

        private void ChangeShadow()
        {
            GameObject.Find("Directional Light").GetComponent<Light>().shadowStrength = sStrength;
        }
    }
}
