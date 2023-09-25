using FMOD;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using System;
using System.Runtime.CompilerServices;

[assembly: IgnoresAccessChecksTo("Celeste")]
namespace Celeste.Mod.CarcinizationHelper {
    public class CarcinizationHelperModule : EverestModule {
        public static CarcinizationHelperModule Instance { get; private set; }

        public override Type SettingsType => typeof(CarcinizationHelperModuleSettings);
        public static CarcinizationHelperModuleSettings Settings => (CarcinizationHelperModuleSettings) Instance._Settings;

        public override Type SessionType => typeof(CarcinizationHelperModuleSession);
        public static CarcinizationHelperModuleSession Session => (CarcinizationHelperModuleSession) Instance._Session;

        public CarcinizationHelperModule() {
            Instance = this;
        }

        public override void Load() {
            //On.Celeste.OuiChapterPanel.Reset += OuiChapterPanel_Reset;
            //On.Celeste.AreaData.Load += AreaData_Load;
            //On.Celeste.AreaStats.Clone += AreaStats_Clone;
            //On.Celeste.OuiChapterPanel.StartRoutine += OuiChapterPanel_StartRoutine;
            //On.Celeste.OuiChapterPanel.UpdateStats += OuiChapterPanel_UpdateStats;
            //On.Celeste.LevelEnter.Go += LevelEnter_Go;
            //On.Celeste.SaveData.StartSession += SaveData_StartSession;
            On.Celeste.Level.CompleteArea_bool_bool_bool += Level_CompleteArea_bool_bool_bool;
            On.Celeste.Dialog.Get += Dialog_Get;
            On.Celeste.Dialog.Clean += Dialog_Clean;
        }
        
