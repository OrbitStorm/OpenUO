﻿#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   ColorConverter.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

namespace OpenUO.Ultima
{
    public class ColorConverter
    {
        public static uint ARGB1555toARGB8888(ushort color16)
        {
            var a = color16 & (uint)0x8000;
            var r = color16 & (uint)0x7C00;
            var g = color16 & (uint)0x03E0;
            var b = color16 & (uint)0x1F;
            var rgb = (r << 9) | (g << 6) | (b << 3);

            return (a * 0x1FE00) | rgb | ((rgb >> 5) & 0x070707);
        }
    }
}