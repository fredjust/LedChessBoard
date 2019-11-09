Imports System
Imports System.Threading
'Imports System.IO.Ports
Imports System.ComponentModel
Imports System.IO
Imports System.Net



Public Class frmMain

    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)

    Public Const MOUSEEVENTF_LEFTDOWN = &H2 ' left button down
    Public Const MOUSEEVENTF_LEFTUP = &H4 ' left button up

    'FredJust@gmail.com
    'avril 2015
    'les graphiques ne font que 88x88
    'pour des écrans de 768 de haut max
    'a tester en Full HD
    'il faudrait peut etre changer les PNG qui existent en tout les tailles sur le web

    'GESTION DU PORT SERIE
    '------------------------------------------------
    Dim SerialPortName As String = "COM6"
    Dim SerialPortSpeed As String = "115200"
    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data


    '------------------------------------------------

    Public Structure UnCoup
        Dim coupPGN As String
        Dim posFEN As String
        Dim timeSEC As ULong
    End Structure

    Public unePartie As New Collection




#Region "Les Images PNG"
    Dim wp As New Bitmap(My.Resources.wp)
    Dim wr As New Bitmap(My.Resources.wr)
    Dim wn As New Bitmap(My.Resources.wn)
    Dim wb As New Bitmap(My.Resources.wb)
    Dim wq As New Bitmap(My.Resources.wq)
    Dim wk As New Bitmap(My.Resources.wk)

    Dim bp As New Bitmap(My.Resources.bp)
    Dim br As New Bitmap(My.Resources.br)
    Dim bn As New Bitmap(My.Resources.bn)
    Dim bb As New Bitmap(My.Resources.bb)
    Dim bq As New Bitmap(My.Resources.bq)
    Dim bk As New Bitmap(My.Resources.bk)

    Dim bboard As New Bitmap(My.Resources.board90)
    Dim bHaut As New Bitmap(My.Resources.bHaut)
    Dim bCote As New Bitmap(My.Resources.bcote)

    Dim greenCircle As New Bitmap(My.Resources.vert)
    Dim redCircle As New Bitmap(My.Resources.rouge)
    Dim blueCircle As New Bitmap(My.Resources.bleu)
    Dim GreenCross As New Bitmap(My.Resources.pg)
#End Region




    Dim PieceSize As Integer

    'graphic sur la picturebox
    'je ne comprends pas trop comment cela fonctionne vraiment
    Dim backBuffer As New Bitmap(My.Resources.board90)
    Dim g As Graphics = Graphics.FromImage(backBuffer)

    'variable global quelle horreur
    Dim sqFrom As String
    Dim sqTo As String
    Dim MoveByClic As Boolean
    Dim EffaceNoir As Boolean = False   'une rustine de dernière minute
    Public ThePOS As ObjFenMoves

    Public Const lv_num As Byte = 0
    Public Const lv_rec As Byte = 1
    Public Const lv_time As Byte = 2
    Public Const lv_off As Byte = 3
    Public Const lv_on As Byte = 4
    Public Const lv_FEN As Byte = 5
    Public Const lv_Nb As Byte = 6
    Public Const lv_Move As Byte = 7

    Dim AttendreCoup As Integer = 0




#Region "Fonction de dessin"

    'retourne le bitmap correspondant à la piece d'une notation FEN
    Private Function bmpPiece(ByVal name As Char) As Bitmap
        Select Case name
            Case "P"
                Return wp
            Case "R"
                Return wr
            Case "N"
                Return wn
            Case "B"
                Return wb
            Case "Q"
                Return wq
            Case "K"
                Return wk

            Case "p"
                Return bp
            Case "r"
                Return br
            Case "n"
                Return bn
            Case "b"
                Return bb
            Case "q"
                Return bq
            Case "k"
                Return bk

            Case "1"
                Return redCircle
            Case "2"
                Return greenCircle
            Case "3"
                Return blueCircle
            Case "4"
                Return GreenCross


        End Select
    End Function

    'retourne la position X de la case
    Private Function Xsqi(ByVal sqi As Byte) As Integer
        Dim colonne As Byte
        Dim ligne As Byte

        colonne = sqi Mod 10
        ligne = sqi \ 10

        Return 18 + (colonne - 1) * PieceSize

    End Function

    'retourne la position Y de la case
    Private Function Ysqi(ByVal sqi As Byte) As Integer
        Dim colonne As Byte
        Dim ligne As Byte

        colonne = sqi Mod 10
        ligne = sqi \ 10

        Return 18 + (8 - ligne) * PieceSize

    End Function

    'dessine une pièce sur une case
    Private Sub PutPiece(ByVal sqIndex As Byte, ByVal Initiale As Char)
        Dim rect As Rectangle
        rect.X = Xsqi(sqIndex)
        rect.Y = Ysqi(sqIndex)
        rect.Width = PieceSize
        rect.Height = PieceSize

        g.DrawImage(bmpPiece(Initiale), rect)



    End Sub

    'dessine un symbole sur une case
    Private Sub PutSymbol(ByVal sqIndex As Byte, ByVal Initiale As Char)
        Dim rect As Rectangle
        Dim p As Graphics = PictureBox1.CreateGraphics
        rect.X = Xsqi(sqIndex)
        rect.Y = Ysqi(sqIndex)
        rect.Width = PieceSize
        rect.Height = PieceSize

        p.DrawImage(bmpPiece(Initiale), rect)

    End Sub

    'dessine l'échiquier puis  les pièces
    Private Sub DrawPiece()
        Dim rect As Rectangle
        Dim pt As Point


        If Me.WindowState <> FormWindowState.Minimized Then

            PictureBox1.Top = 10
            PictureBox1.Left = 10

            PictureBox1.Height = Me.ClientSize.Height - 20
            PictureBox1.Width = Me.ClientSize.Height - 20

            PictureBox1.Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)

            backBuffer = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            g = Graphics.FromImage(backBuffer)

            pt.X = 1 : pt.Y = 1 : g.DrawImage(bHaut, pt)                        'dessine le bord haut
            pt.X = 1 : pt.Y = PictureBox1.Height - 17 : g.DrawImage(bHaut, pt)  'dessine le bord bas
            pt.X = 1 : pt.Y = 1 : g.DrawImage(bCote, pt)                        'dessine le bord gauche
            pt.X = PictureBox1.Width - 17 : pt.Y = 1 : g.DrawImage(bCote, pt)   'dessine le bord droit

            pt.X = bHaut.Width - 1 : pt.Y = 1 : g.DrawImage(bHaut, pt)                        'dessine le bord haut
            pt.X = bHaut.Width - 1 : pt.Y = PictureBox1.Height - 17 : g.DrawImage(bHaut, pt)  'dessine le bord bas
            pt.X = 1 : pt.Y = bCote.Height - 1 : g.DrawImage(bCote, pt)                        'dessine le bord gauche
            pt.X = PictureBox1.Width - 17 : pt.Y = bCote.Height - 1 : g.DrawImage(bCote, pt)   'dessine le bord droit

            rect.X = 0 : rect.Y = 0
            rect.Width = PictureBox1.Width - 1 : rect.Height = PictureBox1.Height - 1
            g.DrawRectangle(Pens.Brown, rect)                                   'dessine le filet exterieur 

            rect.X = 17 : rect.Y = 17
            rect.Width = PictureBox1.Width - 35 : rect.Height = PictureBox1.Height - 35
            g.DrawRectangle(Pens.Black, rect)                                   'dessine le filet intérieur

            rect.X = 18 : rect.Y = 18
            rect.Width = PictureBox1.Width - 36 : rect.Height = PictureBox1.Height - 36
            g.DrawImage(bboard, rect)                                           'dessine l'échiquier

            PieceSize = (PictureBox1.Height - 36) / 8



            With TabControl1
                .Top = 10
                .Left = PictureBox1.Left + PictureBox1.Width + 10
                .Height = Me.ClientSize.Height - .Top - 10
                .Width = Me.ClientSize.Width - (PictureBox1.Width + 30)
            End With



            PictureBox1.Image = backBuffer

            Dim LaPiece As Char
            For i = 11 To 88
                LaPiece = ThePOS.Board10x10(i)
                If LaPiece <> " " And LaPiece <> "*" Then
                    PutPiece(i, LaPiece)
                End If
            Next

        End If

        Dim pBlancs As String = ""
        Dim pNoirs As String = ""

        DiffMat(pBlancs, pNoirs)
        lblBlancs.Text = ToBlackStr(pNoirs)
        lblNoirs.Text = pBlancs

    End Sub

    'place les symboles de déplacement d'une pièce
    Public Sub DrawMove()

        Dim sqMoves() As String
        Dim sq As String
        Dim txtMoves As String

        txtMoves = ThePOS.GetMoves(sqFrom)


        If txtMoves.Length > 0 Then
            sqMoves = txtMoves.Split(" ")

            For i = 0 To sqMoves.Count - 1
                sq = sqMoves(i)
                If sq.Substring(0, 1) <> "x" Then
                    If Not ThePOS.IsValidMove(sqFrom & sq) Then
                        PutSymbol(ThePOS.SquareIndex(sq), "3")
                    Else
                        PutSymbol(ThePOS.SquareIndex(sq), "2")
                    End If
                Else
                    sq = sqMoves(i).Substring(1, 2)
                    If Not ThePOS.IsValidMove(sqFrom & sq) Then
                        PutSymbol(ThePOS.SquareIndex(sq), "3")
                    Else
                        PutSymbol(ThePOS.SquareIndex(sq), "4")
                    End If
                End If
            Next
        End If

        Dim txtCanTake As String = ThePOS.WhoCanTake(sqFrom)

        If txtCanTake.Length > 0 Then
            sqMoves = txtCanTake.Split(" ")
            For i = 0 To sqMoves.Count - 1
                sq = sqMoves(i)
                PutSymbol(ThePOS.SquareIndex(sq), "1")
            Next
        End If

    End Sub


