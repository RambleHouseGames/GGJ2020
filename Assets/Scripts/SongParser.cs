using System.Text.RegularExpressions;
using UnityEngine;

public static class SongParser {
    private const int ADJUST = 3;
    public static BeatType[] ParseSong(TextAsset song) {
        string songText = Regex.Replace(song.text, @"\t|\n|\r", "");
        BeatType[] result = new BeatType[songText.Length + ADJUST];
        for (int i = 0; i < ADJUST; i++) {
            result[i] = BeatType.Normal;
        }
        for (int i = ADJUST; i < songText.Length + ADJUST; i++) {
            char beatChar = songText[i- ADJUST];
            result[i] = BeatTypeForCharacter(beatChar);
        }
        return result;
    }

    private static BeatType BeatTypeForCharacter(char beatChar) {
        switch (beatChar) {
            case '-':
                return BeatType.Normal;
            case 'r':
            case 'R':
                return BeatType.RightBend;
            case 'l':
            case 'L':
                return BeatType.LeftBend;
            default:
                return BeatType.Normal;
        }
    }
}

public enum BeatType {
    Normal,
    LeftBend,
    RightBend
}