<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gameFrm
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.whiteLbl = New System.Windows.Forms.Label()
        Me.blackLbl = New System.Windows.Forms.Label()
        Me.turnBoxPic = New System.Windows.Forms.PictureBox()
        Me.boardPic = New System.Windows.Forms.PictureBox()
        CType(Me.turnBoxPic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.boardPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(418, 211)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(117, 49)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Start New Game"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.Location = New System.Drawing.Point(418, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 70)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "OTHELLO .NET"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'whiteLbl
        '
        Me.whiteLbl.Font = New System.Drawing.Font("Arial Black", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.whiteLbl.Location = New System.Drawing.Point(474, 142)
        Me.whiteLbl.Name = "whiteLbl"
        Me.whiteLbl.Size = New System.Drawing.Size(62, 50)
        Me.whiteLbl.TabIndex = 14
        Me.whiteLbl.Text = "22"
        '
        'blackLbl
        '
        Me.blackLbl.Font = New System.Drawing.Font("Arial Black", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.blackLbl.Location = New System.Drawing.Point(474, 92)
        Me.blackLbl.Name = "blackLbl"
        Me.blackLbl.Size = New System.Drawing.Size(62, 50)
        Me.blackLbl.TabIndex = 13
        Me.blackLbl.Text = "22"
        '
        'turnBoxPic
        '
        Me.turnBoxPic.Location = New System.Drawing.Point(418, 92)
        Me.turnBoxPic.Name = "turnBoxPic"
        Me.turnBoxPic.Size = New System.Drawing.Size(50, 100)
        Me.turnBoxPic.TabIndex = 12
        Me.turnBoxPic.TabStop = False
        '
        'boardPic
        '
        Me.boardPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boardPic.Location = New System.Drawing.Point(12, 12)
        Me.boardPic.Name = "boardPic"
        Me.boardPic.Size = New System.Drawing.Size(400, 400)
        Me.boardPic.TabIndex = 11
        Me.boardPic.TabStop = False
        '
        'gameFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 422)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.whiteLbl)
        Me.Controls.Add(Me.blackLbl)
        Me.Controls.Add(Me.turnBoxPic)
        Me.Controls.Add(Me.boardPic)
        Me.Name = "gameFrm"
        Me.Text = "Form1"
        CType(Me.turnBoxPic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.boardPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents whiteLbl As System.Windows.Forms.Label
    Friend WithEvents blackLbl As System.Windows.Forms.Label
    Friend WithEvents turnBoxPic As System.Windows.Forms.PictureBox
    Friend WithEvents boardPic As System.Windows.Forms.PictureBox

End Class