#End Region

    Private Sub initInfoPgn()
        With InfoGame
            .Black = "BLACK Player"
            .White = "WHITE Player"
            .BlackElo = "1399"
            .WhiteElo = "1399"
            .Date_str = Now.Year.ToString & "/" & Now.Month.ToString & "/" & Now.Day.ToString
            .Result = "*"
            .Round = "1"
            .Event_str = "ARD Chess"
            .Site = "Here"
            .LocalPIECE = "TCFDR"
            .TimeControl = "600"
        End With
    End Sub

    Public Sub initSizeEchiquier()
        Dim tmpPt As Point
        With echiquier
            .Width = 512 '640 ' 512
            .Height = 512 '640 '512
            .X = 660 '587 ' 660
            .Y = 149 ' 164 ' 149
        End With

        IniFile = Application.StartupPath & "\Chessboard.ini"
        If Cls_Ini.INISectionExist(IniFile, "liscreen") Then
            tmpPt.X = Cls_Ini.INIRead(IniFile, "liscreen", "left")
            tmpPt.Y = Cls_Ini.INIRead(IniFile, "liscreen", "top")
            echiquier.Location = tmpPt
            echiquier.Width = Cls_Ini.INIRead(IniFile, "liscreen", "Width")
            echiquier.Height = echiquier.Width
        End If
    End Sub

#Region "Evenement de la form1"

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        DrawPiece()
        lblTimeNoirs.Top = lvMoves.Top
        lblTimeNoirs.Left = lvMoves.Width - 20

        lblTimeBlancs.Top = lvMoves.Height - lblTimeBlancs.Height
        lblTimeBlancs.Left = lvMoves.Width - 20

        lblBlancs.Left = lvMoves.Width + 5
        lblNoirs.Left = lvMoves.Width + 5

        lblNoirs.Top = lblTimeNoirs.Top + lblTimeNoirs.Height
        lblBlancs.Top = lblTimeBlancs.Top - lblBlancs.Height
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        wp.Dispose()
        wr.Dispose()
        wn.Dispose()
        wb.Dispose()
        wq.Dispose()
        wk.Dispose()

        bp.Dispose()
        br.Dispose()
        bn.Dispose()
        bb.Dispose()
        bq.Dispose()
        bk.Dispose()

        bboard.Dispose()
        bHaut.Dispose()
        bCote.Dispose()

        greenCircle.Dispose()
        redCircle.Dispose()
        blueCircle.Dispose()
        GreenCross.Dispose()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PictureBox1.Top = 10
        PictureBox1.Left = 10
        pbReduire.Top = 10
        ThePOS = New ObjFenMoves()
        ThePOS.LocalPiece = "TCFDR"
        AddFirstLine()
        initInfoPgn()
        initSizeEchiquier()
    End Sub

    Private Sub frmMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Right Then
            cmenuBoard.Show(Control.MousePosition)
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Static MustRedraw As Boolean
        pbReduire.Visible = False

        Select Case Me.WindowState
            Case FormWindowState.Maximized
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                DrawPiece()
                MustRedraw = True
                pbReduire.Visible = True
                pbReduire.Left = Me.ClientSize.Width - pbReduire.Width - 10
            Case FormWindowState.Minimized
                MustRedraw = True
            Case FormWindowState.Normal
                If MustRedraw Then
                    DrawPiece()
                    MustRedraw = False
                End If
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End Select

        lblTimeNoirs.Top = lvMoves.Top
        lblTimeNoirs.Left = lvMoves.Width - 20

        lblTimeBlancs.Top = lvMoves.Height - lblTimeBlancs.Height
        lblTimeBlancs.Left = lvMoves.Width - 20

        lblBlancs.Left = lvMoves.Width + 5
        lblNoirs.Left = lvMoves.Width + 5

        lblNoirs.Top = lblTimeNoirs.Top + lblTimeNoirs.Height
        lblBlancs.Top = lblTimeBlancs.Top - lblBlancs.Height


    End Sub

    Private Sub Form1_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        Static WindowsSize As Integer
        If Me.WindowState = FormWindowState.Normal Then
            If WindowsSize <> Me.Width + Me.Height Then
                DrawPiece()
                WindowsSize = Me.Width + Me.Height
            End If
        End If
    End Sub

#End Region

#Region "Evenement de la picturebox1"

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        Dim sqI As String


        'récupère le nom de la case sur lequel le curseur se trouve
        sqI = Chr(97 + Math.Truncate((e.X - 18) / (PictureBox1.Width - 36) * 8)) _
                + (8 - Math.Truncate((e.Y - 18) / (PictureBox1.Height - 36) * 8)).ToString

        PictureBox1.Tag = sqI

        If e.Button = MouseButtons.Left Then



            If sqFrom = "" Then
                sqFrom = sqI
                DrawMove()
            Else
                If sqFrom <> sqI Then
                    sqTo = sqFrom
                End If
            End If

        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim sqI As String


        If e.Button = MouseButtons.Right Then
            cmenuBoard_Allumer.Text = "Allumer " & PictureBox1.Tag
            cmenuBoard_Eteindre.Text = "Eteindre " & PictureBox1.Tag
            cmenuBoard.Show(Control.MousePosition)
        Else

            'récupère le nom de la case sur lequel le curseur se trouve
            sqI = Chr(97 + Math.Truncate((e.X - 18) / (PictureBox1.Width - 36) * 8)) _
                    + (8 - Math.Truncate((e.Y - 18) / (PictureBox1.Height - 36) * 8)).ToString



            'si on a changé de case depuis mousedown
            'déplacement par drag&drop
            If sqI <> sqFrom Then
                sqTo = sqI
                If ThePOS.IsValidMove(sqFrom & sqTo) Then
                    AddMove()
                    DrawPiece()
                    sqFrom = ""
                    sqTo = ""
                Else
                    sqFrom = ""
                    sqTo = ""
                    DrawPiece()

                End If
            End If

        End If

    End Sub

#End Region

#Region "Evenement listView"

    Private Sub lvMoves_MouseClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvMoves.MouseClick
        On Error Resume Next
        If e.X > lvMoves.Columns(0).Width + lvMoves.Columns(2).Width Then
            ThePOS.SetFEN(lvMoves.SelectedItems(0).SubItems(2).Tag)
            EffaceNoir = False
            DrawPiece()
        Else
            ThePOS.SetFEN(lvMoves.SelectedItems(0).SubItems(1).Tag)
            EffaceNoir = True
            DrawPiece()
        End If
    End Sub

#End Region

#Region "Gestion des mouvements et de la ListView"

    'efface tous les items suivant dans la listview
    Public Sub deletenextitem()
        On Error GoTo err

        If lvMoves.SelectedIndices(0) <> lvMoves.Items.Count - 1 Then
            While lvMoves.SelectedIndices(0) <> lvMoves.Items.Count - 1
                lvMoves.Items.RemoveAt(lvMoves.Items.Count - 1)
            End While
            If EffaceNoir Then
                If lvMoves.Items(lvMoves.Items.Count - 1).SubItems.Count = 3 Then
                    lvMoves.Items(lvMoves.Items.Count - 1).SubItems.RemoveAt(2)
                End If
            End If
        End If
