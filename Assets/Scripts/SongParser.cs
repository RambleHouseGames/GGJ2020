using System.Text.RegularExpressions;
using UnityEngine;

public static class SongParser {
    public static BeatType[] ParseSong(TextAsset song) {
        string songText = Regex.Replace(song.text, @"\t|\n|\r", "");
        BeatType[] result = new BeatType[songText.Length + 1];
        result[0] = BeatType.Normal;
        for (int i = 1; i < songText.Length + 1; i++) {
            char beatChar = songText[i-1];
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