using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace World
{

    class WorldMap
    {
        public World.Map.Field.iField[,] fieldList;
        public World.Map.Field.iField[][] yKubelek;

        protected Game1 Game;

        public int width = 1;
        public int height = 1;

        public Vector2 mapZero = new Vector2(0, 0);

        public int getWidth()
        {
            return this.width;
        }

        public int getHeight()
        {
            return this.height;
        }

        public WorldMap(int width, int height, Game1 Game)
        {
            this.width = width;
            this.height = height;
            this.Game = Game;

            this.fieldList = new World.Map.Field.iField[this.width, this.height];
            for (int w = 0; w < this.width; w++)
            {
                for (int h = 0; h < this.height; h++)
                {

                    Vector2 pos = new Vector2(0, 0);

                    pos.Y = w * (64 / 2) - (w * 7);
                    pos.X = -w * (100 / 2); // -(w);

                    pos.Y += h * ((100 / 2) / 2);
                    pos.X += h * (100 / 2);
                    /*
                    if (h % 5 == 1 && w % 5 == 1)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Grass(this.Game);
                        this.fieldList[w, h].Level = 1;
                    }
                    else if (h % 5 == 2 && w % 5 == 1)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillSE(this.Game);
                    }
                    else if (h % 5 == 1 && w % 5 == 2)
                    {
                        //this.fieldList[w, h] = new World.Map.Field.Hill.HillSW(this.Game);
                        this.fieldList[w, h] = new World.Map.Field.Grass(this.Game);
                    }
                    else if (h % 5 == 0 && w % 5 == 2)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillW(this.Game);
                    }
                    else if (h % 5 == 2 && w % 5 == 2)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillS(this.Game);
                    }
                    else if (h % 5 == 0 && w % 5 == 0)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillN(this.Game);
                    }
                    else if (h % 5 == 0 && w % 5 == 1)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillNW(this.Game);
                    }
                    else if (h % 5 == 1 && w % 5 == 0)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillNE(this.Game);
                    }
                    else if (h % 5 == 2 && w % 5 == 0)
                    {
                        this.fieldList[w, h] = new World.Map.Field.Hill.HillE(this.Game);
                    }
                    else
                    {
                        this.fieldList[w, h] = new World.Map.Field.Grass(this.Game);
                    }
                     * */
                    this.fieldList[w, h] = new World.Map.Field.Grass(this.Game);


                    this.fieldList[w, h].mPosition = pos;
                    this.fieldList[w, h].X = w;
                    this.fieldList[w, h].Y = h;
                }
            }
            this.mapZero = this.fieldList[0,0].mPosition;

            for (int w = 0; w < this.width; w++)
            {
                for (int h = 0; h < this.height; h++)
                {
                    this.fieldList[w, h].bindAdjacentField(this);
                }
            }

            this.initKubelki();
        }

        public void initKubelki()
        {
            int steps = this.width + this.height - 1;
            int half = (steps - (steps % 2)) / 2;

            this.yKubelek = new World.Map.Field.iField[steps][];

            int step = 0;
            int x = 0;
            int y = 0;
            int f = 0;
            int k = 0;

            for (step = 0; step <= half; step++)
            {
                x = 0;
                y = step;

                k = steps - step - 1;

                this.yKubelek[step] = new World.Map.Field.iField[step + 1];
                this.yKubelek[k] = new World.Map.Field.iField[step + 1];

                for (f = 0; f <= step; f++)
                {
                    this.yKubelek[step][f] = this.fieldList[x, y];
                    if (step != half)
                    {
                        this.yKubelek[k][step - f] = this.fieldList[half - x, half - y];
                    }
                    x++;
                    y--;
                }
            }
        }

        /// <summary>
        /// Jak generujemy mape to kazde pole szuka sobie sasiada, dla pola polozonego na skraju musi znajowac odpowiedniego sasiada
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public World.Map.Field.iField getField(int X, int Y)
        {
            if (X < 0)
            {
                X = 0;
            }
            if (Y < 0)
            {
                Y = 0;
            }

            if (X >= width)
            {
                X = width-1;
            }
            if (Y >= height)
            {
                Y = height-1;
            }
            return this.fieldList[X, Y];
        }

        public void LoadContent(ContentManager Content)
        {
            for (int h = 0; h < this.height; h++)
            {
                for (int w = 0; w < this.width; w++)
                {
                    this.fieldList[w, h].LoadContent(Content);
                }
            }
        }
    }
}
