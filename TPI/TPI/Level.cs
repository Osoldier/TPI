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
    /// <summary>
    /// Contient toute les plateformes d’un niveau
    /// </summary>
    public class Level
    {
        private List<Block> _elements;
        /// <summary>longueur totale du niveau (en pixels)</summary>
        const int LEVEL_WIDTH = 5000;
        /// <summary>hauteur d’un block</summary>
        const int BLOCK_HEIGHT = 50;
        /// <summary>longueur maximale d’un block</summary>
        const int BLOCK_MAX_WIDTH = 100;
        /// <summary>Verrou de la liste des plateformes</summary>
        public static Object CollectionLocker = new Object();

        /// <summary>
        /// Instancie la liste des éléments et lance la génération si pGenerate = true.
        /// </summary>
        /// <param name="pSeed">seed du niveau, -1 = seed aléatoire</param>
        /// <param name="pGenerate">true = generer le niveau, false = ne pas le generer</param>
        public Level(int pSeed, bool pGenerate)
        {
            this.Elements = new List<Block>();
            if (pGenerate)
                this.generateRandomly(pSeed);
        }

        /// <summary>
        /// Génére un niveau aléatoirement 
        /// </summary>
        /// <param name="pSeed">seed du niveau</param>
        private void generateRandomly(int pSeed)
        {
            Random rand;
            if (pSeed == -1) rand = new Random();
            else rand = new Random(pSeed);

            int width = rand.Next(BLOCK_HEIGHT, BLOCK_MAX_WIDTH);
            int y = 0;
            int gap = 0;
            int lastY = 0;
            for (int i = 0; i <= LEVEL_WIDTH; i += width)
            {
                width = rand.Next(BLOCK_HEIGHT + gap, BLOCK_MAX_WIDTH + gap);
                y = rand.Next(720 / 2 - BLOCK_HEIGHT, 720 / 2 + BLOCK_HEIGHT);
                if (i == 0) lastY = y;

                int deltaHeight = y - lastY;

                if (Math.Abs(deltaHeight) > 100)
                {
                    y = lastY;
                }

                this.Elements.Add(new Platform(new Vector2f(i + gap, y), new Vector2f(width - gap, BLOCK_HEIGHT)));
                gap = 100;
                lastY = y;
            }
            this.Elements[this.Elements.Count - 1].End = true;
        }

        /// <summary>
        /// Rend tout le niveau
        /// </summary>
        public void Render()
        {
            lock (CollectionLocker)
            {
                foreach (Block b in Elements)
                {
                    b.Render();
                }
            }
        }

        /// <summary>
        /// Logique des objets du niveau
        /// </summary>
        public void Update()
        {

        }

        /// <summary>Liste des objets du niveau</summary>
        public List<Block> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }
    }
}
