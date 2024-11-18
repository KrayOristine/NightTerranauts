using BepInEx;
using BepInEx.Logging;

namespace NightTerranauts {

    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERS)]
    public sealed class Plugin : BaseUnityPlugin {
        internal static Plugin instance;
        internal static Harmony? _harmony;

        private void Awake() {
            DontDestroyOnLoad(base.gameObject);

            // Plugin startup logic
            PLog.InitLog(base.Logger);

            PLog.Inf($"{PluginInfo.NAME} - {PluginInfo.VERS} is loaded!");

            _harmony = new(PluginInfo.GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

        }

        private void OnDestroy() {
            if (_harmony != null) {
                _harmony.UnpatchSelf();
                _harmony = null;

                PLog.Wrn($"{PluginInfo.NAME} is destroyed, all feature/patches are removed");
            }
        }


    }
}