        private string crabifyDialog(string text) {
            return text
                .Replace("Madeline", "Crabeline")
                .Replace("{savedata Name}", "Crabeline")
                .Replace("{savedata TheoSisterName}", "Crablex")
                .Replace("Theo", "Crabeo")
                .Replace("El Creepo", "El Crabbo")
                .Replace("Mom", "Crab Mom")
                .Replace("Oshiro", "Crabshiro")
                .Replace("Celestial Resort", "Crab Resort")
                .Replace("Celeste", "Crableste")
                .Replace("Granny", "Crabby")
                .Replace("Part of", "Crab-Part of")
                .Replace("ma'am", "crab'am")
                .Replace("bee", "crab")
                .Replace("doppelgänger", "doppelcrabber")
                .Replace("sister", "crab")
                .Replace("lawyer", "crab")
                .Replace("musician", "crab")
                .Replace("rockstar", "crab")
                .Replace("OPTIONS", "CRAB OPTIONS")
                .Replace("Options", "Crab Options")
                .Replace("Grab", "Crab")
                .Replace("Keyboard", "Crabboard")
                .Replace("Controller", "Crabtroller")
                .Replace("Fullscreen", "Crabscreen")
                .Replace("Photosensitive", "Crabsensitive")
                .Replace("Music", "Music of Crab")
                .Replace("Sounds", "Sound of Crab")
                .Replace("Speedrun Clock", "Speedrun Crab")
                .Replace("Chapter", "Crabter")
                .Replace("Confirm", "Crabfirm")
                .Replace("Cancel", "Crancel")
                .Replace("Crouch Dash", "Crab Dash")
                .Replace("PAUSED", "CRABBED")
                .Replace("PICO-8", "CRAB-8")
                .Replace("Assist", "Crabsist")
                .Replace("ASSIST", "CRABSIST")
                .Replace("Variant", "Crabiant")
                .Replace("Variant", "CRABIANT")
                .Replace("Delete", "Crablete")
                .Replace("Continue", "Crabtinue")
                .Replace("Save and Quit", "Save and Crab")
                .Replace("Quit", "Crab Quit")
                .Replace("pause menu", "pause crab")
                .Replace("resume", "crabsume")
                .Replace("Resume", "Crabsume")
                .Replace("strawberry", "crabberry")
                .Replace("Strawberry", "Crabberry")
                .Replace("strawberries", "Crabberries")
                .Replace("Strawberries", "Crabberries")
                .Replace("Death", "Crab")
                .Replace("friend", "crab")
                .Replace("learning", "carcinizing")
                .Replace("Climbing", "Crabbing")
                .Replace("energy", "crabs")
                .Replace("stamina", "crabs")
                .Replace("climbing", "crabbing")
                .Replace("true reflection", "true pet crab")
                .Replace("own reflection", "own pet crab")
                .Replace("CLIMB", "CRAB")
                .Replace("HOLD", "CRAB")
                .Replace("DASH", "CRAB")
                .Replace("CARRY", "CRAB")
                .Replace("Cutscene", "Crabscene")
                .Replace("Restart", "Recrab")
                .Replace("Return to Map", "Return to Crab")
                .Replace("Retry", "Recrab")
                .Replace("Strawberries", "Crabberries")
                .Replace("sign", "crab")
                .Replace("driveway", "crabway")
                .Replace("traveller", "crab")
                .Replace("human", "crab")
                .Replace("wolf", "crab")
                .Replace("backstory", "crabstory")
                .Replace("toes", "crabs")
                .Replace("Seattle", "Crab Seattle")
                .Replace("mega-corporation", "mega-crab")
                .Replace("InstaPix", "CrabPix")
                .Replace("throw caution", "throw crabs")
                .Replace("observer", "crab")
                .Replace("creepy", "crabby")
                .Replace("crazy", "crabby")
                .Replace("darling", "crabling")
                .Replace("pragmatic", "crabmatic")
                .Replace("payphone", "crabphone")
                .Replace("sweetheart", "sweetcrab")
                .Replace("Sweetheart", "Sweetcrab")
                .Replace("compatriot", "crabpatriot")
                .Replace("selfie", "crabfie")
                .Replace("photogenic", "photocrabic")
                .Replace("Casual", "Crabual")
                .Replace("smartphone", "crabphone")
                .Replace("fortress", "crab")
                .Replace("vulnerable", "crabnerable")
                .Replace("ghost", "lobster")
                .Replace("concierge", "crabcierge")
                .Replace("presidential", "crabidential")
                .Replace("guestbook", "crabbook")
                .Replace("tourism", "crabism")
                .Replace("luggage", "crabbage")
                .Replace("establishment", "escrablishment")
                .Replace("customer", "crabstomer")
                .Replace("cleaning", "carcinizing")
                .Replace("satisfied", "carcinized")
                .Replace("problems", "crablems")
                .Replace("creeps", "crabs")
                .Replace("dude", "crab")
                .Replace("bird", "crab")
                .Replace("Charlotte", "Crablotte")
                .Replace(" man.", " crab.")
                .Replace(" man,", " crab,")
                .Replace("Man,", "Crab,")
                .Replace("weirdo's", "crab's")
                .Replace("feed her twisted ego", "feed them to her carnivorous giant crab")
                .Replace("leaking", "carcinizing")
                .Replace("cleaned up", "carcinized")
                .Replace("morning", "morning morning crabs")
                .Replace("project", "crabject")
                .Replace("Y'know", "Y'crab")
                .Replace("crows short of a murder", "crabs short of a cast")
                .Replace("old bat", "old crab")
                .Replace("stubborn", "crabborn")
                .Replace("determination", "crabtermination")
                .Replace("hermit", "hermit crab")
                .Replace("cabin", "crabin")
                .Replace("N a i l e d", "C r a b b e d")
                .Replace("gondola", "crabola")
                .Replace("stalled", "crabbed")
                .Replace("panic attack", ":crabmadstack:")
                .Replace("man", "crab")
                .Replace("feather", "carapace")
                .Replace("pocket", "claws")
                .Replace("nonchalant", "crabchalant")
                .Replace("magnifies", "crabifies")
                .Replace("babysit", "crabsit")
                .Replace("unravelling", "uncrabelling")
                .Replace("SHUT UP", "CLAM IT")
                .Replace("shut up", "clam it")
                .Replace("Shut up", "Clam it")
                .Replace("trespasser", "trescrabber")
                .Replace("trapped", "crabbed")
                .Replace("monster", "crabster")
                .Replace("inner self", "inner crab")
                .Replace("girl", "crab")
                .Replace("Crushing", "Crabbing")
                .Replace("skeptical", "skepticrab")
                .Replace("kiddo", "crabbo")
                .Replace("crisis", "crabsis")
                .Replace("existential", "exoskeletal")
                .Replace("dead set", "crab set")
                .Replace("destroying", "carcinizing")
                .Replace("claustrophoic", "crabstrophobic")
                .Replace("abyss", "crabyss")
                .Replace("pointless", "crabless")
                .Replace("protecting", "carcinizing")
                .Replace("Canada", "Crabada")
                .Replace("violent", "pinchy")
                .Replace("paranoid", "crabanoid")
                .Replace("leave me behind", "shed me like a shell")
                .Replace("miserable", "craberable")
                .Replace("sabotage", "crabotage")
                .Replace("kid", "crab")
                .Replace("glimpse", "shrimpse")
                .Replace("healing", "carcinizing")
                .Replace("adorable goth", "adorable crab")
                .Replace("capable", "crabable")
                .Replace("much crabs", "many crabs")
                .Replace("snapping", "pinching")
                .Replace("time and crabs", "time and so many crabs")
                .Replace("this body", "this carapace")
                .Replace("struggle", "crabble")
                .Replace("Selfie", "Crabfie")
                .Replace(" pie ", " gumbo ")
                .Replace(" pie{", " gumbo{")
                .Replace(" pie.", " gumbo.")
                .Replace(" pie,", " gumbo,")
                .Replace(" pies ", " gumbo ")
                .Replace("banquet", "crabquet")
                .Replace("magnificent", "crabnificent")
                .Replace("Vancouver", "Crabcouver")
                .Replace("California", "Crabifornia")
                .Replace("law school", "crab school")
                .Replace("mole", "crab")
                .Replace("universe", "crabiverse")
                .Replace("selfish", "shellfish")
                .Replace("BerryOS", "CrabOS")
                .Replace("Bird", "Crab")
                .Replace("Wavedash", "Crabdash")
                .Replace("wavedash", "crabdash")
                .Replace("own heads", "own shells")
                .Replace("fly", "scuttle");
        }