err:

    End Sub

    'affiche le mouvement dans la listview lvmoves
    Private Sub AddMove()
        Dim lvi As New ListViewItem

        If sqFrom <> sqTo Then

            If ThePOS.IsValidMove(sqFrom & sqTo) Then



                deletenextitem()

                If ThePOS.WhiteToPlay Then
                    lvi.Text = ThePOS.MovesPlayed.ToString & "."
                    lvi.SubItems.Add(ThePOS.PGNmove(sqFrom & sqTo))
                    ThePOS.MakeMove(sqFrom & sqTo)
                    lvi.SubItems(1).Tag = ThePOS.GetFEN()
                    lvMoves.Items.Add(lvi)

                Else
                    lvi = lvMoves.Items(lvMoves.Items.Count - 1)
                    lvi.SubItems.Add(ThePOS.PGNmove(sqFrom & sqTo))
                    ThePOS.MakeMove(sqFrom & sqTo)
                    lvi.SubItems(2).Tag = ThePOS.GetFEN()
                End If



                lvMoves.Items(lvMoves.Items.Count - 1).Selected = True



            End If
        End If
    End Sub

    Private Function formatHMS(ByVal temps As Long, Optional ByVal withHour As Boolean = True) As String
        Dim H As Integer
        Dim M As Integer
        Dim S As Integer
        Dim Hstr As String
        Dim Mstr As String
        Dim Sstr As String

        If temps < 0 Then
            If withHour Then Return "0:00:00" Else Return "00:00"
        End If


        H = temps \ 3600
        Hstr = H

        M = (temps - H * 3600) \ 60
        If M < 10 Then
            Mstr = M
        Else
            Mstr = M
        End If

        S = temps Mod 60
        If S < 10 Then
            Sstr = "0" & S
        Else
            Sstr = S
        End If

        If withHour Then
            Return Hstr & ":" & Mstr & ":" & Sstr
        Else
            Return Mstr & ":" & Sstr
        End If

    End Function

    Private Function TransfertMove(Optional ByVal WithTime As Boolean = True) As String
        Dim WhiteToPlay As Boolean = True
        Dim nbCoup As Integer = 0
        Dim PGN As String = ""
        Dim tempo() As String
        Dim LeCoup As UnCoup
        Dim TempsCoup As ULong = 0
        Dim TempsBlancs As Long = 180
        Dim TempsNoirs As Long = 180
        Dim Increment As Long = 2

        Dim lestemps As String()

        lestemps = InfoGame.TimeControl.Split("+")


        TempsBlancs = lestemps(0)
        TempsNoirs = lestemps(0)
        If UBound(lestemps) = 1 Then
            Increment = lestemps(1)
        Else
            Increment = 0
        End If

        Dim lvi As ListViewItem

        PGN = "[Event ""Rapide CPE 2016""]" & vbCrLf & _
                "[Site ""CPE95""]" & vbCrLf & _
                "[Date ""2016.05.15""]" & vbCrLf & _
                "[Round ""1""]" & vbCrLf & _
                "[White ""ChessboARDuinoBlancs""]" & vbCrLf & _
                "[Black ""ChessboARDuinoNoirs""]" & vbCrLf & _
                "[Result ""*""]" & vbCrLf & vbCrLf


        lvMoves.Items.Clear()

        unePartie.Clear()

        For i = 1 To lvRec.Items.Count - 1
            If lvRec.Items(i).ForeColor = Color.Green Then
                lvi = New ListViewItem

                If WhiteToPlay Then
                    nbCoup += 1
                    lvi.Text = nbCoup.ToString & "."
                    PGN &= lvi.Text & " "

                    If lvRec.Items(i).SubItems(lv_time).Text <> "" Then
                        TempsBlancs -= TempsCoup
                        TempsBlancs += Increment
                        TempsCoup = CLng(lvRec.Items(i).SubItems(lv_time).Text / 1000)
                    End If


                    tempo = lvRec.Items(i).SubItems(lv_Move).Text.Split(" ")
                    lvi.SubItems.Add(tempo(1))
                    PGN &= tempo(1) & IIf(WithTime, " {[%clk " & formatHMS(TempsBlancs) & "]} ", " ")
                    lvi.SubItems(1).Tag = lvRec.Items(i).SubItems(lv_FEN).Text
                    lvMoves.Items.Add(lvi)

                    With LeCoup
                        .coupPGN = tempo(1)
                        .posFEN = lvRec.Items(i).SubItems(lv_FEN).Text
                        .timeSEC = TempsCoup
                    End With

                    unePartie.Add(LeCoup, nbCoup & "b")



                Else
                    lvi = lvMoves.Items(lvMoves.Items.Count - 1)
                    tempo = lvRec.Items(i).SubItems(lv_Move).Text.Split(" ")
                    lvi.SubItems.Add(tempo(1))

                    If lvRec.Items(i).SubItems(lv_time).Text <> "" Then

                        TempsNoirs -= TempsCoup
                        TempsNoirs += Increment
                        TempsCoup = CLng(lvRec.Items(i).SubItems(lv_time).Text / 1000)

                    End If

                    PGN &= tempo(1) & IIf(WithTime, " {[%clk " & formatHMS(TempsNoirs) & "]} ", "") & vbCrLf
                    lvi.SubItems(2).Tag = lvRec.Items(i).SubItems(lv_FEN).Text

                    With LeCoup
                        .coupPGN = tempo(1)
                        .posFEN = lvRec.Items(i).SubItems(lv_FEN).Text
                        .timeSEC = TempsCoup
                    End With
                    unePartie.Add(LeCoup, nbCoup & "n")

                End If

                WhiteToPlay = Not WhiteToPlay

            Else
                If Trim(lvRec.Items(i).SubItems(lv_time).Text) <> "" Then
                    Try
                        TempsCoup += CLng(lvRec.Items(i).SubItems(lv_time).Text / 1000)
                    Catch ex As Exception

                    End Try

                End If

            End If
        Next

        If WhiteToPlay Then

            TempsNoirs -= TempsCoup
            TempsNoirs += Increment
            lblTimeBlancs.BackColor = Color.Silver
            lblTimeNoirs.BackColor = Color.White
        Else
            TempsBlancs -= TempsCoup
            TempsBlancs += Increment
            lblTimeBlancs.BackColor = Color.White
            lblTimeNoirs.BackColor = Color.Silver

        End If

        If lvRec.Items.Count - FindLastGreenLine() > 30 Then
            lblTimeBlancs.BackColor = Color.LightCoral
            lblTimeNoirs.BackColor = Color.LightCoral
        End If


        lblTimeNoirs.Text = formatHMS(TempsNoirs, False)
        lblTimeBlancs.Text = formatHMS(TempsBlancs, False)

        If lvMoves.Items.Count > 10 Then lvMoves.Items(lvMoves.Items.Count - 1).EnsureVisible()

        PGN &= "*"
        Clipboard.SetText(PGN)

        DrawPiece()
        Return PGN
    End Function

    Private Function strAllMoves() As String
        Dim PGNgame As String = ""

        With InfoGame
            PGNgame = "[Event ""?""]".Replace("?", .Event_str) & vbCrLf
            PGNgame &= "[Site ""?""]".Replace("?", .Site) & vbCrLf
            PGNgame &= "[Date ""?""]".Replace("?", .Date_str) & vbCrLf
            PGNgame &= "[Round ""?""]".Replace("?", .Round) & vbCrLf
            PGNgame &= "[White ""?""]".Replace("?", .White) & vbCrLf
            PGNgame &= "[Black ""?""]".Replace("?", .Black) & vbCrLf
            PGNgame &= "[Result ""?""]".Replace("?", .Result) & vbCrLf
            PGNgame &= "[WhiteElo ""?""]".Replace("?", .WhiteElo) & vbCrLf
            PGNgame &= "[BlackElo ""?""]".Replace("?", .BlackElo) & vbCrLf
            If .TimeControl <> "" Then
                PGNgame &= "[TimeControl ""?""]".Replace("?", .TimeControl) & vbCrLf
            End If
            PGNgame &= vbCrLf
        End With

        On Error Resume Next

        For i = 0 To lvMoves.Items.Count - 1
            PGNgame &= lvMoves.Items(i).Text & " " & lvMoves.Items(i).SubItems(1).Text & " " & lvMoves.Items(i).SubItems(2).Text
            PGNgame &= vbCrLf
        Next
        Return PGNgame

    End Function

    Private Function strAllRecs() As String
        Dim tempo As String = ""
        For i = 0 To lvRec.Items.Count - 1
            tempo &= lvRec.Items(i).SubItems(lv_rec).Text & "."
            tempo &= vbCrLf
            tempo &= lvRec.Items(i).SubItems(lv_time).Text
            tempo &= vbCrLf

        Next
        Return tempo
    End Function

    Private Sub EffacetousCoup()
        lvMoves.Items.Clear()
        lvRec.Items.Clear()
        ThePOS.Init()
        DrawPiece()
    End Sub

#End Region


#Region "Evenement Bouton reduire"

    Private Sub pbReduire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbReduire.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub pbReduire_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbReduire.MouseEnter
        pbReduire.BackgroundImage = My.Resources.reduire
    End Sub

    Private Sub pbReduire_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbReduire.MouseLeave
        pbReduire.BackgroundImage = My.Resources.reduire0
    End Sub

