using System;
using System.Collections.Generic;

namespace flyweight
{
    public class PieceFactory
    {
        public int pieceCount;

        private Dictionary<string, Piece> pieces = new Dictionary<string, Piece>();
        public Piece GetPiece(string type)
        {
            Piece piece = null;
            if (pieces.ContainsKey(type))
            {
                piece = pieces[type];
            }
            else
            {
                switch (type)
                {
                    case "K": piece = new King(); break;
                    case "Q": piece = new Queen(); break;
                    case "R": piece = new Rook(); break;
                    case "B": piece = new Bishop(); break;
                    case "KN": piece = new Knight(); break;
                    case "P": piece = new Pawn(); break;
                }
                pieces.Add(type, piece);
                pieceCount++;
            }
            return piece;

        }
    }
}
