<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ButtonTimingAnimation = New System.Windows.Forms.Button()
        Me.TaskSettingPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonClearTextBoxTaskPath = New System.Windows.Forms.Button()
        Me.TextBoxTaskPath = New System.Windows.Forms.TextBox()
        Me.LabelTaskPath = New System.Windows.Forms.Label()
        Me.ButtonClearTextBoxMoreInfo = New System.Windows.Forms.Button()
        Me.TextBoxMoreInfo = New System.Windows.Forms.TextBox()
        Me.LabelMoreInfo = New System.Windows.Forms.Label()
        Me.LabelTaskDescription = New System.Windows.Forms.Label()
        Me.TextBoxTaskDescription = New System.Windows.Forms.TextBox()
        Me.ButtonClearTextBoxTaskDescription = New System.Windows.Forms.Button()
        Me.ButtonStart = New System.Windows.Forms.Button()
        Me.TimerButtonTimingAnimationDown = New System.Windows.Forms.Timer(Me.components)
        Me.TaskSettingPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonTimingAnimation
        '
        Me.ButtonTimingAnimation.Enabled = False
        Me.ButtonTimingAnimation.Image = Global.BWTrackerVSVB.My.Resources.Resources.TimingAnimation
        Me.ButtonTimingAnimation.Location = New System.Drawing.Point(13, 13)
        Me.ButtonTimingAnimation.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonTimingAnimation.Name = "ButtonTimingAnimation"
        Me.ButtonTimingAnimation.Size = New System.Drawing.Size(200, 200)
        Me.ButtonTimingAnimation.TabIndex = 0
        Me.ButtonTimingAnimation.UseVisualStyleBackColor = True
        Me.ButtonTimingAnimation.Visible = False
        '
        'TaskSettingPanel
        '
        Me.TaskSettingPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskSettingPanel.AutoScroll = True
        Me.TaskSettingPanel.ColumnCount = 16
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TaskSettingPanel.Controls.Add(Me.ButtonClearTextBoxTaskPath, 16, 1)
        Me.TaskSettingPanel.Controls.Add(Me.TextBoxTaskPath, 1, 1)
        Me.TaskSettingPanel.Controls.Add(Me.LabelTaskPath, 0, 1)
        Me.TaskSettingPanel.Controls.Add(Me.ButtonClearTextBoxMoreInfo, 15, 2)
        Me.TaskSettingPanel.Controls.Add(Me.TextBoxMoreInfo, 3, 2)
        Me.TaskSettingPanel.Controls.Add(Me.LabelMoreInfo, 0, 2)
        Me.TaskSettingPanel.Controls.Add(Me.LabelTaskDescription, 0, 0)
        Me.TaskSettingPanel.Controls.Add(Me.TextBoxTaskDescription, 3, 0)
        Me.TaskSettingPanel.Controls.Add(Me.ButtonClearTextBoxTaskDescription, 15, 0)
        Me.TaskSettingPanel.Controls.Add(Me.ButtonStart, 6, 3)
        Me.TaskSettingPanel.Location = New System.Drawing.Point(13, 13)
        Me.TaskSettingPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.TaskSettingPanel.Name = "TaskSettingPanel"
        Me.TaskSettingPanel.RowCount = 4
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TaskSettingPanel.Size = New System.Drawing.Size(360, 90)
        Me.TaskSettingPanel.TabIndex = 10
        '
        'ButtonClearTextBoxTaskPath
        '
        Me.ButtonClearTextBoxTaskPath.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClearTextBoxTaskPath.Location = New System.Drawing.Point(330, 22)
        Me.ButtonClearTextBoxTaskPath.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonClearTextBoxTaskPath.Name = "ButtonClearTextBoxTaskPath"
        Me.ButtonClearTextBoxTaskPath.Size = New System.Drawing.Size(30, 22)
        Me.ButtonClearTextBoxTaskPath.TabIndex = 14
        Me.ButtonClearTextBoxTaskPath.Text = "X"
        Me.ButtonClearTextBoxTaskPath.UseVisualStyleBackColor = True
        '
        'TextBoxTaskPath
        '
        Me.TextBoxTaskPath.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskSettingPanel.SetColumnSpan(Me.TextBoxTaskPath, 12)
        Me.TextBoxTaskPath.Location = New System.Drawing.Point(66, 22)
        Me.TextBoxTaskPath.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxTaskPath.Name = "TextBoxTaskPath"
        Me.TextBoxTaskPath.Size = New System.Drawing.Size(264, 21)
        Me.TextBoxTaskPath.TabIndex = 13
        '
        'LabelTaskPath
        '
        Me.LabelTaskPath.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTaskPath.AutoSize = True
        Me.TaskSettingPanel.SetColumnSpan(Me.LabelTaskPath, 3)
        Me.LabelTaskPath.Location = New System.Drawing.Point(0, 22)
        Me.LabelTaskPath.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelTaskPath.Name = "LabelTaskPath"
        Me.LabelTaskPath.Size = New System.Drawing.Size(66, 22)
        Me.LabelTaskPath.TabIndex = 12
        Me.LabelTaskPath.Text = "Task Path"
        '
        'ButtonClearTextBoxMoreInfo
        '
        Me.ButtonClearTextBoxMoreInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClearTextBoxMoreInfo.Location = New System.Drawing.Point(330, 44)
        Me.ButtonClearTextBoxMoreInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonClearTextBoxMoreInfo.Name = "ButtonClearTextBoxMoreInfo"
        Me.ButtonClearTextBoxMoreInfo.Size = New System.Drawing.Size(30, 22)
        Me.ButtonClearTextBoxMoreInfo.TabIndex = 11
        Me.ButtonClearTextBoxMoreInfo.Text = "X"
        Me.ButtonClearTextBoxMoreInfo.UseVisualStyleBackColor = True
        '
        'TextBoxMoreInfo
        '
        Me.TextBoxMoreInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskSettingPanel.SetColumnSpan(Me.TextBoxMoreInfo, 12)
        Me.TextBoxMoreInfo.Location = New System.Drawing.Point(66, 44)
        Me.TextBoxMoreInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxMoreInfo.Name = "TextBoxMoreInfo"
        Me.TextBoxMoreInfo.Size = New System.Drawing.Size(264, 21)
        Me.TextBoxMoreInfo.TabIndex = 10
        '
        'LabelMoreInfo
        '
        Me.LabelMoreInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMoreInfo.AutoSize = True
        Me.TaskSettingPanel.SetColumnSpan(Me.LabelMoreInfo, 3)
        Me.LabelMoreInfo.Location = New System.Drawing.Point(0, 44)
        Me.LabelMoreInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelMoreInfo.Name = "LabelMoreInfo"
        Me.LabelMoreInfo.Size = New System.Drawing.Size(66, 22)
        Me.LabelMoreInfo.TabIndex = 9
        Me.LabelMoreInfo.Text = "More Info"
        '
        'LabelTaskDescription
        '
        Me.LabelTaskDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTaskDescription.AutoSize = True
        Me.TaskSettingPanel.SetColumnSpan(Me.LabelTaskDescription, 3)
        Me.LabelTaskDescription.Location = New System.Drawing.Point(0, 0)
        Me.LabelTaskDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelTaskDescription.Name = "LabelTaskDescription"
        Me.LabelTaskDescription.Size = New System.Drawing.Size(66, 22)
        Me.LabelTaskDescription.TabIndex = 0
        Me.LabelTaskDescription.Text = "Task"
        '
        'TextBoxTaskDescription
        '
        Me.TextBoxTaskDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskSettingPanel.SetColumnSpan(Me.TextBoxTaskDescription, 12)
        Me.TextBoxTaskDescription.Location = New System.Drawing.Point(66, 0)
        Me.TextBoxTaskDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.TextBoxTaskDescription.Name = "TextBoxTaskDescription"
        Me.TextBoxTaskDescription.Size = New System.Drawing.Size(264, 21)
        Me.TextBoxTaskDescription.TabIndex = 1
        '
        'ButtonClearTextBoxTaskDescription
        '
        Me.ButtonClearTextBoxTaskDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClearTextBoxTaskDescription.Location = New System.Drawing.Point(330, 0)
        Me.ButtonClearTextBoxTaskDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonClearTextBoxTaskDescription.Name = "ButtonClearTextBoxTaskDescription"
        Me.ButtonClearTextBoxTaskDescription.Size = New System.Drawing.Size(30, 22)
        Me.ButtonClearTextBoxTaskDescription.TabIndex = 2
        Me.ButtonClearTextBoxTaskDescription.Text = "X"
        Me.ButtonClearTextBoxTaskDescription.UseVisualStyleBackColor = True
        '
        'ButtonStart
        '
        Me.ButtonStart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TaskSettingPanel.SetColumnSpan(Me.ButtonStart, 4)
        Me.ButtonStart.Location = New System.Drawing.Point(132, 66)
        Me.ButtonStart.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonStart.Name = "ButtonStart"
        Me.ButtonStart.Size = New System.Drawing.Size(88, 22)
        Me.ButtonStart.TabIndex = 15
        Me.ButtonStart.Text = "START"
        Me.ButtonStart.UseVisualStyleBackColor = True
        '
        'TimerButtonTimingAnimationDown
        '
        Me.TimerButtonTimingAnimationDown.Interval = 5000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 114)
        Me.Controls.Add(Me.TaskSettingPanel)
        Me.Controls.Add(Me.ButtonTimingAnimation)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form1"
        Me.Text = "BWTracker"
        Me.TaskSettingPanel.ResumeLayout(False)
        Me.TaskSettingPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ButtonTimingAnimation As Button
    Friend WithEvents TaskSettingPanel As TableLayoutPanel
    Friend WithEvents ButtonClearTextBoxTaskPath As Button
    Friend WithEvents TextBoxTaskPath As TextBox
    Friend WithEvents LabelTaskPath As Label
    Friend WithEvents ButtonClearTextBoxMoreInfo As Button
    Friend WithEvents TextBoxMoreInfo As TextBox
    Friend WithEvents LabelMoreInfo As Label
    Friend WithEvents LabelTaskDescription As Label
    Friend WithEvents TextBoxTaskDescription As TextBox
    Friend WithEvents ButtonClearTextBoxTaskDescription As Button
    Friend WithEvents ButtonStart As Button
    Friend WithEvents TimerButtonTimingAnimationDown As Timer
End Class
