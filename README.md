# What is DTMF?

**Dual-tone multi-frequency signaling (DTMF)** is a telecommunication signaling system using the voice-frequency band over telephone lines between telephone equipment and other communications devices and switching centers.[1] DTMF was first developed in the Bell System in the United States, and became known under the trademark Touch-Tone for use in push-button telephones supplied to telephone customers, starting in 1963. DTMF is standardized as ITU-T Recommendation Q.23.[2] It is also known in the UK as MF4.

# tl;dr

The process of translating a button pushed on a telephone to its value.

## Running the app

`dotnet run`

## Output

```
Detected 30 DTMF changes
- in 181 ms

Buttons: 1 2 3 4 5 6 7 7 8 9 * * 0 # 1
```

# Show me the code!

```csharp
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
```
