

using Photon.Pun;
using System.Reflection.Emit;

namespace NightTerranauts.Features {

    [HarmonyPatch(typeof(GameController), "StartNewMapRPC")]
    internal static class GameControllerStartNewMapRPCPatches {

        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instrs) {

            return new CodeMatcher(instrs).MatchForward(false,
                   new CodeMatch(OpCodes.Call),
                   new CodeMatch(OpCodes.Ldc_I4_0),
                   new CodeMatch(OpCodes.Callvirt)
                )
                .Advance(1) // get to LDC I4 0 (load integer32 0)
                .SetOpcodeAndAdvance(OpCodes.Ldc_I4_1) // set it to LDC I4 0 (load integer32 1)
                .InstructionEnumeration(); // should work
        }
    }
}
