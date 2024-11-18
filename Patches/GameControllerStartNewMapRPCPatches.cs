
using NightTerranauts.Patches;
using System.Reflection.Emit;

namespace NightTerranauts.Features {

    [HarmonyPatch(typeof(GameController), "StartNewMapRPC")]
    internal static class GameControllerStartNewMapRPCPatches {

        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instrs) {
            var found = false;
            foreach (var instr in instrs) {
                if (found) yield return instr;

                if (instr.opcode == OpCodes.Ldc_I4_0) {
                    found = true;
                    yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                    continue;
                }

                yield return instr;
            }
        }
    }
}
