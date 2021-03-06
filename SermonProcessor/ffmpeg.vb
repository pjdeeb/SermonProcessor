﻿Imports System.IO

Public Class ffmpeg
    Dim ffmpegpath As String

    Public Sub New()
        ffmpegpath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffmpeg.exe")
        If Not File.Exists(ffmpegpath) Then My.Computer.FileSystem.WriteAllBytes(ffmpegpath, My.Resources.ffmpeg, False)
    End Sub

    Public Sub ConvertFile(info As MediaInfo, bitrate As String, savepath As String)
        Dim args As String = "-i " & Quotes(info.filepath) & " -id3v2_version 3 -write_id3v1 1 -y -vn -sn -c:a libmp3lame -b:a " & bitrate & " " & _
                "-metadata title=" & Quotes(info.title) & " " & _
                "-metadata artist=" & Quotes(info.artist) & " " & _
                "-metadata album=" & Quotes(info.album) & " " & _
                "-metadata date=" & Quotes(info.mediadate) & " " & _
                "-metadata genre=" & Quotes(info.genre) & " " & _
                "-metadata track=" & Quotes(info.track) & " " & _
                Quotes(savepath)
        RunFFMpeg(args)
        Dim tagger As New ID3Tagger
        tagger.EditCoverArt(savepath, Image.FromFile(info.coverartpath))
    End Sub

    Public Sub SplitFile(info As MediaInfo, tracklength As Integer, bitrate As String)
        'Dim prcs As New Process()
        'prcs.StartInfo.FileName = ffmpegpath
        'prcs.StartInfo.CreateNoWindow = True
        'prcs.StartInfo.UseShellExecute = False
        'prcs.StartInfo.RedirectStandardError = True
        'prcs.StartInfo.RedirectStandardOutput = True
        'prcs.EnableRaisingEvents = True

        Dim args As String
        Dim n As Integer = Math.Floor(info.duration / tracklength)
        Dim pathroot As String = Path.GetDirectoryName(info.filepath)
        For i As Integer = 0 To n
            Dim newname As String = Path.GetFileNameWithoutExtension(info.filepath) & "_" & i + 1 & ".mp3"
            Dim outputpath As String = Path.Combine(pathroot, newname)
            args = "-i " & Quotes(info.filepath) & " -id3v2_version 3 -write_id3v1 1 -y -vn -sn -c:a libmp3lame -b:a " & bitrate & " -ss " & tracklength * i & " -t " & tracklength & " " & _
                "-metadata title=" & Quotes(info.title) & " " & _
                "-metadata artist=" & Quotes(info.artist) & " " & _
                "-metadata album=" & Quotes(info.album) & " " & _
                "-metadata date=" & Quotes(info.mediadate) & " " & _
                "-metadata genre=" & Quotes(info.genre) & " " & _
                "-metadata track=" & i + 1 & " " & _
                Quotes(outputpath)

            '"-metadata:s:v title=" & Quotes("Album cover") & " -metadata:s:v comment=" & Quotes("Cover (Front)") & " " & _

            RunFFMpeg(args)
            Dim tagger As New ID3Tagger
            tagger.EditCoverArt(info.filepath, Image.FromFile(info.coverartpath))

            'prcs.StartInfo.Arguments = args
            'prcs.Start()
            'While Not prcs.HasExited
            '    Trace.WriteLine(args)
            '    Trace.WriteLine(prcs.StandardOutput.ReadToEnd())
            '    Trace.WriteLine(prcs.StandardError.ReadToEnd())
            'End While
        Next

    End Sub

    Private Sub AddAlbumArt(mp3path As String)
        

    End Sub

    Private Sub RunFFMpeg(args As String)
        Trace.WriteLine(args)
        Dim prcs As New Process()
        prcs.StartInfo.FileName = ffmpegpath
        'prcs.StartInfo.CreateNoWindow = True
        'prcs.StartInfo.UseShellExecute = False
        'prcs.StartInfo.RedirectStandardError = True
        'prcs.StartInfo.RedirectStandardOutput = True
        'prcs.EnableRaisingEvents = True

        prcs.StartInfo.Arguments = args
        prcs.Start()
        prcs.WaitForExit()
        'While Not prcs.HasExited
        '    Trace.WriteLine(args)
        '    Trace.WriteLine(prcs.StandardOutput.ReadToEnd())
        '    Trace.WriteLine(prcs.StandardError.ReadToEnd())
        'End While
    End Sub

    Public Function GetCoverArt(filepath As String) As String
        Dim args As String
        Dim coverartpath As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Path.GetFileNameWithoutExtension(filepath) & ".jpg")
        args = "-y -i " & Quotes(filepath) & " " & Quotes(coverartpath)
        RunFFMpeg(args)
        Threading.Thread.Sleep(200)
        If File.Exists(coverartpath) Then
            Return coverartpath
        Else
            Return Nothing
        End If
    End Function

    Public Function Quotes(input As String) As String
        Return Chr(34) & input & Chr(34)
    End Function