#End Region

    ' renvoie le nombre de pièce sur une colonne
    Public Function NbPieceColonne(ByVal y) As Byte
        Dim a As Byte = 0
        Dim x As Byte
        Dim z As Byte
        Dim e As Byte
        z = y
        Do
            x = Int(z / 2)
            If z = 2 * x + 1 Then a = a + 1
            e = e + 1
            z = x
        Loop While z > 0
        NbPieceColonne = a
    End Function

    ' renvoie le nombre de piècce sur l'échiquier
    Public Function NbPiece(ByVal rec As String) As String
        Dim colonnes() As String
        Dim tempo As Byte = 0

        colonnes = rec.Split(".")
        For i = 0 To 7
            tempo = tempo + NbPieceColonne(colonnes(i))
        Next
        Return tempo
    End Function

    Public Function CompterMot(ByVal strPhrase As String, ByVal strMot As String) As Integer
        Dim strTab() As String
        strTab = Split(strPhrase, strMot)
        CompterMot = UBound(strTab)
    End Function

    Public Function ToBlackStr(ByVal strWhite As String) As String
        Dim tempo As String = ""

        tempo = strWhite.Replace("q", "w")
        tempo = tempo.Replace("r", "t")
        tempo = tempo.Replace("b", "v")
        tempo = tempo.Replace("n", "m")
        tempo = tempo.Replace("p", "o")

        Return tempo
    End Function

    Public Sub DiffMat(ByRef MatBlancs As String, ByRef MatNoirs As String)
        Dim tempoB As Byte
        Dim TempoN As Byte
        Dim FEN As String
        Dim champs As String()

        FEN = ThePOS.GetFEN()
        champs = FEN.Split(" ")
        FEN = champs(0)

        MatBlancs = ""
        MatNoirs = ""

        'cherchons les dames
        tempoB = CompterMot(FEN, "Q")
        TempoN = CompterMot(FEN, "q")

        If tempoB > TempoN Then
            MatNoirs &= StrDup(tempoB - TempoN, "q")
        End If
        If tempoB < TempoN Then
            MatBlancs &= StrDup(TempoN - tempoB, "q")
        End If

        'cherchons les tours
        tempoB = CompterMot(FEN, "R")
        TempoN = CompterMot(FEN, "r")
        If tempoB > TempoN Then
            MatNoirs &= StrDup(tempoB - TempoN, "r")
        End If
        If tempoB < TempoN Then
            MatBlancs &= StrDup(TempoN - tempoB, "r")
        End If

        'cherchons les fous
        tempoB = CompterMot(FEN, "B")
        TempoN = CompterMot(FEN, "b")
        If tempoB > TempoN Then
            MatNoirs &= StrDup(tempoB - TempoN, "b")
        End If
        If tempoB < TempoN Then
            MatBlancs &= StrDup(TempoN - tempoB, "b")
        End If

        'cherchons les cavaliers
        tempoB = CompterMot(FEN, "N")
        TempoN = CompterMot(FEN, "n")
        If tempoB > TempoN Then
            MatNoirs &= StrDup(tempoB - TempoN, "n")
        End If
        If tempoB < TempoN Then
            MatBlancs &= StrDup(TempoN - tempoB, "n")
        End If

        'cherchons les pions
        tempoB = CompterMot(FEN, "P")
        TempoN = CompterMot(FEN, "p")
        If tempoB > TempoN Then
            MatNoirs &= StrDup(tempoB - TempoN, "p")
        End If
        If tempoB < TempoN Then
            MatBlancs &= StrDup(TempoN - tempoB, "p")
        End If

    End Sub


    'ajoute une ligne correspondant à la position initiale de l'échiquier
    Private Sub AddFirstLine()
        lvRec.Items.Clear()
        '___________________________________________________________________________________________________________
        'première ligne
        lvRec.Items.Add(0)
        lvRec.Items(0).SubItems.Add("195.195.195.195.195.195.195.195")
        lvRec.Items(0).SubItems.Add("time")
        lvRec.Items(0).SubItems.Add("")
        lvRec.Items(0).SubItems.Add("")
        lvRec.Items(0).SubItems.Add("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        lvRec.Items(0).SubItems.Add("32")
        lvRec.Items(0).SubItems.Add("")
        lvRec.Items(0).ForeColor = Color.Green


    End Sub

    'rallume manuellement une case sur une line
    Private Function CaseOn(ByVal sqN As String, ByVal rec As String) As String
        Dim sqIndex As Byte
        Dim colonne As Byte
        Dim ligne As Byte
        Dim LesColonnes As String()

        sqIndex = ThePOS.SquareIndex(sqN) 'index de la case

        colonne = sqIndex Mod 10
        ligne = sqIndex \ 10

        LesColonnes = rec.Split(".")

        LesColonnes(colonne - 1) = LesColonnes(colonne - 1) + Math.Pow(2, (ligne) - 1)

        Return (Join(LesColonnes, "."))



    End Function

    'etteint manuellement une ligne
    Private Function CaseOff(ByVal sqN As String, ByVal rec As String) As String
        Dim sqIndex As Byte
        Dim colonne As Byte
        Dim ligne As Byte
        Dim LesColonnes As String()

        sqIndex = ThePOS.SquareIndex(sqN) 'index de la case

        colonne = sqIndex Mod 10
        ligne = sqIndex \ 10

        LesColonnes = rec.Split(".")

        LesColonnes(colonne - 1) = LesColonnes(colonne - 1) - Math.Pow(2, (ligne) - 1)

        Return (Join(LesColonnes, "."))



    End Function



    ' cherche les cases qui s'allument et s'éteingnent entre deux enregistrements
    Private Sub FindOnOff(ByVal new_line As String, ByVal last_line As String, ByRef SquareOn As String, ByRef SquareOff As String)
        Dim ligne As Integer
        Dim colonne As Byte

        Dim new_pos(9) As String
        Dim last_pos(9) As String

        Dim last_byte As Byte
        Dim new_byte As Byte

        SquareOff = ""
        SquareOn = ""

        last_pos = Split(new_line, ".") 'recupère les 8 bytes de la positions précedente
        new_pos = Split(last_line, ".") 'récupère les 8 bytes de la position 

        For colonne = 1 To 8
            new_byte = new_pos(colonne - 1) 'bytes des colonnes
            last_byte = last_pos(colonne - 1)
            If (new_byte <> last_byte) Then 'si les deux colonnes sont différentes
                For ligne = 7 To 0 Step -1
                    If (new_byte And (2 ^ ligne)) <> (last_byte And (2 ^ ligne)) Then   'si la case est diffétente
                        If (new_byte And (2 ^ ligne)) Then  's'il s'agit d'une extinction
                            SquareOff = Chr(96 + colonne) & (ligne + 1).ToString
                        Else
                            SquareOn = Chr(96 + colonne) & (ligne + 1).ToString
                        End If
                    End If
                Next
            End If
        Next
    End Sub



    Public Sub DrawCase()
        Dim sqOn As String
        Dim sqOff As String
        Dim sqOnI As Byte
        Dim sqOffI As Byte

        On Error Resume Next

        sqOn = lvRec.SelectedItems(0).SubItems(lv_on).Text
        sqOff = lvRec.SelectedItems(0).SubItems(lv_off).Text
        If sqOn <> "" Then sqOnI = ThePOS.SquareIndex(sqOn)
        If sqOff <> "" Then sqOffI = ThePOS.SquareIndex(sqOff)
        PutSymbol(sqOnI, "2")
        PutSymbol(sqOffI, "1")
    End Sub



    'si un FEN existe sur la ligne sélectionnée de lvREC appelle l'affichage
    Private Sub lvRec_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvRec.ItemSelectionChanged
        Dim aFen As String
        If e.IsSelected Then
            aFen = lvRec.SelectedItems.Item(0).SubItems(lv_FEN).Text
            If aFen <> "" Then
                ThePOS.SetFEN(aFen)
                DrawPiece()

            End If
        End If
    End Sub

    Private Sub lvRec_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvRec.KeyUp
        DrawCase()
    End Sub






    'affiche le menu contextuelle de lvREC
    Private Sub lvRec_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvRec.MouseUp
        If e.Button = MouseButtons.Right Then
            cmenuLigne.Show(Control.MousePosition)
        End If

    End Sub


    ''renvoi l'index de la première ligne correspondant a une signature possible 
    ''dans les lignes non déjà rejetées
    ''relativement à l'index iLigne 
    ''lorsque la case sqLift disparait
    'Private Function indexNextRec(idep As Integer, sqLift As String, ByRef uci_move As String) As Byte
    '    Dim LesRecs As String
    '    Dim RecPossibles As String()
    '    Dim LeCoup As String()
    '    Dim iMin As Byte = 255

    '    LesRecs = ThePOS.GetRecs(sqLift) 'récupère les signatures possibles forme : 195.195...195 a1h8|195.195...195 a1h8
    '    RecPossibles = LesRecs.Split("|") 'sépare le rec

    '    For c = 0 To RecPossibles.Count - 1 'pour chaque signature possible
    '        LeCoup = RecPossibles(c).Split(" ") 'sépare la signature du coup
    '        For i = 0 To 10 'cherche dans les 10 signatures suivantes
    '            If lvRec.Items(idep + i).SubItems(lv_rec).Text = LeCoup(0) Then 'si la signature correspond
    '                If lvRec.Items(idep + i).ForeColor <> Color.Red Then 'si la ligne n'a pas été rejeté
    '                    If lvRec.Items(idep + i).SubItems(lv_on).Text = LeCoup(1).Substring(2, 2) Then 'si la case d'arrivé du coup est celle qui vient de s'allumer
    '                        If i < iMin Then
    '                            uci_move = LeCoup(1)
    '                            iMin = i
    '                        End If
    '                    Else
    '                        If LeCoup(1) = "e1g1" Or LeCoup(1) = "e8g8" Then
    '                            If i < iMin Then
    '                                uci_move = LeCoup(1)
    '                                iMin = i
    '                            End If
    '                        End If
    '                    End If

    '                Else
    '                    'Debug.Print("ligne " & idep + i & " déjà rejeté")
    '                End If
    '            End If
    '        Next
    '    Next
    '    Return iMin 'on a rien trouvé
    'End Function

    'regarde si une case c'est allumée entre deux lignes
    Private Function CestEteint(ByVal CetteCase As String, ByVal entreLaLigne As Integer, ByVal etLaligne As Integer) As Boolean
        Dim i As Integer

        For i = entreLaLigne To etLaligne
            If lvRec.Items(i).SubItems(lv_off).Text = CetteCase Then Return True
        Next
        Return False
    End Function

    'regarde si une case c'est éteinte entre deux lignes
    Private Function CestAllume(ByVal CetteCase As String, ByVal entreLaLigne As Integer, ByVal etLaligne As Integer) As Boolean
        For i = entreLaLigne To etLaligne
            If lvRec.Items(i).SubItems(lv_on).Text = CetteCase Then Return True
        Next
        Return False
    End Function


    'renvoi l'index de la première ligne correspondant a une signature possible 
    'dans les lignes non déjà rejetées
    'relativement à l'index iLigne 
    Private Function indexNextAllRec(ByVal idep As Integer, ByRef uci_move As String) As Byte
        Dim LesRecs As String
        Dim RecPossibles As String()
        Dim LeCoup As String()
        Dim iMin As Byte = 255

        LesRecs = ThePOS.GetAllRecs()  'récupère les signatures possibles forme : 195.195...195 a1h8|195.195...195 a1h8

        If LesRecs <> "" Then
            RecPossibles = LesRecs.Split("|") 'sépare le rec

            For c = 0 To RecPossibles.Count - 1 'pour chaque signature possible
                LeCoup = RecPossibles(c).Split(" ") 'sépare la signature du coup
                For i = 0 To 30 'cherche dans les signatures suivantes
                    If (idep + i) <= lvRec.Items.Count - 1 Then
                        'If Convert.ToByte(lvRec.Items(idep + i).SubItems(lv_Nb).Text) >= Convert.ToByte(lvRec.Items(idep + i).SubItems(lv_Nb).Text) Then 'si le nombre de pièce n'augmente pas 

                        If lvRec.Items(idep + i).SubItems(lv_rec).Text = LeCoup(0) Then 'si la signature correspond



                            If lvRec.Items(idep + i).ForeColor <> Color.Red Then 'si la ligne n'a pas été rejeté




                                'ET SI LA CASE DE DEPART c EST ETEINTE ET LA CASE D'ARRIVEE C'EST ALLUMEE DEPUIS LE DERNIER COUP
                                If CestEteint(LeCoup(1).Substring(0, 2), idep, idep + i) And CestAllume(LeCoup(1).Substring(2, 2), idep, idep + i) Then

                                    lvRec.Items(idep + i).ForeColor = Color.Aqua
                                    lvRec.Items(idep + i).SubItems(lv_Move).Text = ThePOS.MovesPlayed.ToString & IIf(ThePOS.WhiteToPlay, " ", "... ") & LeCoup(2)

                                    Dim bakfen As String
                                    bakfen = ThePOS.GetFEN

                                    ThePOS.MakeMove(LeCoup(1))
                                    lvRec.Items(idep + i).SubItems(lv_FEN).Text = ThePOS.GetFEN

                                    ThePOS.SetFEN(bakfen)

                                    If i < iMin Then
                                        uci_move = LeCoup(1)
                                        iMin = i
                                    End If
                                End If

                                'If lvRec.Items(idep + i).SubItems(lv_on).Text = LeCoup(1).Substring(2, 2) Then 'si la case d'arrivé du coup est celle qui vient de s'allumer
                                '    If i < iMin Then
                                '        uci_move = LeCoup(1)
                                '        iMin = i
                                '    End If
                                'Else
                                '    If LeCoup(1) = "e1g1" Or LeCoup(1) = "e8g8" Or LeCoup(1) = "e1c1" Or LeCoup(1) = "e8c8" Then
                                '        If i < iMin Then
                                '            uci_move = LeCoup(1)
                                '            iMin = i
                                '        End If
                                '    End If
                                'End If

                                'If i < iMin Then
                                '    uci_move = LeCoup(1)
                                '    iMin = i
                                'End If

                            End If
                        End If
                        'End If
                        'Else
                        '    Return 255
                    End If
                Next
            Next

        End If

        Return iMin 'on a rien trouvé
    End Function

    'reprise de l'ancienne fonction avec les modifications nécessaire au LIVE
    Private Function indexNextAllRecLive(ByVal idep As Integer, ByRef uci_move As String) As Byte
        Dim LesRecs As String
        Dim RecPossibles As String()
        Dim LeCoup As String()
        Dim iMin As Byte = 255

        LesRecs = ThePOS.GetAllRecs()  'récupère les signatures possibles forme : 195.195...195 a1h8|195.195...195 a1h8



        RecPossibles = LesRecs.Split("|") 'sépare le rec

        For c = 0 To RecPossibles.Count - 1 'pour chaque signature possible
            LeCoup = RecPossibles(c).Split(" ") 'sépare la signature du coup
            For i = 0 To 30 'cherche dans les signatures suivantes
                If (idep + i) < lvRec.Items.Count Then
                    If lvRec.Items(idep + i).SubItems(lv_rec).Text = LeCoup(0) Then 'si la signature correspond
                        'ET SI LA CASE DE DEPART c EST ETEINTE ET LA CASE D'ARRIVEE C'EST ALLUMEE DEPUIS LE DERNIER COUP
                        If CestEteint(LeCoup(1).Substring(0, 2), idep, idep + i) And CestAllume(LeCoup(1).Substring(2, 2), idep, idep + i) Then

                            ThePOS.MakeMove(LeCoup(1))
                            lvRec.Items(idep + i).SubItems(lv_FEN).Text = ThePOS.GetFEN
                            lvRec.Items(idep + i).ForeColor = Color.Green

                            Return i
                        End If
                    End If
                End If
            Next
        Next


        Return 255


    End Function


    'cherche une correspondance dans les signatures suivantes
    'Private Sub AddLineToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles mniFindRec.Click

    '    Dim idep As Integer
    '    Dim RecPossibles As String()
    '    Dim LesRecs As String
    '    Dim imax As Integer
    '    Dim sqFrom As String
    '    Dim sqTo As String
    '    Dim LeCoup As String()
    '    Dim OnJoue As String

    '    sqFrom = lvRec.SelectedItems.Item(0).SubItems(lv_off).Text 'récupère la case qui s'éteint
    '    LesRecs = ThePOS.GetRecs(sqFrom) 'récupère les signatures possibles forme : 195.195...195 a1h8|195.195...195 a1h8

    '    RecPossibles = LesRecs.Split("|")
    '    idep = lvRec.SelectedItems.Item(0).Index 'ligne de départ

    '    For c = 0 To RecPossibles.Count - 1 'pour chaque signature possible
    '        LeCoup = RecPossibles(c).Split(" ")
    '        For i = 1 To 10 'cherche dans les 10 signatures suivantes
    '            If lvRec.Items(idep + i).SubItems(lv_rec).Text = LeCoup(0) Then
    '                lvRec.Items(idep + imax).ForeColor = Color.Green
    '                If i >= imax Then
    '                    imax = i
    '                    OnJoue = LeCoup(1)
    '                End If

    '            End If
    '        Next
    '    Next
    '    lvRec.Items(idep + imax).ForeColor = Color.Blue
    '    sqTo = lvRec.Items(idep + imax).SubItems(lv_on).Text
    '    If ThePOS.IsValidMove(OnJoue) Then
    '        ThePOS.MakeMove(OnJoue)
    '    End If
    '    lvRec.Items(idep + imax).SubItems(lv_FEN).Text = ThePOS.GetFEN
    'End Sub

    'cherche le coup suivant


    'renvoie la dernière ligne de couleur verte
    Private Function FindLastGreenLine() As Integer

        Dim i As Integer

        i = lvRec.Items.Count 'nombre d'élément de la LV


        While i > 0
            i = i - 1
            If lvRec.Items(i).ForeColor = Color.Green Then
                Return i
            End If
        End While

        Return -1
    End Function

    'renvoie la dernière ligne de couleur verte
    Private Function CancelLastGreenLine(Optional ByVal Nbligne As Byte = 1) As Integer
        Dim findgreen As Byte = 0
        Dim i As Integer
        Dim LastGreenLine As UInteger
        i = lvRec.Items.Count 'nombre d'élément de la LV


        While i > 0 And findgreen < Nbligne
            i = i - 1
            If lvRec.Items(i).ForeColor = Color.Green Then
                findgreen += 1
                LastGreenLine = i
            End If
        End While

        If LastGreenLine = 0 Then LastGreenLine = 1

        For i = LastGreenLine To lvRec.Items.Count - 1
            lvRec.Items(i).ForeColor = Color.White
            lvRec.Items(i).SubItems(lv_FEN).Text = ""
            lvRec.Items(i).SubItems(lv_Move).Text = ""
        Next


    End Function

    Private Function CancelAllMoves()
        For i = 2 To lvRec.Items.Count - 1
            lvRec.Items(i).ForeColor = Color.White
            lvRec.Items(i).SubItems(lv_FEN).Text = ""
            lvRec.Items(i).SubItems(lv_Move).Text = ""
        Next
    End Function


    Private Function FindNextMove() As Boolean
        Dim LeSuivant As Byte
        Dim idep As Integer

        Dim lecoup, leboncoup As String

        Dim PasTrouve As Boolean = True
        Dim i As Integer
        Dim aFen As String

        Dim OnStop As Boolean

        'On Error GoTo err

        ThePOS.onLive = False
        leboncoup = ""
        i = lvRec.Items.Count - 1 'nombre d'élément de la LV



        'ALGO ON SE PLACE SUR LE DERNIER COUP RECONNU (ligne verte) ET ON RECUPERE SON FEN
        'While PasTrouve
        '    i = i - 1
        '    If i = -1 Then Return False
        '    If lvRec.Items(i).ForeColor = Color.Green Then

        '        aFen = lvRec.Items(i).SubItems(lv_FEN).Text
        '        ThePOS.SetFEN(aFen)

        '        PasTrouve = False
        '        idep = i + 1
        '    End If

        'End While

        i = FindLastGreenLine()


        idep = i + 1
        If i = -1 Then Return False
        If i = lvRec.Items.Count - 1 Then Return False

        aFen = lvRec.Items(i).SubItems(lv_FEN).Text
        ThePOS.SetFEN(aFen)


        ''si le nombre augmente de deux
        'If CByte(lvRec.Items(idep - 1).SubItems(lv_Nb).Text) + 2 = CByte(lvRec.Items(idep + 1).SubItems(lv_Nb).Text) Then
        '    If MsgBox("Le nombre de pièce augmente de deux" & vbCrLf & "après la ligne " & idep.ToString & vbCrLf & "Continuer ?", MsgBoxStyle.YesNo, "Fin possible !") = vbNo Then
        '        lvRec.Items(idep).EnsureVisible()
        '        Return False

        '    End If

        'End If


        'ALGO ON CHERCHE LA PROCHAINE POSITION POSSIBLE
        LeSuivant = indexNextAllRec(idep, lecoup)
        If LeSuivant <> 255 Then
            lvRec.Items(idep + LeSuivant).ForeColor = Color.Green
            lvRec.Items(idep + LeSuivant).EnsureVisible()
            If ThePOS.IsValidMove(lecoup) Then
                If lvRec.Items(idep + LeSuivant).SubItems(lv_time).Text = "" Then
                    If Environment.TickCount - LastReceive > 1000 Then
                        Debug.Print(ThePOS.PGNmove(lecoup))
                        leboncoup = lecoup
                        ThePOS.MakeMove(lecoup)
                        lvRec.Items(idep + LeSuivant).SubItems(lv_FEN).Text = ThePOS.GetFEN
                    End If
                End If


                'DrawPiece()

            End If
        Else
            'ON VERIFIE QU IL RESTE DES LIGNES CYAN APRES SINON ON STOP
            OnStop = True
            For suivant = 1 To 30
                If idep + suivant < lvRec.Items.Count Then
                    If lvRec.Items(idep + suivant).ForeColor = Color.Aqua Then
                        OnStop = False
                        Exit For
                    End If
                End If
            Next
            If OnStop Then
                'MsgBox("Plus de coup après la ligne " & idep, vbInformation)
                'lvRec.Items(idep).EnsureVisible()
                Return False
            Else
                lvRec.Items(idep - 1).ForeColor = Color.Red
            End If


        End If
        If leboncoup <> "" Then
            joueLiChess(leboncoup)
        End If

        Return True

    End Function

    '*************************** FONCTION LICHESS **************************************
#Region "LICHESS BOARD"


    '************************************************************************************************
    'renvoie les deux cases qui viennent d'etre jouées en verifiant si elles sont vertes 
    'case1 la case de départ c-a-d celle qui est vide
    'case2 la case de fin
    '************************************************************************************************
    Private Sub FindNewCase(ByRef case1 As Byte, ByRef case2 As Byte)

        Dim myBmp As New Bitmap(echiquier.Width, echiquier.Height)
        Dim g As Graphics = Graphics.FromImage(myBmp)
        g.CopyFromScreen(echiquier.Location, Point.Empty, myBmp.Size)
        g.Dispose()

        case1 = 0
        case2 = 0

        For ligne = 1 To 8
            For colonne = 1 To 8
                'si la case est verte claire
                If myBmp.GetPixel((echiquier.Width \ 8) * (colonne - 1) + 5, (echiquier.Width \ 8) * (8 - ligne) + 5) = _
                    Color.FromArgb(255, 205, 210, 106) Then
                    'si la case est verte  claire au centre
                    'If myBmp.GetPixel((echiquier.Width \ 8) * (colonne - 1) + echiquier.Width / 16, _
                    '(echiquier.Width \ 8) * (8 - ligne) + echiquier.Width / 16) = Color.FromArgb(255, 205, 210, 106) Then
                    'la case est vide
                    If case1 = 0 Then
                        case1 = ligne * 10 + colonne
                    Else
                        case2 = ligne * 10 + colonne
                    End If

                    'Else
                    'case2 = ligne * 10 + colonne
                    'End If

                End If
                'si la case est verte foncé
                If myBmp.GetPixel((echiquier.Width \ 8) * (colonne - 1) + 5, (echiquier.Width \ 8) * (8 - ligne) + 5) = _
                    Color.FromArgb(255, 170, 162, 58) Then
                    'si la case est verte foncé au centre
                    'If myBmp.GetPixel((echiquier.Width \ 8) * (colonne - 1) + echiquier.Width / 16, _
                    '(echiquier.Width \ 8) * (8 - ligne) + echiquier.Width / 16) = Color.FromArgb(255, 170, 162, 58) Then
                    If case1 = 0 Then
                        'la case est vide
                        case1 = ligne * 10 + colonne
                    Else
                        case2 = ligne * 10 + colonne
                    End If

                    'End If
                End If
                If case2 <> 0 And case1 <> 0 Then Exit For
            Next
            If case2 <> 0 And case1 <> 0 Then Exit For
        Next

        myBmp.Dispose()

    End Sub


    Private Sub joueLiChess(ByVal lescases As String)

        Dim lastPos As Point
        Dim ToClic As Point

        Dim dep, arr As String
        Dim sq1, sq2 As Byte
        Dim New_sq1, New_sq2 As Byte
        Dim l1, c1 As Byte
        Dim l2, c2 As Byte
        Dim Nb_Essai As Byte = 0

        If CheckMoveToolStripMenuItem.Checked Then

            Debug.Print("JOUE LICHESS")
            effaceBoard()


            dep = lescases.Substring(0, 2)
            arr = lescases.Substring(2, 2)

            sq1 = ThePOS.SquareIndex(dep)
            sq2 = ThePOS.SquareIndex(arr)

            l1 = sq1 \ 10
            c1 = sq1 Mod 10

            l2 = sq2 \ 10
            c2 = sq2 Mod 10

            lastPos = Cursor.Position

            FindNewCase(New_sq1, New_sq2)

            '          While ((New_sq1 <> sq1 And New_sq1 <> sq2) Or _
            '    (New_sq2 <> sq1 And New_sq2 <> sq2)) And _
            '   Nb_Essai < 10

            'Nb_Essai += 1

            ToClic.X = echiquier.X + echiquier.Width \ 16 + (echiquier.Width \ 8) * (c1 - 1)
            ToClic.Y = echiquier.Y + echiquier.Width \ 16 + (echiquier.Width \ 8) * (8 - l1)

            Cursor.Position = ToClic
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            System.Threading.Thread.Sleep(50)
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)

            System.Threading.Thread.Sleep(100)

            ToClic.X = echiquier.X + echiquier.Width \ 16 + (echiquier.Width \ 8) * (c2 - 1)
            ToClic.Y = echiquier.Y + echiquier.Width \ 16 + (echiquier.Width \ 8) * (8 - l2)

            Cursor.Position = ToClic
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
            System.Threading.Thread.Sleep(50)
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)

            FindNewCase(New_sq1, New_sq2)

            'System.Threading.Thread.Sleep(100)
            '         End While

            Cursor.Position = lastPos
        End If
    End Sub

    'Private Sub allumeled(ByVal lescases As String)
    '    Dim dep, arr As String
    '    Dim sq1, sq2 As Byte
    '    Dim l1, c1 As Byte
    '    Dim l2, c2 As Byte

    '    SerialPort1.Write("c")

    '    dep = lescases.Substring(0, 2)
    '    arr = lescases.Substring(2, 2)

    '    sq1 = ThePOS.SquareIndex(dep)
    '    sq2 = ThePOS.SquareIndex(arr)

    '    l1 = sq1 \ 10
    '    c1 = sq1 Mod 10

    '    l2 = sq2 \ 10
    '    c2 = sq2 Mod 10

    '    SerialPort1.Write("o" & l1.ToString & "," & c1.ToString)
    '    SerialPort1.Write("o" & l2.ToString & "," & c2.ToString)

    'End Sub

    Private Sub effaceBoard()
        If SerialPort1.IsOpen Then
            Debug.Print("EFFACE")
            SerialPort1.WriteLine("c")
        End If
    End Sub

    Private Sub allumeLedId(ByVal sq1 As Byte, ByVal sq2 As Byte)
        Dim cmdserial As String

        Dim l1, c1 As Byte
        Dim l2, c2 As Byte

        If sq1 <> 0 And sq2 <> 0 Then

            l1 = sq1 \ 10
            c1 = sq1 Mod 10

            l2 = sq2 \ 10
            c2 = sq2 Mod 10

            If SerialPort1.IsOpen Then
                'cmdserial = "s" & l1.ToString & c1.ToString & l2.ToString & c2.ToString & _
                '                      "c" & l1.ToString & c1.ToString & l2.ToString & c2.ToString & "e"
                cmdserial = "o" & l1.ToString & "," & c1.ToString & "," & l2.ToString & "," & c2.ToString

                SerialPort1.WriteLine(cmdserial)
                Debug.Print(cmdserial)
            End If
        End If

    End Sub

