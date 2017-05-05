﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cube_exp.Scene
{
    class MapRawData
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int SizeZ { get; private set; }
        public int[][][] Data;

        public IEnumerator<BoxObject> Blocks { get; private set; }

        public void LoadMapFile(string fileName)
        {
            var file = asd.Engine.File.CreateStaticFile(fileName);
            var raw = Encoding.UTF8.GetString(file.Buffer)
                .Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToArray();

            SizeX = raw[0];
            SizeY = raw[1];
            SizeZ = raw[2];

            Data = new int[SizeY][][];
            int counter = 3;
            for (int y = SizeY - 1; y >= 0; y--)
            {
                Data[y] = new int[SizeX][];
                for (int x = 0; x < SizeX; x++)
                {
                    Data[y][x] = new int[SizeZ];
                    for (int z = 0; z < SizeZ; z++)
                    {
                        Data[y][x][z] = raw[counter++];
                    }
                }
            }
        }

        public IEnumerator<BoxObject> GetEnumerator()
        {
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int z = 0; z < SizeZ; z++)
                    {
                        if (Data[y][x][z] != 0)
                        {
                            yield return BoxObjectFactory.Create<BoxObject>(new Vector3DI(x, y, z), Data[y][x][z]);
                        }
                    }
                }
            }
        }
    }
}
