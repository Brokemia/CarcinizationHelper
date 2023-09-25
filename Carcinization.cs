using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Celeste.Mod.CarcinizationHelper {
    public class Carcinization : Entity {
        private string type;

        private Level lvl;
        // Returns whether things should fade out
        private Func<Carcinization, bool> after;

        private bool crabifyPlayer;

        private float centerImgAlpha = 1;
        private MTexture centerImg;

        private MTexture crabImg = GFX.Game["BrokemiaHelper/carcinization/crab"];

        public Dictionary<string, Func<IEnumerator>> routines;

        public Carcinization(string type, Func<Carcinization, bool> after = null) {
            routines = new() {
                { "carcinized", GTAVRoutine },
                { "minecraft", MCRoutine },
                { "dark souls", DarkSoulsRoutine },
                { "skyrim", SkyrimRoutine },
                { "generic", GenericRoutine }
            };
            Depth = -9999999;
            this.after = after;
            if (type == "random") {
                this.type = routines.Keys.ToList()[new Random().Next(routines.Count)];
            } else {
                this.type = type.ToLower();
            }
        }

        public override void Awake(Scene scene) {
            base.Awake(scene);
            lvl = SceneAs<Level>();
            Add(new Coroutine(routines[type].Invoke()));
        }

        private const float GTAVFadeInTime = 1.5f;
        public IEnumerator GTAVRoutine() {
            var player = lvl.Tracker.GetEntity<Player>();
            if (player != null) {
                player.StateMachine.State = Player.StDummy;
            }
            Engine.TimeRate = 0.2f;
            var oldColorGrade = lvl.Session.ColorGrade;
            lvl.Session.ColorGrade = "templevoid";
            lvl.colorGradeEaseSpeed = 1 / GTAVFadeInTime / (Engine.TimeRate * Engine.TimeRateB);
            Audio.Play("event:/brokemia/carcinization/gtav_death_sound");
            yield return GTAVFadeInTime * (Engine.TimeRate * Engine.TimeRateB);

            if (player != null) {
                player.Visible = false;
                crabifyPlayer = true;
            }
            centerImg = GFX.Game["BrokemiaHelper/carcinization/wasted"];
            yield return 7 * (Engine.TimeRate * Engine.TimeRateB);
            if (after?.Invoke(this) ?? true) {
                lvl.colorGradeEaseSpeed = 1;
                lvl.Session.ColorGrade = oldColorGrade;
                while (centerImgAlpha > 0) {
                    yield return null;
                    centerImgAlpha -= Engine.RawDeltaTime;
                    Engine.TimeRate = Math.Min(1, Engine.TimeRate + Engine.DeltaTime);
                }
                centerImgAlpha = 0;
                centerImg = null;
                Engine.TimeRate = 1;
                if (player != null) {
                    player.StateMachine.State = Player.StNormal;
                    player.Visible = true;
                    crabifyPlayer = false;
                }
                RemoveSelf();
            }
        }

        public IEnumerator MCRoutine() {
            var player = lvl.Tracker.GetEntity<Player>();
            if (player != null) {
                player.StateMachine.State = Player.StDummy;
                player.Visible = false;
                crabifyPlayer = true;
            }

            var redBG = new Image(Draw.Pixel) {
                Scale = new Vector2(320, 181) * lvl.Camera.Zoom,
                Color = Color.Red * 0.4f,
                RenderPosition = lvl.Camera.Position - Vector2.UnitY
            };
            Add(redBG);
            var crabbedText = new Image(GFX.Game["BrokemiaHelper/carcinization/mc_crabbed"]);
            crabbedText.RenderPosition = lvl.Camera.Position + new Vector2(160 - crabbedText.Width, 40) * lvl.Camera.Zoom;
            crabbedText.Scale = new(2);
            Add(crabbedText);
            var scoreText = new Image(GFX.Game["BrokemiaHelper/carcinization/mc_score"]);
            scoreText.RenderPosition = lvl.Camera.Position + new Vector2(160 - scoreText.Width / 2, 70) * lvl.Camera.Zoom;
            Add(scoreText);
            var score = SaveData.Instance.TotalStrawberries;
            var scoreIdx = 1;
            while (score > 0 || scoreIdx == 1) {
                var digit = new Image(GFX.Game["BrokemiaHelper/carcinization/mc_" + (score % 10)]);
                digit.RenderPosition = lvl.Camera.Position + new Vector2(160 + scoreText.Width / 2 - scoreIdx * 6, 70) * lvl.Camera.Zoom;
                Add(digit);
                score /= 10;
                scoreIdx++;
            }
            var buttonTex = GFX.Game["BrokemiaHelper/carcinization/mc_button"];
            var buttonSelectedTex = GFX.Game["BrokemiaHelper/carcinization/mc_button_selected"];
            var buttonOne = new Image(buttonTex);
            buttonOne.RenderPosition = lvl.Camera.Position + new Vector2(160 - buttonOne.Width / 2, 95) * lvl.Camera.Zoom;
            Add(buttonOne);
            var buttonTwo = new Image(buttonTex);
            buttonTwo.RenderPosition = lvl.Camera.Position + new Vector2(160 - buttonTwo.Width / 2, 119) * lvl.Camera.Zoom;
            Add(buttonTwo);

            int selected = -1;
            while (selected < 0 || !Input.MenuConfirm.Pressed) {
                yield return null;
                if (Input.MenuUp.Pressed) {
                    selected = 0;
                    buttonOne.Texture = buttonSelectedTex;
                    buttonTwo.Texture = buttonTex;
                } else if (Input.MenuDown.Pressed) {
                    selected = 1;
                    buttonOne.Texture = buttonTex;
                    buttonTwo.Texture = buttonSelectedTex;
                }
            }
            Input.MenuConfirm.ConsumePress();

            if (after?.Invoke(this) ?? true) {
                if (player != null) {
                    player.StateMachine.State = Player.StNormal;
                    player.Visible = true;
                    crabifyPlayer = false;
                }
                RemoveSelf();
            }
        }

        public IEnumerator DarkSoulsRoutine() {
            var player = lvl.Tracker.GetEntity<Player>();
            if (player != null) {
                player.StateMachine.State = Player.StDummy;
                player.Visible = false;
                crabifyPlayer = true;
            }

            centerImgAlpha = 0;
            var bg = new Image(GFX.Game["BrokemiaHelper/carcinization/darksouls_bg"]);
            bg.RenderPosition = lvl.Camera.Position + new Vector2(0, 90 - bg.Height / 2) * lvl.Camera.Zoom;
            bg.Color = Color.Transparent;
            Add(bg);

            var text = new Image(GFX.Game["BrokemiaHelper/carcinization/darksouls_text"]);
            text.Scale = new(0.7f);
            text.RenderPosition = lvl.Camera.Position + new Vector2(160 - text.Width / 2 * text.Scale.X, 90 - text.Height / 2 * text.Scale.Y) * lvl.Camera.Zoom;
            Add(text);

            Audio.Play("event:/brokemia/carcinization/dark_souls_death_sound");

            var textAlpha = 0f;
            while (textAlpha < 1) {
                centerImgAlpha = Math.Min(1, centerImgAlpha + Engine.DeltaTime * 3);
                bg.Color = Color.White * centerImgAlpha;
                textAlpha = Math.Min(1, textAlpha + Engine.DeltaTime);
                text.Color = Color.White * textAlpha;
                text.Scale = new(0.7f + 0.3f * textAlpha);
                text.RenderPosition = lvl.Camera.Position + new Vector2(160 - text.Width / 2 * text.Scale.X, 90 - text.Height / 2 * text.Scale.Y) * lvl.Camera.Zoom;
                yield return null;
            }

            yield return 5f;

            if (after?.Invoke(this) ?? true) {
                if (player != null) {
                    player.StateMachine.State = Player.StNormal;
                    player.Visible = true;
                    crabifyPlayer = false;
                }
                RemoveSelf();
            }
        }

        public IEnumerator SkyrimRoutine() {
            Dialog.FallbackLanguage.Dialog["BrokemiaHelper_Skyrim1"] = "Hey, you. You're finally awake.";
            Dialog.FallbackLanguage.Dialog["BrokemiaHelper_Skyrim2"] = "You were trying to cross the border, right? Walked right into that Imperial ambush, same as us.";
            Dialog.FallbackLanguage.Dialog["BrokemiaHelper_Skyrim3"] = "And that thief over there.";

            var player = lvl.Tracker.GetEntity<Player>();
            if (player != null) {
                player.StateMachine.State = Player.StDummy;
                player.Visible = false;
                crabifyPlayer = true;
            }

            var image = new Image(GFX.Game["BrokemiaHelper/carcinization/skyrim"]);
            image.RenderPosition = lvl.Camera.Position;
            image.Color = Color.Transparent;
            Add(image);

            var darkness = new Image(Draw.Pixel);
            darkness.RenderPosition = lvl.Camera.Position - Vector2.UnitY;
            darkness.Scale = new Vector2(320, 181) * lvl.Camera.Zoom;
            darkness.Color = Color.Transparent;
            Add(darkness);

            var darkAlpha = 0f;
            while (darkAlpha < 1) {
                darkAlpha = Math.Min(1, darkAlpha + Engine.DeltaTime / 1.5f);
                darkness.Color = Color.Black * darkAlpha;
                yield return null;
            }

            yield return 0.5f;

            Audio.Play("event:/brokemia/carcinization/skyrim");

            yield return 2f;

            image.Color = Color.White;

            while (darkAlpha > 0) {
                darkAlpha = Math.Max(0, darkAlpha - Engine.DeltaTime / 7);
                darkness.Color = Color.Black * Ease.CubeInOut(darkAlpha);
                yield return null;
            }

            yield return 26;
            Scene.Add(new MiniTextbox("BrokemiaHelper_Skyrim1"));

            yield return 4;
            Scene.Add(new MiniTextbox("BrokemiaHelper_Skyrim2"));

            yield return 6;
            Scene.Add(new MiniTextbox("BrokemiaHelper_Skyrim3"));

            yield return 4;

            if (after?.Invoke(this) ?? true) {

                while (darkAlpha < 1) {
                    darkAlpha = Math.Min(1, darkAlpha + Engine.DeltaTime);
                    darkness.Color = Color.Black * darkAlpha;
                    yield return null;
                }
                image.Color = Color.Transparent;
                yield return 0.4f;
                crabifyPlayer = false;
                if (player != null) {
                    player.Visible = true;
                }

                while (darkAlpha > 0) {
                    darkAlpha = Math.Max(0, darkAlpha - Engine.DeltaTime);
                    darkness.Color = Color.Black * darkAlpha;
                    yield return null;
                }
                if (player != null) {
                    player.StateMachine.State = Player.StNormal;
                }
                RemoveSelf();
            }
        }

        public IEnumerator GenericRoutine() {
            string[] dialogs = {
                "Carcinization gets us all in the end.",
                "Despite all my rage, I am still just a crab in a cage.",
                "He's having a smoke, and she's calling a cab, and she's wearing a wig, and she's also a crab."
            };
            Dialog.FallbackLanguage.Dialog["BrokemiaHelper_GenericCarcinizationText"] = Calc.Random.Choose(dialogs);
            var player = lvl.Tracker.GetEntity<Player>();
            if (player != null) {
                player.StateMachine.State = Player.StDummy;
            }
            yield return Textbox.Say("BrokemiaHelper_GenericCarcinizationText");
            yield return 0.5f;
            crabifyPlayer = true;
            if (player != null) {
                player.Visible = false;
            }

            yield return 4f;
            if (after?.Invoke(this) ?? true) {
                crabifyPlayer = false;
                if (player != null) {
                    player.Visible = true;
                    player.StateMachine.State = Player.StNormal;
                }
                RemoveSelf();
            }
        }

        public override void Render() {
            if (crabifyPlayer && lvl.Tracker.GetEntity<Player>() is { } player) {
                crabImg.Draw(player.Sprite.RenderPosition.Floor(), player.Sprite.Origin, player.Sprite.Color, player.Sprite.Scale, player.Sprite.Rotation, player.Sprite.Effects);
            }
            base.Render();
            centerImg?.DrawCentered(lvl.Camera.Position + new Vector2(160, 90) / lvl.Camera.Zoom, Color.White * centerImgAlpha);
        }
    }
}