#End Region


    'meme chose mais pour la recherche en direct
    'reprise de l'ancienne fonction avec les modifications nécessaire au LIVE
    Private Function FindNextMoveLive() As Boolean
        Dim LeSuivant As Byte
        Dim idep As Integer

        Dim lecoup As String = ""
        Dim PasTrouve As Boolean = True
        Dim i As Integer
        Dim aFen As String


        ThePOS.onLive = True

        i = lvRec.Items.Count - 1 'nombre d'élément de la LV

        i = FindLastGreenLine()
        idep = i + 1
        If i = -1 Then Return False

        aFen = lvRec.Items(i).SubItems(lv_FEN).Text
        ThePOS.SetFEN(aFen)

        'ALGO ON CHERCHE LA PROCHAINE POSITION POSSIBLE
        LeSuivant = indexNextAllRecLive(idep, lecoup)

        If LeSuivant <> 255 Then
            DrawPiece()
        End If

        Return True

    End Function






    Private Sub sslbl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sslbl1.Click
        FindNextMove()
    End Sub





    Private Sub OuvreFichier()
        Dim nomfichier As String = ""

        On Error GoTo ErrorHandler

        '___________________________________________________________________________________________________________
        'ouverture du fichier
        With OpenFileDialog1
            'On spécifie l'extension de fichiers visibles
            .FileName = ""
            .Filter = "ARD Files (*.*) | *.*"
            'On affiche et teste le retour du dialogue
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                'On récupère le nom du fichier
                nomfichier = .FileName
            End If
        End With
        '___________________________________________________________________________________________________________

        Dim RecTempo() As String
        Dim RecordsInFile As String



        RecordsInFile = My.Computer.FileSystem.ReadAllText(nomfichier)

        RecTempo = RecordsInFile.Split(Chr(10) + Chr(13)) 'sépare les différentes lignes

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim NewLine As String
        Dim LastLine As String
        Dim sqOn As String = ""
        Dim sqOff As String = ""
        Dim NbP As Integer
        LastLine = RecTempo(0).Substring(0, RecTempo(0).Length - 2)

        AddFirstLine()

        '___________________________________________________________________________________________________________    
        j = j + 2
        i = i + 1
        '___________________________________________________________________________________________________________
        'rempli lvREC
        While j < RecTempo.Length - 2

            lvRec.Items.Add(i)
            NewLine = RecTempo(j).Substring(0, RecTempo(j).Length - 2) 'supprimer l . en fin de ligne
            lvRec.Items(i).SubItems.Add(NewLine)
            'If RecTempo(j + 1) > 99 Then
            '    lvRec.Items(i).SubItems.Add(RecTempo(j + 1)) 'ajoute le temps de stabilité
            'Else
            '    lvRec.Items(i).SubItems.Add("")
            'End If
            lvRec.Items(i).SubItems.Add(RecTempo(j + 1)) 'ajoute le temps de stabilité

            FindOnOff(NewLine, LastLine, sqOn, sqOff) 'cherche les différence            

            If sqOn <> "" And sqOff <> "" Then

                Dim NewLine2 As String

                NewLine2 = CaseOff(sqOff, LastLine) 'on etteint la case qui s'était allumé
                lvRec.Items(i).SubItems(lv_rec).Text = NewLine2
                lvRec.Items(i).SubItems.Add(sqOff)
                lvRec.Items(i).SubItems.Add("")
                lvRec.Items(i).SubItems.Add("")
                NbP = NbPiece(NewLine)
                lvRec.Items(i).SubItems.Add(NbP) 'nombre de piece
                lvRec.Items(i).SubItems.Add("") 'mouvement
                lvRec.Items(i).ForeColor = Color.Cyan

                i = i + 1

                '--------------------------------------------

                NewLine2 = CaseOn(sqOn, LastLine) 'on etteint la case qui s'était allumé
                lvRec.Items.Add(i)

                lvRec.Items(i).SubItems.Add(NewLine2)
                lvRec.Items(i).SubItems.Add(RecTempo(j + 1))
                lvRec.Items(i).SubItems.Add("")
                lvRec.Items(i).SubItems.Add(sqOn)
                lvRec.Items(i).SubItems.Add("")
                NbP = NbPiece(NewLine)
                lvRec.Items(i).SubItems.Add(NbP) 'nombre de piece
                lvRec.Items(i).SubItems.Add("") 'mouvement
                lvRec.Items(i).ForeColor = Color.Cyan
                i = i + 1

                '--------------------------------------------
                lvRec.Items.Add(i)
                NewLine2 = CaseOff(sqOff, LastLine) 'on etteint la case qui s'était allumé
                NewLine2 = CaseOn(sqOn, NewLine2) 'on etteint la case qui s'était allumé
                lvRec.Items(i).SubItems.Add(NewLine2)
                lvRec.Items(i).SubItems.Add(RecTempo(j + 1))
                lvRec.Items(i).SubItems.Add(sqOff)
                lvRec.Items(i).SubItems.Add("")
                lvRec.Items(i).SubItems.Add("")
                NbP = NbPiece(NewLine)
                lvRec.Items(i).SubItems.Add(NbP) 'nombre de piece
                lvRec.Items(i).SubItems.Add("") 'mouvement
                lvRec.Items(i).ForeColor = Color.Cyan
                i = i + 1

                j = j + 2
                LastLine = NewLine
            Else

                lvRec.Items(i).SubItems.Add(sqOff)
                lvRec.Items(i).SubItems.Add(sqOn)
                lvRec.Items(i).SubItems.Add("")
                NbP = NbPiece(NewLine)
                lvRec.Items(i).SubItems.Add(NbP) 'nombre de piece
                lvRec.Items(i).SubItems.Add("") 'mouvement



                LastLine = NewLine

                If NewLine = "195.195.195.195.195.195.195.195" Then
                    If j < 50 Then
                        AddFirstLine()

                        i = 0
                    End If

                End If


                j = j + 2
                i = i + 1

            End If



        End While
        '___________________________________________________________________________________________________________
        Exit Sub
