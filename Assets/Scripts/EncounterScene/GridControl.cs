using System;
using UnityEngine;

namespace EncounterScene
{
    public static class GridControl
    {
        public static Vector2 GetPlateCoordinates(Vector2 plateCoord, Vector2 size, int plateSize)
        {
            float x = (float) (plateCoord.x - size.x / 2 - 0.5) * plateSize;
            float y = (float) (plateCoord.y - size.y / 2 - 0.5) * plateSize;

            return new Vector2(x, y);
        }
        
        public static Vector2 GetPlateByCoordinates(Vector2 coord, Vector2 size, int plateSize)
        {
            float xPlate = coord.x / plateSize + size.x / 2 + 0.5f;
            float yPlate = coord.y / plateSize + size.y / 2 + 0.5f;

            return new Vector2(xPlate, yPlate);
        }

        public static Vector2 MapToGridCoordinates(Vector2 coord, Vector2 size, float plateSize)
        {
            float X = (float)(size.x % 2 != 0
                ? Math.Round(coord.x / plateSize) * plateSize
                : (Math.Round(coord.x / plateSize - 0.5) + 0.5) * plateSize);
            float Y = (float)(size.y % 2 != 0
                ? Math.Round(coord.y / plateSize) * plateSize
                : (Math.Round(coord.y / plateSize - 0.5) + 0.5) * plateSize);

            float xMaxAbs = (float)(size.x % 2 == 0 ? (size.x / 2 - 0.5) * plateSize : size.x / 2 * plateSize);
            float yMaxAbs = (float)(size.y % 2 == 0 ? (size.y / 2 - 0.5) * plateSize : size.y / 2 * plateSize);

            return new Vector2(Mathf.Clamp(X, -xMaxAbs, xMaxAbs), Mathf.Clamp(Y, -yMaxAbs, yMaxAbs));
        }
    }
}
