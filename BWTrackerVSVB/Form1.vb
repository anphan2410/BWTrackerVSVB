Imports Excel = Microsoft.Office.Interop.Excel
Imports MSOffice = Microsoft.Office.Core
Imports MSVBCollection = Microsoft.VisualBasic.Collection 'A Collection With Base Zero
Imports StringCollection = System.Collections.Specialized.StringCollection
Public Class Form1
    Private ConfigFilePath As String = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory, "BWTrackerVSVB.conf")
    Private BWTrackerExcelFilePath As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BWTracker.xlsx")
    Private ExcelApp As New Excel.Application
    Private BWTrackerWB As Excel.Workbook
    Private BWTrackerWS As Excel.Worksheet
    Private KeyHierarchyWS As Excel.Worksheet
    Private TaskPathCollection As New StringCollection
    Private TaskPathKeyInOneStringForFastKeyScan As String
    Private currentComboBoxTaskPathTabBiasDirection As Integer = 1
    Private HeaderCell_key As Excel.Range
    Private HeaderCell_parentkey As Excel.Range
    Private KeyRng1 As Excel.Range
    Private BWTrackerWSHeaderCellCollection As New MSVBCollection
    Private currentRowInBWTrackerWS As Integer
    Private lastStartingMoment As Date
    Private lastStopMoment As Date
    Private lastTaskDuration As TimeSpan
    Dim todayTimeSpan As TimeSpan
    Dim thisWeekTimeSpan As TimeSpan

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
                                               LookAt:=Excel.XlLookAt.xlWhole,
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
            Else
                HeaderCell = Nothing
            End If
        End If
        If (PureDataMode) Then
            RestoreHiddenPropertyOfRowsFromACollection(ASheet, HiddenRowCollection)
            RestoreHiddenPropertyOfColumnsFromACollection(ASheet, HiddenColumnCollection)
        End If
    End Function

    Private Sub ButtonClearTextBoxTaskDescription_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxTaskDescription.Click
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
        currentRowInBWTrackerWS = LastDataCellOfAColumn(BWTrackerWS.Columns(BWTrackerWSHeaderCellCollection.Item("task description").Column), True).Row + 1
        lastStartingMoment = Now
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("day").Column).Value _
            = lastStartingMoment.DayOfWeek.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("date").Column).Value _
            = lastStartingMoment.Day.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("month").Column).Value _
            = MonthName(lastStartingMoment.Month)
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("year").Column).Value _
            = lastStartingMoment.Year.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("week").Column).Value _
            = DateDiff("ww", DateSerial(Today.Year, 1, 1), lastStartingMoment, FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1).ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("start hour").Column).Value _
            = lastStartingMoment.Hour.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("start min").Column).Value _
            = lastStartingMoment.Minute.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("start sec").Column).Value _
            = lastStartingMoment.Second.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("task description").Column).Value _
            = TextBoxTaskDescription.Text.Trim
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("more information").Column).Value _
           = TextBoxMoreInfo.Text.Trim
        Dim TaskPath As String() = ComboBoxTaskPath.Text.Split("/".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
        For i As Integer = 0 To (TaskPath.Count - 1) Step 1
            BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item(i.ToString).Column).Value _
                = TaskPath.ElementAt(i)
        Next
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
        todayTimeSpan += lastTaskDuration
        thisWeekTimeSpan += lastTaskDuration
        LabelTodayDuration.Text = todayTimeSpan.ToString
        LabelThisWeekDuration.Text = thisWeekTimeSpan.ToString
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim tmp As DialogResult
        Dim tmpStr As String = ""
        Dim tmpStr1 As String = ""
        Dim isConfigFileAccessible As Boolean = False
        If (System.IO.File.Exists(ConfigFilePath)) Then
            Try
                Dim fileReader As System.IO.StreamReader = New System.IO.StreamReader(ConfigFilePath)
                Do While (fileReader.Peek >= 0)
                    tmpStr1 = fileReader.ReadLine()
                    If (((tmpStr1.IndexOf("tracker", 0, StringComparison.CurrentCultureIgnoreCase) >= 0) _
                        Or (tmpStr1.IndexOf("bw", 0, StringComparison.CurrentCultureIgnoreCase) >= 0)) _
                        And ((tmpStr1.IndexOf("file", 0, StringComparison.CurrentCultureIgnoreCase) >= 0) _
                        Or (tmpStr1.IndexOf("path", 0, StringComparison.CurrentCultureIgnoreCase) >= 0) _
                        Or (tmpStr1.IndexOf("excel", 0, StringComparison.CurrentCultureIgnoreCase) >= 0) _
                        Or (tmpStr1.IndexOf("xls", 0, StringComparison.CurrentCultureIgnoreCase) >= 0))) Then
                        Dim tmpStrSplit As String() = tmpStr1.Split("=".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
                        If (tmpStrSplit.Count > 1) Then
                            BWTrackerExcelFilePath = tmpStrSplit.ElementAt(1).Trim
                            tmpStr &= "The config file specifies the excel file path: " & Chr(10) & "   " & BWTrackerExcelFilePath & Chr(10)
                        End If
                        Exit Do
                    End If
                Loop
                fileReader.Close()
            Catch ex As Exception
                tmpStr &= "Error: " & ex.ToString
            End Try
        Else
            tmpStr &= "The config file can not be found !" & Chr(10)
        End If
        If (Not System.IO.File.Exists(BWTrackerExcelFilePath)) Then
            tmpStr &= "The excel file can not be found !" & Chr(10)
            MsgBox(tmpStr)
            ExcelApp.Quit()
            End
            Exit Sub
        End If
        BWTrackerWB = ExcelApp.Workbooks.Open(BWTrackerExcelFilePath)
        While (BWTrackerWB.ReadOnly)
            tmp = MessageBox.Show("The file is currently being used by an another program." & Chr(10) &
                    "Please make sure the file is closed, then try again !", "File Is Being Opened" _
                    , MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)
            If (tmp = DialogResult.Cancel) Then
                BWTrackerWB.Close(SaveChanges:=False)
                ExcelApp.Quit()
                End
                Exit Sub
            End If
            BWTrackerWB = ExcelApp.Workbooks.Open(BWTrackerExcelFilePath)
        End While
        BWTrackerWS = BWTrackerWB.Worksheets("BWTracker")
        KeyHierarchyWS = BWTrackerWB.Worksheets("KeyHierarchy")
        HeaderCell_key = HeaderCell(KeyHierarchyWS, "key")
        HeaderCell_parentkey = HeaderCell(KeyHierarchyWS, "parent key")
        KeyRng1 = KeyHierarchyWS.Range(KeyHierarchyWS.Cells(HeaderCell_key.Row + 1, HeaderCell_key.Column),
                                                          LastDataCellOfAColumn(KeyHierarchyWS.Columns(HeaderCell_key.Column)))
        tmpStr = ""
        tmpStr1 = ""
        Dim currentColumn As Integer
        Dim currentRow As Integer
        Dim tmpRng As Excel.Range
        For Each aKey As Excel.Range In KeyRng1
            currentRow = aKey.Row
            currentColumn = aKey.Column
            tmpStr = Trim(CStr(aKey.Value))
            TaskPathKeyInOneStringForFastKeyScan &= "    " & tmpStr & "   "
            Do
                If (currentColumn = HeaderCell_key.Column) Then
                    currentColumn = HeaderCell_parentkey.Column
                    tmpRng = DirectCast(KeyHierarchyWS.Cells(currentRow, currentColumn), Excel.Range)
                    tmpStr1 = Trim(CStr(tmpRng.Value))
                    tmpStr = tmpStr1 & "/" & tmpStr
                ElseIf (currentColumn = HeaderCell_parentkey.Column) Then
                    currentColumn = HeaderCell_key.Column
                    currentRow = KeyRng1.Find(tmpStr1,
                                              LookIn:=Excel.XlFindLookIn.xlValues,
                                              LookAt:=Excel.XlLookAt.xlWhole,
                                              SearchOrder:=Excel.XlSearchOrder.xlByRows,
                                              SearchDirection:=Excel.XlSearchDirection.xlNext,
                                              MatchCase:=True).Row
                End If
            Loop Until (String.IsNullOrEmpty(tmpStr1))
            TaskPathCollection.Add(tmpStr)
            ComboBoxTaskPath.Items.Add(tmpStr)
        Next
        tmpRng = HeaderCell(BWTrackerWS,,,, True)
        currentRow = tmpRng.Row
        currentColumn = tmpRng.Column
        For i As Integer = 1 To currentColumn Step 1
            tmpRng = DirectCast(BWTrackerWS.Cells(currentRow, i), Excel.Range)
            BWTrackerWSHeaderCellCollection.Add(Item:=tmpRng,
                                                Key:=Trim(CStr(tmpRng.Value)))
        Next
        Dim aDateColNum As Integer = BWTrackerWSHeaderCellCollection.Item("date").Column
        Dim aMonthColNum As Integer = BWTrackerWSHeaderCellCollection.Item("month").Column
        Dim aYearColNum As Integer = BWTrackerWSHeaderCellCollection.Item("year").Column
        Dim aDurationColumn As Integer = BWTrackerWSHeaderCellCollection.Item("duration").Column
        Dim aWeekColumn As Integer = BWTrackerWSHeaderCellCollection.Item("week").Column
        Dim currentWeek As String = DateDiff("ww", DateSerial(Today.Year, 1, 1), Today, FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1).ToString
        Dim sameYear As Boolean = False
        For i As Integer = 1 To BWTrackerWS.UsedRange.Rows.Count Step 1
            sameYear = (BWTrackerWS.Cells(i, aYearColNum).Value = Today.Year.ToString)
            If ((BWTrackerWS.Cells(i, aDateColNum).Value = Today.Day.ToString) _
                And (BWTrackerWS.Cells(i, aMonthColNum).Value = MonthName(Today.Month)) _
                And sameYear) Then
                todayTimeSpan += TimeSpan.Parse(BWTrackerWS.Cells(i, aDurationColumn).Value)
            End If
            If (sameYear And (BWTrackerWS.Cells(i, aWeekColumn).Value = currentWeek)) Then
                thisWeekTimeSpan += TimeSpan.Parse(BWTrackerWS.Cells(i, aDurationColumn).Value)
            End If
        Next
        LabelTodayDuration.Text = todayTimeSpan.ToString
        LabelThisWeekDuration.Text = thisWeekTimeSpan.ToString
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If (ButtonTimingAnimation.Enabled) Then
            SaveBWTrackerWB()
        End If
        BWTrackerWB.Close(SaveChanges:=False)
        ExcelApp.Quit()
    End Sub

    Private Sub SaveBWTrackerWB()
        lastStopMoment = Now
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("stop hour").Column).Value _
            = lastStopMoment.Hour.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("stop min").Column).Value _
            = lastStopMoment.Minute.ToString
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("stop sec").Column).Value _
            = lastStopMoment.Second.ToString
        lastTaskDuration = TimeSpan.FromSeconds(DateDiff("s", lastStopMoment, lastStartingMoment)).Duration
        BWTrackerWS.Cells(currentRowInBWTrackerWS, BWTrackerWSHeaderCellCollection.Item("duration").Column).Value _
            = lastTaskDuration.ToString
        BWTrackerWB.Save()
    End Sub

    Private Sub ButtonClearTextBoxMoreInfo_Click(sender As Object, e As EventArgs) Handles ButtonClearTextBoxMoreInfo.Click
        TextBoxMoreInfo.Clear()
    End Sub

    Private Sub ButtonClearComboBoxTaskPath_Click(sender As Object, e As EventArgs) Handles ButtonClearComboBoxTaskPath.Click
        ComboBoxTaskPath.ResetText()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If (ComboBoxTaskPath.Focused) Then
            If (keyData = Keys.Tab) Then
                Dim currentMaxIndex As Integer = ComboBoxTaskPath.Items.Count - 1
                If (currentMaxIndex > 0) Then
                    If (ComboBoxTaskPath.SelectedIndex < 1) Then
                        currentComboBoxTaskPathTabBiasDirection = 1
                    ElseIf (ComboBoxTaskPath.SelectedIndex >= currentMaxIndex) Then
                        currentComboBoxTaskPathTabBiasDirection = -1
                    End If
                    ComboBoxTaskPath.SelectedIndex += currentComboBoxTaskPathTabBiasDirection
                ElseIf (currentMaxIndex = 0) Then
                    ComboBoxTaskPath.SelectedIndex = 0
                End If
                ComboBoxTaskPath.Select(0, ComboBoxTaskPath.Text.Length)
                Return True
            ElseIf (keyData = Keys.Enter) Then
                If (Not TaskPathCollection.Contains(ComboBoxTaskPath.Text.Trim)) Then
                    ComboBoxTaskPath.ResetText()
                End If
                TextBoxMoreInfo.Select()
            End If
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub ComboBoxTaskPath_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBoxTaskPath.KeyPress
        If e.KeyChar = ChrW(13) Then
            Exit Sub
        End If
        Dim currentText As String = ComboBoxTaskPath.Text.Trim
        If (currentText.Length > 1) Then
            For i As Integer = 1 To ComboBoxTaskPath.Items.Count Step 1
                ComboBoxTaskPath.Items.RemoveAt(0)
            Next
            If ((TaskPathKeyInOneStringForFastKeyScan.IndexOf(currentText, 0, StringComparison.CurrentCultureIgnoreCase) >= 0) _
                Or (currentText.Contains("/"))) Then
                For Each tmpStr As String In TaskPathCollection
                    If (tmpStr.IndexOf(currentText, 0, StringComparison.CurrentCultureIgnoreCase) >= 0) Then
                        ComboBoxTaskPath.Items.Add(tmpStr)
                    End If
                Next
            End If
            ComboBoxTaskPath.Update()
            Try
                If (ComboBoxTaskPath.Items.Count > 0) Then
                    If (Not ComboBoxTaskPath.DroppedDown) Then
                        ComboBoxTaskPath.DroppedDown = True
                    End If
                Else
                    If (ComboBoxTaskPath.DroppedDown) Then
                        ComboBoxTaskPath.DroppedDown = False
                    End If
                End If
            Catch
            End Try
        End If
        Me.Cursor = Cursors.Default
        Cursor.Show()
    End Sub

    Private Sub TextBoxTaskDescription_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxTaskDescription.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            ComboBoxTaskPath.Select()
        End If
    End Sub

    Private Sub TextBoxMoreInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxMoreInfo.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            ButtonStart.Select()
        End If
    End Sub
End Class
