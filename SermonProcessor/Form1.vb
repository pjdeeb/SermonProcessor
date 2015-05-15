Imports System.IO

Public Class Form1

    Dim probe As ffprobe
    Dim converter As ffmpeg
    Dim player As ffplay
    Dim info As MediaInfo

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateExecutables()
        bitrateCB.SelectedIndex = 0
        progressTxt.Text = ""
    End Sub

    Private Sub CreateExecutables()
        probe = New ffprobe()
        converter = New ffmpeg()
        player = New ffplay()
    End Sub


    Private Sub OpenFileBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles OpenFileBtn.LinkClicked
        Dim ofd As New OpenFileDialog()
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            info = probe.GetInfo(ofd.FileName)
            durationTxt.Text = SecondsToMinutes(info.duration)
            bitrateTxt.Text = info.bitrate
            titleTxt.Text = info.title
            artistTxt.Text = info.artist
            albumTxt.Text = info.album
            genreTxt.Text = info.genre
            info.coverartpath = converter.GetCoverArt(ofd.FileName)
            PictureBox1.ImageLocation = info.coverartpath
        End If
    End Sub

    Private Function SecondsToMinutes(totalseconds As Double) As String
        Dim hours, minutes, seconds As Integer
        hours = Math.Floor(totalseconds / 3600)
        totalseconds = totalseconds - 3600 * hours
        minutes = Math.Floor(totalseconds / 60)
        totalseconds = totalseconds - 60 * minutes
        seconds = Math.Round(totalseconds)
        Return hours & "h:" & minutes & "m:" & seconds & "s"
    End Function

    Private Sub SplitAudioBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles SplitAudioBtn.LinkClicked
        info.title = titleTxt.Text
        info.artist = artistTxt.Text
        info.album = albumTxt.Text
        info.genre = genreTxt.Text
        info.coverartpath = PictureBox1.ImageLocation
        converter.SplitFile(info, trackLengthTxt.Text, bitrateCB.SelectedItem)
    End Sub

    'Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    '    Dim ofd As New OpenFileDialog()
    '    ofd.Filter = ".jpg,.png|*.jpg;*.png"
    '    If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
    '        info.coverartpath = ofd.FileName
    '        PictureBox1.ImageLocation = ofd.FileName
    '    End If
    'End Sub

    Private Sub convertBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles convertBtn.LinkClicked
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "*.mp3|*.mp3"
        sfd.InitialDirectory = Path.GetDirectoryName(info.filepath)
        If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
            info.title = titleTxt.Text
            info.artist = artistTxt.Text
            info.album = albumTxt.Text
            info.genre = genreTxt.Text
            info.coverartpath = PictureBox1.ImageLocation
            converter.ConvertFile(info, bitrateCB.SelectedItem, sfd.FileName)
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'Dim mp3 As New Id3.Mp3File(info.filepath)
        'Dim id3tag As Id3.Id3Tag = mp3.GetTag(Id3.Id3TagFamily.FileStartTag)
        'Dim pics = id3tag.Pictures
        'PictureBox2.Image = ConvertByteArrayToImage(pics(0).PictureData)
        Dim id3 As New HundredMilesSoftware.UltraID3Lib.UltraID3
        id3.Read(info.filepath)
        Dim imageFrames = id3.ID3v2Tag.Frames.GetFrames(HundredMilesSoftware.UltraID3Lib.CommonMultipleInstanceID3v2FrameTypes.Picture)
        Dim cover As HundredMilesSoftware.UltraID3Lib.ID3v2PictureFrame
        If imageFrames.Count > 0 Then
            cover = imageFrames(0)
            PictureBox2.Image = cover.Picture
        End If
    End Sub

    Private Sub selectCoverArtBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles selectCoverArtBtn.LinkClicked
        Dim ofd As New OpenFileDialog()
        ofd.Filter = ".jpg,.png|*.jpg;*.png"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.ImageLocation = ofd.FileName
            info.coverartpath = ofd.FileName
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim tagger As New ID3Tagger()
        tagger.EditID3(info.filepath, titleTxt.Text, artistTxt.Text, albumTxt.Text, genreTxt.Text, Image.FromFile(info.coverartpath))
    End Sub

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

    Public Function ConvertImageToByteArray(imagePath As String) As Byte()
        Dim img As Image = Image.FromFile(imagePath)
        Dim ms As New MemoryStream()
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        Return ms.ToArray()
    End Function

    Public Function ConvertByteArrayToImage(bytearray As Byte()) As Image
        Dim ms As New MemoryStream(bytearray)
        Dim img As Image = Image.FromStream(ms)
        Return img
    End Function

    Private Sub convertWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles convertWorker.DoWork

    End Sub

End Class
