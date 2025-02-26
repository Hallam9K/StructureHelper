﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StructureHelper
{
    public static class Helper
    {
        //Fisher-Yates algorithm
        public static void RandomizeList<T>(ref List<T> input)
        {
            int n = input.Count();
            while (n > 1)
            {
                n--;
                int k = WorldGen.genRand.Next(n + 1);
                T value = input[k];
                input[k] = input[n];
                input[n] = value;
            }
        }

        public static Texture2D GetItemTexture(Item item)
        {
            if (item.type < Main.maxItemTypes) return Terraria.GameContent.TextureAssets.Item[item.type].Value;
            else return ModContent.Request<Texture2D>(item.ModItem.Texture).Value;
        }
    }
}
