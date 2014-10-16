using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HeartStopper;
using WindowsGame1;

namespace HeartStopper
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Map : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public const int TILE_SIZE = 24; // pretzels

        //public Tile[,] grid;
        public int[,] grid;
        private Texture2D texture;
        private Color[] colours;
        private int width;
        private int height;
        private Game1 game;
        public HeightMap hmap;

   

        public static SpriteBatch spriteBatch; // For all the map info.

        public Map(Game1 game, int width, int height)
            : base(game)
            
        {
            // TODO: Construct any child components here
            this.game = game;
            this.width = width;
            this.height = height;
            this.game.Components.Add(this);
           /* Hunter hunter = new Hunter(game,200f,200f);
            hunter = new Hunter(game, 200f, 200f);
            hunter = new Hunter(game, 250f, 200f);
            hunter = new Hunter(game, 3500f, 350f);
            hunter = new Hunter(game, 400f, 200f);
            hunter = new Hunter(game, 500f, 450f);
            hunter = new Hunter(game, 5500f, 780f);
            hunter = new Hunter(game, 200f, 4500f);
            hunter = new Hunter(game, 350f, 200f);
            hunter = new Hunter(game, 100f, 200f);
            hunter = new Hunter(game, 200f, 240f);*/
            //hunter = new Hunter(game, 200f, 200f);
            Sheep sheep = new Sheep(game, 300, 300);
           sheep = new Sheep(game, 300, 300);
            sheep = new Sheep(game, 120, 300);
            sheep = new Sheep(game, 300, 1300);
             sheep = new Sheep(game, 20, 300);
             sheep = new Sheep(game, 50, 1500);
            sheep = new Sheep(game, 400, 300);
            sheep = new Sheep(game, 2000, 50);
             sheep = new Sheep(game, 100, 300);
             for (var i = 0; i < 30; i++)
             {
                 Random randomy = new Random(i * 33);
                 Random randomx = new Random(i * 51);
                 sheep = new Sheep(game, randomx.Next(0,getWidth()), randomy.Next(0, getHeight()));
             }

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
           
            spriteBatch = new SpriteBatch(Game1.graphics.GraphicsDevice);

            hmap = new HeightMap(Game1.MAP_SIZE, 0);

            
            
            // Init the map in a dumb way for now.
            grid = new int[width, height];

            /*
            int[,] elevationMap = new int[width, height];

            // ***** This hard-coded elevation map is 21x11 ******
            elevationMap = new int[,]
            
            {
                {4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,7,8,8,8,8,8,8,8,8,8,8,8,8,7,6,5,4,3,3,3,3,3,3,4,4,5,5,5,5,6,6,7,7,7,7,7,7,7,7,7,7,7},
                {4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,7,7,8,8,8,8,8,8,8,8,8,8,8,7,6,5,4,3,3,3,3,3,4,4,4,5,5,5,5,5,6,6,7,7,7,7,7,7,7,7,7,7},
                {3,3,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,7,7,8,8,8,8,8,8,8,8,8,8,8,8,7,6,5,4,4,3,3,3,3,3,3,4,4,4,5,5,5,5,6,6,6,7,7,7,7,7,7,7,6},
                {3,3,3,4,4,4,4,4,4,5,5,5,5,5,6,7,7,8,8,8,8,8,8,8,8,8,8,8,8,8,8,7,6,5,5,4,3,3,3,3,3,4,4,4,4,5,5,5,5,5,5,6,6,6,7,7,7,7,6,6},
                {3,3,3,4,4,4,4,4,4,5,5,5,5,5,6,7,8,8,8,8,8,8,8,8,8,8,8,7,8,7,7,7,6,6,5,4,3,3,3,3,3,3,3,4,4,4,5,5,5,5,5,5,6,6,6,7,7,7,7,6},
                {3,3,3,3,4,4,4,4,4,5,5,5,5,5,6,7,8,8,8,8,8,8,8,8,8,7,7,7,7,7,7,7,7,6,5,4,4,3,3,3,3,3,3,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6,6,6},
                {3,3,3,3,4,4,4,4,4,5,5,5,5,5,6,7,7,8,8,8,8,8,8,8,8,7,7,7,7,7,7,7,7,6,5,4,3,3,3,3,3,3,3,4,4,4,4,5,5,5,5,5,5,6,6,6,6,4,6,6},
                {3,3,3,3,4,4,4,4,5,5,5,5,5,5,6,6,7,7,8,8,8,8,8,8,7,7,7,7,7,7,7,7,7,6,5,4,4,3,3,3,3,3,3,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,6,6},
                {3,3,3,3,4,4,4,5,5,5,5,5,5,5,5,6,6,7,8,8,8,8,8,7,7,7,7,7,7,7,7,7,7,6,5,4,4,3,3,3,3,3,3,3,3,4,4,4,4,4,4,4,4,4,4,5,5,6,6,6},
                {3,3,3,3,4,4,4,5,5,5,5,5,5,5,5,5,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,6,5,5,4,4,3,3,3,3,3,3,3,4,4,4,4,4,4,3,3,3,4,4,5,5,6,6},
                {3,3,3,4,4,4,4,4,4,5,5,5,5,5,4,5,6,6,7,7,7,7,7,7,7,7,7,7,7,7,7,7,7,6,6,5,4,4,3,3,3,3,3,3,4,4,4,4,4,4,3,3,3,3,3,4,4,5,5,6},
                {3,3,4,4,4,4,4,4,4,5,5,5,5,4,4,5,5,6,6,6,7,7,7,7,7,7,7,7,7,7,7,7,6,5,5,4,4,4,4,4,3,3,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3,4,4,5},
                {3,4,4,5,5,4,4,4,4,4,5,5,5,4,4,4,5,5,6,6,7,7,7,7,7,7,7,7,7,7,7,6,6,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3,3,4,4},
                {4,4,5,5,5,5,5,5,5,5,5,5,5,4,4,4,5,5,5,6,6,6,7,7,7,7,7,7,7,7,6,6,6,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3,3,3},
                {4,5,5,5,5,5,6,6,5,5,5,5,5,4,4,4,5,5,5,5,5,6,6,6,6,7,7,7,7,7,6,6,6,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3},
                {5,5,5,6,6,6,6,6,5,5,5,5,5,4,4,4,5,5,5,5,5,5,6,6,6,6,7,7,7,6,6,6,6,5,5,4,4,4,4,4,4,4,4,4,5,5,4,4,4,4,5,4,4,3,3,3,3,3,3,3},
                {5,5,6,6,6,6,6,6,5,5,5,5,4,4,4,4,5,5,5,5,5,5,5,5,6,6,7,7,7,6,6,6,6,5,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,4,4,3,3,3,3,3,4},
                {5,6,6,6,6,5,5,5,5,5,5,5,4,4,4,4,5,5,5,5,5,5,5,5,5,6,6,6,6,6,6,6,6,5,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,4,4,4,4,4,4,4},
                {6,6,6,6,6,5,5,5,5,5,5,5,5,4,4,4,5,5,5,5,5,5,5,5,5,5,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5},
                {6,6,6,6,6,5,5,5,5,5,5,5,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,6,6,6,6,6,6,5,5,6,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,6,6,6,6,5,5},
                {6,6,6,5,5,5,5,4,4,4,4,4,4,4,4,4,4,5,4,4,5,5,5,5,5,5,5,6,6,6,6,6,6,5,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,6,6,6,4,6,5,5},
                {6,6,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,5,5,5,5,6,6,6,6,6,6,6,6,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,5,5,6,6,6,6,6,6},
                {6,5,5,5,4,4,4,3,3,3,3,4,4,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,6,6,6,6,6,6,5,4,4,4,4,4,4,5,5,5,5,5,5,5,5,5,5,5,5,5,6,6,6,6,6},
                {5,5,5,5,4,4,3,3,3,3,3,4,4,4,4,4,4,4,4,4,4,4,4,4,5,5,5,5,6,6,6,6,5,5,5,5,5,5,5,4,4,5,5,4,5,5,5,5,5,5,5,5,5,5,5,6,6,6,6,6},
                {5,5,5,5,4,4,3,2,2,2,3,3,4,4,4,4,4,4,4,4,4,4,4,4,5,5,6,6,6,6,6,6,6,5,5,5,5,5,5,5,5,5,5,5,4,4,4,4,5,5,5,5,5,5,5,6,6,6,6,6},
                {5,5,5,5,4,4,3,2,2,2,3,3,4,4,4,4,4,4,4,4,4,4,4,4,5,6,6,6,6,6,6,6,5,5,5,5,5,5,5,5,5,5,5,5,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6},
                {4,4,4,4,4,3,3,2,2,2,2,3,3,4,4,4,4,4,4,4,4,4,4,5,5,6,6,6,6,6,6,6,6,5,5,5,5,5,5,5,5,5,5,5,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6},
                {4,4,4,4,4,3,5,2,2,2,5,2,3,3,4,4,4,4,4,4,5,5,5,5,5,6,6,6,6,5,5,5,5,5,5,5,4,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6,6},
                {4,4,4,4,3,3,2,2,2,2,2,3,3,3,3,4,4,4,5,5,5,5,5,5,5,6,6,6,6,5,5,5,5,5,5,5,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,6,6,6,7,7,7,6},
                {4,4,4,4,3,2,2,2,2,2,2,3,3,3,4,4,4,5,5,5,5,5,5,5,6,6,6,6,6,6,5,5,5,5,5,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,7,7,7,7,7,7},
                {4,4,4,4,3,2,2,2,2,2,3,3,3,4,4,4,5,5,5,5,5,5,5,6,6,6,6,6,5,5,5,5,5,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,5,5,6,7,7,7,7,7,7,7,7},
                {5,5,5,4,3,2,2,2,3,3,3,3,3,4,4,4,5,5,5,5,5,5,5,6,6,6,6,6,6,6,6,6,5,5,5,4,4,4,4,4,4,4,4,4,5,4,5,5,5,5,6,6,7,7,7,7,7,7,7,7},
                {5,5,5,4,3,2,2,3,3,4,4,4,4,4,4,4,5,5,5,5,5,5,5,6,6,6,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,7,7,7,7,7,7,7,7,7},
                {6,5,5,4,3,2,2,3,3,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,7,7,7,7,7,7,7,7,7},
                {6,6,5,4,3,3,3,3,3,4,4,4,4,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,6,7,7,7,7,7,7,7,7,7},
                {7,6,6,5,4,4,4,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,5,5,5,5,5,5,6,7,7,7,7,7,7,7,7,7,7},
                {7,7,6,5,4,4,4,4,4,4,4,3,3,3,3,3,4,4,5,5,5,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5,4,4,4,4,4,4,5,5,5,5,5,5,6,7,7,7,7,7,7,7,7,7,7},
                {7,7,6,5,5,4,4,4,4,4,3,3,3,3,3,3,4,4,5,5,5,6,6,6,6,6,6,7,7,6,6,6,6,6,6,6,5,4,4,4,4,4,5,5,5,5,5,4,4,6,6,6,7,7,7,7,7,7,7,7},
                {7,7,6,6,5,5,4,4,4,4,3,3,3,3,3,3,3,4,4,5,5,6,6,6,6,5,6,7,7,6,6,6,6,6,6,6,5,5,4,4,4,4,5,5,5,5,5,4,4,4,6,6,6,7,7,7,7,7,7,7},
                {7,7,7,6,6,5,5,4,4,4,4,3,3,3,3,3,3,4,5,5,5,6,6,6,5,6,7,7,7,6,6,6,6,6,6,6,5,5,4,4,4,4,5,5,5,4,4,4,4,4,4,4,6,7,7,7,7,7,7,6},
                {7,7,7,6,6,5,5,4,4,3,3,3,3,3,3,3,3,4,4,4,5,5,5,5,5,6,7,7,7,6,6,6,6,6,6,5,5,5,5,5,5,5,5,5,4,4,4,4,4,4,4,5,5,6,6,6,6,6,6,5},
                {7,7,7,6,6,5,5,4,4,4,4,3,3,3,3,3,3,3,4,4,5,5,5,5,5,6,7,7,7,7,7,6,6,6,5,5,6,6,6,6,5,5,5,5,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,4},
                {7,7,7,6,6,5,5,5,4,4,3,3,3,3,3,3,3,3,4,4,5,5,5,5,5,6,7,7,7,7,7,7,6,5,5,6,6,6,7,6,5,5,5,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,5,4},
                {7,7,7,7,6,5,5,5,4,4,4,4,4,4,4,4,4,4,4,4,5,5,5,5,5,6,7,7,7,7,7,7,6,5,5,6,6,7,7,6,5,5,5,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
                {7,7,7,7,6,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,6,7,7,7,7,7,7,6,6,6,6,7,7,7,6,5,5,5,4,4,4,4,4,4,3,3,3,3,3,3,3,3,3,3,3},
                {8,8,8,7,6,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,6,7,7,7,7,7,7,7,7,6,7,7,7,6,6,5,5,4,4,4,4,4,4,4,3,3,3,3,2,2,2,2,2,2,2},
                {8,8,8,7,7,6,5,5,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,6,7,7,7,7,7,7,7,7,7,7,7,6,6,5,5,5,4,4,4,4,4,4,4,3,3,3,3,3,3,2,2,2,2,2},
                {8,8,8,7,7,7,6,6,6,5,5,5,5,5,5,5,5,5,4,4,4,4,4,4,5,6,6,6,7,7,7,7,7,7,7,7,7,6,5,5,5,4,4,4,4,4,4,4,4,4,3,3,3,3,3,3,2,2,2,2},
                {8,8,8,8,7,7,7,7,6,6,6,5,5,5,5,5,5,4,4,4,4,3,4,4,4,5,5,5,6,7,7,7,7,7,7,7,7,6,5,5,5,5,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3,2,2,2},
                {8,8,8,8,8,8,8,7,7,6,5,6,6,5,5,5,4,4,4,4,4,3,3,3,4,4,4,5,6,6,6,7,7,7,7,6,6,6,5,5,5,5,5,4,4,4,4,4,4,4,3,3,3,3,3,3,3,2,2,2},
                {8,8,8,8,8,8,8,8,8,7,6,5,5,5,5,4,4,4,4,4,3,3,3,3,3,3,4,4,5,6,6,6,6,6,6,6,6,6,6,5,5,5,5,5,4,4,4,4,4,4,4,4,4,3,3,3,3,2,2,2},
                {8,8,8,8,8,8,8,8,8,7,7,6,5,5,5,4,4,4,4,3,3,3,2,2,2,2,3,4,5,6,6,6,6,6,6,7,7,7,6,6,5,5,5,5,5,5,4,4,4,4,4,4,3,3,3,3,3,3,3,3},
                {8,8,8,8,8,8,8,8,8,8,7,6,5,5,5,4,4,4,4,4,3,3,2,2,2,2,3,4,5,6,6,6,6,6,6,7,8,8,7,6,6,5,5,5,5,5,4,4,4,4,4,4,3,3,3,3,3,3,3,3},
                {8,8,8,7,7,7,8,8,8,8,7,6,5,4,4,4,4,4,4,4,3,3,2,2,2,2,3,4,5,5,6,6,6,7,7,8,8,8,8,7,6,6,6,6,5,5,4,4,4,4,4,4,3,3,3,3,3,3,3,3},
                {8,8,7,7,7,7,8,8,8,8,7,6,5,4,4,4,4,4,4,4,4,3,2,2,2,2,2,3,4,5,6,6,6,7,8,8,8,8,8,8,7,7,7,6,5,5,5,4,4,4,4,4,4,3,3,3,3,3,3,4},
                {7,7,7,7,7,8,8,8,8,7,6,5,5,4,4,4,4,4,4,4,4,3,2,2,2,2,2,3,4,4,5,6,7,8,8,8,8,8,8,8,8,8,8,7,6,5,5,5,4,4,4,4,4,3,3,4,4,4,4,4},
                {7,7,7,7,7,7,7,7,7,6,5,5,4,4,4,4,4,4,4,4,4,3,2,2,2,2,2,2,3,4,5,6,7,8,8,8,8,8,8,8,8,8,8,7,6,5,5,4,5,4,4,4,4,3,3,4,4,4,4,4},
                {7,7,7,7,7,7,7,7,6,5,5,4,4,4,4,4,4,4,4,4,3,3,2,2,2,2,2,2,3,4,5,6,7,8,8,8,8,8,8,8,8,8,8,8,7,6,6,5,5,4,4,4,4,4,3,3,4,4,4,4},
                {7,7,7,7,7,7,7,7,7,6,5,5,4,4,4,4,4,4,4,4,3,2,2,2,2,2,2,2,3,4,5,6,7,8,8,8,8,8,8,8,8,8,8,8,8,7,6,5,5,4,4,4,4,3,3,4,4,4,4,4},
                {7,7,7,7,7,7,7,7,7,7,6,5,5,4,4,4,4,4,4,3,3,2,2,2,2,2,2,2,3,4,5,6,7,8,8,8,8,8,8,8,8,8,8,8,8,7,6,6,5,4,4,4,4,3,3,4,4,4,4,4}
            };
            */
            
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //grid[i, j] = new Tile(game, i, j, hmap.getHeight(i, j));
                    grid[i, j] = hmap.getHeight(i, j);
                }
            }

            Console.WriteLine("map initialization complete.");
            base.Initialize();
        }

        protected override void LoadContent()
        {

            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new[] { new Color(255,255,255) });
            colours = new Color[HeightMap.MAX - HeightMap.MIN + 1];
            for (int i = HeightMap.MIN; i <= HeightMap.MAX; i++)
            {
                float g = ((float)i / (float)HeightMap.MAX) * 255f;
                colours[i - HeightMap.MIN] = new Color(0, (int)g, 0);
            }

            base.LoadContent();

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            for (var i = 0; i < Sheep.sheepListCount; i++)
            {
                if (Math.Abs(Math.Sqrt(Math.Pow(game.wW.x - Sheep.sheep[i].getX(), 2) + Math.Pow(game.wW.y - Sheep.sheep[i].getY(), 2))) < 50)
                    Sheep.sheep[i].isAlive(false);
                if (Math.Abs(Math.Sqrt(Math.Pow(game.wW.x - Sheep.sheep[i].getX(), 2) + Math.Pow(game.wW.y - Sheep.sheep[i].getY(), 2))) < 300)
                    Sheep.sheep[i].runAway();
            }
          
           
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

            //Game1.spriteBatch.Begin();

            int elevation;
            for (int i = 0; i < this.width; i++) {
                for (int j = 0; j < this.height; j++) {
                    elevation = grid[i, j]; // index into colours[]
                    Game1.spriteBatch.Draw(texture, new Rectangle(i * TILE_SIZE - (int)((Game1)Game).cam.X, j * TILE_SIZE - (int)((Game1)Game).cam.Y, TILE_SIZE, TILE_SIZE), colours[elevation - 1]);
                }   
            }
            

            //Game1.spriteBatch.End();
        }

        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }

        public Vector2 getTile(float x, float y)
        {
            if (x >= 0 && x < width * TILE_SIZE && y >= 0 && y < height * TILE_SIZE)
            {
                int i = (int) x / TILE_SIZE;
                int j = (int) y / TILE_SIZE;
                return new Vector2(i, j);
            }

            return new Vector2((int) -1, (int) -1);
        }
    }
    

}
