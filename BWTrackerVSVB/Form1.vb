Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Public Class Form1
    Dim ExcelApp As New Excel.Application
    Dim BWTrackerWB As Excel.Workbook = ExcelApp _
        .Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) _
        & "\BWTracker.xlsx")
    Dim BWTrackerWS As Excel.Worksheet = BWTrackerWB.Worksheets("BWTracker")
    Dim KeyHierarchyWS As Excel.Worksheet = BWTrackerWB.Worksheets("KeyHierarchy")

    Private Sub SaveBWTrackerAndExitExcelApplication()
        BWTrackerWB.Save()
        BWTrackerWB.Close()
        ExcelApp.Quit()
    End Sub
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
        BWTrackerWB.Save()
        ButtonTimingAnimation.Enabled = False
        ButtonTimingAnimation.Visible = False
        Me.Width = 400
        Me.Height = 152
        TaskSettingPanel.Enabled = True
        TaskSettingPanel.Visible = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveBWTrackerAndExitExcelApplication()
    End Sub
End Class
