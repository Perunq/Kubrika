using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubrika
{
    
    [HarmonyPatch(typeof(SourceManager), "Init")]
    internal class NewNpc
    {



        //[HarmonyPriority(Priority.Last)]

        public static void Postfix(SourceManager __instance)
        {


            var kyria = __instance.charas.GetRow("adv_kiria");

            var tiles = kyria.tiles;

            SourceChara.Row newchar = new SourceChara.Row()
            {
                id = "Kubrika",
                _id = 99999,
                name_JP = "Kubrika",
                name = "Kubrika",
                aka_JP = kyria.aka_JP,
                aka = kyria.aka,
                idActor = kyria.idActor,
                sort = kyria.sort,
                size = kyria.size,
                renderData = kyria.renderData,
                _idRenderData = kyria._idRenderData,
                _tiles = kyria._tiles,
                tiles = tiles,
                _tiles_snow = kyria._tiles_snow,
                tiles_snow = kyria.tiles_snow,
                colorMod = kyria.colorMod,
                components = kyria.components,
                defMat = kyria.defMat,
                LV = kyria.LV,
                chance = kyria.chance,
                quality = kyria.quality,
                hostility = kyria.hostility,
                biome = kyria.biome,
                tag = kyria.tag,
                trait = kyria.trait,
                race = kyria.race,
                job = kyria.job,
                tactics = kyria.tactics,

                aiIdle = kyria.aiIdle,

                aiParam = kyria.aiParam,
                actCombat = kyria.actCombat,
                mainElement = kyria.mainElement,
                elements = kyria.elements,
                equip = kyria.equip,
                loot = kyria.loot,
                category = kyria.category,
                filter = kyria.filter,
                gachaFilter = kyria.gachaFilter,
                tone = kyria.tone,
                actIdle = kyria.actIdle,
                lightData = kyria.lightData,
                idExtra = kyria.idExtra,
                bio = kyria.bio,
                faith = kyria.faith,
                works = kyria.works,
                hobbies = kyria.hobbies,
                idText = kyria.idText,
                moveAnime = kyria.moveAnime,
                factory = kyria.factory,
                //components = kyria.components,
                detail_JP = kyria.detail_JP,
                detail = kyria.detail
            };
            __instance.charas.rows.Add(newchar);
            __instance.charas.SetRow(newchar);
            __instance.cards.AddRow(newchar, true);

        }


        public static bool isCustomAdvLoaded()
        {
            foreach (var item in EClass.core.game.cards.listAdv)
            {
                if (item.id == "Kubrika")
                {
                    Kubrika.Log("CustomAdv is loaded");
                    
                    return true;
                }
            }
            Kubrika.Log("CustomAdv is not loaded");
            return false;
        }

        public static void AddCustomAdventurer()
        {
            Kubrika.Log("Adding CustomAdv");
            Chara chara = CharaGen.Create("Kubrika", -1);
            List<Zone> list = EClass.game.world.region.ListTowns();
            Zone homeZone = list.RandomItem<Zone>();

            chara.SetHomeZone(homeZone);
            chara.global.transition = new ZoneTransition
            {
                state = ZoneTransition.EnterState.RandomVisit
            };
            homeZone.AddCard(chara);
            EClass.game.cards.listAdv.Add(chara);
        }
    }

    // Game.Load
    [HarmonyPatch(typeof(Scene), nameof(Scene.Init))]
    class SceneInitPatch
    {
        static void Postfix(Scene __instance, Scene.Mode newMode)
        {
            
            if (newMode == Scene.Mode.StartGame && EClass.core != null && EClass.core.game != null && EClass.player != null && !NewNpc.isCustomAdvLoaded())
            {
                
               NewNpc.AddCustomAdventurer();
            }
        }


    }
}