End Class

Public Class ffprobe
    Dim ffprobepath As String

    Public Sub New()
        ffprobepath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffprobe.exe")
        If Not File.Exists(ffprobepath) Then My.Computer.FileSystem.WriteAllBytes(ffprobepath, My.Resources.ffprobe, False)
    End Sub

    Public Function GetInfo(filename As String) As MediaInfo
        Dim info As New MediaInfo(filename)
        Try
            Dim prcs As New Process()
            Dim args As String = " -of xml -v error -show_format -show_entries format=duration -show_entries format=bit_rate -i " & Chr(34) & filename & Chr(34)
            prcs.StartInfo.FileName = ffprobepath
            prcs.StartInfo.Arguments = args

            prcs.StartInfo.CreateNoWindow = True
            prcs.StartInfo.UseShellExecute = False
            prcs.StartInfo.RedirectStandardError = True
            prcs.StartInfo.RedirectStandardOutput = True

            prcs.EnableRaisingEvents = True
            prcs.Start()

            Dim xml As XDocument
            While Not prcs.HasExited
                xml = XDocument.Parse(prcs.StandardOutput.ReadToEnd())
            End While

            info.duration = xml...<ffprobe>...<format>.@duration
            info.bitrate = xml...<ffprobe>...<format>.@bit_rate
            Dim tags = xml...<ffprobe>...<format>...<tag>

            For Each tag In tags
                Select Case tag.@key
                    Case "title"
                        info.title = tag.@value
                    Case "artist"
                        info.artist = tag.@value
                    Case "album"
                        info.album = tag.@value
                    Case "track"
                        info.track = tag.@value
                    Case "genre"
                        info.genre = tag.@value
                    Case "date"
                        info.mediadate = tag.@value
                End Select
            Next

        Catch ex As Exception

        End Try

        Return info
    End Function

    

End Class

Public Class ffplay
    Dim ffplaypath As String

    Public Sub New()
        ffplaypath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffplay.exe")
        If Not File.Exists(ffplaypath) Then My.Computer.FileSystem.WriteAllBytes(ffplaypath, My.Resources.ffplay, False)
    End Sub

End Class

Public Class MediaInfo
    Public duration As Double
    Public bitrate As Double
    Public title As String
    Public artist As String
    Public album As String
    Public track As String
    Public genre As String
    Public mediadate As String
    Public width As Integer
    Public height As Integer
    Public framerate As Double
    Public filepath As String
    Public coverartpath As String

    Public Sub New(_filepath As String)
        filepath = _filepath
    End Sub

End Class

Public Class ID3Tagger

    Public Sub EditID3(mp3path As String, title As String, artist As String, album As String, genre As String, coverart As Image)
        Dim id3 As New HundredMilesSoftware.UltraID3Lib.UltraID3
        id3.Read(mp3path)
        id3.Title = title
        id3.Artist = artist
        id3.Album = album
        id3.Genre = genre
        id3.ID3v2Tag.Frames.Remove(HundredMilesSoftware.UltraID3Lib.CommonID3v2FrameTypes.Picture)
        Dim cover As New HundredMilesSoftware.UltraID3Lib.ID3v23PictureFrame(coverart, HundredMilesSoftware.UltraID3Lib.PictureTypes.CoverFront, "", HundredMilesSoftware.UltraID3Lib.TextEncodingTypes.ISO88591)
        id3.ID3v2Tag.Frames.Add(cover)
        id3.Write()
    End Sub

    Public Sub EditCoverArt(mp3path As String, coverart As Image)
        Dim id3 As New HundredMilesSoftware.UltraID3Lib.UltraID3
        id3.Read(mp3path)
        id3.ID3v2Tag.Frames.Remove(HundredMilesSoftware.UltraID3Lib.CommonID3v2FrameTypes.Picture)
        Dim cover As New HundredMilesSoftware.UltraID3Lib.ID3v23PictureFrame(coverart, HundredMilesSoftware.UltraID3Lib.PictureTypes.CoverFront, "", HundredMilesSoftware.UltraID3Lib.TextEncodingTypes.ISO88591)
        id3.ID3v2Tag.Frames.Add(cover)
        id3.Write()
    End Sub

End Class