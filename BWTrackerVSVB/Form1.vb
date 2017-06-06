Imports Excel = Microsoft.Office.Interop.Excel
Imports MSOffice = Microsoft.Office.Core
Imports MSVBCollection = Microsoft.VisualBasic.Collection
Public Class Form1
    Private ExcelFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) _
                                    & "\BWTracker.xlsx"
    Private BWTrackerWSName As String = "BWTracker"
    Private KeyHierarchyWSName As String = "KeyHierarchy"
    Private ExcelApp As New Excel.Application
    Private BWTrackerWB As Excel.Workbook
    Private BWTrackerWS As Excel.Worksheet
    Private KeyHierarchyWS As Excel.Worksheet
    Private TaskPathCollection As New MSVBCollection

    Private Sub StoreNumbersOfHiddenRowsIntoACollection(ASheet As Excel.Worksheet, ByRef DestinationCollection As MSVBCollection)
        For i As Integer = 1 To ASheet.UsedRange.Rows.Count
            If (ASheet.Rows(i).EntireRow.Hidden) Then
                DestinationCollection.Add(Item:=i)
            End If
        Next
    End Sub

    Private Sub RestoreHiddenPropertyOfRowsFromACollection(ASheet As Excel.Worksheet, SourceCollection As MSVBCollection)
        For Each AHiddenRow As VariantType In SourceCollection
            ASheet.Rows(DirectCast(AHiddenRow, Integer)).EntireRow.Hidden = True
        Next
    End Sub

    Private Sub StoreNumbersOfHiddenColumnsIntoACollection(ASheet As Excel.Worksheet, ByRef DestinationCollection As MSVBCollection)
        For i As Integer = 1 To ASheet.UsedRange.Columns.Count
            If (ASheet.Columns(i).EntireColumn.Hidden) Then
                DestinationCollection.Add(Item:=i)
            End If
        Next
    End Sub

    Private Sub RestoreHiddenPropertyOfColumnsFromACollection(ASheet As Excel.Worksheet, SourceCollection As MSVBCollection)
        For Each AHiddenColumn As VariantType In SourceCollection
            ASheet.Columns(DirectCast(AHiddenColumn, Integer)).EntireColumn.Hidden = True
        Next
    End Sub

    Private Function LastDataCellOfAColumn(ARangeOfASingleColumn As Excel.Range,
                                           Optional ByVal PureDataMode As Boolean = False) As Excel.Range
        Dim TmpWS As Excel.Worksheet = ARangeOfASingleColumn.Parent
        Dim HiddenRowCollection As New MSVBCollection
        If (PureDataMode) Then
            StoreNumbersOfHiddenRowsIntoACollection(TmpWS, HiddenRowCollection)
            TmpWS.UsedRange.EntireRow.Hidden = False
        End If
        Dim FullColRng As Excel.Range = TmpWS.UsedRange.Columns(ARangeOfASingleColumn.Column)
        LastDataCellOfAColumn = FullColRng.Range("A" & (FullColRng.Rows.Count + 1)).End(Excel.XlDirection.xlUp)
        If (PureDataMode) Then
            RestoreHiddenPropertyOfRowsFromACollection(TmpWS, HiddenRowCollection)
        End If
        HiddenRowCollection.Clear()
        HiddenRowCollection = Nothing
    End Function

    Private Function LastDataCellOfARow(ARangeOfASingleRow As Excel.Range,
                                        Optional ByVal PureDataMode As Boolean = False) As Excel.Range
        Dim TmpWS As Excel.Worksheet = ARangeOfASingleRow.Parent
        Dim HiddenColumnCollection As New MSVBCollection
        If (PureDataMode) Then
            StoreNumbersOfHiddenColumnsIntoACollection(TmpWS, HiddenColumnCollection)
            TmpWS.UsedRange.EntireColumn.Hidden = False
        End If
        LastDataCellOfARow = DirectCast(TmpWS.Cells(ARangeOfASingleRow.Row,
                                                    TmpWS.UsedRange.Columns.Count + 1),
                                                    Excel.Range).
                                                    End(Excel.XlDirection.xlToLeft)
        If (PureDataMode) Then
            RestoreHiddenPropertyOfColumnsFromACollection(TmpWS, HiddenColumnCollection)
        End If
        HiddenColumnCollection.Clear()
        HiddenColumnCollection = Nothing
    End Function

    Private Function HeaderCell(ASheet As Excel.Worksheet,
                                Optional UniqueKey As String = Nothing,
                                Optional IsBold As Boolean = True,
                                Optional AccuracyPercent As Double = 1,
                                Optional PureDataMode As Boolean = False) As Excel.Range
        Dim HiddenRowCollection As New MSVBCollection
        Dim HiddenColumnCollection As New MSVBCollection
        Dim HeaderCellIsSet As Boolean = False
        If (PureDataMode) Then
            StoreNumbersOfHiddenRowsIntoACollection(ASheet, HiddenRowCollection)
            StoreNumbersOfHiddenColumnsIntoACollection(ASheet, HiddenColumnCollection)
            ASheet.UsedRange.EntireRow.Hidden = False
            ASheet.UsedRange.EntireColumn.Hidden = False
        End If
        If ((UniqueKey IsNot Nothing) And (UniqueKey <> "")) Then
            HeaderCell = ASheet.UsedRange.Find(UniqueKey,
                                               LookIn:=Excel.XlFindLookIn.xlValues,
                                               SearchOrder:=Excel.XlSearchOrder.xlByRows,
                                               SearchDirection:=Excel.XlSearchDirection.xlNext,
                                               MatchCase:=True)
        Else
            Dim LastDataRowOfFirstColumn As Integer = LastDataCellOfAColumn(ASheet.Columns(1)).Row
            Dim maxNumbersOfBoldCellsInARow As Integer = 1
            Dim tmpHeaderRowNumber As Integer = LastDataRowOfFirstColumn
            Dim PercentMatched As Double = 0
            If (IsBold) Then
                For i As Integer = 1 To LastDataRowOfFirstColumn Step 1
                    If (DirectCast(ASheet.Cells(i, 1), Excel.Range).Font.Bold) Then
                        Dim count As Integer = 1
                        Dim LastDataColumnOfCurrentRow = LastDataCellOfARow(ASheet.Rows(i)).Column
                        For j As Integer = 2 To LastDataColumnOfCurrentRow Step 1
                            If (DirectCast(ASheet.Cells(i, j), Excel.Range).Font.Bold) Then
                                count += 1
                            End If
                        Next
                        PercentMatched = count / LastDataColumnOfCurrentRow
                        If (PercentMatched >= AccuracyPercent) Then
                            HeaderCell = DirectCast(ASheet.Cells(i, LastDataColumnOfCurrentRow), Excel.Range)
                            HeaderCellIsSet = True
                            Exit For
                        End If
                        If (count > maxNumbersOfBoldCellsInARow) Then
                            maxNumbersOfBoldCellsInARow = count
                            tmpHeaderRowNumber = i
                        End If
                    End If
                Next
                If (Not HeaderCellIsSet) Then
                    HeaderCell = DirectCast(ASheet.Cells(tmpHeaderRowNumber,
                                                         LastDataCellOfARow(ASheet.Rows(tmpHeaderRowNumber)).Column), Excel.Range)
                End If
            End If
        End If
        If (PureDataMode) Then
            RestoreHiddenPropertyOfRowsFromACollection(ASheet, HiddenRowCollection)
            RestoreHiddenPropertyOfColumnsFromACollection(ASheet, HiddenColumnCollection)
        End If
    End Function

    Private Sub ButtonClearTextBoxTaskDescription_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxTaskDescription.Click
        'MsgBox(KeyHierarchyWS.UsedRange.Find(TextBoxTaskDescription.Text, LookIn:=Excel.XlFindLookIn.xlValues).Address)
        MsgBox(HeaderCell(BWTrackerWS).Address)
        TextBoxTaskDescription.Clear()
    End Sub

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        If (String.IsNullOrEmpty(Trim(TextBoxTaskDescription.Text))) Then
            MsgBox("Please assign a description for this task", MsgBoxStyle.OkOnly, "Empty Task Description")
            Exit Sub
        End If
        If (String.IsNullOrEmpty(Trim(ComboBoxTaskPath.Text))) Then
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
        Dim HeaderCell_key As Excel.Range = HeaderCell(KeyHierarchyWS, "key")
        Dim HeaderCell_parentkey As Excel.Range = HeaderCell(KeyHierarchyWS, "parent key")
        Dim KeyRng1 As Excel.Range = KeyHierarchyWS.Range(KeyHierarchyWS.Cells(HeaderCell_key.Row + 1, HeaderCell_key.Column),
                                                          LastDataCellOfAColumn(KeyHierarchyWS.Columns(HeaderCell_key.Column)))
        Dim ParentOfKeyRng1 As Excel.Range = KeyHierarchyWS.Range(KeyHierarchyWS.Cells(HeaderCell_parentkey.Row + 1, HeaderCell_parentkey.Column),
                                                          LastDataCellOfAColumn(KeyHierarchyWS.Columns(HeaderCell_parentkey.Column)))
        Dim currentRowNumber As Integer
        Do

        Loop Until ParentOfKeyRng1
        'Dim TaskPathSource As New AutoCompleteStringCollection()
        'TaskPathSource.Add("AscenX")
        'TaskPathSource.Add("AscenX/TamIoT")
        'TaskPathSource.Add("AscenX/HieuCMS")
        'TaskPathSource.Add("AscenX/SonSETraining")
        'ComboBoxTaskPath.AutoCompleteCustomSource = TaskPathSource
        'ComboBoxTaskPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        'ComboBoxTaskPath.AutoCompleteSource = AutoCompleteSource.CustomSource
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

    Private Sub ButtonClearComboBoxTaskPath_Click(sender As Object, e As EventArgs) Handles ButtonClearComboBoxTaskPath.Click
        ComboBoxTaskPath.ResetText()
    End Sub
End Class
