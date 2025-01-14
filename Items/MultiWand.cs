﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.IO;
using Terraria.ID;

namespace StructureHelper.Items
{
    class MultiWand : ModItem
    {
        public bool SecondPoint;
        public Point16 TopLeft;
        public int Width;
        public int Height;
        internal List<TagCompound> StructureCache = new List<TagCompound>();

        public Rectangle target => new Rectangle(TopLeft.X, TopLeft.Y, Width, Height);

        public override bool CanRightClick() => true;

        public override bool AltFunctionUse(Player player) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Multistructure Wand");
            Tooltip.SetDefault("Select 2 points in the world, then right click to add a structure. Right click in your inventory when done to save.");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = 1;
        }

        public override void RightClick(Player player)
        {
            Item.stack++;
            if (StructureCache.Count > 1)
                Saver.SaveMultistructureToFile(ref StructureCache);
            else
                Main.NewText("Not enough structures! If you want to save a single structure, use the normal structure wand instead!", Color.Red);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !SecondPoint && TopLeft != default)
                StructureCache.Add(Saver.SaveStructure(target));

            else if (!SecondPoint)
            {
                TopLeft = (Main.MouseWorld / 16).ToPoint16();
                Width = 0;
                Height = 0;
                Main.NewText("Select Second Point");
                SecondPoint = true;
            }

            else
            {
                Point16 bottomRight = (Main.MouseWorld / 16).ToPoint16();
                Width = bottomRight.X - TopLeft.X - 1;
                Height = bottomRight.Y - TopLeft.Y - 1;
                Main.NewText("Ready to add! Right click to add this structure, Right click in inventory to save all structures");
                SecondPoint = false;
            }

            return true;
        }     
    }
}
