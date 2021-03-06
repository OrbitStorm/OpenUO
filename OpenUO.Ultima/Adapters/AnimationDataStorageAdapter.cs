﻿#region License Header

// Copyright (c) 2015 OpenUO Software Team.
// All Right Reserved.
// 
// AnimationDataStorageAdapter.cs
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace OpenUO.Ultima.Adapters
{
    public class AnimationDataStorageAdapter : StorageAdapterBase, IAnimationDataStorageAdapter<AnimationData>
    {
        private AnimationData[] _animationData;

        public override int Length
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                return _animationData.Length;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            var install = Install;

            var animationData = new List<AnimationData>();

            using (var stream = File.Open(install.GetPath("animdata.mul"), FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var totalBlocks = (int) (reader.BaseStream.Length/548);

                    for (var i = 0; i < totalBlocks; i++)
                    {
                        var header = reader.ReadInt32();
                        var frameData = reader.ReadBytes(64);

                        var animData = new AnimationData
                        {
                            FrameData = new sbyte[64],
                            Unknown = reader.ReadByte(),
                            FrameCount = reader.ReadByte(),
                            FrameInterval = reader.ReadByte(),
                            FrameStart = reader.ReadByte()
                        };

                        Buffer.BlockCopy(frameData, 0, animData.FrameData, 0, 64);
                        animationData.Add(animData);
                    }
                }
            }

            _animationData = animationData.ToArray();
        }

        public AnimationData GetAnimationData(int index)
        {
            return index < _animationData.Length ? _animationData[index] : AnimationData.Empty;
        }

        public Task<AnimationData> GetAnimationDataAsync(int index)
        {
            return Task.FromResult(GetAnimationData(index));
        }
    }
}