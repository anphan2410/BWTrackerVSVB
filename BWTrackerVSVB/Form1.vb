Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office
Public Class Form1
    Private ExcelFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) _
                                    & "\BWTracker.xlsx"
    Private BWTrackerWSName As String = "BWTracker"
    Private KeyHierarchyWSName As String = "KeyHierarchy"
    Private ExcelApp As New Excel.Application
    Private BWTrackerWB As Excel.Workbook
    Private BWTrackerWS As Excel.Worksheet
    Private KeyHierarchyWS As Excel.Worksheet

    Private Function LastDataCell(ByRef AnyColumnRange As Excel.Range _
            , Optional ByVal ExactMode As Boolean = False) As Excel.Range
        Dim TmpWS As Excel.Worksheet = AnyColumnRange.Parent
        Dim HiddenRowCollection As New Microsoft.VisualBasic.Collection()
        If (ExactMode) Then
            For i As UInteger = 1 To TmpWS.UsedRange.Rows.Count
                If (TmpWS.Rows(i).EntireRow.Hidden) Then
                    HiddenRowCollection.Add(Item:=i)
                End If
            Next
            TmpWS.UsedRange.EntireRow.Hidden = False
        End If
        Dim FullColRng As Excel.Range = TmpWS.UsedRange.Columns(AnyColumnRange.Column)
        Dim TmpDataCell As Excel.Range = FullColRng.Range("A" & FullColRng.Rows.Count).End(Excel.XlDirection.xlUp)
        If (ExactMode) Then
            For Each AHiddenRow As VariantType In HiddenRowCollection
                TmpWS.Rows(DirectCast(AHiddenRow, Integer)).EntireRow.Hidden = True
            Next
        End If
        HiddenRowCollection = Nothing
        LastDataCell = TmpDataCell
    End Function

    Private Sub ButtonClearTextBoxTaskDescription_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxTaskDescription.Click
        LastDataCell(KeyHierarchyWS.Columns(2), True).Value = "test"

        TextBoxTaskDescription.Clear()
    End Sub

    Private Sub ButtonClearTextBoxTaskPath_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxTaskPath.Click
        TextBoxTaskPath.Clear()
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        If (String.IsNullOrEmpty(Trim(TextBoxTaskDescription.Text))) Then
            MsgBox("Please assign a description for this task", MsgBoxStyle.OkOnly, "Empty Task Description")
            Exit Sub
        End If
        If (String.IsNullOrEmpty(Trim(TextBoxTaskPath.Text))) Then
            MsgBox("Please assign a path for this task", MsgBoxStyle.OkOnly, "Empty Task Path")
            Exit Sub
        End If
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
                                    & "And Hold for 5 sec !"
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
        SaveBWTrackerWB()
        ButtonTimingAnimation.Enabled = False
        ButtonTimingAnimation.Visible = False
        Me.Width = 400
        Me.Height = 152
        TaskSettingPanel.Enabled = True
        TaskSettingPanel.Visible = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BWTrackerWB = ExcelApp.Workbooks.Open(ExcelFilePath)
        Dim tmp As DialogResult
        While (BWTrackerWB.ReadOnly)
            tmp = MessageBox.Show("The file is currently being used by an another program." & Chr(10) &
                    "Please make sure the file is closed, then try again !", "File Is Being Opened" _
                    , MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)
            If (tmp = DialogResult.Cancel) Then
                Application.Exit()
                Exit Sub
            End If
            BWTrackerWB = ExcelApp.Workbooks.Open(ExcelFilePath)
        End While
        BWTrackerWS = BWTrackerWB.Worksheets(BWTrackerWSName)
        KeyHierarchyWS = BWTrackerWB.Worksheets(KeyHierarchyWSName)
        'Dim KeyRng As Excel.Range = KeyHierarchyWS.Ra
        Dim TaskPathSource As New AutoCompleteStringCollection()
        TaskPathSource.Add("AscenX")
        TaskPathSource.Add("AscenX/TamIoT")
        TaskPathSource.Add("AscenX/HieuCMS")
        TaskPathSource.Add("AscenX/SonSETraining")
        TextBoxTaskPath.AutoCompleteCustomSource = TaskPathSource
        TextBoxTaskPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        TextBoxTaskPath.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (ButtonTimingAnimation.Enabled) Then
            SaveBWTrackerWB()
        End If
        BWTrackerWB.Close(SaveChanges:=False)
        ExcelApp.Quit()
    End Sub

    Private Sub SaveBWTrackerWB()

        BWTrackerWB.Save()
    End Sub

    Private Sub ButtonClearTextBoxMoreInfo_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxMoreInfo.Click
        TextBoxMoreInfo.Clear()
    End Sub
End Class
