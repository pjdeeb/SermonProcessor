<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.trackLengthTxt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bitrateCB = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenMediaFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitAudioIntoTracksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractAudioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 27)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(312, 262)
        Me.TextBox1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.trackLengthTxt)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.bitrateCB)
        Me.GroupBox1.Location = New System.Drawing.Point(330, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(262, 262)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Output Settings"
        '
        'trackLengthTxt
        '
        Me.trackLengthTxt.Location = New System.Drawing.Point(103, 52)
        Me.trackLengthTxt.Name = "trackLengthTxt"
        Me.trackLengthTxt.Size = New System.Drawing.Size(100, 20)
        Me.trackLengthTxt.TabIndex = 3
        Me.trackLengthTxt.Text = "300"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Track Length (s)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Bit Rate"
        '
        'bitrateCB
        '
        Me.bitrateCB.FormattingEnabled = True
        Me.bitrateCB.Items.AddRange(New Object() {"32k", "64k", "96k", "128k", "160k", "192k", "256k"})
        Me.bitrateCB.Location = New System.Drawing.Point(103, 25)
        Me.bitrateCB.Name = "bitrateCB"
        Me.bitrateCB.Size = New System.Drawing.Size(121, 21)
        Me.bitrateCB.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(604, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenMediaFileToolStripMenuItem, Me.SplitAudioIntoTracksToolStripMenuItem, Me.ExtractAudioToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenMediaFileToolStripMenuItem
        '
        Me.OpenMediaFileToolStripMenuItem.Name = "OpenMediaFileToolStripMenuItem"
        Me.OpenMediaFileToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.OpenMediaFileToolStripMenuItem.Text = "Open Media File"
        '
        'SplitAudioIntoTracksToolStripMenuItem
        '
        Me.SplitAudioIntoTracksToolStripMenuItem.Name = "SplitAudioIntoTracksToolStripMenuItem"
        Me.SplitAudioIntoTracksToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SplitAudioIntoTracksToolStripMenuItem.Text = "Split Audio Into Tracks"
        '
        'ExtractAudioToolStripMenuItem
        '
        Me.ExtractAudioToolStripMenuItem.Name = "ExtractAudioToolStripMenuItem"
        Me.ExtractAudioToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ExtractAudioToolStripMenuItem.Text = "Extract Audio"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 301)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bitrateCB As System.Windows.Forms.ComboBox
    Friend WithEvents trackLengthTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenMediaFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitAudioIntoTracksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExtractAudioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
