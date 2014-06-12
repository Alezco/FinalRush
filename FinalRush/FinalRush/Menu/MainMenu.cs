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
            InGame, InGame2, InGame3, InGame4, InGame5, InGame6, InGameMulti,
            InOptions,
            InClose,
            InPause,
            HowToPlay,
            SelectionMap,
            Credits,
            Won,
            Chapitre1, Chapitre2, Chapitre3, Chapitre4, Chapitre5, Chapitre6,
            Shop,
            Intro, Scenario,
            GameOver,
            Multi,
            player1win,
            player2win
        }

        public GameState gameState;
        GameMain Main;
        GameMain2 Main2;
        GameMain3 Main3;
        GameMain4 Main4;
        GameMain5 Main5;
        GameMain6 Main6;
        GameMainMulti MainMulti;
        //Multi multi;
        SpriteFont piece_font, players_dead;
        public int HS1, HS2, HS3, HS4, HS5, HS6 = 0;
        int old_HS1, old_HS2, old_HS3, old_HS4, old_HS5, old_HS6 = 0;
        List<Enemy> enemies;
        List<Enemy2> enemies2;
        public Player player, player2, player3, player4, player5, player6, p7, p8;
        bool HasPlayed;
        public bool boss_already_appeared;
        public bool boss_appeared;
        public int comptlevel = 0;
        public int total_piece = 0;
        public int nb_players_dead = 0;
        int compt = 0;
        int lvlcomplete = 0;  //niveaux terminés
        bool downcolor = true;
        bool goforgame = false;
        public bool enjeu;
        SoundEffectInstance mort_enemies;
        Color colour = new Color(255, 255, 255, 255);
        Color colourScenario = new Color(255, 255, 255, 255);
        Texture2D Intro_fond, Intro_fond2, coin, deathhead, zombie;
        int framecolumn = 1;
        int compteurpourframecolumn = 1;
        Texture2D fond_menu, fond_win, fond_gameover, fond_how2play;

        public SpriteFont font, nb_enemies_killed;
        public string text, piece_text;
        public float time, timer_bonus;
        public int score = 0;
        public int enemies_dead;
        private Vector2 position;
        public bool started;
        public bool paused, has_Lost;
        KeyboardState pastkey;
        public bool finished;
        public bool total_piece_updated;
        float deltaTime;
        public int nb_pieces;
        float TextScroll = 100f;


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
        List<GUIElement> Shop = new List<GUIElement>();
        List<GUIElement> player1win = new List<GUIElement>();
        List<GUIElement> player2win = new List<GUIElement>();

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
            boss_appeared = false;
            has_Lost = false;
            boss_already_appeared = false;
            total_piece_updated = false;
            Text = "0";
            nb_pieces = 0;
            mort_enemies = Resources.enemies_sound.CreateInstance();
            piece_font = Resources.piece_font;
            piece_text = " 0";

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
            Won.Add(new GUIElement(@"Sprites\Menu\Bouton_Boutique"));

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

            Shop.Add(new GUIElement(@"Sprites\Menu\Bouton_RetourToHasWon"));

            player1win.Add(new GUIElement(@"Sprites\Menu\Bouton_MenuPrincipal"));
            player2win.Add(new GUIElement(@"Sprites\Menu\Bouton_MenuPrincipal"));

            player = Global.Player;
            player2 = Global.Player;
            player3 = Global.Player;
            player4 = Global.Player;
            player5 = Global.Player;
            player6 = Global.Player;
            p7 = Global.Player;
            p8 = Global.Player;
            Main = Global.GameMain;
            Main2 = Global.GameMain2;
            Main3 = Global.GameMain3;
            Main4 = Global.GameMain4;
            Main5 = Global.GameMain5;
            Main6 = Global.GameMain6;
            MainMulti = Global.GameMainMulti;
            //multi = Global.Multi;
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
            deathhead = content.Load<Texture2D>(@"SpriteFonts\deathhead");
            zombie = content.Load<Texture2D>(@"SpriteFonts\zombie");
            fond_how2play = content.Load<Texture2D>(@"Sprites\Menu\HowToPlayPage1");
            fond_menu = content.Load<Texture2D>(@"Sprites\Menu\Menu_fond");
            fond_win = content.Load<Texture2D>(@"Sprites\Menu\Won");
            fond_gameover = content.Load<Texture2D>(@"Sprites\Menu\GameOver");
            Font = content.Load<SpriteFont>(@"SpriteFonts\TimerFont");
            nb_enemies_killed = content.Load<SpriteFont>(@"SpriteFonts\nb_enemies_killed");
            players_dead = content.Load<SpriteFont>(@"SpriteFonts\nb_players_dead");
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

            main.Find(x => x.AssetName == @"Sprites\Menu\Button_jouer").MoveElement(0, -30);
            main.Find(x => x.AssetName == @"Sprites\Menu\Button_options").MoveElement(0, 50);
            main.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Credits").MoveElement(0, 120);
            main.Find(x => x.AssetName == @"Sprites\Menu\Button_quitter").MoveElement(0, 200);

            // Options

            //De même pour l'option

            foreach (GUIElement element in InOptions)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Volume").MoveElement(-253, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Plus").MoveElement(0, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Moins").MoveElement(-70, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Bruitages").MoveElement(-240, 15);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Plus2").MoveElement(0, 15);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Moins2").MoveElement(-70, 15);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Commandes").MoveElement(-222, 80);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_PleinEcran").MoveElement(-225, 145);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Retour").MoveElement(-200, 210);

            // De même pour InPause

            foreach (GUIElement element in InPause)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            InPause.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Continuer").MoveElement(0, -50);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Button_options").MoveElement(0, 30);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipalGros").MoveElement(0, 110);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Button_quitter").MoveElement(0, 190);

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
            Won.Find(x => x.AssetName == @"Sprites\Menu\Bouton_Boutique").MoveElement(240, 160);

            foreach (GUIElement element in Chapitre1)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Level1").MoveElement(0, 50);
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite").MoveElement(50, 200);

            foreach (GUIElement element in Chapitre2)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Level2").MoveElement(0, 50);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite2").MoveElement(50, 200);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre3)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Level3").MoveElement(0, 50);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite3").MoveElement(50, 200);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche2").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre4)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Level4").MoveElement(0, 50);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite4").MoveElement(50, 200);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche3").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre5)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Level5").MoveElement(0, 50);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\fleche_droite5").MoveElement(50, 200);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche4").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre6)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToJouer").MoveElement(-300, 200);
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Level6").MoveElement(0, 50);
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\fleche_gauche5").MoveElement(-50, 200);

            foreach (GUIElement element in Shop)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            Shop.Find(x => x.AssetName == @"Sprites\Menu\Bouton_RetourToHasWon").MoveElement(-70, 200);

            foreach (GUIElement element in player1win)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            player1win.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipal").MoveElement(0, 210);

            foreach (GUIElement element in player2win)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            player2win.Find(x => x.AssetName == @"Sprites\Menu\Bouton_MenuPrincipal").MoveElement(0, 210);
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
        }

        public bool EnJeu(bool enjeu)
        {
            this.enjeu = enjeu;
            return enjeu;
        }

        #endregion

        #region Méthode CreateGame
        public void CreateGame(int i)
        {
            total_piece_updated = false;
            enjeu = true;
            has_Lost = false;
            score = 300;
            nb_pieces = 0;
            time = 0f;
            enemies_dead = 0;
            Global.Handler.ammo_left = 6;
            Global.Handler.recharge_left = 5;
            MediaPlayer.IsRepeating = true;
            HasPlayed = true;
            switch (i)
            {
                case 1:
                    gameState = GameState.InGame;
                    MediaPlayer.Play(Resources.MusiqueMain);
                    Main = new GameMain();
                    player = new Player();
                    enemies = Global.GameMain.enemies;
                    enemies2 = Global.GameMain.enemies2;
                    break;
                case 2:
                    gameState = GameState.InGame2;
                    MediaPlayer.Play(Resources.Musique2);
                    Main2 = new GameMain2();
                    player2 = new Player();
                    enemies = Global.GameMain2.enemies;
                    enemies2 = Global.GameMain2.enemies2;
                    break;
                case 3:
                    gameState = GameState.InGame3;
                    MediaPlayer.Play(Resources.Musique3);
                    Main3 = new GameMain3();
                    player3 = new Player();
                    enemies = Global.GameMain3.enemies;
                    enemies2 = Global.GameMain3.enemies2;
                    break;
                case 4:
                    gameState = GameState.InGame4;
                    MediaPlayer.Play(Resources.Musique4);
                    Main4 = new GameMain4();
                    player4 = new Player();
                    enemies = Global.GameMain4.enemies;
                    enemies2 = Global.GameMain4.enemies2;
                    break;
                case 5:
                    gameState = GameState.InGame5;
                    MediaPlayer.Play(Resources.Musique5);
                    Main5 = new GameMain5();
                    player5 = new Player();
                    enemies = Global.GameMain5.enemies;
                    enemies2 = Global.GameMain5.enemies2;
                    break;
                case 6:
                    gameState = GameState.InGame6;
                    MediaPlayer.Play(Resources.Musique6);
                    Main6 = new GameMain6();
                    player6 = new Player();
                    enemies = Global.GameMain6.enemies;
                    enemies2 = Global.GameMain6.enemies2;
                    break;
                case 7:
                    gameState = GameState.InGameMulti;
                    MediaPlayer.Play(Resources.Musique3);
                    MainMulti = new GameMainMulti();
                    p7 = new Player();
                    p8 = new Player();
                    break;

            }
        }
        #endregion

        #region Méthode UpdateGame
        public void UpdateGame(Player player, GameTime gameTime, int level, int distanceToWin, int timeToLoose)
        {
            #region Timer
            if (Global.Collisions.collision_speed)
            {
                GameTime timer_b = new GameTime();
                timer_bonus += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer_bonus > 10)
                {
                    Global.Collisions.collision_speed = false;
                    timer_bonus = 0;
                }
            }
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            score = (nb_pieces * 10) + timeToLoose - ((int)time) + enemies_dead * 20 - nb_players_dead * 10;

            if (time == timeToLoose)
            {
                gameState = GameState.GameOver;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueGameOver);
                MediaPlayer.IsRepeating = false;
            }
            #endregion

            if (player.Hitbox.X > distanceToWin)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueVictory);
                MediaPlayer.IsRepeating = false;
                gameState = GameState.Won;
                if (lvlcomplete < level)
                    lvlcomplete = level;
            }
            if ((Keyboard.GetState().IsKeyDown(Keys.P) && pastkey.IsKeyUp(Keys.P)) || (Keyboard.GetState().IsKeyDown(Keys.Escape) && pastkey.IsKeyUp(Keys.Escape)))
            {
                gameState = GameState.InPause;
                MediaPlayer.Volume = 0.2f;
            }
            pastkey = Keyboard.GetState();
            enjeu = true;
            if (player.health == 0 || player.dead && gameState !=GameState.InGameMulti)
            {
                if (comptlevel == 6)
                {
                    boss_already_appeared = false;
                    boss_appeared = false;
                }

                enjeu = false;
                nb_players_dead++;
                gameState = GameState.GameOver;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueGameOver);
                MediaPlayer.IsRepeating = false;
            }
            
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
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.InOptions:
                    foreach (GUIElement element in InOptions)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.GameOver:
                    has_Lost = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        CreateGame(comptlevel);
                    foreach (GUIElement element in GameOver)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.InGame:
                    comptlevel = 1;
                    Main.Update(Mouse.GetState(), Keyboard.GetState());
                    player.Update(Mouse.GetState(), Keyboard.GetState(), Main.Walls, Main.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }


                    Global.Collisions.CollisionHealthBonus(player.Hitbox, Main.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player.Hitbox, Main.speedbonus, gameTime);

                    if (Global.Collisions.CollisionBonus(player.Hitbox, Main.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
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

                    UpdateGame(player, gameTime, 1, 4600, 300);
                    break;
                case GameState.InGame2:
                    comptlevel = 2;
                    Main2.Update(Mouse.GetState(), Keyboard.GetState());
                    player2.Update(Mouse.GetState(), Keyboard.GetState(), Main2.Walls, Main2.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }

                    Global.Collisions.CollisionHealthBonus(player2.Hitbox, Main2.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player2.Hitbox, Main2.speedbonus, gameTime);

                    if (Global.Collisions.CollisionBonus(player2.Hitbox, Main2.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player2.Hitbox, Main2.enemies))
                    {
                    }
                    if (Global.Collisions.CollisionEnemy2(player2.Hitbox, Main2.enemies2))
                    {
                    }
                    /*if (Global.Collisions.CollisionPiques(player2.Hitbox, Main2.piques))
                      {
                      }*/
                    UpdateGame(player2, gameTime, 2, 4600, 300);
                    break;
                case GameState.InGame3:
                    comptlevel = 3;
                    Main3.Update(Mouse.GetState(), Keyboard.GetState());
                    player3.Update(Mouse.GetState(), Keyboard.GetState(), Main3.Walls, Main3.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }

                    Global.Collisions.CollisionPiques(player3.Hitbox, Main3.piques);
                    Global.Collisions.CollisionHealthBonus(player3.Hitbox, Main3.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player3.Hitbox, Main3.speedbonus, gameTime);

                    if (Global.Collisions.CollisionBonus(player3.Hitbox, Main3.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(player3.Hitbox, Main3.enemies))
                    {
                    }

                    if (Global.Collisions.CollisionEnemy2(player3.Hitbox, Main3.enemies2))
                    {
                    }

                    UpdateGame(player3, gameTime, 3, 4600, 300);
                    break;
                case GameState.InGame4:
                    comptlevel = 4;
                    Main4.Update(Mouse.GetState(), Keyboard.GetState());
                    player4.Update(Mouse.GetState(), Keyboard.GetState(), Main4.Walls, Main4.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }

                    Global.Collisions.CollisionHealthBonus(player4.Hitbox, Main4.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player4.Hitbox, Main4.speedbonus, gameTime);

                    if (Global.Collisions.CollisionBonus(player4.Hitbox, Main4.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
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

                    UpdateGame(player4, gameTime, 4, 4600, 300);
                    break;
                case GameState.InGame5:
                    comptlevel = 5;
                    Main5.Update(Mouse.GetState(), Keyboard.GetState());
                    player5.Update(Mouse.GetState(), Keyboard.GetState(), Main5.Walls, Main5.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }

                    Global.Collisions.CollisionHealthBonus(player5.Hitbox, Main5.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player5.Hitbox, Main5.speedbonus, gameTime);

                    if (Global.Collisions.CollisionBonus(player5.Hitbox, Main5.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
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

                    UpdateGame(player5, gameTime, 5, 4600, 300);
                    break;
                case GameState.InGame6:
                    comptlevel = 6;
                    Main6.Update(Mouse.GetState(), Keyboard.GetState());
                    player6.Update(Mouse.GetState(), Keyboard.GetState(), Main6.Walls, Main6.bonus);

                    for (int i = 0; i < enemies.Count(); i++)
                    {
                        if (enemies[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < enemies2.Count(); i++)
                    {
                        if (enemies2[i].isDead)
                        {
                            enemies_dead++;
                            mort_enemies.Play();
                            enemies2.RemoveAt(i);
                        }
                    }

                    if (player6.Hitbox.X == 1000)
                        MediaPlayer.Stop();
                    else
                        if (player6.Hitbox.X >= 1100 && !boss_appeared && !boss_already_appeared)
                        {
                            boss_appeared = true;
                            boss_already_appeared = true;
                        }
                    if (boss_appeared && boss_already_appeared)
                    {
                        boss_appeared = false;
                        MediaPlayer.Play(Resources.MusiqueBoss);
                    }

                    if (player6.Hitbox.X > 1000 && player6.Hitbox.X < 1300 && MediaPlayer.Volume > 0f)
                        MediaPlayer.Volume = MediaPlayer.Volume - 0.01f;

                    if (player6.Hitbox.X > 1300 && MediaPlayer.Volume < 1f)
                        MediaPlayer.Volume = MediaPlayer.Volume + 0.01f;

                    Global.Collisions.CollisionHealthBonus(player6.Hitbox, Main6.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player6.Hitbox, Main6.speedbonus, gameTime);
                    Global.Collisions.CollisionBoss(player6.Hitbox, Global.GameMain6.boss);

                    if (Global.Collisions.CollisionBonus(player6.Hitbox, Main6.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
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

                    UpdateGame(player6, gameTime, 6, 4600, 300);
                    break;
                case GameState.InGameMulti:
                    comptlevel = 7;
                    //p7.Update(Mouse.GetState(), Keyboard.GetState(), MainMulti.Walls, MainMulti.bonus);
                    //p8.Update(Mouse.GetState(), Keyboard.GetState(), MainMulti.Walls, MainMulti.bonus);
                    MainMulti.Update(Mouse.GetState(), Keyboard.GetState());

                    if (MainMulti.player.Hitbox.X > 4600)
                    {
                        gameState = GameState.player1win;
                        MainMulti.client.Close();
                        MainMulti.player.Hitbox = new Rectangle(50, 376, MainMulti.player.Hitbox.Width, MainMulti.player.Hitbox.Height);
                    }

                    if (MainMulti.player2.Hitbox.X > 4600)
                    {
                        gameState = GameState.player2win;
                        MainMulti.client.Close();
                        MainMulti.player.Hitbox = new Rectangle(50, 376, MainMulti.player.Hitbox.Width, MainMulti.player.Hitbox.Height);
                    }

                    if (Global.Collisions.CollisionBonus(p7.Hitbox, MainMulti.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    if (Global.Collisions.CollisionEnemy(p7.Hitbox, MainMulti.enemies))
                    {
                    }
                    if (Global.Collisions.CollisionEnemy2(p7.Hitbox, MainMulti.enemies2))
                    {
                    }
                    /*if (Global.Collisions.CollisionPiques(p7.Hitbox, Main2.piques))
                      {
                      }*/
                    UpdateGame(p7, gameTime, 2, 4600, 30);
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

                    if (comptlevel == 1)
                    {
                        HS1 = score;
                        if (HS1 >= old_HS1)
                            old_HS1 = HS1;
                    }
                    if (comptlevel == 2)
                    {
                        HS2 = score;
                        if (HS2 >= old_HS2)
                            old_HS2 = HS2;
                    }
                    if (comptlevel == 3)
                    {
                        HS3 = score;
                        if (HS3 >= old_HS1)
                            old_HS3 = HS3;
                    }
                    if (comptlevel == 4)
                    {
                        HS4 = score;
                        if (HS4 >= old_HS4)
                            old_HS4 = HS4;
                    }
                    if (comptlevel == 5)
                    {
                        HS5 = score;
                        if (HS5 >= old_HS5)
                            old_HS5 = HS5;
                    }
                    if (comptlevel == 6)
                    {
                        HS6 = score;
                        if (HS6 >= old_HS6)
                            old_HS6 = HS6;
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
                        else if (comptlevel == 6)
                            gameState = GameState.InGame6;
                        else
                            gameState = GameState.InGameMulti;
                    }
                    pastkey = Keyboard.GetState();
                    foreach (GUIElement element in InPause)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.HowToPlay:
                    foreach (GUIElement element in HowToPlay)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.SelectionMap:
                    foreach (GUIElement element in SelectionMap)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Credits:
                    foreach (GUIElement element in Credits)
                        element.Update();
                    TextScroll -= 0.6f;
                    enjeu = false;
                    break;
                case GameState.Chapitre1:
                    foreach (GUIElement element in Chapitre1)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Chapitre2:
                    foreach (GUIElement element in Chapitre2)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Chapitre3:
                    foreach (GUIElement element in Chapitre3)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Chapitre4:
                    foreach (GUIElement element in Chapitre4)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Chapitre5:
                    foreach (GUIElement element in Chapitre5)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Chapitre6:
                    foreach (GUIElement element in Chapitre6)
                        element.Update();
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
                            colourScenario.R = 0;
                            colourScenario.G = 0;
                            colourScenario.B = 0;
                        }
                    }
                    if (!downcolor && colourScenario.R < 254)
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
                        CreateGame(1);
                    }
                    break;
                case GameState.InClose:
                    Global.Handler.Exit();
                    break;
                case GameState.Multi:
                    foreach (GUIElement element in Multi)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.Shop:
                    foreach (GUIElement element in Shop)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.player1win:
                    foreach (GUIElement element in player1win)
                        element.Update();
                    enjeu = false;
                    break;
                case GameState.player2win:
                    foreach (GUIElement element in player2win)
                        element.Update();
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
                        element.Draw(spriteBatch);
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
                case GameState.InGameMulti:
                    MainMulti.Draw(spriteBatch);
                    break;
                case GameState.InOptions:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);

                    foreach (GUIElement element in InOptions)
                        element.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(fond_gameover, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in GameOver)
                        element.Draw(spriteBatch);
                    break;
                case GameState.Won:
                    spriteBatch.Draw(fond_win, new Rectangle(0, 0, 800, 480), Color.White);
                    spriteBatch.Draw(coin, new Rectangle(350, 250, 25, 25), Color.White);
                    spriteBatch.Draw(zombie, new Rectangle(355, 280, 20, 30), Color.White);
                    spriteBatch.Draw(deathhead, new Rectangle(350, 310, 30, 30), Color.White);
                    foreach (GUIElement element in Won)
                        element.Draw(spriteBatch);
                    spriteBatch.Draw(Resources.MarcoWon, new Rectangle(400, 160, 38, 43), new Rectangle((framecolumn - 1) * 38, 0, 38, 43), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    spriteBatch.DrawString(nb_enemies_killed, "x " + enemies_dead, new Vector2(Global.Handler.Window.ClientBounds.Width / 2, 280), Color.White);
                    spriteBatch.DrawString(players_dead, "x " + nb_players_dead, new Vector2(Global.Handler.Window.ClientBounds.Width / 2, 310), Color.White);
                    break;
                case GameState.InPause:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in InPause)
                        element.Draw(spriteBatch);
                    break;
                case GameState.HowToPlay:
                    spriteBatch.Draw(fond_how2play, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in HowToPlay)
                        element.Draw(spriteBatch);
                    break;
                case GameState.SelectionMap:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in SelectionMap)
                        element.Draw(spriteBatch);
                    break;
                case GameState.Credits:
                    foreach (GUIElement element in Credits)
                        element.Draw(spriteBatch);

                    Global.Handler.GraphicsDevice.Clear(Color.Black);

                    #region Crédits

                    string message = "\n   A game created and directed by      " +
                                     "\n         The Walking Coders          \n" +
                                     "\n             Producers                 " +
                                     "\n         The Walking Coders          \n" +
                                     "\n         Artistic Director             " +
                                     "\n              Didule                   " +
                                     "\n              Iksame                 \n" +
                                     "\n   Animation Lead Artistic Director    " +
                                     "\n              Alezco                   " +
                                     "\n              Yaumy                    " +
                                     "\n              Iksame                 \n" +
                                     "\n       Audio Artistic Director         " +
                                     "\n             WhiteDevil              \n" +
                                     "\n         Lead Game Designer            " +
                                     "\n               Alezco                  " +
                                     "\n               Yaumy                   " +
                                     "\n               Iksame                \n" +
                                     "\n  Lead Architect and Gameplay Designer " +
                                     "\n              ArrenKae                 " +
                                     "\n               Yaumy                   " +
                                     "\n               Alezco                  " +
                                     "\n             WhiteDevil                " +
                                     "\n               Iksame                \n" +
                                     "\n          Online Programmers           " +
                                     "\n              ArrenKae                 " +
                                     "\n               Yaumy                   " +
                                     "\n               Alezco                \n" +
                                     "\n     Level Design Director Closing     " +
                                     "\n               Yaumy                   " +
                                     "\n               Alezco                \n" +
                                     "\n     Art Graphic Technical Director    " +
                                     "\n               Didule                  " +
                                     "\n                Ixam                 \n" +
                                     "\n   Lead Sound Audio Artistic Director  " +
                                     "\n             WhiteDevil              \n" +
                                     "\n        Additional Sound Design        " +
                                     "\n             WhiteDevil              \n" +
                                     "\n Script Artistic Lead Creating Producer" +
                                     "\n              GamerDuPC                " +
                                     "\n               Alezco                \n" +
                                     "\n     MainMenu.cs Dirty Code Creator    " +
                                     "\n              WhiteDevil             \n" +
                                     "\n   Artificial Intelligence Specialist  " +
                                     "\n                Yaumy                  " +
                                     "\n              WhiteDevil             \n" +
                                     "\n     Cruel and Heartless Dictator      " +
                                     "\n               Alezco                \n" +
                                     "\n            Beta Testers               " +
                                     "\n              ArrenKae                 " +
                                     "\n               Yaumy                   " +
                                     "\n               Alezco                  " +
                                     "\n             WhiteDevil                " +
                                     "\n               Iksame                  " +
                                     "\n               Charkk                  " +
                                     "\n              GamerduPC                " +
                                     "\n               Jorcau                  " +
                                     "\n               Didule                  " +
                                     "\n               Pouale                  " +
                                     "\n               Drifer                  " +
                                     "\n             Nemsadomaso               " +
                                     "\n              DarkPrime                " +
                                     "\n              Mickelbaz                " +
                                     "\n               Denwau                  " +
                                     "\n              Jujubingo                " +
                                     "\n               Awakee                  " +
                                     "\n               J0bba                   " +
                                     "\n             AlexBarry                 " +
                                     "\n              Kleubeur                 " +
                                     "\n                 Ed                    " +
                                     "\n               Kakarl                  " +
                                     "\n                Mama                   " +
                                     "\n             SebLaMouche             \n" +
                                     "\n      Amazing OP Fantastic Great       " +
                                     "\n      Incredible Perfect Precise       " +
                                     "\n     Collisions Lead Game Physics      " +
                                     "\n     Specialist Creator Technical      " +
                                     "\n    Producing Designing Programmer     " +
                                     "\n                Yaumy            \n\n\n" +
                                     "\n         Thank you for playing         ";

                    spriteBatch.DrawString(nb_enemies_killed, message, new Vector2(Global.Handler.Window.ClientBounds.Width / 2 - 210, TextScroll), Color.White);

                    #endregion
                    break;

                #region Highscores
                case GameState.Chapitre1:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre1)
                        element.Draw(spriteBatch);
                    if (old_HS1 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS1, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    break;
                case GameState.Chapitre2:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre2)
                        element.Draw(spriteBatch);
                    if (old_HS2 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS2, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 1) spriteBatch.Draw(Resources.lvl2_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre3:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre3)
                        element.Draw(spriteBatch);
                    if (old_HS3 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS3, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 2) spriteBatch.Draw(Resources.lvl3_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre4:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre4)
                        element.Draw(spriteBatch);
                    if (old_HS4 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS4, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 3) spriteBatch.Draw(Resources.lvl4_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre5:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre5)
                        element.Draw(spriteBatch);
                    if (old_HS5 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS5, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 4) spriteBatch.Draw(Resources.lvl5_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre6:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre6)
                        element.Draw(spriteBatch);
                    if (old_HS6 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS6, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 5) spriteBatch.Draw(Resources.lvl6_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                #endregion

                case GameState.Multi:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Multi)
                        element.Draw(spriteBatch);
                    break;
                case GameState.player1win:
                    spriteBatch.Draw(fond_win, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in player1win)
                        element.Draw(spriteBatch);
                    break;
                case GameState.player2win:
                    spriteBatch.Draw(fond_gameover, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in player2win)
                        element.Draw(spriteBatch);
                    break;
                case GameState.Scenario:
                    if (colourScenario.R == 255) spriteBatch.Draw(Resources.Scenario, new Rectangle(0, 0, 800, 480), Color.Black);
                    else
                        spriteBatch.Draw(Resources.Scenario, new Rectangle(0, 0, 800, 480), colourScenario);
                    break;
                case GameState.Shop:
                    spriteBatch.Draw(fond_menu, new Rectangle(0, 0, 800, 480), Color.White);
                    spriteBatch.DrawString(piece_font, " x " + total_piece, new Vector2(200, 0), Color.White);
                    foreach (GUIElement element in Shop)
                        element.Draw(spriteBatch);
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
                gameState = GameState.SelectionMap;

            if (element == @"Sprites\Menu\Button_options")
                gameState = GameState.InOptions;

            if (element == @"Sprites\Menu\Button_quitter")
                gameState = GameState.InClose;

            if (element == @"Sprites\Menu\Bouton_Retour")
            {
                if (HasPlayed == true)
                    gameState = GameState.InPause;
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
                CreateGame(comptlevel);

            if (element == @"Sprites\Menu\Bouton_MenuPrincipal" || element == @"Sprites\Menu\Bouton_MenuPrincipalGros")
            {
                gameState = GameState.MainMenu;
                HasPlayed = false;
                MediaPlayer.Volume = 0.6f;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMenu);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Bouton_Commandes")
                gameState = GameState.HowToPlay;

            if (element == @"Sprites\Menu\Bouton_NouvellePartie")
                gameState = GameState.Scenario;

            if (element == @"Sprites\Menu\Boutton_Rejouer")
            {
                total_piece_updated = false;
                if (!has_Lost)
                    nb_players_dead = 0;
                CreateGame(comptlevel);

            }

            if (element == @"Sprites\Menu\Bouton_Credits")
            {
                gameState = GameState.Credits;
                TextScroll = 100f;
            }

            if (element == @"Sprites\Menu\Bouton_RetourToOptions")
                gameState = GameState.InOptions;

            if (element == @"Sprites\Menu\Bouton_RetourToJouer")
                gameState = GameState.SelectionMap;

            if (element == @"Sprites\Menu\Bouton_Chapitres")
                gameState = GameState.Chapitre1;

            if (element == @"Sprites\Menu\Level1")
                CreateGame(1);

            if ((element == @"Sprites\Menu\Level2" || element == @"Sprites\Menu\Level2_block") && lvlcomplete >= 1) //retire deuxieme condition pour les tests
                CreateGame(2);

            if (element == @"Sprites\Menu\Level3" && lvlcomplete >= 2)
                CreateGame(3);

            if (element == @"Sprites\Menu\Level4" && lvlcomplete >= 3)
                CreateGame(4);

            if (element == @"Sprites\Menu\Level5" && lvlcomplete >= 4)
                CreateGame(5);

            if (element == @"Sprites\Menu\Level6" && lvlcomplete >= 5)
                CreateGame(6);

            if (element == @"Sprites\Menu\Bouton_NiveauSuivant")
            {
                CreateGame(comptlevel + 1);
                nb_players_dead = 0;
            }
            if (element == @"Sprites\Menu\Bouton_PleinEcran")
                Global.Handler.graphics.ToggleFullScreen();

            if (element == @"Sprites\Menu\fleche_droite")
                gameState = GameState.Chapitre2;

            if (element == @"Sprites\Menu\fleche_droite2")
                gameState = GameState.Chapitre3;

            if (element == @"Sprites\Menu\fleche_droite3")
                gameState = GameState.Chapitre4;

            if (element == @"Sprites\Menu\fleche_droite4")
                gameState = GameState.Chapitre5;

            if (element == @"Sprites\Menu\fleche_droite5")
                gameState = GameState.Chapitre6;

            if (element == @"Sprites\Menu\fleche_gauche")
                gameState = GameState.Chapitre1;

            if (element == @"Sprites\Menu\fleche_gauche2")
                gameState = GameState.Chapitre2;

            if (element == @"Sprites\Menu\fleche_gauche3")
                gameState = GameState.Chapitre3;

            if (element == @"Sprites\Menu\fleche_gauche4")
                gameState = GameState.Chapitre4;

            if (element == @"Sprites\Menu\fleche_gauche5")
                gameState = GameState.Chapitre5;

            if (element == @"Sprites\Menu\Bouton_Multijoueur")
            {
                CreateGame(7);
                MainMulti.Initialize();
            }

            if (element == @"Sprites\Menu\Bouton_Boutique" && !total_piece_updated)
            {
                total_piece = nb_pieces + total_piece;
                gameState = GameState.Shop;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueBoutique);
                MediaPlayer.IsRepeating = true;
                total_piece_updated = true;
            }

            if (element == @"Sprites\Menu\Bouton_RetourToHasWon")
            {
                gameState = GameState.Won;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueVictory);
                MediaPlayer.IsRepeating = false;
            }
        }
        #endregion
    }
}