        private string Dialog_Clean(On.Celeste.Dialog.orig_Clean orig, string name, Language language) {
            var res = orig(name, language);
            if((Session?.CrabSide ?? false) && Engine.Scene is Level) {
                res = crabifyDialog(res);
            }
            return res;
        }

        private string Dialog_Get(On.Celeste.Dialog.orig_Get orig, string name, Language language) {
            var res = orig(name, language);
            if ((Session?.CrabSide ?? false) && Engine.Scene is Level) {
                res = crabifyDialog(res);
            }
            return res;
        }

        private ScreenWipe Level_CompleteArea_bool_bool_bool(On.Celeste.Level.orig_CompleteArea_bool_bool_bool orig, Level self, bool spotlightWipe, bool skipScreenWipe, bool skipCompleteScreen) {
            if(!Session.CrabSide) {
                return orig(self, spotlightWipe, skipScreenWipe, skipCompleteScreen);
            }

            self.RegisterAreaComplete();
            self.PauseLock = true;
            Action oldAction = (!(AreaData.Get(self.Session).Interlude_Safe || skipCompleteScreen)) ?
                (() => Engine.Scene = new LevelExit(LevelExit.Mode.Completed, self.Session)) :
                (() => Engine.Scene = new LevelExit(LevelExit.Mode.CompletedInterlude, self.Session, self.HiresSnow));
            Action action = delegate {
                self.Remove(self.Entities.FindAll<TempleBigEyeball.Fader>());
                self.Add(new Carcinization("random", _ => {
                    oldAction();
                    return false;
                }));
            };
            if (!self.SkippingCutscene && !skipScreenWipe) {
                if (spotlightWipe) {
                    Player entity = self.Tracker.GetEntity<Player>();
                    if (entity != null) {
                        SpotlightWipe.FocusPoint = entity.Position - self.Camera.Position - new Vector2(0f, 8f);
                    }
                    return new SpotlightWipe(self, wipeIn: false, action);
                }
                return new FadeWipe(self, wipeIn: false, action);
            }
            Audio.BusStopAll("bus:/gameplay_sfx", immediate: true);
            action();
            return null;
        }

