Imports System.Threading
Public Class Form1
    Private Sub ButtonClearTextBoxTaskDescription_Click(sender As Object, e As EventArgs)
        TextBoxTaskDescription.Clear()
    End Sub

    Private Sub ButtonClearMoreInfo_Click(sender As Object, e As EventArgs)
        TextBoxMoreInfo.Clear()
    End Sub

    Private Sub ButtonClearTextBoxTaskPath_Click(sender As Object, e As EventArgs)
        TextBoxTaskPath.Clear()
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        TaskSettingPanel.Enabled = False
        TaskSettingPanel.Visible = False
        Me.Width = 240
        Me.Height = 262
        ButtonTimingAnimation.Enabled = True
        ButtonTimingAnimation.Visible = True
    End Sub

    Private Sub ButtonTimingAnimation_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonTimingAnimation.MouseDown
        If Not TimerButtonTimingAnimationDown.Enabled Then
            TimerButtonTimingAnimationDown.Enabled = True
        End If
    End Sub

    Private Sub ButtonTimingAnimation_MouseEnter(sender As Object, e As EventArgs) Handles ButtonTimingAnimation.MouseEnter
        ButtonTimingAnimation.Text = "STOP" & Chr(10) _
                                    & "Press Left Mouse" & Chr(10) _
                                    & "and Hold for 5 sec !"
    End Sub

    Private Sub ButtonTimingAnimation_MouseLeave(sender As Object, e As EventArgs) Handles ButtonTimingAnimation.MouseLeave
        ButtonTimingAnimation.Text = ""
    End Sub

    Private Sub TimerButtonTimingAnimationDown_Tick(sender As Object, e As EventArgs) Handles TimerButtonTimingAnimationDown.Tick
        ButtonTimingAnimationSTOP()
        TimerButtonTimingAnimationDown.Enabled = False
    End Sub

    Private Sub ButtonTimingAnimation_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonTimingAnimation.MouseUp
        If TimerButtonTimingAnimationDown.Enabled Then
            TimerButtonTimingAnimationDown.Enabled = False
        End If
    End Sub

    Private Sub ButtonTimingAnimationSTOP()
        MsgBox(TextBoxTaskDescription.Text)
        ButtonTimingAnimation.Enabled = False
        ButtonTimingAnimation.Visible = False
        Me.Width = 400
        Me.Height = 152
        TaskSettingPanel.Enabled = True
        TaskSettingPanel.Visible = True
    End Sub
End Class
