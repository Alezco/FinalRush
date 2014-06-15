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
using System.Diagnostics;

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
            Intro, Scenario,
            GameOver,
            Multi,
            ChooseIp,
            player1win,
            player2win
        }

        public bool english;
        public GameState gameState;
        GameMain Main;
        SoundEffectInstance Boss_dead_instance;
        GameMain2 Main2;
        GameMain3 Main3;
        GameMain4 Main4;
        GameMain5 Main5;
        GameMain6 Main6;
        float volume_sons;
        GameMainMulti MainMulti;
        SpriteFont piece_font, players_dead;
        public int HS1, HS2, HS3, HS4, HS5, HS6 = 0;
        int old_HS1, old_HS2, old_HS3, old_HS4, old_HS5, old_HS6 = 0;
        List<Enemy> enemies;
        List<Enemy2> enemies2;
        bool shift_pressed = false;
        public Player player, player2, player3, player4, player5, player6, p7, p8;
        bool HasPlayed;
        public bool boss_already_appeared;
        public bool boss_appeared;
        public int comptlevel = 0;
        public int total_piece = 0;
        public int nb_players_dead = 0;
        int x = 0;
        int compt = 0;
        int compt2 = 0;
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
        Texture2D fond, fond_menu1, fond_menu2, fond_menu3, fond_menu4, fond_menu5, fond_menu6, fond_win, fond_gameover, fond_how2play;

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
        private Keys[] lastPressedKeys = new Keys[20];
        public string ip;
        public List<char> ip_list = new List<char>();


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
        List<GUIElement> GameEnded = new List<GUIElement>();
        List<GUIElement> ChooseIp = new List<GUIElement>();

        #endregion

        #region Constructeur (Add)

        public MainMenu(Game1 game, float startTime)
            : base(game)
        {
            // Constructeur:
            volume_sons = 0.6f;
            english = false;
            time = startTime;
            started = false;
            paused = false;
            finished = false;
            boss_appeared = false;
            has_Lost = false;
            boss_already_appeared = false;
            Boss_dead_instance = Resources.boss_mort_sound.CreateInstance();
            total_piece_updated = false;
            Text = "0";
            nb_pieces = 0;
            mort_enemies = Resources.enemies_sound.CreateInstance();
            piece_font = Resources.piece_font;
            piece_text = " 0";

            //Ajout des boutons nécessaires

            Intro.Add(new GUIElement(@"Sprites\Menu\Intro\Intro2_Fond"));
            Intro.Add(new GUIElement(@"Sprites\Menu\Intro\Intro_Fond"));


            main.Add(new GUIElement(@"Sprites\Menu\English\Bouton_jouer"));
            main.Add(new GUIElement(@"Sprites\Menu\English\Bouton_quitter"));
            main.Add(new GUIElement(@"Sprites\Menu\Francais\Button_jouer"));
            main.Add(new GUIElement(@"Sprites\Menu\Francais\Button_options"));
            main.Add(new GUIElement(@"Sprites\Menu\Francais\Button_quitter"));
            main.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Credits"));

            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Bruitages"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Retour"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_PleinEcran"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Commandes"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_French"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Retour"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Volume"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Plus"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Moins"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Bruitages"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Plus2"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Moins2"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Commandes"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_PleinEcran"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Anglais"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Website"));
            InOptions.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Website"));

            InPause.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Continuer"));
            InPause.Add(new GUIElement(@"Sprites\Menu\English\Bouton_quitter"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Continuer"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Francais\Button_options"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipalGros"));
            InPause.Add(new GUIElement(@"Sprites\Menu\Francais\Button_quitter"));

            HowToPlay.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToOptions"));

            SelectionMap.Add(new GUIElement(@"Sprites\Menu\English\Bouton_NouvellePartie"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Multijoueur"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Retour"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Chapitres"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_NouvellePartie"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Multijoueur"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Retour"));
            SelectionMap.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Chapitres"));

            GameOver.Add(new GUIElement(@"Sprites\Menu\Francais\Boutton_Rejouer"));
            GameOver.Add(new GUIElement(@"Sprites\Menu\English\Boutton_Rejouer"));
            GameOver.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipal"));

            Credits.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Retour"));
            Credits.Add(new GUIElement(@"Sprites\Menu\English\Bouton_Retour"));

            Won.Add(new GUIElement(@"Sprites\Menu\English\Bouton_NiveauSuivant"));
            Won.Add(new GUIElement(@"Sprites\Menu\English\Boutton_Rejouer"));
            Won.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_NiveauSuivant"));
            Won.Add(new GUIElement(@"Sprites\Menu\Francais\Boutton_Rejouer"));
            Won.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipal"));

            GameEnded.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Credits"));
            GameEnded.Add(new GUIElement(@"Sprites\Menu\Francais\Boutton_Rejouer"));
            GameEnded.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipal"));
            GameEnded.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_Boutique"));

            Chapitre1.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre1.Add(new GUIElement(@"Sprites\Menu\Francais\Level1"));
            Chapitre1.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_droite"));
            Chapitre1.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Francais\Level2"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_droite2"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_gauche"));
            Chapitre2.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Francais\Level3"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_droite3"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_gauche2"));
            Chapitre3.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Francais\Level4"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_droite4"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_gauche3"));
            Chapitre4.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Francais\Level5"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_droite5"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_gauche4"));
            Chapitre5.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            Chapitre6.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            Chapitre6.Add(new GUIElement(@"Sprites\Menu\Francais\Level6"));
            Chapitre6.Add(new GUIElement(@"Sprites\Menu\Francais\fleche_gauche5"));
            Chapitre6.Add(new GUIElement(@"Sprites\Menu\English\Bouton_RetourToJouer"));

            player1win.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipal"));
            player2win.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_MenuPrincipal"));

            ChooseIp.Add(new GUIElement(@"Sprites\Menu\Francais\Bouton_RetourToJouer"));
            ChooseIp.Add(new GUIElement(@"Sprites\Menu\Francais\EnterIp"));
            ChooseIp.Add(new GUIElement(@"Sprites\Menu\Francais\Ok"));

            ip = "";
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
            fond_how2play = content.Load<Texture2D>(@"Sprites\Menu\Francais\HowToPlayPage1");
            fond_menu1 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond1");
            fond_menu2 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond2");
            fond_menu3 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond3");
            fond_menu4 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond4");
            fond_menu5 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond5");
            fond_menu6 = content.Load<Texture2D>(@"Sprites\Menu\Francais\Menu_fond6");
            fond_win = content.Load<Texture2D>(@"Sprites\Menu\Francais\Won");
            fond_gameover = content.Load<Texture2D>(@"Sprites\Menu\Francais\GameOver");
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
            if (!english)
            {
                main.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_jouer").MoveElement(0, 800);
                main.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_quitter").MoveElement(0, 800);
                main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_quitter").MoveElement(0, 200);
                main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_jouer").MoveElement(0, -30);
            }
            else
            {
                main.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_jouer").MoveElement(0, -30);
                main.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_quitter").MoveElement(0, 200);
                main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_quitter").MoveElement(0, 800);
                main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_jouer").MoveElement(0, 800);
            }
            main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_options").MoveElement(0, 50);
            main.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Credits").MoveElement(0, 120);

            // Options

            //De même pour l'option

            foreach (GUIElement element in InOptions)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Bruitages").MoveElement(-200, 15);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Commandes").MoveElement(-200, 80);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_PleinEcran").MoveElement(-200, 145);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(-200, 210);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Website").MoveElement(225, 80);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Anglais").MoveElement(225, 145);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Bruitages").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Commandes").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_PleinEcran").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_French").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Website").MoveElement(0, 800);

            }
            else
            {
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Website").MoveElement(225, 80);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Bruitages").MoveElement(-200, 15);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Commandes").MoveElement(-200, 80);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_PleinEcran").MoveElement(-200, 145);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(-200, 210);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_French").MoveElement(225, 145);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Bruitages").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Commandes").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_PleinEcran").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Anglais").MoveElement(0, 800);
                InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Website").MoveElement(0, 800);
            }
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Volume").MoveElement(-200, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Plus").MoveElement(70, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Moins").MoveElement(0, -50);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Plus2").MoveElement(70, 15);
            InOptions.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Moins2").MoveElement(0, 15);


            foreach (GUIElement element in ChooseIp)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            ChooseIp.Find(x => x.AssetName == @"Sprites\Menu\Francais\EnterIp").CenterElement(400, 800);
            ChooseIp.Find(x => x.AssetName == @"Sprites\Menu\Francais\Ok").CenterElement(550, 800);
            ChooseIp.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-200, 210);

            // De même pour InPause

            foreach (GUIElement element in InPause)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Continuer").MoveElement(0, -50);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_quitter").MoveElement(0, 190);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Continuer").MoveElement(0, 800);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_quitter").MoveElement(0, 800);
            }
            else
            {
                InPause.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Continuer").MoveElement(0, -50);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_quitter").MoveElement(0, 190);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Continuer").MoveElement(0, 800);
                InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_quitter").MoveElement(0, 800);
            }
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Button_options").MoveElement(0, 30);
            InPause.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipalGros").MoveElement(0, 110);

            // De même pour GameOver

            foreach (GUIElement element in GameOver)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                GameOver.Find(x => x.AssetName == @"Sprites\Menu\Francais\Boutton_Rejouer").MoveElement(-200, 210);
                GameOver.Find(x => x.AssetName == @"Sprites\Menu\English\Boutton_Rejouer").MoveElement(0, 800);
            }
            else
            {
                GameOver.Find(x => x.AssetName == @"Sprites\Menu\English\Boutton_Rejouer").MoveElement(-200, 210);
                GameOver.Find(x => x.AssetName == @"Sprites\Menu\Francais\Boutton_Rejouer").MoveElement(0, 800);
            }
            GameOver.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipal").MoveElement(200, 210);

            // De même pour Commandes & Tips

            foreach (GUIElement element in HowToPlay)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            HowToPlay.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToOptions").MoveElement(-260, 210);

            foreach (GUIElement element in SelectionMap)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }

            if (!english)
            {
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_NouvellePartie").MoveElement(0, -20);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Multijoueur").MoveElement(0, 50);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(0, 200);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Chapitres").MoveElement(0, 120);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_NouvellePartie").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Multijoueur").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Chapitres").MoveElement(0, 800);
            }
            else
            {
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_NouvellePartie").MoveElement(0, -20);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Multijoueur").MoveElement(0, 50);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(0, 200);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Chapitres").MoveElement(0, 120);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_NouvellePartie").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Multijoueur").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(0, 800);
                SelectionMap.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Chapitres").MoveElement(0, 800);
            }
            foreach (GUIElement element in Credits)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Credits.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(0, 200);
                Credits.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(0, 800);
            }
            else
            {
                Credits.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_Retour").MoveElement(0, 200);
                Credits.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Retour").MoveElement(0, 800);
            }

            foreach (GUIElement element in Won)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Won.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_NiveauSuivant").MoveElement(260, 210);
                Won.Find(x => x.AssetName == @"Sprites\Menu\Francais\Boutton_Rejouer").MoveElement(-60, 210);
                Won.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_NiveauSuivant").MoveElement(0, 800);
                Won.Find(x => x.AssetName == @"Sprites\Menu\English\Boutton_Rejouer").MoveElement(0, 800);
            }
            else
            {
                Won.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_NiveauSuivant").MoveElement(260, 210);
                Won.Find(x => x.AssetName == @"Sprites\Menu\English\Boutton_Rejouer").MoveElement(-60, 210);
                Won.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_NiveauSuivant").MoveElement(0, 800);
                Won.Find(x => x.AssetName == @"Sprites\Menu\Francais\Boutton_Rejouer").MoveElement(0, 800);
            }
            Won.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipal").MoveElement(-320, 210);

            foreach (GUIElement element in GameEnded)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            GameEnded.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipal").MoveElement(-320, 210);
            GameEnded.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Credits").MoveElement(260, 210);
            GameEnded.Find(x => x.AssetName == @"Sprites\Menu\Francais\Boutton_Rejouer").MoveElement(-60, 210);
            GameEnded.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_Boutique").MoveElement(240, 160);

            foreach (GUIElement element in Chapitre1)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level1").MoveElement(0, 50);
            Chapitre1.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_droite").MoveElement(50, 200);

            foreach (GUIElement element in Chapitre2)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level2").MoveElement(0, 50);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_droite2").MoveElement(50, 200);
            Chapitre2.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_gauche").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre3)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level3").MoveElement(0, 50);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_droite3").MoveElement(50, 200);
            Chapitre3.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_gauche2").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre4)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level4").MoveElement(0, 50);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_droite4").MoveElement(50, 200);
            Chapitre4.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_gauche3").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre5)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level5").MoveElement(0, 50);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_droite5").MoveElement(50, 200);
            Chapitre5.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_gauche4").MoveElement(-50, 200);

            foreach (GUIElement element in Chapitre6)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            if (!english)
            {
                Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            else
            {
                Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\English\Bouton_RetourToJouer").MoveElement(-300, 200);
                Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_RetourToJouer").MoveElement(0, 800);
            }
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Francais\Level6").MoveElement(0, 50);
            Chapitre6.Find(x => x.AssetName == @"Sprites\Menu\Francais\fleche_gauche5").MoveElement(-50, 200);

            foreach (GUIElement element in player1win)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            player1win.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipal").MoveElement(0, 210);

            foreach (GUIElement element in player2win)
            {
                element.LoadContent(content);
                element.CenterElement(480, 800);
                element.clickEvent += OnClick;
            }
            player2win.Find(x => x.AssetName == @"Sprites\Menu\Francais\Bouton_MenuPrincipal").MoveElement(0, 210);
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
            Global.Handler.recharge_left = 30;
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
                    MainMulti = new GameMainMulti(ip);
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
            if (player.health == 0 || player.dead && gameState != GameState.InGameMulti)
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
            switch (lvlcomplete)
            {
                default:
                    fond = fond_menu1;
                    break;
                case 1:
                    fond = fond_menu2;
                    break;
                case 2:
                    fond = fond_menu3;
                    break;
                case 3:
                    fond = fond_menu4;
                    break;
                case 4:
                    fond = fond_menu5;
                    break;
                case 5:
                    fond = fond_menu6;
                    break;
            }

            if (lvlcomplete > 5)
            {
                if (compt2 == 100)
                {
                    x++;
                    if (x > 6)
                        x = 1;
                    compt2 = 0;
                }
                compt2++;
                switch (x)
                {
                    case 1:
                        fond = fond_menu1;
                        break;
                    case 2:
                        fond = fond_menu2;
                        break;
                    case 3:
                        fond = fond_menu3;
                        break;
                    case 4:
                        fond = fond_menu4;
                        break;
                    case 5:
                        fond = fond_menu5;
                        break;
                    case 6:
                        fond = fond_menu6;
                        break;
                }
            }

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
                case GameState.ChooseIp:
                    foreach (GUIElement element in ChooseIp)
                    {
                        element.Update();
                    }
                    GetKeys();
                    break;
                case GameState.InGame:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 1;
                    Main.Update(Mouse.GetState(), Keyboard.GetState());
                    player.Update(Mouse.GetState(), Keyboard.GetState(), Main.Walls, Main.bonus);
                    Global.Collisions.CollisionEnemy(player.Hitbox, Main.enemies);
                    Global.Collisions.CollisionEnemy2(player.Hitbox, Main.enemies2);
                    Global.Collisions.CollisionHealthBonus(player.Hitbox, Main.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player.Hitbox, Main.speedbonus, gameTime);

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

                    if (Global.Collisions.CollisionBonus(player.Hitbox, Main.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player, gameTime, 1, 4600, 300);
                    break;
                case GameState.InGame2:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 2;
                    Main2.Update(Mouse.GetState(), Keyboard.GetState());
                    player2.Update(Mouse.GetState(), Keyboard.GetState(), Main2.Walls, Main2.bonus);
                    Global.Collisions.CollisionEnemy(player2.Hitbox, Main2.enemies);
                    Global.Collisions.CollisionEnemy2(player2.Hitbox, Main2.enemies2);
                    Global.Collisions.CollisionHealthBonus(player2.Hitbox, Main2.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player2.Hitbox, Main2.speedbonus, gameTime);
                    Global.Collisions.CollisionLow(player2.Hitbox, Main2.lowspeedarea);  

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

                    if (Global.Collisions.CollisionBonus(player2.Hitbox, Main2.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player2, gameTime, 2, 4600, 300);
                    break;
                case GameState.InGame3:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 3;
                    Main3.Update(Mouse.GetState(), Keyboard.GetState());
                    player3.Update(Mouse.GetState(), Keyboard.GetState(), Main3.Walls, Main3.bonus);
                    Global.Collisions.CollisionEnemy(player3.Hitbox, Main3.enemies);
                    Global.Collisions.CollisionEnemy2(player3.Hitbox, Main3.enemies2);
                    Global.Collisions.CollisionPiques(player3.Hitbox, Main3.piques);
                    Global.Collisions.CollisionHealthBonus(player3.Hitbox, Main3.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player3.Hitbox, Main3.speedbonus, gameTime);

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

                    if (Global.Collisions.CollisionBonus(player3.Hitbox, Main3.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player3, gameTime, 3, 4600, 300);
                    break;
                case GameState.InGame4:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 4;
                    Main4.Update(Mouse.GetState(), Keyboard.GetState());
                    player4.Update(Mouse.GetState(), Keyboard.GetState(), Main4.Walls, Main4.bonus);                   //bonus
                    Global.Collisions.CollisionEnemy(player4.Hitbox, Main4.enemies);
                    Global.Collisions.CollisionEnemy2(player4.Hitbox, Main4.enemies2);
                    Global.Collisions.CollisionHealthBonus(player4.Hitbox, Main4.healthbonus);           //healthbonus
                    Global.Collisions.CollisionSpeedBonus(player4.Hitbox, Main4.speedbonus, gameTime);   //speedbonus 
                    Global.Collisions.CollisionLow(player4.Hitbox, Main4.lowspeedarea);                  //zone ralentie

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

                    if (Global.Collisions.CollisionBonus(player4.Hitbox, Main4.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player4, gameTime, 4, 4600, 300);
                    break;
                case GameState.InGame5:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 5;
                    Main5.Update(Mouse.GetState(), Keyboard.GetState());
                    player5.Update(Mouse.GetState(), Keyboard.GetState(), Main5.Walls, Main5.bonus);
                    Global.Collisions.CollisionEnemy(player5.Hitbox, Main5.enemies);
                    Global.Collisions.CollisionEnemy2(player5.Hitbox, Main5.enemies2);
                    Global.Collisions.CollisionHealthBonus(player5.Hitbox, Main5.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player5.Hitbox, Main5.speedbonus, gameTime);

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

                    if (Global.Collisions.CollisionBonus(player5.Hitbox, Main5.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player5, gameTime, 5, 4600, 300);
                    break;
                case GameState.InGame6:
                    MediaPlayer.Volume = volume_sons;
                    comptlevel = 6;
                    Main6.Update(Mouse.GetState(), Keyboard.GetState());
                    player6.Update(Mouse.GetState(), Keyboard.GetState(), Main6.Walls, Main6.bonus);
                    Global.Collisions.CollisionEnemy(player6.Hitbox, Main6.enemies);
                    Global.Collisions.CollisionEnemy2(player6.Hitbox, Main6.enemies2);
                    Global.Collisions.CollisionHealthBonus(player6.Hitbox, Main6.healthbonus);
                    Global.Collisions.CollisionSpeedBonus(player6.Hitbox, Main6.speedbonus, gameTime);
                    Global.Collisions.CollisionBoss(player6.Hitbox, Global.GameMain6.boss);
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

                    for (int i = 0; i < Global.GameMain6.boss.Count(); i++)
                    {
                        if (Global.GameMain6.boss[i].isDead)
                        {
                            Boss_dead_instance.Play();
                            Global.GameMain6.boss.RemoveAt(i);
                        }

                    }

                    if (player6.Hitbox.X > 1000 && player6.Hitbox.X < 1300 && MediaPlayer.Volume > 0f)
                        MediaPlayer.Volume = MediaPlayer.Volume - 0.01f;

                    if (player6.Hitbox.X > 1300 && MediaPlayer.Volume < 1f)
                        MediaPlayer.Volume = MediaPlayer.Volume + 0.01f;

                    if (Global.Collisions.CollisionBonus(player6.Hitbox, Main6.bonus))
                    {
                        nb_pieces++;
                        SoundEffectInstance piece_sound_instance = Resources.piece.CreateInstance();
                        piece_sound_instance.Play();
                    }

                    UpdateGame(player6, gameTime, 6, 4600, 300);
                    break;
                case GameState.InGameMulti:
                    comptlevel = 7;
                    MainMulti.Update(Mouse.GetState(), Keyboard.GetState());
                    Global.Collisions.CollisionSpeedBonus(player6.Hitbox, MainMulti.speedbonus, gameTime);
                    Global.GameMainMulti.IP = this.ip;

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
                        {
                            gameState = GameState.Credits;
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Resources.MusiqueBoutique);
                        }
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
                    HasPlayed = false;
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
                    if (compt < 255)
                        spriteBatch.Draw(Intro_fond, new Rectangle(0, 0, 800, 480), colour);
                    else
                        spriteBatch.Draw(Intro_fond2, new Rectangle(0, 0, 800, 480), Color.White);
                    break;
                case GameState.MainMenu:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
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
                case GameState.ChooseIp:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in ChooseIp)
                        element.Draw(spriteBatch);
                    spriteBatch.DrawString(Resources.ammo_font, ip, new Vector2(330, 180), Color.Black);
                    break;
                case GameState.InOptions:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);

                    foreach (GUIElement element in InOptions)
                        element.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(fond_gameover, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in GameOver)
                        element.Draw(spriteBatch);
                    break;
                case GameState.Won:
                    if (!english)
                        spriteBatch.Draw(fond_win, new Rectangle(0, 0, 800, 480), Color.White);
                    else
                        spriteBatch.Draw(Resources.Won_English, new Rectangle(0, 0, 800, 480), Color.White);
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
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in InPause)
                        element.Draw(spriteBatch);
                    break;
                case GameState.HowToPlay:
                    if (!english)
                        spriteBatch.Draw(fond_how2play, new Rectangle(0, 0, 800, 480), Color.White);
                    else
                        spriteBatch.Draw(Resources.Controls, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in HowToPlay)
                        element.Draw(spriteBatch);
                    break;
                case GameState.SelectionMap:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
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
                                     "\n     MainMenu.cs Longest Code Creator    " +
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
                                     "\n               Fatir                   " +
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
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre1)
                        element.Draw(spriteBatch);
                    if (old_HS1 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS1, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    break;
                case GameState.Chapitre2:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre2)
                        element.Draw(spriteBatch);
                    if (old_HS2 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS2, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 1) spriteBatch.Draw(Resources.lvl2_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre3:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre3)
                        element.Draw(spriteBatch);
                    if (old_HS3 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS3, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 2) spriteBatch.Draw(Resources.lvl3_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre4:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre4)
                        element.Draw(spriteBatch);
                    if (old_HS4 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS4, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 3) spriteBatch.Draw(Resources.lvl4_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre5:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Chapitre5)
                        element.Draw(spriteBatch);
                    if (old_HS5 != 0)
                        spriteBatch.DrawString(Resources.highscores_font, "Highscore: " + old_HS5, new Vector2(Global.Handler.Window.ClientBounds.X + 50, 390), Color.Black);
                    else
                        spriteBatch.DrawString(Resources.highscores_font, "No Highscore", new Vector2(Global.Handler.Window.ClientBounds.X + 40, 390), Color.Black);
                    if (lvlcomplete < 4) spriteBatch.Draw(Resources.lvl5_block, new Rectangle(232, 190, 336, 200), Color.White);
                    break;
                case GameState.Chapitre6:
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
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
                    spriteBatch.Draw(fond, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in Multi)
                        element.Draw(spriteBatch);
                    break;
                case GameState.player1win:
                    if (!english)
                        spriteBatch.Draw(fond_win, new Rectangle(0, 0, 800, 480), Color.White);
                    else
                        spriteBatch.Draw(Resources.Won_English, new Rectangle(0, 0, 800, 480), Color.White);

                    foreach (GUIElement element in player1win)
                        element.Draw(spriteBatch);
                    break;
                case GameState.player2win:
                    if (!english)
                        spriteBatch.Draw(Resources.Defaite, new Rectangle(0, 0, 800, 480), Color.White);
                    else
                        spriteBatch.Draw(Resources.Defeat, new Rectangle(0, 0, 800, 480), Color.White);
                    foreach (GUIElement element in player2win)
                        element.Draw(spriteBatch);
                    break;
                case GameState.Scenario:
                    if (!english)
                    {
                        if (colourScenario.R == 255) spriteBatch.Draw(Resources.Scenario, new Rectangle(0, 0, 800, 480), Color.Black);
                        else
                            spriteBatch.Draw(Resources.Scenario, new Rectangle(0, 0, 800, 480), colourScenario);
                    }
                    else
                    {
                        if (colourScenario.R == 255) spriteBatch.Draw(Resources.English_Scenar, new Rectangle(0, 0, 800, 480), Color.Black);
                        else
                            spriteBatch.Draw(Resources.English_Scenar, new Rectangle(0, 0, 800, 480), colourScenario);
                    }
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
            if (element == @"Sprites\Menu\Francais\Button_jouer" || element == @"Sprites\Menu\English\Bouton_jouer")
                gameState = GameState.SelectionMap;

            if (element == @"Sprites\Menu\Francais\Button_options")
                gameState = GameState.InOptions;

            if (element == @"Sprites\Menu\Francais\Button_quitter" || element == @"Sprites\Menu\English\Bouton_quitter")
                gameState = GameState.InClose;

            if (element == @"Sprites\Menu\Francais\Bouton_Retour" || element == @"Sprites\Menu\English\Bouton_Retour")
            {
                if (HasPlayed == true)
                    gameState = GameState.InPause;
                else
                {
                    if (gameState == GameState.Credits)
                    {
                        MediaPlayer.Stop();
                        MediaPlayer.Play(Resources.MusiqueMenu);
                    }
                    gameState = GameState.MainMenu;
                }
            }

            //Modification du volume

            if (element == @"Sprites\Menu\Francais\Bouton_Plus")
            {
                if (MediaPlayer.Volume >= 0.9f)
                    MediaPlayer.Volume = 1f;
                else MediaPlayer.Volume = MediaPlayer.Volume + 0.1f;

                volume_sons = MediaPlayer.Volume;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_Moins")
            {
                if (MediaPlayer.Volume <= 0.1f)
                    MediaPlayer.Volume = 0f;
                else MediaPlayer.Volume = MediaPlayer.Volume - 0.1f;
                volume_sons = MediaPlayer.Volume;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_Plus2")
            {
                if (SoundEffect.MasterVolume >= 0.9f)
                    SoundEffect.MasterVolume = 1f;
                else SoundEffect.MasterVolume = SoundEffect.MasterVolume + 0.1f;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_Moins2")
            {
                if (SoundEffect.MasterVolume <= 0.1f)
                    SoundEffect.MasterVolume = 0f;
                else SoundEffect.MasterVolume = SoundEffect.MasterVolume - 0.1f;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_Continuer" || element == @"Sprites\Menu\English\Bouton_Continuer")
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
            if (element == @"Sprites\Menu\Francais\Boutton_Rejouer" || element == @"Sprites\Menu\English\Boutton_Rejouer")
                CreateGame(comptlevel);

            if (element == @"Sprites\Menu\Francais\Bouton_MenuPrincipal" || element == @"Sprites\Menu\Francais\Bouton_MenuPrincipalGros")
            {
                gameState = GameState.MainMenu;
                HasPlayed = false;
                MediaPlayer.Volume = 0.6f;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueMenu);
                MediaPlayer.Volume = MediaPlayer.Volume;
                MediaPlayer.IsRepeating = true;
            }

            if (element == @"Sprites\Menu\Francais\Bouton_Commandes" || element == @"Sprites\Menu\English\Bouton_Commandes")
                gameState = GameState.HowToPlay;

            if (element == @"Sprites\Menu\Francais\Bouton_NouvellePartie" || element == @"Sprites\Menu\English\Bouton_NouvellePartie")
                gameState = GameState.Scenario;

            if (element == @"Sprites\Menu\Francais\Boutton_Rejouer" || element == @"Sprites\Menu\English\Boutton_Rejouer")
            {
                total_piece_updated = false;
                if (!has_Lost)
                    nb_players_dead = 0;
                CreateGame(comptlevel);

            }

            if (element == @"Sprites\Menu\Francais\Bouton_Credits")
            {
                gameState = GameState.Credits;
                MediaPlayer.Play(Resources.MusiqueBoutique);
                TextScroll = 100f;
            }

            if (element == @"Sprites\Menu\Francais\Bouton_RetourToOptions")
                gameState = GameState.InOptions;

            if (element == @"Sprites\Menu\Francais\Bouton_RetourToJouer" || element == @"Sprites\Menu\English\Bouton_RetourToJouer")
                gameState = GameState.SelectionMap;

            if (element == @"Sprites\Menu\Francais\Bouton_Chapitres" || element == @"Sprites\Menu\English\Bouton_Chapitres")
                gameState = GameState.Chapitre1;

            if (element == @"Sprites\Menu\Francais\Level1")
                CreateGame(1);

            if ((element == @"Sprites\Menu\Francais\Level2" || element == @"Sprites\Menu\Francais\Level2_block") && lvlcomplete >= 1) //retire deuxieme condition pour les tests
                CreateGame(2);

            if (element == @"Sprites\Menu\Francais\Level3" && lvlcomplete >= 2)
                CreateGame(3);

            if (element == @"Sprites\Menu\Francais\Level4" && lvlcomplete >= 3)
                CreateGame(4);

            if (element == @"Sprites\Menu\Francais\Level5" && lvlcomplete >= 4)
                CreateGame(5);

            if (element == @"Sprites\Menu\Francais\Level6" && lvlcomplete >= 5)
                CreateGame(6);

            if (element == @"Sprites\Menu\Francais\Bouton_NiveauSuivant" || element == @"Sprites\Menu\English\Bouton_NiveauSuivant")
            {
                CreateGame(comptlevel + 1);
                nb_players_dead = 0;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_PleinEcran" || element == @"Sprites\Menu\English\Bouton_PleinEcran")
                Global.Handler.graphics.ToggleFullScreen();

            if (element == @"Sprites\Menu\Francais\fleche_droite")
                gameState = GameState.Chapitre2;

            if (element == @"Sprites\Menu\Francais\fleche_droite2")
                gameState = GameState.Chapitre3;

            if (element == @"Sprites\Menu\Francais\fleche_droite3")
                gameState = GameState.Chapitre4;

            if (element == @"Sprites\Menu\Francais\fleche_droite4")
                gameState = GameState.Chapitre5;

            if (element == @"Sprites\Menu\Francais\fleche_droite5")
                gameState = GameState.Chapitre6;

            if (element == @"Sprites\Menu\Francais\fleche_gauche")
                gameState = GameState.Chapitre1;

            if (element == @"Sprites\Menu\Francais\fleche_gauche2")
                gameState = GameState.Chapitre2;

            if (element == @"Sprites\Menu\Francais\fleche_gauche3")
                gameState = GameState.Chapitre3;

            if (element == @"Sprites\Menu\Francais\fleche_gauche4")
                gameState = GameState.Chapitre4;

            if (element == @"Sprites\Menu\Francais\fleche_gauche5")
                gameState = GameState.Chapitre5;

            if (element == @"Sprites\Menu\Francais\Bouton_Multijoueur" || element == @"Sprites\Menu\English\Bouton_Multijoueur")
            {
                gameState = GameState.ChooseIp;
            }

            if (element == @"Sprites\Menu\Francais\Ok")
            {
                Global.GameMainMulti.IP = ip;
                CreateGame(7);
                MainMulti.Initialize(ip);
            }


            if (element == @"Sprites\Menu\Francais\Bouton_RetourToHasWon")
            {
                gameState = GameState.Won;
                MediaPlayer.Stop();
                MediaPlayer.Play(Resources.MusiqueVictory);
                MediaPlayer.IsRepeating = false;
            }
            if (element == @"Sprites\Menu\Francais\Bouton_Anglais")
            {
                ContentManager content = Global.Handler.Content;
                english = true;
                LoadContent(content);
            }
            if (element == @"Sprites\Menu\English\Bouton_French")
            {
                ContentManager content = Global.Handler.Content;
                english = false;
                LoadContent(content);
            }
            if (element == @"Sprites\Menu\English\Bouton_Website" || element == @"Sprites\Menu\Francais\Bouton_Website")
                Process.Start("http://finalrush.alwaysdata.net/index.php");

        }
        #endregion
        public void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();
            Keys[] pressedKeys = kbState.GetPressedKeys();
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    OnKeyUp(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyDown(key);
                }
            }
            lastPressedKeys = pressedKeys;
        }

        public void OnKeyUp(Keys key)
        {
            if (key == Keys.LeftShift)
                if (Keyboard.GetState().IsKeyUp(Keys.LeftShift))
                    shift_pressed = false;
        }

        public void OnKeyDown(Keys key)
        {
            if (key == Keys.NumPad1)
                ip += "1";

            else if (key == Keys.NumPad2)
                ip += "2";
            else if (key == Keys.NumPad3)
                ip += "3";
            else if (key == Keys.NumPad4)
                ip += "4";
            else if (key == Keys.NumPad5)
                ip += "5";
            else if (key == Keys.NumPad6)
                ip += "6";
            else if (key == Keys.NumPad7)
                ip += "7";
            else if (key == Keys.NumPad8)
                ip += "8";
            else if (key == Keys.NumPad9)
                ip += "9";
            else if (key == Keys.NumPad0)
                ip += "0";
            else if (key == Keys.Decimal)
                ip += ".";
            else if (key == Keys.LeftShift || key == Keys.RightShift)
                shift_pressed = true;

            if (shift_pressed && key == Keys.OemPeriod)
                ip += ".";
            if (shift_pressed && key == Keys.D0)
                ip += "0";
            if (shift_pressed && key == Keys.D1)
                ip += "1";
            if (shift_pressed && key == Keys.D2)
                ip += "2";
            if (shift_pressed && key == Keys.D3)
                ip += "3";
            if (shift_pressed && key == Keys.D4)
                ip += "4";
            if (shift_pressed && key == Keys.D5)
                ip += "5";
            if (shift_pressed && key == Keys.D6)
                ip += "6";
            if (shift_pressed && key == Keys.D7)
                ip += "7";
            if (shift_pressed && key == Keys.D8)
                ip += "8";
            if (shift_pressed && key == Keys.D9)
                ip += "9";
            if (key == Keys.Back && ip.Length > 0)
                ip = ip.Remove(ip.Length - 1);
        }
    }

}