ErrorHandler:

        MsgBox("Error in LoadRecords : " & vbCrLf & Err.Description)

    End Sub






#Region "Menu"

    Private Sub menuOuvrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuOuvrir.Click
        OuvreFichier()


    End Sub


    Private Sub menuCoup_Tous_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCoup_Tous.Click
        While FindNextMove()

        End While
        DrawPiece()
    End Sub

    Private Sub nouveauDepart()
        Dim idep As Integer


        idep = lvRec.SelectedItems.Item(0).Index + 1 'ligne de départ


        For l = idep To lvRec.Items.Count - 1
            lvRec.Items(l).ForeColor = Color.White
            lvRec.Items(l).SubItems(lv_FEN).Text = ""
            lvRec.Items(l).SubItems(lv_Move).Text = ""
        Next
    End Sub


    Private Sub menuLigne_depart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLigne_depart.Click
        nouveauDepart()

    End Sub

    Private Sub menuCoup_EnPGN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCoup_EnPGN.Click
        'My.Computer.FileSystem.WriteAllText("c:\live.pgn", TransfertMove(False), False)
        TransfertMove(False)

    End Sub

    Private Sub cmenuLigne_Depart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuLigne_Depart.Click
        nouveauDepart()

    End Sub

    Private Sub changecouleur(ByVal ToColor As Color)
        Dim i As Integer

        i = lvRec.SelectedItems.Item(0).Index

        lvRec.Items(i).ForeColor = ToColor

    End Sub


    Private Sub cmenuLigne_Verte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuLigne_Verte.Click
        changecouleur(Color.Green)

    End Sub

    Private Sub menuCouleurVerte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCouleurVerte.Click
        changecouleur(Color.Green)
    End Sub

    Private Sub menuCouleur_Rouge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCouleur_Rouge.Click
        changecouleur(Color.Red)
    End Sub

    Private Sub cmenuLigne_Rouge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuLigne_Rouge.Click
        changecouleur(Color.Red)
    End Sub

    Private Sub cmenuLigne_Blanche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuLigne_Blanche.Click
        changecouleur(Color.White)
    End Sub

    Private Sub menuCouleurBlanche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCouleurBlanche.Click
        changecouleur(Color.White)
    End Sub

    Private Sub EffacerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLigne_Supprimer.Click
        Dim i As Integer
        i = lvRec.SelectedItems.Item(0).Index

        lvRec.Items(i).Remove()

    End Sub

    Private Sub menuLigne_Derniere_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuLigne_Derniere.Click
        While lvRec.SelectedIndices(0) <> lvRec.Items.Count - 1
            lvRec.Items.RemoveAt(lvRec.Items.Count - 1)
        End While
    End Sub


    Private Sub MenuModifier_Rec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuModifier_Rec.Click
        Dim laCase As String
        Dim tempo As String = ""

        tempo = lvRec.SelectedItems(0).SubItems(lv_rec).Text
        laCase = InputBox("Modifier ligne", , tempo)

        lvRec.SelectedItems(0).SubItems(lv_rec).Text = laCase
    End Sub

