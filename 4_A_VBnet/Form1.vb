Public Class Form1

    Public r As Random = New Random()
    Private nl As String = System.Environment.NewLine
    Private ListOfAthletes As List(Of Athlete) = New List(Of Athlete)()

    ' HANDLERS
    Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Me.RichTextBox1.Clear()
        Me.ListOfAthletes.Clear()
        Me.RichTextBox1.AppendText("Dataset of Athletes competing in a marathon" & nl)
        Me.RichTextBox1.AppendText("___________________________________" & nl)
        Me.Timer1.Start()
    End Sub

    Private Sub button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Me.RichTextBox2.Clear()
        Dim OnlineMean As Double = computeOnlineMean(ListOfAthletes.Select(Function(ByVal a As Athlete) CDbl(a.Age)).ToList())
        Me.RichTextBox2.AppendText("________________________" & nl)
        Me.RichTextBox2.AppendText("Arithmetic Mean: " & OnlineMean.ToString(".##") & nl)
    End Sub

    Private Sub button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Me.RichTextBox3.Clear()
        Dim FrequencyDistribution As SortedDictionary(Of Integer, FrequencyItem)
        FrequencyDistribution = computeDiscreteFrequencyDistribution(ListOfAthletes.Select(Function(ByVal a As Athlete) a.Age).ToList())
        Dim FrequencyDistribution2 As List(Of Interval) = New List(Of Interval)()
        FrequencyDistribution2 = computeDiscreteFrequencyInterval(ListOfAthletes.Select(Function(ByVal a As Athlete) a.Age).ToList())
        printFrequencyDistributionInterval(FrequencyDistribution2)
    End Sub

    Private Sub button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Me.Timer1.Stop()
    End Sub

    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        addAthlete(Me.ListOfAthletes)
    End Sub

    'FUNCTIONS

    Private Function computeDiscreteFrequencyInterval(ByVal L As List(Of Integer)) As List(Of Interval)
        Dim StartingPoint As Integer = 50
        Dim IntervalStep As Integer = 5
        Dim ListOfIntervals As List(Of Interval) = New List(Of Interval)()
        Dim Interval0 As Interval = New Interval()
        Interval0.LowerInclusiveBound = StartingPoint
        Interval0.IntervalStep = IntervalStep
        ListOfIntervals.Add(Interval0)

        For Each d In L
            Dim ValueInserted As Boolean = False

            For Each I In ListOfIntervals

                If I.containsValue(d) Then
                    I.Count += 1
                    ValueInserted = True
                    Exit For
                End If
            Next

            If ValueInserted <> True Then

                If d < ListOfIntervals(0).LowerInclusiveBound Then

                    While ValueInserted <> True
                        Dim I As Interval = New Interval()
                        I.LowerInclusiveBound = ListOfIntervals(0).LowerInclusiveBound - IntervalStep
                        I.IntervalStep = IntervalStep

                        If I.containsValue(d) Then
                            ValueInserted = True
                            I.Count += 1
                        End If

                        ListOfIntervals.Insert(0, I)
                    End While
                ElseIf d >= ListOfIntervals(ListOfIntervals.Count - 1).LowerInclusiveBound Then

                    While ValueInserted <> True
                        Dim I As Interval = New Interval()
                        I.LowerInclusiveBound = ListOfIntervals(ListOfIntervals.Count - 1).LowerInclusiveBound + IntervalStep
                        I.IntervalStep = IntervalStep

                        If I.containsValue(d) Then
                            ValueInserted = True
                            I.Count += 1
                        End If

                        ListOfIntervals.Add(I)
                    End While
                Else
                    Throw New Exception("Not Accepted value")
                End If
            End If
        Next

        For Each I In ListOfIntervals
            I.RelativeFrequency = CDbl(I.Count) / L.Count()
            I.Percentage = I.RelativeFrequency * 100
        Next

        Return ListOfIntervals
    End Function

    Private Sub addAthlete(ByVal L As List(Of Athlete))
        Dim MinAge As Integer = 20
        Dim MaxAge As Integer = 65
        Dim a As Athlete = New Athlete()
        a.Age = r.Next(MinAge, MaxAge)
        L.Add(a)
        Me.RichTextBox1.AppendText(($"Athlete {L.Count}").PadRight(20) & ("Age " & a.Age).PadRight(20) & nl)
    End Sub

    Private Function generateDatasetAthletes(ByVal numOfAthletes As Integer) As List(Of Athlete)
        Dim L As List(Of Athlete) = New List(Of Athlete)()
        Dim MinAge As Integer = 18
        Dim MaxAge As Integer = 65

        For i As Integer = 0 To numOfAthletes - 1
            Dim a As Athlete = New Athlete()
            a.Age = r.Next(MinAge, MaxAge)
            L.Add(a)
            Me.RichTextBox1.AppendText(("Athlete " & (i + 1)).PadRight(20) & ("Age " & a.Age).PadRight(20) & nl)
        Next

        Return L
    End Function

    Private Function computeOnlineMean(ByVal L As List(Of Double)) As Double
        Dim avg As Double = 0
        Dim i As Integer = 0

        For Each d In L
            i += 1
            avg += (d - avg) / i
        Next

        Return avg
    End Function

    Private Function computeDiscreteFrequencyDistribution(ByVal ListOfUnits As List(Of Integer)) As SortedDictionary(Of Integer, FrequencyItem)
        Dim FreqDistr As SortedDictionary(Of Integer, FrequencyItem) = New SortedDictionary(Of Integer, FrequencyItem)()

        For Each d In ListOfUnits

            If FreqDistr.ContainsKey(d) Then
                FreqDistr(d).Count += 1
            Else
                FreqDistr.Add(d, New FrequencyItem())
            End If
        Next

        For Each Item In FreqDistr.Values
            Item.RelativeFrequency = Item.Count / CDbl(ListOfUnits.Count)
            Item.Percentage = Item.RelativeFrequency * 100
        Next

        Return FreqDistr
    End Function

    Private Sub printFrequencyDistributionInterval(ByVal L As List(Of Interval))
        Dim tot As Double = 0
        Dim count As Integer = 0
        Me.RichTextBox3.AppendText("Intervals  --> " & "Num - ".PadRight(12) & "Rel Freq" & " - " & "Percentage".PadRight(15) & nl)

        For Each I In L
            Me.RichTextBox3.AppendText($"[{I.LowerInclusiveBound} - " & $"{I.LowerInclusiveBound + I.IntervalStep}]  --> " & $"{I.Count} - ".PadRight(15) & $"{I.RelativeFrequency.ToString("0.##")}".PadRight(15) & " - " & $"{I.Percentage.ToString("###.##")} %".PadRight(15) & nl)
            tot += I.RelativeFrequency
            count += I.Count
        Next

        Me.RichTextBox3.AppendText($"Sum of relative frequencies: {tot}" & nl)
        Me.RichTextBox3.AppendText($"Total units: {count}")
    End Sub

    Private Sub printFrequencyDistribution(ByVal F As SortedDictionary(Of Integer, FrequencyItem))
        Me.RichTextBox3.Clear()
        Me.RichTextBox3.AppendText("________________________" & nl)
        Me.RichTextBox3.AppendText("Age  Count  Rel  Per" & nl)
        Dim tot As Double = 0

        For Each Item In F
            Me.RichTextBox3.AppendText($"{Item.Key} -- " & $"{Item.Value.Count} -- " & $"{Item.Value.RelativeFrequency} -- " & $"{Item.Value.Percentage} %" & nl)
            tot += Item.Value.RelativeFrequency
        Next

        Me.RichTextBox3.AppendText($"Sum of relative frequencies: {tot}" & nl)
    End Sub

End Class
