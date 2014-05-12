using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace FinalRush
{
    class MainMenu : GameComponent
    {
        #region Variables

        //Enum des cas possibles

        public enum GameState
        {
            MainMenu,
            InGame,
            InGame2,
            InGame3,
            InGame4,
            InGame5,
            InGame6,
            InOptions,
            InClose,
            InPause,
            HowToPlay,
            HowToPlay2,
            SelectionMap,
            Credits,
            Won,
            Chapitre1,
            Chapitre2,
            Chapitre3,
            Chapitre4,
            Chapitre5,
            Chapitre6,
            Intro,
            Scenario,
            GameOver,
            Multi
        }
        public SoundEffectInstance coeur_sound_instance;

        public GameState gameState;
        GameMain Main;
        GameMain2 Main2;
        GameMain3 Main3;
        GameMain4 Main4;
        GameMain5 Main5;
        GameMain6 Main6;
        public Player player, player2, player3, player4, player5, player6;
        bool HasPlayed;
        public int comptlevel = 0;
        int compt = 0;
        bool downcolor = true;
        bool goforgame = false;
        public bool enjeu;
        Color colour = new Color(255, 255, 255, 255);
        Color colourScenario = new Color(255, 255, 255, 255);
        Texture2D Intro_fond, Intro_fond2, coin;
        int framecolumn = 1;
        int compteurpourframecolumn = 1;
        Texture2D fond_menu, fond_win, fond_gameover, fond_how2play;

        public SpriteFont font;
        public string text;
        public float time;
        public int score = 0;
        private Vector2 position;
        public bool started;
        public bool paused;
        KeyboardState pastkey;
        public bool finished;
        float deltaTime;
        public int nb_pieces;
        SoundEffectInstance piece_sound_instance;

        //Liste qui contiendra tous les rectangles (donc les boutons) nécessaires

        List<GUIElement> Intro = new List<GUIElement>();
        List<GUIElement> main = new List<GUIElement>();
        List<GUIElement> InOptions = new List<GUIElement>();
        List<GUIElement> InPause = new List<GUIElement>();
        List<GUIElement> HowToPlay = new List<GUIElement>();
        List<GUIElement> HowToPlay2 = new List<GUIElement>();
        List<GUIElement> SelectionMap = new List<GUIElement>();
        List<GUIElement> GameOver = new List<GUIElement>();
        List<GUIElement> Credits = new List<GUIElement>();
        List<GUIElement> Won = new List<GUIElement>();
        List<GUIElement> Chapitre1 = new List<GUIElement>();
        List<GUIElement> Chapitre2 = new List<GUIElement>();
        List<GUIElement> Chapitre3 = new List<GUIElement>();
        List<GUIElement> Chapitre4 = new List<GUIElement>();
        List<GUIElement> Chapitre5 = new List<GUIElement>();
        List<GUIElement> Chapitre6 = new List<GUIElement>();
        List<GUIElement> Multi = new List<GUIElement>();

        #endregion

        #region Constructeur (Add)

        public MainMenu(Game1 game, float startTime)
            : base(game)
        {
            // Constructeur:
            time = startTime;
            started = false;
            paused = false;
            finished = false;
            Text = "0";
            coeur_sound_instance = Resources.CoeurRapide.CreateInstance();
            piece_sound_instance = Resources.piece.CreateInstance();
            nb_pieces = 0;

            //Ajout des boutons nécessaires

            Intro.Add(new GUIElement(@"Sprites\Menu\Intro\Intro2_Fond"));
            Intro.Add(new GUIElement(@"Sprites\Menu\Intro\Intro_Fond"));

            main.Add(new GUIElement(@"Sprites\Menu\Button_jouer"));
            main.Add(new GUIElement(@"Sprites\Menu\Button_options"));
            main.Add(new GUIElement(@"Sprites\Menu\Button_quitter"));
            main.Add(new GUIElement(@"Sprites\Menu\Bouton_Credits"));

            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Retour"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Volume"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Plus"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Moins"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Bruitages"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Plus2"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Moins2"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_Commandes"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Bouton_PleinEcran"));

            InPause.Add(new GUIElement(@"Sprites\Menu\Bouton_Continuer"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Button_options"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Bouton_MenuPrincipalGros"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Button_quitter"));

            HowToPlay.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToOptions"));

            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Bouton_NouvellePartie"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Bouton_Multijoueur"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Bouton_Retour"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Bouton_Chapitres"));

            GameOver.Add(new GUIElement(@"Sprites\Menu\Boutton_Rejouer"));
            GameOver.Add(new GUIElement(@"Sprites\Menu\Bouton_MenuPrincipal"));

            Credits.Add(new GUIElement(@"Sprites\Menu\Bouton_Retour"));

            Won.Add(new GUIElement(@"Sprites\Menu\Bouton_NiveauSuivant"));
            Won.Add(new GUIElement(@"Sprites\Menu\Boutton_Rejouer"));
            Won.Add(new GUIElement(@"Sprites\Menu\Bouton_MenuPrincipal"));

            Chapitre1.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre1.Add(new GUIElement(@"Sprites\Menu\Level1"));
            Chapitre1.Add(new GUIElement(@"Sprites\Menu\fleche_droite"));


            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Level2"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\fleche_droite2"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\fleche_gauche"));

            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Level3"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\fleche_droite3"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\fleche_gauche2"));

            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Level4"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\fleche_droite4"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\fleche_gauche3"));

            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Level5"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\fleche_droite5"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\fleche_gauche4"));

            Chapitre6.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));
            Chapitre6.Add(new GUIElement(@"Sprites\Menu\Level6"));
            Chapitre6.Add(new GUIElement(@"Sprites\Menu\fleche_gauche5"));

            Multi.Add(new GUIElement(@"Sprites\Menu\Join"));
            Multi.Add(new GUIElement(@"Sprites\Menu\Create"));
            Multi.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToJouer"));

            player = Global.Player;
            player2 = Global.Player;
            player3 = Global.Player;
            player4 = Global.Player;
            player5 = Global.Player;
            player6 = Global.Player;
            Main = Global.GameMain;
            Main2 = Global.GameMain2;
            Main3 = Global.GameMain3;
            Main4 = Global.GameMain4;
            Main5 = Global.GameMain5;
            Main6 = Global.GameMain6;
            Global.MainMenu = this;
        }

        #endregion

        #region Properties

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool Started
        {
            get { return started; }
            set { started = value; }
        }

        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion

        #region LoadContent (Find)

        public void LoadContent(ContentManager content)
        {
            coin = content.Load<Texture2D>(@"SpriteFonts\coin");
            fond_how2play = content.Load<Texture2D>(@"Sprites\Menu\HowToPlayPage1");
            fond_menu = content.Load<Texture2D>(@"Sprites\Menu\Menu_fond");
            fond_win = content.Load<Texture2D>(@"Sprites\Menu\Won");
            fond_gameover = content.Load<Texture2D>(@"Sprites\Menu\GameOver");
            Font = content.Load<SpriteFont>(@"SpriteFonts\TimerFont");
            Intro_fond = content.Load<Texture2D>(@"Sprites\Menu\Intro\Intro_Fond");
            foreach (GUIElement element in Intro)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
            }
            Intro_fond2 = content.Load<Texture2D>(@"Sprites\Menu\Intro\Intro2_Fond");

            // Load le menu ainsi que les boutons dans la liste qui lui sont associés

            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            // On déplace les boutons du menu à notre guise (par rapport à (0,0))

            main.Find(x => x.AssetName == @"Sprites\Menu\Button_jouer").MoveElement(0, -50);
            main.Find(x => x.AssetName == @"Sprites\Menu\Button_options").MoveElement(0, 30);
            main.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Credits").MoveElement(0, 110);
            main.Find(x => x.AssetName == @"Sprites\Menu\Button_quitter").MoveElement(0, 190);

            // Options

            //De même pour l'option

            foreach (GUIElement element in InOptions)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Volume").MoveElement(-253, -90);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Plus").MoveElement(0, -90);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Moins").MoveElement(-70, -90);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Bruitages").MoveElement(-240, -30);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Plus2").MoveElement(0, -30);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Moins2").MoveElement(-70, -30);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Commandes").MoveElement(-222, 30);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_PleinEcran").MoveElement(-225, 90);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Retour").MoveElement(-270, 210);

            // De même pour InPause

            foreach (GUIElement element in InPause)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            InPause.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Continuer").MoveElement(-80, -50);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Button_options").MoveElement(-80, 30);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipalGros").MoveElement(-80, 110);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Button_quitter").MoveElement(-80, 190);

            // De même pour GameOver

            foreach (GUIElement element in GameOver)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            GameOver.Find(x => x.AssetName == @"Sprites\Menu\Boutton_Rejouer").MoveElement(-200, 210);
            GameOver.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipal").MoveElement(200, 210);

            // De même pour Commandes & Tips

            foreach (GUIElement element in HowToPlay)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            HowToPlay.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToOptions").MoveElement(-260, 210);

            foreach (GUIElement element in SelectionMap)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Bouton_NouvellePartie").MoveElement(0, -20);
            SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Multijoueur").MoveElement(0, 50);
            SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Retour").MoveElement(0, 200);
            SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Chapitres").MoveElement(0, 120);

            foreach (GUIElement element in Credits)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            Credits.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Retour").MoveElement(0, 200);

            foreach (GUIElement element in Won)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Won.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipal").MoveElement(-320, 210);
            Won.Find(x => x.AssetName == @"Sprites\Menu\Bouton_NiveauSuivant").MoveElement(260, 210);
            Won.Find(x => x.AssetName == @"Sprites\Menu\Boutton_Rejouer").MoveElement(-60, 210);

            foreach (GUIElement element in Chapitre1)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Level1").MoveElement(-70, 0);
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite").MoveElement(0, 140);

            foreach (GUIElement element in Chapitre2)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Level2").MoveElement(-70, 0);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite2").MoveElement(0, 140);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche").MoveElement(-139, 140);

            foreach (GUIElement element in Chapitre3)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Level3").MoveElement(-70, 0);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite3").MoveElement(0, 140);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche2").MoveElement(-139, 140);

            foreach (GUIElement element in Chapitre4)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Level4").MoveElement(-70, 0);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite4").MoveElement(0, 140);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche3").MoveElement(-139, 140);

            foreach (GUIElement element in Chapitre5)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Level5").MoveElement(-70, 0);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite5").MoveElement(0, 140);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche4").MoveElement(-139, 140);

            foreach (GUIElement element in Chapitre6)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 200);
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Level6").MoveElement(-70, 0);
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche5").MoveElement(-139, 140);

            foreach (GUIElement element in Multi)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Multi.Find(x => x.AssetName == @"Sprites\Menu\Join").MoveElement(-70, 0);
            Multi.Find(x => x.AssetName == @"Sprites\Menu\Create").MoveElement(-70, 100);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-70, 400);
        }

        #endregion

        #region Initialize

        // On lance l'initialize de notre jeu donc ici la musique du menu

        public override void Initialize()
        {
            gameState = GameState.Intro;
            MediaPlayer.Play(Resources.MusiqueIntro);
            MediaPlayer.Volume = 0.6f;
            MediaPlayer.IsRepeating = true;
            switch (gameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.InGame:
                    break;
                case GameState.InGame2:
                    break;
                case GameState.InGame3:
                    break;
                case GameState.InOptions:
                    break;
                case GameState.InPause:
                    break;
                case GameState.InClose:
                    break;
                case GameState.Won:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
        }

        public bool EnJeu(bool enjeu)
        {
            this.enjeu = enjeu;
            return enjeu;
        }

        #endregion

        #region Update

        //Update va detecter le moindre changement d'état et appliquer les modifs' à faire

        public override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Intro:
                    compt = compt + 1;
                    if (colour.R == 255) downcolor = true;
                    if (colour.R == 0) downcolor = false;
                    if (downcolor)
                    {
                        colour.R -= 1;
                        colour.G -= 1;
                        colour.B -= 1;
                    }
                    else
                    {
                        colour.R += 1;
                        colour.G += 1;
                        colour.B += 1;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Escape) || compt > 910)
                    {
                        downcolor = true;
                        gameState = GameState.MainMenu;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueMenu);
                        MediaPlayer.IsRepeating = true;
                    }
                    enjeu = false;
                    break;
                case GameState.MainMenu:
                    foreach (GUIElement element in main)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.InGame:
                    comptlevel = 1;
                    Main.Update(Mouse.GetState(), Keyboard.GetState());
                    player.Update(Mouse.GetState(), Keyboard.GetState(), Main.Walls, Main.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player.Hitbox, Main.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player.Hitbox, Main.enemies))
                    {
                    }
                    if (Global.Collisions.CollisionEnemy2(player.Hitbox, Main.enemies2))
                    {
                    }
                    /*if (Global.Collisions.CollisionPiques(player.Hitbox, Main.piques))
                    {
                    }*/

                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Global.Player.health <= 20 && gameState == GameState.InGame)
                    {
                        coeur_sound_instance.Play();
                    }
                    else
                        coeur_sound_instance.Stop();

                    if ((Keyboard.GetState().IsKeyDown(Keys.P) && pastkey.IsKeyUp(Keys.P)) || (Keyboard.GetState().IsKeyDown(Keys.Escape) && pastkey.IsKeyUp(Keys.Escape)))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    pastkey = Keyboard.GetState();
                    enjeu = true;
                    if (Global.Player.health == 0 || player.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InGame2:
                    comptlevel = 2;
                    Main2.Update(Mouse.GetState(), Keyboard.GetState());
                    player2.Update(Mouse.GetState(), Keyboard.GetState(), Main2.Walls, Main2.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player2.Hitbox, Main2.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player2.Hitbox, Main2.enemies))
                    {
                    }
                    if (Global.Collisions.CollisionEnemy2(player2.Hitbox, Main2.enemies2))
                    {
                    }
                    /*     if (Global.Collisions.CollisionPiques(player2.Hitbox, Main2.piques))
                         {
                         }*/


                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        time = 0f;
                        score = 300;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player2.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    enjeu = true;
                    if (Global.Player.health == 0 || player2.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InGame3:
                    comptlevel = 3;
                    Main3.Update(Mouse.GetState(), Keyboard.GetState());
                    player3.Update(Mouse.GetState(), Keyboard.GetState(), Main3.Walls, Main3.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player3.Hitbox, Main3.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player3.Hitbox, Main3.enemies))
                    {
                    }

                    if (Global.Collisions.CollisionEnemy2(player3.Hitbox, Main3.enemies2))
                    {
                    }

                    if (Global.Collisions.CollisionPiques(player3.Hitbox, Main3.piques))
                    {
                    }

                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player3.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    enjeu = true;
                    if (Global.Player.health == 0 || player3.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InGame4:
                    comptlevel = 4;
                    Main4.Update(Mouse.GetState(), Keyboard.GetState());
                    player4.Update(Mouse.GetState(), Keyboard.GetState(), Main4.Walls, Main4.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player4.Hitbox, Main4.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player4.Hitbox, Main4.enemies))
                    {
                    }

                    if (Global.Collisions.CollisionEnemy2(player4.Hitbox, Main4.enemies2))
                    {
                    }

                    //if (Global.Collisions.CollisionPiques(player4.Hitbox, Main4.piques))
                    //{
                    //}

                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player4.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    enjeu = true;
                    if (Global.Player.health == 0 || player4.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InGame5:
                    comptlevel = 5;
                    Main5.Update(Mouse.GetState(), Keyboard.GetState());
                    player5.Update(Mouse.GetState(), Keyboard.GetState(), Main5.Walls, Main5.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player5.Hitbox, Main5.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player5.Hitbox, Main5.enemies))
                    {
                    }

                    if (Global.Collisions.CollisionEnemy2(player5.Hitbox, Main5.enemies2))
                    {
                    }

                    //if (Global.Collisions.CollisionPiques(player5.Hitbox, Main5.piques))
                    //{
                    //}

                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player5.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    enjeu = true;
                    if (Global.Player.health == 0 || player5.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InGame6:
                    comptlevel = 6;
                    Main6.Update(Mouse.GetState(), Keyboard.GetState());
                    player6.Update(Mouse.GetState(), Keyboard.GetState(), Main6.Walls, Main6.bonus);

                    #region Timer
                    deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (Global.Collisions.CollisionBonus(player6.Hitbox, Main6.bonus))
                    {
                        nb_pieces++;
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player6.Hitbox, Main6.enemies))
                    {
                    }

                    if (Global.Collisions.CollisionEnemy2(player6.Hitbox, Main6.enemies2))
                    {
                    }

                    //if (Global.Collisions.CollisionPiques(player6.Hitbox, Main6.piques))
                    //{
                    //}

                    if (started)
                    {
                        if (!paused)
                        {
                            if (time >= 0)
                                time += deltaTime;
                            else
                                finished = true;
                        }
                    }

                    Text = ((int)time).ToString();
                    score = (nb_pieces * 10) + 300 - ((int)time);

                    if (time == 300)
                    {
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    #endregion

                    if (player6.Hitbox.X > 4600)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueVictory);
                        MediaPlayer.IsRepeating = false;
                        gameState = GameState.Won;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        gameState = GameState.InPause;
                        MediaPlayer.Volume = 0.2f;
                    }
                    enjeu = true;
                    if (Global.Player.health == 0 || player6.dead)
                    {
                        enjeu = false;
                        gameState = GameState.GameOver;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueGameOver);
                        MediaPlayer.IsRepeating = false;
                    }
                    break;
                case GameState.InOptions:
                    foreach (GUIElement element in InOptions)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.GameOver:
                    foreach (GUIElement element in GameOver)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Won:
                    foreach (GUIElement element in Won)
                    {
                        element.Update();
                        if (compteurpourframecolumn == 35)
                        {
                            compteurpourframecolumn = 1;
                            if (framecolumn == 7) framecolumn = 1;
                            else framecolumn++;
                        }
                        else
                            compteurpourframecolumn++;
                    }
                    enjeu = false;
                    break;
                case GameState.InPause:
                    if ((Keyboard.GetState().IsKeyDown(Keys.P) && pastkey.IsKeyUp(Keys.P)) || (Keyboard.GetState().IsKeyDown(Keys.Escape) && pastkey.IsKeyUp(Keys.Escape)))
                    {
                        if (comptlevel == 1)
                            gameState = GameState.InGame;
                        else if (comptlevel == 2)
                            gameState = GameState.InGame2;
                        else if (comptlevel == 3)
                            gameState = GameState.InGame3;
                        else if (comptlevel == 4)
                            gameState = GameState.InGame4;
                        else if (comptlevel == 5)
                            gameState = GameState.InGame5;
                        else
                            gameState = GameState.InGame6;
                    }
                    pastkey = Keyboard.GetState();
                    foreach (GUIElement element in InPause)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.HowToPlay:
                    foreach (GUIElement element in HowToPlay)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.SelectionMap:
                    foreach (GUIElement element in SelectionMap)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Credits:
                    foreach (GUIElement element in Credits)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre1:
                    foreach (GUIElement element in Chapitre1)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre2:
                    foreach (GUIElement element in Chapitre2)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre3:
                    foreach (GUIElement element in Chapitre3)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre4:
                    foreach (GUIElement element in Chapitre4)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre5:
                    foreach (GUIElement element in Chapitre5)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Chapitre6:
                    foreach (GUIElement element in Chapitre6)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                case GameState.Scenario:
                    if (colourScenario.R < 10) downcolor = false;

                    if (downcolor)
                    {
                        if (goforgame)
                        {
                            colourScenario.R -= 2;
                            colourScenario.G -= 2;
                            colourScenario.B -= 2;
                        }
                        else
                        {
                            colourScenario.R -= 125;
                            colourScenario.G -= 125;
                            colourScenario.B -= 125;
                        }
                    }
                    if (!downcolor && colourScenario.R < 255)
                    {
                        colourScenario.R += 1;
                        colourScenario.G += 1;
                        colourScenario.B += 1;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        downcolor = true;
                        goforgame = true;
                    }
                    if (colourScenario.R < 10 && goforgame)
                    {
                        gameState = GameState.InGame;
                    }
                    break;
                case GameState.InClose:
                    Global.Handler.Exit();
                    break;
                case GameState.Multi:
                    foreach (GUIElement element in Multi)
                    {
                        element.Update();
                    }
                    enjeu = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Draw

        // Draw va dessiner les modifs' à faire

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.Intro:
                    if (compt < 255) spriteBatch.Draw(Intro_fond, new Rectangle(0, 0, 800, 480), colour);
                    else spriteBatch.Draw(Intro_fond2, new Rectangle(0, 0, 800, 480), Color.White);

                    break;
                case GameState.MainMenu:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.InGame:
                    Main.Draw(spriteBatch);
                    break;
                case GameState.InGame2:
                    Main2.Draw(spriteBatch);
                    break;
                case GameState.InGame3:
                    Main3.Draw(spriteBatch);
                    break;
                case GameState.InGame4:
                    Main4.Draw(spriteBatch);
                    break;
                case GameState.InGame5:
                    Main5.Draw(spriteBatch);
                    break;
                case GameState.InGame6:
                    Main6.Draw(spriteBatch);
                    break;
                case GameState.InOptions:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);

                    foreach (GUIElement element in InOptions)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(fond_gameover, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in GameOver)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Won:
                    spriteBatch.Draw(fond_win, new Rectangle(0, 0, 800, 480), Color.White);
                    spriteBatch.Draw(coin, new Rectangle(250, 250, 30, 30), Color.White);
                    foreach (GUIElement element in Won)
                    {
                        element.Draw(spriteBatch);
                    }
                    spriteBatch.Draw(Resources.MarcoWon, new Rectangle(400, 160, 38, 43), new Rectangle((framecolumn - 1) * 38, 0, 38, 43), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    break;
                case GameState.InPause:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in InPause)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.HowToPlay:
                    spriteBatch.Draw(fond_how2play, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in HowToPlay)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.SelectionMap:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in SelectionMap)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Credits:
                    foreach (GUIElement element in Credits)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre1:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre1)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre2:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre2)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre3:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre3)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre4:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre4)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre5:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre5)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Chapitre6:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre6)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Multi:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Multi)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.Scenario:
                    spriteBatch.Draw(Resources.Scenario, new Rectangle(0, 0, 800, 480), colourScenario);
                    break;
                case GameState.InClose:
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Clic sur un bouton

        // Détecte si l'on clique sur un GUIRect donc sur un bouton et fait le changement d'état.

        public void OnClick(string element)
        {
            if (element == @"Sprites\Menu\Button_jouer")
            {
                gameState = GameState.SelectionMap;
            }

            if (element == @"Sprites\Menu\Button_options")
            {
                gameState = GameState.InOptions;
            }

            if (element == @"Sprites\Menu\Button_quitter")
            {
                gameState = GameState.InClose;
            }
            if (element == @"Sprites\Menu\Bouton_Retour")
            {
                if (HasPlayed == true)
                {
                    gameState = GameState.InPause;
                }
                else
                    gameState = GameState.MainMenu;

            }

            //Modification du volume

            if (element == @"Sprites\Menu\Bouton_Plus")
            {
                if (MediaPlayer.Volume >= 0.9f)
                    MediaPlayer.Volume = 1f;
                else MediaPlayer.Volume = MediaPlayer.Volume + 0.1f;
            }
            if (element == @"Sprites\Menu\Bouton_Moins")
            {
                if (MediaPlayer.Volume <= 0.1f)
                    MediaPlayer.Volume = 0f;
                else MediaPlayer.Volume = MediaPlayer.Volume - 0.1f;
            }
            if (element == @"Sprites\Menu\Bouton_Plus2")
            {
                if (SoundEffect.MasterVolume >= 0.9f)
                    SoundEffect.MasterVolume = 1f;
                else SoundEffect.MasterVolume = SoundEffect.MasterVolume + 0.1f;
            }
            if (element == @"Sprites\Menu\Bouton_Moins2")
            {
                if (SoundEffect.MasterVolume <= 0.1f)
                    SoundEffect.MasterVolume = 0f;
                else SoundEffect.MasterVolume = SoundEffect.MasterVolume - 0.1f;
            }
            if (element == @"Sprites\Menu\Bouton_Continuer")
            {
                if (comptlevel == 1)
                    gameState = GameState.InGame;
                else if (comptlevel == 2)
                    gameState = GameState.InGame2;
                else if (comptlevel == 3)
                    gameState = GameState.InGame3;
                else if (comptlevel == 4)
                    gameState = GameState.InGame4;
                else if (comptlevel == 5)
                    gameState = GameState.InGame5;
                else if (comptlevel == 6)
                    gameState = GameState.InGame6;
            }
            if (element == @"Sprites\Menu\Boutton_Rejouer")
            {
                enjeu = true;
                Global.Handler.ammo_left = 6;
                Global.Handler.recharge_left = 5;
                score = 300;
                time = 0f;
                MediaPlayer.Volume = 0.6f;
                HasPlayed = true;
                {
                    if (comptlevel == 1)
                    {
                        Main = new GameMain();
                        player = new Player();
                        gameState = GameState.InGame;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueMain);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (comptlevel == 2)
                    {
                        Main2 = new GameMain2();
                        player2 = new Player();
                        gameState = GameState.InGame2;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique2);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (comptlevel == 3)
                    {
                        Main3 = new GameMain3();
                        player3 = new Player();
                        gameState = GameState.InGame3;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique3);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (comptlevel == 4)
                    {
                        Main4 = new GameMain4();
                        player4 = new Player();
                        gameState = GameState.InGame4;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique3);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (comptlevel == 5)
                    {
                        Main5 = new GameMain5();
                        player5 = new Player();
                        gameState = GameState.InGame5;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique3);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else if (comptlevel == 6)
                    {
                        Main6 = new GameMain6();
                        player6 = new Player();
                        gameState = GameState.InGame6;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique3);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }

                }
            }
            if (element == @"Sprites\Menu\Bouton_MenuPrincipal")
            {
                //On réinitialise le HUD
                nb_pieces = 0;
                score = 300;
                time = 0f;
                comptlevel = 0;

                gameState = GameState.MainMenu;
                HasPlayed = false;
                MediaPlayer.Volume = 0.6f;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMenu);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Bouton_MenuPrincipalGros")
            {
                //On réinitialise le HUD
                score = 300;
                nb_pieces = 0;
                time = 0f;
                comptlevel = 0;

                gameState = GameState.MainMenu;
                HasPlayed = false;
                MediaPlayer.Volume = 0.6f;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMenu);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Bouton_Commandes")
            {
                gameState = GameState.HowToPlay;
            }
            if (element == @"Sprites\Menu\Bouton_NouvellePartie")
            {
                score = 300;
                time = 0f;
                nb_pieces = 0;
                enjeu = true;
                // HasPlayed détecte si le joueur a déjà lancé une partie pour pouvoir la récupérer après.
                Global.Handler.ammo_left = 6;
                Global.Handler.recharge_left = 5;
                HasPlayed = true;
                comptlevel = 1;
                Main = new GameMain();
                player = new Player();
                gameState = GameState.Scenario;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMain);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Boutton_Rejouer")
            {
                nb_pieces = 0;
                HasPlayed = true;
                if (comptlevel == 1)
                {
                    Main = new GameMain();
                    player = new Player();
                    gameState = GameState.InGame;
                }
                else if (comptlevel == 2)
                {
                    Main2 = new GameMain2();
                    player2 = new Player();
                    gameState = GameState.InGame2;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique2);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                }
                else if (comptlevel == 3)
                {
                    Main3 = new GameMain3();
                    player3 = new Player();
                    gameState = GameState.InGame3;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique3);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                }
                else if (comptlevel == 4)
                {
                    Main4 = new GameMain4();
                    player4 = new Player();
                    gameState = GameState.InGame4;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique3);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                }
                else if (comptlevel == 5)
                {
                    Main5 = new GameMain5();
                    player5 = new Player();
                    gameState = GameState.InGame5;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique3);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                }
                else if (comptlevel == 6)
                {
                    Main6 = new GameMain6();
                    player6 = new Player();
                    gameState = GameState.InGame6;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique3);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                }
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMain);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Bouton_Credits")
            {
                gameState = GameState.Credits;
            }
            if (element == @"Sprites\Menu\Bouton_RetourToOptions")
            {
                gameState = GameState.InOptions;
            }
            if (element == @"Sprites\Menu\Bouton_RetourToJouer")
            {
                gameState = GameState.SelectionMap;
            }
            if (element == @"Sprites\Menu\Bouton_Chapitres")
            {
                gameState = GameState.Chapitre1;
            }
            if (element == @"Sprites\Menu\Level1")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 1;
                Main = new GameMain();
                player = new Player();
                gameState = GameState.InGame;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMain);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Level2")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 2;
                Main2 = new GameMain2();
                player2 = new Player();
                gameState = GameState.InGame2;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.Musique2);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }
            if (element == @"Sprites\Menu\Level3")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 3;
                Main3 = new GameMain3();
                player3 = new Player();
                gameState = GameState.InGame3;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.Musique3);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Level4")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 4;
                Main4 = new GameMain4();
                player4 = new Player();
                gameState = GameState.InGame4;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.Musique3);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Level5")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 5;
                Main5 = new GameMain5();
                player5 = new Player();
                gameState = GameState.InGame5;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.Musique3);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Level6")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                HasPlayed = true;
                comptlevel = 6;
                Main6 = new GameMain6();
                player6 = new Player();
                gameState = GameState.InGame6;
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.Musique3);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Bouton_NiveauSuivant")
            {
                enjeu = true;
                time = 0f;
                score = 300;
                nb_pieces = 0;
                Global.Handler.recharge_left = 5;
                Global.Handler.ammo_left = 6;
                if (comptlevel == 1)
                {
                    Main2 = new GameMain2();
                    player2 = new Player();
                    gameState = GameState.InGame2;
                    MediaPlayer.Volume = MediaPlayer.Volume;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Resources.Musique2);
                    MediaPlayer.Volume = MediaPlayer.Volume;
                    MediaPlayer.IsRepeating = true;
                }
                //Sinon on était au level 2 donc on passe au 3
                else
                    if (comptlevel == 2)
                    {
                        Main3 = new GameMain3();
                        player3 = new Player();
                        gameState = GameState.InGame3;
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.Musique3);
                        MediaPlayer.Volume = MediaPlayer.Volume;
                        MediaPlayer.IsRepeating = true;
                    }
                    else
                        if (comptlevel == 3)
                        {
                            Main4 = new GameMain4();
                            player4 = new Player();
                            gameState = GameState.InGame4;
                            MediaPlayer.Volume = MediaPlayer.Volume;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Resources.Musique3);
                            MediaPlayer.Volume = MediaPlayer.Volume;
                            MediaPlayer.IsRepeating = true;
                        }
                        else
                            if (comptlevel == 4)
                            {
                                Main5 = new GameMain5();
                                player5 = new Player();
                                gameState = GameState.InGame5;
                                MediaPlayer.Volume = MediaPlayer.Volume;
                                MediaPlayer.Stop();
                                MediaPlayer.Play(Resources.Musique3);
                                MediaPlayer.Volume = MediaPlayer.Volume;
                                MediaPlayer.IsRepeating = true;
                            }
                            else
                            {
                                Main6 = new GameMain6();
                                player6 = new Player();
                                gameState = GameState.InGame6;
                                MediaPlayer.Volume = MediaPlayer.Volume;
                                MediaPlayer.Stop();
                                MediaPlayer.Play(Resources.Musique3);
                                MediaPlayer.Volume = MediaPlayer.Volume;
                                MediaPlayer.IsRepeating = true;
                            }
            }

            if (element == @"Sprites\Menu\Bouton_PleinEcran")
            {
                Global.Handler.graphics.ToggleFullScreen();
            }

            if (element == @"Sprites\Menu\fleche_droite")
            {
                gameState = GameState.Chapitre2;
            }

            if (element == @"Sprites\Menu\fleche_droite2")
            {
                gameState = GameState.Chapitre3;
            }

            if (element == @"Sprites\Menu\fleche_droite3")
            {
                gameState = GameState.Chapitre4;
            }

            if (element == @"Sprites\Menu\fleche_droite4")
            {
                gameState = GameState.Chapitre5;
            }

            if (element == @"Sprites\Menu\fleche_droite5")
            {
                gameState = GameState.Chapitre6;
            }

            if (element == @"Sprites\Menu\fleche_gauche")
            {
                gameState = GameState.Chapitre1;
            }

            if (element == @"Sprites\Menu\fleche_gauche2")
            {
                gameState = GameState.Chapitre2;
            }

            if (element == @"Sprites\Menu\fleche_gauche3")
            {
                gameState = GameState.Chapitre3;
            }

            if (element == @"Sprites\Menu\fleche_gauche4")
            {
                gameState = GameState.Chapitre4;
            }

            if (element == @"Sprites\Menu\fleche_gauche5")
            {
                gameState = GameState.Chapitre5;
            }

            if (element == @"Sprites\Menu\Bouton_Multijoueur")
            {
                gameState = GameState.Multi;
            }

            if (element == @"Sprites\Menu\Create")
            {
                gameState = GameState.Multi;
            }

            if (element == @"Sprites\Menu\Join")
            {
                gameState = GameState.Multi;
            }
        }
        #endregion
    }
}