<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Me.components = New System.ComponentModel.Container
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me._Big = New System.Windows.Forms.Label
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me._small = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        '_Big
        '
        Me._Big.BackColor = System.Drawing.Color.Transparent
        Me._Big.Font = New System.Drawing.Font("Calibri", 27.75!, System.Drawing.FontStyle.Bold)
        Me._Big.ForeColor = System.Drawing.Color.Black
        Me._Big.Location = New System.Drawing.Point(-6, 62)
        Me._Big.Name = "_Big"
        Me._Big.Size = New System.Drawing.Size(487, 43)
        Me._Big.TabIndex = 2
        Me._Big.Text = "Label1"
        Me._Big.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer2
        '
        Me.Timer2.Interval = 1500
        '
        '_small
        '
        Me._small.BackColor = System.Drawing.Color.Transparent
        Me._small.Font = New System.Drawing.Font("Consolas", 18.0!)
        Me._small.ForeColor = System.Drawing.Color.Black
        Me._small.Location = New System.Drawing.Point(-6, 121)
        Me._small.Name = "_small"
        Me._small.Size = New System.Drawing.Size(487, 94)
        Me._small.TabIndex = 3
        Me._small.Text = "Label1"
        Me._small.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Car_Load.My.Resources.Resources.car_load
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(474, 284)
        Me.Controls.Add(Me._small)
        Me.Controls.Add(Me._Big)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "About"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "About"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents _Big As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents _small As System.Windows.Forms.Label
End Class
