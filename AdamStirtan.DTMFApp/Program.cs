using System.Diagnostics;

using DtmfDetection;
using DtmfDetection.NAudio;

using NAudio.Wave;

using var tones = new AudioFileReader("dtmf_tones.mp3");

Stopwatch stopwatch = Stopwatch.StartNew();

List<DtmfChange> dtmfChanges = tones.DtmfChanges();

stopwatch.Stop();

Console.WriteLine($"Detected {dtmfChanges.Count} DTMF changes");
Console.WriteLine($"- in {stopwatch.ElapsedMilliseconds} ms\n");

string buttonSequence = string.Join(" ",
    dtmfChanges.Where(x => x.IsStart).Select(key =>
    {
        int value = (int)key.Key;

        return value switch
        {
            34 => "#",
            35 => "*",
            _ => value.ToString(),
        };
    }).ToArray());

Console.WriteLine($"Buttons: {buttonSequence}");