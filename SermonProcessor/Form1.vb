Imports System.IO

Public Class Form1

    'Dim ffmpegPath, ffplayPath, ffprobePath As String
    Dim probe As ffprobe
    Dim converter As ffmpeg
    Dim player As ffplay
    Dim info As MediaInfo

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateExecutables()
        bitrateCB.SelectedIndex = 0
    End Sub

    Private Sub CreateExecutables()
        probe = New ffprobe()
        converter = New ffmpeg()
        player = New ffplay()
        'ffmpegPath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffmpeg.exe")
        'ffplayPath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffplay.exe")
        'ffprobePath = Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "ffprobe.exe")
        'If Not File.Exists(ffmpegPath) Then My.Computer.FileSystem.WriteAllBytes(ffmpegPath, My.Resources.ffmpeg, False)
        'If Not File.Exists(ffplayPath) Then My.Computer.FileSystem.WriteAllBytes(ffplayPath, My.Resources.ffplay, False)
        'If Not File.Exists(ffprobePath) Then My.Computer.FileSystem.WriteAllBytes(ffprobePath, My.Resources.ffprobe, False)
    End Sub

    'Private Sub saveAudioBtn_Click(sender As Object, e As EventArgs) Handles saveAudioBtn.Click
    '    Dim ofd As New OpenFileDialog()
    '    If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
    '        Dim prcs As New Process()
    '        Try
    '            Dim args As String = " -of xml -v error -show_format -show_entries format=duration -show_entries format=bit_rate -i " & Chr(34) & ofd.FileName & Chr(34)
    '            prcs.StartInfo.FileName = ffprobePath
    '            prcs.StartInfo.Arguments = args

    '            prcs.StartInfo.CreateNoWindow = True
    '            prcs.StartInfo.UseShellExecute = False
    '            prcs.StartInfo.RedirectStandardError = True
    '            prcs.StartInfo.RedirectStandardOutput = True

    '            prcs.EnableRaisingEvents = True
    '            prcs.Start()

    '            Dim xml As XDocument
    '            While Not prcs.HasExited
    '                xml = XDocument.Parse(prcs.StandardOutput.ReadToEnd())
    '            End While

    '            Dim duration As Double = xml...<ffprobe>...<format>.@duration
    '            TextBox1.Text = "Duration: " & duration

    '            prcs.Close()
    '        Catch ex As Exception
    '            MsgBox(ex.ToString)
    '            prcs.Close()
    '        End Try
    '    End If
    'End Sub

    Private Sub prcsOutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        AddActivity(e.Data, TextBox1, True)
    End Sub

    Private Delegate Sub UpdateDelegate(ByVal msg As String, ByVal ctl As Control, ByVal append As Boolean)
    Private Sub AddActivity(ByVal message As String, ByVal control As Control, ByVal append As Boolean)
        If control.InvokeRequired Then
            Dim args() = {message, control, append}
            control.Invoke(New UpdateDelegate(AddressOf SendUpdate), args)
        Else
            If append Then
                control.Text = LimitLength(message & vbNewLine & control.Text)
            Else
                control.Text = message
            End If
        End If
    End Sub

    Private Sub SendUpdate(ByVal msg As String, ByVal ctl As Control, ByVal append As Boolean)
        If append Then
            ctl.Text = LimitLength(msg & vbNewLine & ctl.Text)
        Else
            ctl.Text = msg
        End If
    End Sub

    Private Function LimitLength(ByVal input As String) As String
        If input.Length > 1000 Then
            input = input.Substring(0, 1000)
        End If
        Return input
    End Function

    Private Sub OpenFileBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles OpenFileBtn.LinkClicked
        Dim ofd As New OpenFileDialog()
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            info = probe.GetInfo(ofd.FileName)
            durationTxt.Text = info.duration
            bitrateTxt.Text = info.bitrate
            titleTxt.Text = info.title
            artistTxt.Text = info.artist
            albumTxt.Text = info.album
            genreTxt.Text = info.genre
            dateTxt.Text = info.mediadate

            PictureBox1.Image = converter.GetCoverArt(ofd.FileName)
            'TextBox1.Text = "Duration: " & info.duration & vbNewLine & _
            '    "Bit Rate: " & info.bitrate & vbNewLine & _
            '    "Title: " & info.title & vbNewLine & _
            '    "Artist: " & info.artist & vbNewLine & _
            '    "Album: " & info.album & vbNewLine & _
            '    "Track: " & info.track & vbNewLine & _
            '    "Genre: " & info.genre & vbNewLine & _
            '    "Date: " & info.mediadate
        End If
    End Sub

    Private Sub SplitAudioBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles SplitAudioBtn.LinkClicked
        converter.SplitFile(info, trackLengthTxt.Text, bitrateCB.SelectedItem)
    End Sub

End Class
