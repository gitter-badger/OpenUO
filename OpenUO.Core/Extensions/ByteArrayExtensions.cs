﻿#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   ByteArrayExtensions.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

#region Usings

using System.IO;
using System.Text;

#endregion

namespace OpenUO.Core
{
    public static class ByteArrayExtensions
    {
        public static string ToFormattedString(this byte[] buffer)
        {
            return buffer.ToFormattedString(buffer.Length);
        }

        public static string ToFormattedString(this byte[] buffer, int length)
        {
            var stream = new MemoryStream(buffer);
            return stream.ToFormattedString();
        }

        public static void ToFormattedString(this byte[] buffer, int length, StringBuilder builder)
        {
            var stream = new MemoryStream(buffer);
            stream.ToFormattedString(length, builder);
        }

        public static void ToFormattedString(this byte[] buffer, int length, StreamWriter writer)
        {
            var stream = new MemoryStream(buffer);
            stream.ToFormattedString(length, writer);
        }
    }
}