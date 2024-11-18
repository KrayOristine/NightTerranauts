using Photon.Pun;
using TMPro;

namespace NightTerranauts.Patches {

    [HarmonyPatch(typeof(UIRoomCodeDisplay), "Update")]
    internal class UIRoomCodeDisplayPatches {
        internal static AccessTools.FieldRef<UIRoomCodeDisplay, bool> hiddenRef;
        internal static AccessTools.FieldRef<UIRoomCodeDisplay, TMP_Text> textRef;
        internal static bool _cached = false;
        internal static bool _GameStarted = false;

        internal static void CacheAccess() {
            _cached = true;

            hiddenRef = AccessTools.FieldRefAccess<UIRoomCodeDisplay, bool>("hidden");
            textRef = AccessTools.FieldRefAccess<UIRoomCodeDisplay, TMP_Text>("text");
        }

        [HarmonyPrefix]
        internal static bool Prefix(UIRoomCodeDisplay __instance) {
            if (!_cached) CacheAccess();

            ref var text = ref textRef.Invoke(__instance);

            if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) {
                text.enabled = false;
                return false;
            }

            ref var hidden = ref hiddenRef.Invoke(__instance);

            text.enabled = !GameController.instance.gameStarted;

            if (hidden) {
                text.text = $"PRESS [{SettingsManager.instance.GetKeybind("remove_battery")}] to show code";
            } else {
                text.text = $"CODE: {PhotonNetwork.CurrentRoom.Name}";
            }

            if (PlayerInputController.instance.removeBatteryPress) {
                hidden = !hidden;
            }

            return false;
        }
    }
}
