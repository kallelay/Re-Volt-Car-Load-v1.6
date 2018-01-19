<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class startfromhere
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Label3.AutoEllipsis = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.CausesValidation = False
        Me.Label3.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label3.Location = New System.Drawing.Point(12, 205)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(382, 23)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Loading..."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(9, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 23)
        Me.Label2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Label1.AutoEllipsis = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.CausesValidation = False
        Me.Label1.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(131, 182)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(263, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Loading..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.Label4.AutoEllipsis = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.CausesValidation = False
        Me.Label4.Font = New System.Drawing.Font("Calibri", 11.25!)
        Me.Label4.Location = New System.Drawing.Point(-53, -27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(476, 266)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Loading..."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'startfromhere
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.Car_Load.My.Resources.Resources.car_load
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(420, 248)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "startfromhere"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "startfromhere"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class