        private void SaveData_StartSession(On.Celeste.SaveData.orig_StartSession orig, SaveData self, Session session) {
            orig(self, session);
            if(DynamicData.For(session).Get<bool?>("crabifyMeCaptain") ?? false) {
                Session.CrabSide = true;
            }
        }

        private void LevelEnter_Go(On.Celeste.LevelEnter.orig_Go orig, Session session, bool fromSaveData) {
            if(markSessionAsCrabSide) {
                DynamicData.For(session).Set("crabifyMeCaptain", true);
            }
            orig(session, fromSaveData);
        }

        private bool markSessionAsCrabSide;
        private System.Collections.IEnumerator OuiChapterPanel_StartRoutine(On.Celeste.OuiChapterPanel.orig_StartRoutine orig, OuiChapterPanel self, string checkpoint) {
            if(DynamicData.For(self).Get<int?>("CarcinizationHelper_CrabSideModeIdx").GetValueOrDefault(-1) == (int)self.Area.Mode) {
                self.Area.Mode = AreaMode.Normal;
                markSessionAsCrabSide = true;
                yield return new SwapImmediately(orig(self, checkpoint));
                markSessionAsCrabSide = false;
            } else {
                yield return new SwapImmediately(orig(self, checkpoint));
            }
        }

        private void OuiChapterPanel_UpdateStats(On.Celeste.OuiChapterPanel.orig_UpdateStats orig, OuiChapterPanel self, bool wiggle, bool? overrideStrawberryWiggle, bool? overrideDeathWiggle, bool? overrideHeartWiggle) {
            if (DynamicData.For(self).Get<int?>("CarcinizationHelper_CrabSideModeIdx").GetValueOrDefault(-1) == (int)self.Area.Mode) {
                var oldMode = self.Area.Mode;
                self.Area.Mode = AreaMode.Normal;
                orig(self, wiggle, overrideStrawberryWiggle, overrideDeathWiggle, overrideHeartWiggle);
                self.Area.Mode = oldMode;
            } else {
                orig(self, wiggle, overrideStrawberryWiggle, overrideDeathWiggle, overrideHeartWiggle);
            }
        }

        private AreaStats AreaStats_Clone(On.Celeste.AreaStats.orig_Clone orig, AreaStats self) {
            // Remove a crab-side if it's there
            if (self.Modes.Length != 1 && self.Modes[0] == self.Modes[self.Modes.Length - 1]) {
                Array.Resize(ref self.Modes, self.Modes.Length - 1);
            }
            return orig(self);
        }

        private void AreaData_Load(On.Celeste.AreaData.orig_Load orig) {
            orig();
            for (int i = 0; i < AreaData.Areas.Count; i++) {
                AreaData data = AreaData.Areas[i];
                // We only want to mess with vanilla
                if (!data.IsOfficialLevelSet()) continue;
                int modeCount;
                for (modeCount = 0; modeCount < data.Mode.Length; modeCount++) {
                    ModeProperties modeProperties2 = data.Mode[modeCount];
                    if (modeProperties2 == null || string.IsNullOrEmpty(modeProperties2.Path)) {
                        break;
                    }
                }
                Array.Resize(ref data.Mode, modeCount + 1);
                data.Mode[modeCount] = data.Mode[0];
            }
        }

        private void OuiChapterPanel_Reset(On.Celeste.OuiChapterPanel.orig_Reset orig, OuiChapterPanel self) {
            orig(self);
            if (self.Data.IsOfficialLevelSet()) {
                self.modes.Add(new OuiChapterPanel.Option {
                    Label = Dialog.Clean("CarcinizationHelper_SideLabel").ToUpper(),
                    Icon = GFX.Gui["BrokemiaHelper/menu/crab"],
                    ID = "CarcinizationHelper"
                });
                if(self.RealStats.Modes.Length < self.modes.Count) {
                    Array.Resize(ref self.RealStats.Modes, self.modes.Count);
                }
                self.RealStats.Modes[self.modes.Count - 1] = self.RealStats.Modes[0];
                DynamicData.For(self).Set("CarcinizationHelper_CrabSideModeIdx", self.modes.Count - 1);
            }
        }

        public override void Unload() {
            On.Celeste.OuiChapterPanel.Reset -= OuiChapterPanel_Reset;
        }
    }
}