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

        public static Texture2D Marco, MarcoSaut, MarcoSquat, MarcoTir, MarcoCut;
        public static Texture2D Player2, Controls, Defaite, Defeat, English_Scenar, Won_English;
        public static Texture2D Zombie, Elite, Boss;
        public static Texture2D Environnment, Pixel, Herbe, Ground, Platform, Environnment2, Environnment3, Herbe_neige, Terre_neige, Ice, Ice_top, Sand, Sand_top, Rock, Rock_top,Piques, Environnment4, Environnment5, Environnment6, Roche, Roche_top, Lave;
        public static Texture2D Foreground, Foreground2, Foreground3, Foreground4, Foreground5, Foreground6;
        public static Texture2D MarcoWon, Scenario, lvl2_block, lvl3_block, lvl4_block, lvl5_block, lvl6_block, Bullet_Texture;
        public static Song MusiqueMain, MusiqueMenu, MusiquePause, MusiqueGameOver, MusiqueVictory, MusiqueIntro, Musique2, Musique3, MusiqueBoutique, Musique4,Musique5,Musique6,MusiqueBoss;
        public static SoundEffect saut, piece, CoeurRapide, coup_de_feu, ammo_vide, reload_sound, tir_rafale, couteau, bonus_bruitage, enemies_sound, boss_mort_sound, marco_touched_sound;
        public static Texture2D HealthBar, bullet;
        public static Texture2D Coin, Health, Speed, particule1, particule2, particule3;
        public static SpriteFont ammo_font, piece_font, highscores_font,pourcent_life;

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

            //Player2

            Player2 = Content.Load<Texture2D>(@"Sprites\Player2\player2");

            // IA

            Zombie = Content.Load<Texture2D>(@"Sprites\Enemy\Zombie_marche");
            Elite = Content.Load<Texture2D>(@"Sprites\Enemy\Elite");
            Boss = Content.Load<Texture2D>(@"Sprites\Enemy\Zomboss");

            //Environnement

            Environnment = Content.Load<Texture2D>(@"Sprites\Environnment\environnment");
            Pixel = Content.Load<Texture2D>(@"Sprites\Environnment\blank");
            Herbe = Content.Load<Texture2D>(@"Sprites\Environnment\herbe");
            Ground = Content.Load<Texture2D>(@"Sprites\Environnment\ground");
            Platform = Content.Load<Texture2D>(@"Sprites\Environnment\platform");
            Environnment2 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment2");
            Herbe_neige = Content.Load<Texture2D>(@"Sprites\Environnment\herbe_neige");
            Environnment3 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment3");
            Terre_neige = Content.Load<Texture2D>(@"Sprites\Environnment\terre_neige");
            Ice = Content.Load<Texture2D>(@"Sprites\Environnment\ice");
            Ice_top = Content.Load<Texture2D>(@"Sprites\Environnment\ice_top");
            Piques = Content.Load<Texture2D>(@"Sprites\Environnment\piques");
            Environnment4 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment4");
            Roche = Content.Load<Texture2D>(@"Sprites\Environnment\roche");
            Roche_top = Content.Load<Texture2D>(@"Sprites\Environnment\roche_top");
            Environnment5 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment5");
            Sand = Content.Load<Texture2D>(@"Sprites\Environnment\sand");
            Sand_top = Content.Load<Texture2D>(@"Sprites\Environnment\sand_top");
            Environnment6 = Content.Load<Texture2D>(@"Sprites\Environnment\environnment6");
            Rock = Content.Load<Texture2D>(@"Sprites\Environnment\rock");
            Rock_top = Content.Load<Texture2D>(@"Sprites\Environnment\rock_top");
            Foreground = Content.Load<Texture2D>(@"Sprites\Environnment\foreground1");
            Foreground2 = Content.Load<Texture2D>(@"Sprites\Environnment\foreground2");
            Foreground3 = Content.Load<Texture2D>(@"Sprites\Environnment\foreground3");
            Foreground4 = Content.Load<Texture2D>(@"Sprites\Environnment\foreground4");
            Foreground5 = Content.Load<Texture2D>(@"Sprites\Environnment\foreground5");
            Foreground6 = Content.Load<Texture2D>(@"Sprites\Environnment\foreground6");
            Lave = Content.Load<Texture2D>(@"Sprites\Environnment\lava");
            Bullet_Texture = Content.Load<Texture2D>(@"SpriteFonts\Bullet_Texture");

            //Bonus

            Coin = Content.Load<Texture2D>(@"Sprites\Bonus\coin");
            Health = Content.Load<Texture2D>(@"Sprites\Bonus\HealthBonus");
            Speed = Content.Load<Texture2D>(@"Sprites\Bonus\SpeedBonus");
            particule1 = Content.Load<Texture2D>(@"Sprites\Bonus\particules1");
            particule2 = Content.Load<Texture2D>(@"Sprites\Bonus\particules2");
            particule3 = Content.Load<Texture2D>(@"Sprites\Bonus\particules3");

            //Menu

            MarcoWon = Content.Load<Texture2D>(@"Sprites\Menu\Francais\MarcoHappy");
            lvl2_block = Content.Load<Texture2D>(@"Sprites\Menu\Francais\level2_block");
            lvl3_block = Content.Load<Texture2D>(@"Sprites\Menu\Francais\level3_block");
            lvl4_block = Content.Load<Texture2D>(@"Sprites\Menu\Francais\level4_block");
            lvl5_block = Content.Load<Texture2D>(@"Sprites\Menu\Francais\level5_block");
            lvl6_block = Content.Load<Texture2D>(@"Sprites\Menu\Francais\level6_block");
            Scenario = Content.Load<Texture2D>(@"Sprites\Menu\Francais\Scenario");
            HealthBar = Content.Load<Texture2D>(@"Sprites\Hero\HealthBar");
            Controls = Content.Load<Texture2D>(@"Sprites\Menu\English\Controls");
            Defaite = Content.Load<Texture2D>(@"Sprites\Menu\English\Defaite");
            Defeat = Content.Load<Texture2D>(@"Sprites\Menu\English\Defeat");
            English_Scenar = Content.Load<Texture2D>(@"Sprites\Menu\English\EnglishScenario");
            Won_English = Content.Load<Texture2D>(@"Sprites\Menu\English\WonEnglish");


            //Musique

            MusiqueMain = Content.Load<Song>(@"Sons\Musiques\MusiqueTest");
            MusiqueMenu = Content.Load<Song>(@"Sons\Musiques\Musique_Menu_Test");
            MusiquePause = Content.Load<Song>(@"Sons\Musiques\MusiquePausefinal");
            MusiqueGameOver = Content.Load<Song>(@"Sons\Musiques\MusiqueGameOver");
            MusiqueVictory = Content.Load<Song>(@"Sons\Musiques\Musique_Victory");
            MusiqueIntro = Content.Load<Song>(@"Sons\Musiques\Musique_Intro");
            Musique2 = Content.Load<Song>(@"Sons\Musiques\Musique2");
            Musique3 = Content.Load<Song>(@"Sons\Musiques\Musique3");
            Musique4 = Content.Load<Song>(@"Sons\Musiques\Musique4");
            Musique5 = Content.Load<Song>(@"Sons\Musiques\Musique_lvl5");
            Musique6 = Content.Load<Song>(@"Sons\Musiques\Musique_lvl6");
            MusiqueBoss = Content.Load<Song>(@"Sons\Musiques\Musique_boss");
            MusiqueBoutique = Content.Load<Song>(@"Sons\Musiques\MusiqueBoutique");

            // Bruitages

            saut = Content.Load<SoundEffect>(@"Sons\Bruitages\saut_mario");
            piece = Content.Load<SoundEffect>(@"Sons\Bruitages\piece");
            CoeurRapide = Content.Load<SoundEffect>(@"Sons\Bruitages\Coeur_Rapide");
            coup_de_feu = Content.Load<SoundEffect>(@"Sons\Bruitages\coup_de_feu");
            ammo_vide = Content.Load<SoundEffect>(@"Sons\Bruitages\noammo_sound");
            reload_sound = Content.Load<SoundEffect>(@"Sons\Bruitages\reload_sound");
            tir_rafale = Content.Load<SoundEffect>(@"Sons\Bruitages\tir_rafale");
            couteau = Content.Load<SoundEffect>(@"Sons\Bruitages\couteau");
            bonus_bruitage = Content.Load<SoundEffect>(@"Sons\Bruitages\Bonus_Bruitages");
            enemies_sound = Content.Load<SoundEffect>(@"Sons\Bruitages\Mort_Enemies");
            boss_mort_sound = Content.Load<SoundEffect>(@"Sons\Bruitages\Boss_Mort_Sound");
            marco_touched_sound = Content.Load<SoundEffect>(@"Sons\Bruitages\marco_touched_sound");

            // SpriteFont
            ammo_font = Content.Load<SpriteFont>(@"SpriteFonts\Ammo");
            piece_font = Content.Load<SpriteFont>(@"SpriteFonts\piece_font");
            highscores_font = Content.Load<SpriteFont>(@"SpriteFonts\HighScores");
            pourcent_life = Content.Load<SpriteFont>(@"SpriteFonts\pourcent_life");
        }
    }
}