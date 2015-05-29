/**
 * Document: Level.cs
 * Description: Niveau du jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System;
using System.Collections.Generic;
using TPI.Engine;
using TPI.Entities;

namespace TPI
{
    public class Level
    {
        private List<Block> _elements;
        const int LEVEL_WIDTH = 5000;
        const int BLOCK_HEIGHT = 50;
        const int BLOCK_MAX_WIDTH = 100;

        public Level(int pSeed)
        {
            this.Elements = new List<Block>();
            this.generateRandomly(pSeed);
        }

        private void generateRandomly(int pSeed)
        {
            Random rand;
            if (pSeed == -1) rand = new Random();
            else rand = new Random(pSeed);

            int width = rand.Next(BLOCK_HEIGHT, BLOCK_MAX_WIDTH);
            int height = 0;
            int gap = 0;
            int lastHeight = 0;
            for (int i = 0; i <= LEVEL_WIDTH; i += width)
            {
                width = rand.Next(BLOCK_HEIGHT + gap, BLOCK_MAX_WIDTH + gap);
                height = rand.Next(720 / 2 - BLOCK_HEIGHT, 720 / 2 + BLOCK_HEIGHT);
                if (i == 0) lastHeight = height;

                int deltaHeight = height - lastHeight;

                if (Math.Abs(deltaHeight) > 100)
                {
                    height = lastHeight;
                }

                this.Elements.Add(new Platform(new Vector2f(i + gap, height), new Vector2f(width - gap, BLOCK_HEIGHT)));
                gap = 100;
                lastHeight = height;
            }

        }

        public void Render()
        {
            foreach (Block b in Elements)
            {
                b.Render();
            }
        }

        public void Update()
        {

        }

        public List<Block> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
    }
}
