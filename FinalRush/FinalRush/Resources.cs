using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace FinalRush
{
    class Resources
    {
        // STATIC FIELDS

        public static Texture2D Marco, MarcoSaut,MarcoSquat,MarcoTir, MarcoCut;
        public static Texture2D Zombie, Elite;
        public static Texture2D Environnment, Pixel, Herbe, Ground, Platform, Coin, Environnment2, Environnment3, Herbe_neige, Terre_neige, Ice, Ice_top;
        public static Texture2D Menu_fond, Button_jouer, Button_options, Bouton_multijoueur, Button_quitter, Bouton_retour, Bouton_Commandes, HowToPlayPage1, Scenario, GameOver, Boutton_Rejouer, Won, MarcoWon;
        public static Song MusiqueMain, MusiqueMenu, MusiquePause, MusiqueGameOver, MusiqueVictory, MusiqueIntro, Musique2, Musique3;
        public static SoundEffect saut, piece, CoeurRapide, coup_de_feu, ammo_vide, reload_sound, tir_rafale, couteau;
        public static Texture2D HealthBar, bullet;
        public static SpriteFont ammo_font, piece_font;

        // LOAD CONTENT

        public static void LoadContent(ContentManager Content)
        {
            //Personnage

            Marco = Content.Load<Texture2D>(@"Sprites\Hero\Marco");
            MarcoSaut = Content.Load<Texture2D>(@"Sprites\Hero\MarcoVertical");
            MarcoSquat = Content.Load<Texture2D>(@"Sprites\Hero\MarcoSquat");
            MarcoTir = Content.Load<Texture2D>(@"Sprites\Hero\MarcoTir");
            MarcoCut = Content.Load<Texture2D>(@"Sprites\Hero\MarcoCut");
            bullet = Content.Load<Texture2D>(@"Sprites\Hero\Shoot");

            // IA

            Zombie = Content.Load<Texture2D>(@"Sprites\Enemy\Zombie_marche");
            Elite = Content.Load<Texture2D>(@"Sprites\Enemy\Elite");

            //Environnement

            Environnment = Content.Load<Texture2D>(@"Sprites\Environnment\environnment");
            Pixel = Content.Load<Texture2D>(@"Sprites\Environnment\blank");
            Herbe = Content.Load<Texture2D>(@"Sprites\Environnment\herbe");
            Ground = Content.Load<Texture2D>(@"Sprites\Environnment\ground");
            Platform = Content.Load<Texture2D>(@"Sprites\Environnment\platform");
            Coin = Content.Load<Texture2D>(@"Sprites\Environnment\coin");
            Environnment2 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment2");
            Herbe_neige = Content.Load<Texture2D>(@"Sprites\Environnment\herbe_neige");
            Environnment3 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment3");
            Terre_neige = Content.Load<Texture2D>(@"Sprites\Environnment\terre_neige");
            Ice = Content.Load<Texture2D>(@"Sprites\Environnment\ice");
            Ice_top = Content.Load<Texture2D>(@"Sprites\Environnment\ice_top");

            //Menu

            Menu_fond = Content.Load<Texture2D>(@"Sprites\Menu\Menu_fond");
            Button_jouer = Content.Load<Texture2D>(@"Sprites\Menu\Button_jouer");
            Button_options = Content.Load<Texture2D>(@"Sprites\Menu\Button_options");
            Button_quitter = Content.Load<Texture2D>(@"Sprites\Menu\Button_quitter");
            Bouton_retour = Content.Load<Texture2D>(@"Sprites\Menu\Bouton_retour");
            Bouton_multijoueur = Content.Load<Texture2D>(@"Sprites\Menu\Bouton_Multijoueur");
            HowToPlayPage1 = Content.Load<Texture2D>(@"Sprites\Menu\HowToPlayPage1");
            GameOver = Content.Load<Texture2D>(@"Sprites\Menu\GameOver");
            Boutton_Rejouer = Content.Load<Texture2D>(@"Sprites\Menu\Boutton_Rejouer");
            Won = Content.Load<Texture2D>(@"Sprites\Menu\Won");
            MarcoWon = Content.Load<Texture2D>(@"Sprites\Menu\MarcoHappy");
            Scenario = Content.Load<Texture2D>(@"Sprites\Menu\Scenario");
            HealthBar = Content.Load<Texture2D>(@"Sprites\Hero\HealthBar");


            //Musique

            MusiqueMain = Content.Load<Song>(@"Sons\Musiques\MusiqueTest");
            MusiqueMenu = Content.Load<Song>(@"Sons\Musiques\Musique_Menu_Test");
            MusiquePause = Content.Load<Song>(@"Sons\Musiques\MusiquePausefinal");
            MusiqueGameOver = Content.Load<Song>(@"Sons\Musiques\MusiqueGameOver");
            MusiqueVictory = Content.Load<Song>(@"Sons\Musiques\Musique_Victory");
            MusiqueIntro = Content.Load<Song>(@"Sons\Musiques\Musique_Intro");
            Musique2 = Content.Load<Song>(@"Sons\Musiques\Musique2");
            Musique3 = Content.Load<Song>(@"Sons\Musiques\Musique3");

            // Bruitages

            saut = Content.Load<SoundEffect>(@"Sons\Bruitages\saut");
            piece = Content.Load<SoundEffect>(@"Sons\Bruitages\piece");
            CoeurRapide = Content.Load<SoundEffect>(@"Sons\Bruitages\Coeur_Rapide");
            coup_de_feu = Content.Load<SoundEffect>(@"Sons\Bruitages\coup_de_feu");
            ammo_vide = Content.Load<SoundEffect>(@"Sons\Bruitages\noammo_sound");
            reload_sound = Content.Load<SoundEffect>(@"Sons\Bruitages\reload_sound");
            tir_rafale = Content.Load<SoundEffect>(@"Sons\Bruitages\tir_rafale");
            couteau = Content.Load<SoundEffect>(@"Sons\Bruitages\couteau");

            // SpriteFont
            ammo_font = Content.Load<SpriteFont>(@"SpriteFonts\Ammo");
            piece_font = Content.Load<SpriteFont>(@"SpriteFonts\piece_font");
        }
    }
}