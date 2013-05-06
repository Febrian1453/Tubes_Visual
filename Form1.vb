Option Explicit On

Public Class gameFrm
    Structure playerStructure
        Dim pieceColor As Integer
        Dim playerType As Integer
    End Structure

    Enum playerType As Integer
        HUMAN
    End Enum

    Private Const TILEWIDTH As Integer = 50, TILEHEIGHT As Integer = 50
    Private Const MAXPLAYER As Integer = 2
    Private Const BLACKPLAYER As Integer = 0, WHITEPLAYER As Integer = 1
    Dim screenBuffer As Bitmap
    Dim board As othello
    Dim player(MAXPLAYER - 1) As playerStructure
    Dim playerInTurn As Integer
    Dim animationRunning As Boolean = False
    Dim gameOver As Boolean = False

    Private Sub delay(ByVal miliSec As Single)
        Dim lastTick As Long
        ' 1 tick = 0.1 microSec = 0.0001 milisec
        lastTick = Now.Ticks
        Do
            Application.DoEvents()
        Loop Until (((Now.Ticks - lastTick) * 0.0001) > miliSec)
    End Sub

    Private Sub updatePieceNum()
        whiteLbl.Text = board.whitePieceNum.ToString
        blackLbl.Text = board.blackPieceNum.ToString
    End Sub

    Private Sub drawTurnBox(ByVal inTurnPiece As Integer)
        Dim g As Graphics, b As Bitmap
        Dim yTurnBox As Integer = 0
        b = New Bitmap(TILEWIDTH, TILEHEIGHT * 2)
        g = Graphics.FromImage(b)
        g.DrawImageUnscaled(board.blackPieceBMP, New Point(0, 0))
        g.DrawImageUnscaled(board.whitePieceBMP, New Point(0, TILEHEIGHT))
        If (inTurnPiece = othello.piece.WHITE) Then
            yTurnBox = TILEHEIGHT
        Else
            yTurnBox = 0
        End If
        g.DrawRectangle(New Pen(Color.Yellow, 2), New Rectangle(0, yTurnBox, TILEWIDTH, TILEHEIGHT))
        turnBoxPic.Image = CType(b.Clone, Image)
        g.Dispose()
    End Sub

    Private Sub drawWholeBoard()
        Dim g As Graphics
        Dim col As Integer, row As Integer
        g = Graphics.FromImage(screenBuffer)
        g.FillRectangle(Brushes.Green, New Rectangle(0, 0, 400, 400))
        For row = 0 To othello.MAXROW - 1
            For col = 0 To othello.MAXCOL - 1
                Select Case board.tile(row, col)
                    Case othello.piece.WHITE
                        g.DrawImageUnscaled(board.whitePieceBMP, New Point(col * TILEWIDTH, row * TILEHEIGHT))
                    Case othello.piece.BLACK
                        g.DrawImageUnscaled(board.blackPieceBMP, New Point(col * TILEWIDTH, row * TILEHEIGHT))
                    Case Else
                        g.DrawImageUnscaled(board.noPieceBMP, New Point(col * TILEWIDTH, row * TILEHEIGHT))
                End Select
            Next
        Next
        boardPic.Image = CType(screenBuffer.Clone, Image)
        g.Dispose()
    End Sub

    Private Sub initializeGame()
        player(BLACKPLAYER).pieceColor = othello.piece.BLACK
        player(BLACKPLAYER).playerType = playerType.HUMAN
        player(WHITEPLAYER).pieceColor = othello.piece.WHITE
        player(WHITEPLAYER).playerType = playerType.HUMAN
        board = New othello(TILEWIDTH, TILEHEIGHT, Color.Green)
        Call drawWholeBoard()
        playerInTurn = BLACKPLAYER
        gameOver = False
        Call updatePieceNum()
        Call drawTurnBox(player(playerInTurn).pieceColor)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        screenBuffer = New Bitmap(TILEWIDTH * othello.MAXCOL, TILEHEIGHT * othello.MAXROW)
        Call initializeGame()
    End Sub

    Private Sub changeTurn()
        Dim humanTurn As Boolean = False
        Do While (Not (gameOver) And Not (humanTurn))
            If playerInTurn = BLACKPLAYER Then playerInTurn = WHITEPLAYER Else playerInTurn = BLACKPLAYER
            If Not (board.isForfeit(player(playerInTurn).pieceColor)) Then
                humanTurn = True
            Else
                If (player(playerInTurn).pieceColor = othello.piece.BLACK) Then
                    MsgBox("Black pass. No valid movement", MsgBoxStyle.Exclamation, "Forfeit")
                Else
                    MsgBox("White pass. No valid movement", MsgBoxStyle.Exclamation, "Forfeit")
                End If
                gameOver = board.isGameOver()
            End If
        Loop
        If (gameOver) Then
            MsgBox("Game set!", MsgBoxStyle.Information)
            Select Case (board.whoIsWinner)
                Case othello.piece.BLACK
                    MsgBox("Black is the winner", MsgBoxStyle.Information, "Winner")
                Case othello.piece.WHITE
                    MsgBox("White is the winner", MsgBoxStyle.Information, "Winner")
                Case othello.piece.NONE
                    MsgBox("It's a draw", MsgBoxStyle.Information, "Draw")
            End Select
        End If
        Call drawTurnBox(player(playerInTurn).pieceColor)
    End Sub

    Private Sub flipPiece(ByVal inTurnPiece As Integer, ByVal targetCol As Integer, ByVal targetRow As Integer)
        Dim xoffset As Integer, yoffset As Integer, estCapturedPiece As Integer
        Dim iterateCol As Integer, iterateRow As Integer
        animationRunning = True
        For yoffset = -1 To 1
            For xoffset = -1 To 1
                If Not ((xoffset = 0) And (yoffset = 0)) Then
                    If (board.isLegalDirection(inTurnPiece, xoffset, yoffset, targetCol, targetRow, estCapturedPiece)) Then
                        iterateCol = targetCol
                        iterateRow = targetRow
                        Do
                            iterateCol = iterateCol + xoffset
                            iterateRow = iterateRow + yoffset
                            If (board.tile(iterateRow, iterateCol) <> inTurnPiece) Then
                                board.tile(iterateRow, iterateCol) = inTurnPiece
                                Call drawPortion(iterateCol, iterateRow)
                                Call delay(250)
                            Else
                                Exit Do
                            End If
                        Loop
                    End If
                End If
            Next
        Next
        animationRunning = False
        Call board.calculatePieceNum()
    End Sub

    Private Sub drawPortion(ByVal col As Integer, ByVal row As Integer)
        Dim g As Graphics
        Dim p As Point = New Point(col * TILEWIDTH, row * TILEHEIGHT)
        g = Graphics.FromImage(screenBuffer)
        Select Case board.tile(row, col)
            Case othello.piece.WHITE
                g.DrawImageUnscaled(board.whitePieceBMP, p)
            Case othello.piece.BLACK
                g.DrawImageUnscaled(board.blackPieceBMP, p)
            Case othello.piece.NONE
                g.DrawImageUnscaled(board.noPieceBMP, p)
        End Select
        g.Dispose()
        boardPic.Image = CType(screenBuffer.Clone, Image)
    End Sub

    Private Sub putPiece(ByVal inTurnPiece As Integer, ByVal col As Integer, ByVal row As Integer)
        board.tile(row, col) = inTurnPiece
        Call drawPortion(col, row)
        animationRunning = True
        Call delay(500)
        animationRunning = False
        Call flipPiece(inTurnPiece, col, row)
        gameOver = board.isGameOver()
        Call updatePieceNum()
    End Sub

    Private Sub boardPic_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles boardPic.MouseClick
        Dim col As Integer, row As Integer
        Dim EstCapturedPiece As Integer
        If (player(playerInTurn).playerType <> playerType.HUMAN) Then Exit Sub
        If (animationRunning) Then Exit Sub
        If (gameOver) Then Exit Sub
        col = CInt(Int(e.X / TILEWIDTH))
        row = CInt(Int(e.Y / TILEHEIGHT))
        If (board.isLegalMove(player(playerInTurn).pieceColor, col, row, EstCapturedPiece)) Then
            Call putPiece(player(playerInTurn).pieceColor, col, row)
            Call changeTurn()
        Else
            MsgBox("Invalid movement", MsgBoxStyle.Exclamation, "Invalid")
        End If
    End Sub

    Private Sub startNewBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        board = Nothing
        Call initializeGame()
    End Sub
End Class
