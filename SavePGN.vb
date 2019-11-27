Public Class frmPgnInfo


    Private Sub rbCheck()
        Select Case InfoGame.Result
            Case "1-0"
                rbWhiteWin.Checked = True
            Case "1/2-1/2"
                rbDrawGame.Checked = True
            Case "0-1"
                rbBlackWin.Checked = True
            Case "*"
                rbNoResult.Checked = True
        End Select
    End Sub


    Private Function txtResult() As String
        If rbBlackWin.Checked Then
            Return "0-1"
        End If
        If rbWhiteWin.Checked Then
            Return "1-0"
        End If
        If rbDrawGame.Checked Then
            Return "1/2-1/2"
        End If
        If rbNoResult.Checked Then
            Return "*"
        End If
        Return "*"
    End Function

    Private Function strTimeControl() As String
      
        Dim strMin As String = ""
        Dim strSec As String = ""

        If nudMin.Value <> 0 Then
            strMin = (nudMin.Value * 60).ToString
        End If

        If nudSec.Value <> 0 Then
            strSec = "+" & nudSec.Value.ToString
        End If

        strTimeControl = strMin & strSec

    End Function

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        With InfoGame
            .Black = txtBlack.Text
            .BlackElo = txtEloBlack.Text
            .Date_str = dtpDate.Text
            .Event_str = txtEvent.Text
            .Round = txtRound.Text
            .Site = txtSite.Text
            .White = txtWhite.Text
            .WhiteElo = txtEloWhite.Text
            .Result = txtResult()
            .TimeControl = strTimeControl()
        End With

        Me.Close()

    End Sub


    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPgnInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lestemps As String()

        lestemps = InfoGame.TimeControl.Split("+")


        nudMin.Value = lestemps(0) \ 60

        If UBound(lestemps) = 1 Then
            nudSec.Value = lestemps(1)
        Else
            nudSec.Value = 0
        End If

        With InfoGame
            txtBlack.Text = .Black
            txtEloBlack.Text = .BlackElo
            txtWhite.Text = .White
            txtEloWhite.Text = .WhiteElo
            txtEvent.Text = .Event_str
            txtRound.Text = .Round
            dtpDate.Text = .Date_str
            txtSite.Text = .Site
            rbCheck()
        End With
    End Sub
End Class