#End Region



    Private Sub cmenuBoard_Allumer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuBoard_Allumer.Click
        Dim sqI As String

        sqI = PictureBox1.Tag

        For i = 0 To lvRec.SelectedItems.Count - 1
            lvRec.SelectedItems(i).SubItems(lv_rec).Text = CaseOn(sqI, lvRec.SelectedItems(i).SubItems(lv_rec).Text)

        Next






    End Sub


    Private Sub cmenuBoard_Eteindre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmenuBoard_Eteindre.Click
        Dim sqI As String

        sqI = PictureBox1.Tag

        For i = 0 To lvRec.SelectedItems.Count - 1
            lvRec.SelectedItems(i).SubItems(lv_rec).Text = CaseOff(sqI, lvRec.SelectedItems(i).SubItems(lv_rec).Text)

        Next
    End Sub



    Private Sub NouveauToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NouveauToolStripMenuItem.Click
        EffacetousCoup()
        AddFirstLine()

    End Sub

    Public Sub SaveNextFile()
        Dim NumFile As Integer

        On Error Resume Next

        While My.Computer.FileSystem.FileExists("c:\GAME\game" & NumFile.ToString & ".txt")
            NumFile += 1
        End While

        My.Computer.FileSystem.WriteAllText("c:\GAME\game" & NumFile.ToString & ".txt", strAllRecs, True)


    End Sub


    Private Sub SerialPort1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Debug.Print("received")
        ReceivedText(SerialPort1.ReadLine())
        LastReceive = Environment.TickCount

    End Sub



    Private Sub SetRec(ByVal aRec As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.lvRec.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetRec)
            Me.Invoke(d, New Object() {aRec})
        Else
            AddRec(aRec)
        End If
    End Sub

    Private Sub SetTime(ByVal aTime As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.lvRec.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetTime)
            Me.Invoke(d, New Object() {aTime})
        Else
            AddTime(aTime)
        End If
    End Sub


    Private Sub ReceivedText(ByVal [text] As String) 'input from ReadExisting

        Debug.Print([text])
        If Not [text].Contains(".") Then
            SetTime([text])
        Else
            SetRec([text])
        End If

    End Sub

    'Permet de changer l'orientation de base du plateau
    Public Function Modif_Orientation(ByVal aRec As String) As String
        Dim theByte(8) As Byte
        Dim theCol As String()
        Dim thebits(8, 8) As Byte
        Dim strPos As String = ""
        Dim tempoByte As Byte


        'If aRec.EndsWith(".") Then
        '    strPos = aRec.Substring(0, aRec.Length - 2) 'enlève le . de la fin et l'espace ?!
        'End If


        If aRec = "" Then Exit Function

        theCol = aRec.Split(".") 'sépare les différents bytes
        strPos = ""

        'décompose les bytes en tableau de bit
        For i = 0 To 7
            theByte(i) = theCol(i)
            For j = 0 To 7
                thebits(i, j) = IIf(theByte(i) And (2 ^ j), 1, 0)
            Next
        Next

        If menuA1.Checked Then
            If menuFlip.Checked Then
                'les lignes deviennent des colonnes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(7 - j, 7 - i)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos

            Else
                'inversons les colonnes et les lignes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(7 - i, 7 - j)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos
            End If
        End If
        If menuA8.Checked Then
            If menuFlip.Checked Then
                'on inverse les colonnes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(7 - i, j)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos

            Else
                'les lignes deviennent des colonnes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(j, 7 - i)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos

            End If
        End If
        If menuH1.Checked Then
            If menuFlip.Checked Then

                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(i, 7 - j)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos

            Else
                'les lignes deviennent des colonnes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(7 - j, i)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos
            End If
        End If
        If menuH8.Checked Then
            If menuFlip.Checked Then
                'les lignes deviennent des colonnes
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(j, i)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos

            Else
                'on inverse rien
                For i = 0 To 7
                    tempoByte = 0
                    For j = 0 To 7
                        tempoByte += (2 ^ j) * thebits(i, j)
                    Next
                    strPos = strPos & tempoByte & "."
                Next
                Return strPos
            End If
        End If
    End Function

    Private Sub AddRec(ByVal aRec As String)
        Dim i As Integer
        Dim mod_rec As String
        Dim NewLine As String
        Dim LastLine As String
        Dim sqOn As String = ""
        Dim sqOff As String = ""
        Dim nbP As String

        Debug.Print(aRec)

        mod_rec = Modif_Orientation(aRec)

        i = lvRec.Items.Count


        NewLine = mod_rec.Substring(0, mod_rec.Length - 1)
        LastLine = lvRec.Items(i - 1).SubItems(lv_rec).Text

        FindOnOff(NewLine, LastLine, sqOn, sqOff) 'cherche les différences    



        If sqOn <> "" And sqOff <> "" Then

            Dim NewLine2 As String

            NewLine2 = CaseOff(sqOff, LastLine) 'on etteint la case qui s'était allumé
            nbP = NbPiece(NewLine2)

            lvRec.Items.Add(i)
            lvRec.Items(i).SubItems.Add(NewLine2)
            lvRec.Items(i).SubItems.Add("") 'temps
            lvRec.Items(i).SubItems.Add(sqOff) 'off
            lvRec.Items(i).SubItems.Add("") 'on
            lvRec.Items(i).SubItems.Add("") 'fen
            lvRec.Items(i).SubItems.Add(nbP) 'nombre de piece
            lvRec.Items(i).SubItems.Add("") 'mouvement
            lvRec.Items(i).ForeColor = Color.Cyan
            i = i + 1

            '--------------------------------------------

            NewLine2 = CaseOn(sqOn, LastLine) 'on etteint la case qui s'était allumé
            nbP = NbPiece(NewLine2)

            lvRec.Items.Add(i)
            lvRec.Items(i).SubItems.Add(NewLine2)
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(sqOn)
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(nbP) 'nombre de piece
            lvRec.Items(i).SubItems.Add("") 'mouvement
            lvRec.Items(i).ForeColor = Color.Cyan
            i = i + 1

            '--------------------------------------------

            NewLine2 = CaseOff(sqOff, LastLine) 'on etteint la case qui s'était allumé
            NewLine2 = CaseOn(sqOn, NewLine2) 'on etteint la case qui s'était allumé
            nbP = NbPiece(NewLine2)

            lvRec.Items.Add(i)
            lvRec.Items(i).SubItems.Add(NewLine2)
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(sqOff)
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(nbP) 'nombre de piece
            lvRec.Items(i).SubItems.Add("") 'mouvement
            lvRec.Items(i).ForeColor = Color.Cyan

        Else
            nbP = NbPiece(NewLine)
            lvRec.Items.Add(i)

            lvRec.Items(i).SubItems.Add(NewLine) 'supprime le point
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(sqOff)
            lvRec.Items(i).SubItems.Add(sqOn)
            lvRec.Items(i).SubItems.Add("")
            lvRec.Items(i).SubItems.Add(nbP)
            lvRec.Items(i).SubItems.Add("")

        End If

        If NewLine = "195.195.195.195.195.195.195.195" Then
            If lvRec.Items.Count > 50 Then SaveNextFile()
            EffacetousCoup()
            effaceBoard()

            AddFirstLine()
        Else

            AttendreCoup = 0
            TimerCoup.Enabled = True
        End If


    End Sub

    Private Sub AddTime(ByVal aTime As String)
        Dim i As Integer
        i = lvRec.Items.Count - 1
        If i > 0 Then lvRec.Items(i).SubItems(lv_time).Text = aTime


    End Sub



    Private Sub menuCOM_6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_6.Click

        On Error GoTo err

        myPort = IO.Ports.SerialPort.GetPortNames()

        menuCOM_6.Visible = False

        menuCOM_1.Text = myPort(0)
        menuCOM_1.Enabled = True
        menuCOM_1.Visible = True

        menuCOM_2.Text = myPort(1)
        menuCOM_2.Enabled = True
        menuCOM_2.Visible = True

        menuCOM_3.Text = myPort(2)
        menuCOM_3.Enabled = True
        menuCOM_3.Visible = True

        menuCOM_4.Text = myPort(3)
        menuCOM_4.Enabled = True
        menuCOM_4.Visible = True

        menuCOM_5.Text = myPort(4)
        menuCOM_5.Enabled = True
        menuCOM_5.Visible = True



err:


    End Sub

    Private Sub chgMenuInit()
        menuSerialInit.Text = "Init " & SerialPortName & " " & SerialPortSpeed
        menuSerialInit.Enabled = True

    End Sub

    Private Sub menuSerialInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSerialInit.Click
        SerialPort1.PortName = SerialPortName
        SerialPort1.BaudRate = SerialPortSpeed

        Try

            SerialPort1.Open()
            menuSerialInit.Enabled = False
            menuSerialClose.Enabled = True
            TimerCoup.Enabled = True
        Catch ex As Exception
            MsgBox("Erreur lors de l'ouverture du port")
        End Try


    End Sub

    Private Sub menuCOM_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_1.Click
        SerialPortName = menuCOM_1.Text
        chgMenuInit()
    End Sub

    Private Sub menuCOM_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_2.Click
        SerialPortName = menuCOM_2.Text
        chgMenuInit()
    End Sub

    Private Sub menuCOM_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_3.Click
        SerialPortName = menuCOM_3.Text
        chgMenuInit()
    End Sub

    Private Sub menuCOM_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_4.Click
        SerialPortName = menuCOM_4.Text
        chgMenuInit()
    End Sub

    Private Sub menuCOM_5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCOM_5.Click
        SerialPortName = menuCOM_5.Text
        chgMenuInit()
    End Sub

    Private Sub menuSPEED_1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSPEED_1.Click
        SerialPortSpeed = "9600"
        chgMenuInit()
    End Sub

    Private Sub menuSPEED_4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSPEED_4.Click
        SerialPortSpeed = "115200"
        chgMenuInit()
    End Sub

    Private Sub menuSpeed_2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSPEED_2.Click
        SerialPortSpeed = "38400"
        chgMenuInit()
    End Sub

    Private Sub menuSPEED_3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSPEED_3.Click
        SerialPortSpeed = "57600"
        chgMenuInit()
    End Sub

    Private Sub menuSerialClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSerialClose.Click
        SerialPort1.Close()
        menuSerialClose.Enabled = False
        menuSerialInit.Enabled = True
        AttendreCoup = 0
        TimerCoup.Enabled = False
    End Sub

    Private Sub SauvegarderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SauvegarderToolStripMenuItem.Click
        SaveNextFile()
    End Sub

    Private Sub SendFTP()
        Try 'Instance d'essaie qui retournera une erreur en cas de problème

            Dim request As FtpWebRequest = DirectCast(WebRequest.Create("ftp://ftp.diagonaletv.com/live.pgn"), System.Net.FtpWebRequest) 'On renseigne la futur destination du fichier à envoyer
            request.Credentials = New NetworkCredential("userName", "Pass") 'On rentre les identifiant et mot de passe

            request.Method = System.Net.WebRequestMethods.Ftp.UploadFile 'On indique qu'on veut upload un fichier

            Dim files() As Byte = File.ReadAllBytes("C:\live.pgn") 'On indique le chemin du fichier à upload

            Dim strz As Stream = request.GetRequestStream() 'On créer un stream qui va nous permettre d'envoyer le fichier

            strz.Write(files, 0, files.Length) 'On envoie le fichier

            strz.Close() 'On ferme la connection
            strz.Dispose() 'On supprime la connection

        Catch ex As Exception 'Une erreur c'est produite, on récupère l'erreur et on l'affiche.
            Debug.Print("Erreur :" & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub TimerCoup_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCoup.Tick

        Dim onatrouve = True

        If Environment.TickCount - LastReceive < 1000 Then Exit Sub

        'AttendreCoup += 1
        'Debug.Print(AttendreCoup)


        'Dim ts As TimeSpan = TimeSpan.FromMilliseconds()

        While onatrouve
            If lvRec.Items.Count - FindLastGreenLine() > 2 Then
                onatrouve = FindNextMove()
                Debug.Print("FIND NEXT")
                TransfertMove(False)
            Else
                onatrouve = False
            End If
        End While




        'If CheckMoveToolStripMenuItem.Checked = False Then
        '    If AttendreCoup > 10 Then

        '        CancelLastGreenLine(2)
        '        'CancelAllMoves()
        '        While FindNextMove() And AttendreCoup < 1000
        '            AttendreCoup += 1
        '        End While
        '        'My.Computer.FileSystem.WriteAllText("c:\live.pgn", TransfertMove(), False)
        '        TransfertMove(False)
        '        AttendreCoup = 0
        '        TimerCoup.Enabled = False
        '        'SendFTP()
        '    End If
        'End If 
    End Sub

    Private Sub MenuCase_Etteindre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuCase_Etteindre.Click
        Dim laCase As String
        Dim tempo As String = ""

        tempo = lvRec.SelectedItems(0).SubItems(lv_on).Text
        laCase = InputBox("Modifier Case On", , tempo)

        lvRec.SelectedItems(0).SubItems(lv_on).Text = laCase
    End Sub

    Private Sub OffToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OffToolStripMenuItem.Click
        Dim laCase As String
        Dim tempo As String = ""

        tempo = lvRec.SelectedItems(0).SubItems(lv_off).Text
        laCase = InputBox("Modifier Case Off", , tempo)

        lvRec.SelectedItems(0).SubItems(lv_off).Text = laCase
    End Sub

    Private Sub SauvegarderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SauvegarderToolStripMenuItem1.Click
        Dim NumFile As Integer

        While My.Computer.FileSystem.FileExists("c:\GAME\game" & NumFile.ToString & ".pgn")
            NumFile += 1
        End While

        My.Computer.FileSystem.WriteAllText("c:\GAME\game" & NumFile.ToString & ".pgn", strAllMoves, True)

    End Sub

    Private Sub menuA1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuA1.Click
        menucheck("a1")
    End Sub

    Public Sub menucheck(ByVal lesens As String)

        menuA1.Checked = False
        menuA8.Checked = False
        menuH1.Checked = False
        menuH8.Checked = False
        Select Case lesens
            Case "a1"
                menuA1.Checked = True
                menuPrise.Text = "Prise en A1"
            Case "a8"
                menuA8.Checked = True
                menuPrise.Text = "Prise en A8"
            Case "h1"
                menuH1.Checked = True
                menuPrise.Text = "Prise en H1"
            Case "h8"
                menuH8.Checked = True
                menuPrise.Text = "Prise en H8"
        End Select
    End Sub

    Private Sub menuA8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuA8.Click
        menucheck("a8")
    End Sub

    Private Sub menuH1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuH1.Click
        menucheck("h1")
    End Sub

    Private Sub menuH8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuH8.Click
        menucheck("h8")
    End Sub


    Private Sub menuFlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuFlip.Click
        menuFlip.Checked = Not menuFlip.Checked
    End Sub

    Private Sub menuCoup_Suivant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCoup_Suivant.Click
        FindNextMove()

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Function [With]() As Object
        Throw New NotImplementedException
    End Function

    Private Sub EditerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditerToolStripMenuItem.Click
        frmPgnInfo.Show()


    End Sub


    '************************************************************************************************
    '************************************************************************************************
    '************************************************************************************************
    '************************************************************************************************
    'vérifie si les cases case1 et case2 (en index 10) sont toujours vertes
    'renvoie TRUE si elles ne le sont plus
    'FALSE si pas de changement
    '************************************************************************************************
    Private Function ChangeCase(ByVal case1 As Byte, ByVal case2 As Byte) As Boolean

        Dim ligne1 As Byte
        Dim ligne2 As Byte
        Dim colonne1 As Byte
        Dim colonne2 As Byte

        Dim couleur1 As Color
        Dim couleur2 As Color
        Dim couleur3 As Color


        If case1 <> 0 And case2 <> 0 Then

            Dim myBmp As New Bitmap(echiquier.Width, echiquier.Height)
            Dim g As Graphics = Graphics.FromImage(myBmp)
            g.CopyFromScreen(echiquier.Location, Point.Empty, myBmp.Size)

            g.Dispose()

            ligne1 = case1 \ 10
            colonne1 = case1 Mod 10
            ligne2 = case2 \ 10
            colonne2 = case2 Mod 10

            couleur1 = myBmp.GetPixel((echiquier.Width \ 8) * (colonne1 - 1) + 5, (echiquier.Width \ 8) * (8 - ligne1) + 5)
            couleur2 = myBmp.GetPixel((echiquier.Width \ 8) * (colonne2 - 1) + 5, (echiquier.Width \ 8) * (8 - ligne2) + 5)
            couleur3 = myBmp.GetPixel((echiquier.Width \ 8) * (colonne1 - 1) + echiquier.Width / 16, (echiquier.Width \ 8) * (8 - ligne1) + echiquier.Width / 16)

            If (couleur1 = Color.FromArgb(255, 205, 210, 106) _
                Or couleur1 = Color.FromArgb(255, 170, 162, 58)) _
                And (couleur2 = Color.FromArgb(255, 205, 210, 106) _
                Or couleur2 = Color.FromArgb(255, 170, 162, 58)) Then '_
                'And (couleur3 = couleur1) Then
                myBmp.Dispose()
                Return False
            Else
                myBmp.Dispose()
                Return True
            End If
        Else
            Return True
        End If

    End Function



   



    Private Sub ConfigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigToolStripMenuItem.Click
        Form3.Show()
        'CheckBoard()
    End Sub





    Private Sub TimerLichess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerLichess.Tick
        Static sq1, sq2, CompterSec As Byte

        'permet d'initialiser les varialbles static en désactivant puis ré activant l'option
        If TimerLichess.Interval <> 250 Then
            TimerLichess.Interval = 250
            sq1 = 0
            sq2 = 0
        End If

        'si les cases vertes ne sont pas encore trouvées
        If sq1 = 0 Or sq2 = 0 Then
            FindNewCase(sq1, sq2)
            If sq1 = 0 Or sq2 = 0 Then
                'effaceBoard()  'si on a pas trouvé les deux cases on efface tout

            Else
                allumeLedId(sq1, sq2)   'sinon on allume les led

                CompterSec = 0
            End If

        Else 'déja trouvées
            If ChangeCase(sq1, sq2) Then
                FindNewCase(sq1, sq2)
                allumeLedId(sq1, sq2)

                CompterSec = 0
            Else 'elles n'ont pas changées
                If CompterSec = 0 Then
                    allumeLedId(sq1, sq2)

                End If
                CompterSec += 1
                If CompterSec > 50 Then
                    effaceBoard()
                    CompterSec = 1

                End If
            End If

        End If
    End Sub

    Private Sub CheckBoard()
        TimerLichess.Interval = 151
        TimerLichess.Enabled = Not TimerLichess.Enabled
        CheckMoveToolStripMenuItem.Checked = TimerLichess.Enabled
    End Sub

    Private Sub CheckMoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckMoveToolStripMenuItem.Click
        CheckBoard()
    End Sub

End Class
