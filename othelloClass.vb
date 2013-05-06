Public Class othello

    Public Structure boardPoint
        Dim col As Integer
        Dim row As Integer
    End Structure

    Public Const MAXCOL As Integer = 8
    Public Const MAXROW As Integer = 8

    Public Enum piece As Integer
        NONE
        WHITE
        BLACK
    End Enum

    Public tile(MAXROW - 1, MAXCOL - 1) As Integer
    Public gridValue(MAXROW - 1, MAXCOL - 1) As Integer
    Private TILEWIDTH As Integer = 50, TILEHEIGHT As Integer = 50
    Private bgColor As Color
    Public whitePieceBMP As Bitmap, blackPieceBMP As Bitmap, noPieceBMP As Bitmap
    Public blackPieceNum As Integer, whitePieceNum As Integer


    Public Sub New(ByVal tile_width As Integer, ByVal tile_height As Integer, ByVal bg_color As Color)
        Dim i As Integer, j As Integer
        TILEWIDTH = tile_width
        TILEHEIGHT = tile_height
        bgColor = bg_color
        For i = 0 To MAXROW - 1
            For j = 0 To MAXCOL - 1
                tile(i, j) = piece.NONE
            Next
        Next

        tile(3, 3) = piece.BLACK
        tile(4, 4) = piece.BLACK
        tile(3, 4) = piece.WHITE
        tile(4, 3) = piece.WHITE
        Call calculatePieceNum()
        whitePieceBMP = drawPiece(piece.WHITE)
        blackPieceBMP = drawPiece(piece.BLACK)
        noPieceBMP = drawPiece(piece.NONE)
    End Sub

    Public Sub calculatePieceNum()
        Dim col As Integer, row As Integer
        blackPieceNum = 0
        whitePieceNum = 0
        For row = 0 To MAXROW - 1
            For col = 0 To MAXCOL - 1
                Select Case tile(row, col)
                    Case piece.BLACK
                        blackPieceNum = blackPieceNum + 1
                    Case piece.WHITE
                        whitePieceNum = whitePieceNum + 1
                End Select
            Next
        Next
    End Sub

    Public Function isForfeit(ByVal inTurnPiece As Integer) As Boolean
        Dim col As Integer, row As Integer
        isForfeit = True
        For row = 0 To MAXROW - 1
            For col = 0 To MAXCOL - 1
                If (tile(row, col) = piece.NONE) Then
                    isForfeit = Not (isLegalMove(CInt(inTurnPiece), col, row))
                End If
                If Not (isForfeit) Then Exit For
            Next
            If Not (isForfeit) Then Exit For
        Next
        Return isForfeit
    End Function

    Private Function isInsideBoard(ByVal col As Integer, ByVal row As Integer) As Boolean
        If ((col >= 0) And (col < MAXCOL)) And _
            ((row >= 0) And (row < MAXROW)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function isLegalDirection(ByVal inTurnPiece As Integer, _
                                     ByVal xoffset As Integer, ByVal yoffset As Integer, _
                                     ByVal targetCol As Integer, ByVal targetRow As Integer, _
                                     ByRef capturedPieces As Integer) As Boolean

        Dim iterateCol As Integer, iterateRow As Integer
        Dim continueIterate As Boolean
        Dim legalDirection As Boolean = False
        Dim opponent As Integer
        If (inTurnPiece = piece.WHITE) Then opponent = piece.BLACK Else opponent = piece.WHITE
        iterateCol = targetCol
        iterateRow = targetRow
        capturedPieces = 0

        ' *** first check neighbor
        iterateCol = iterateCol + xoffset
        iterateRow = iterateRow + yoffset
        If (isInsideBoard(iterateCol, iterateRow)) Then
            ' *** start iterate if neighbor is opponent, otherwise skip this direction
            If (tile(iterateRow, iterateCol) = opponent) Then
                capturedPieces = capturedPieces + 1
                Do
                    iterateCol = iterateCol + xoffset
                    iterateRow = iterateRow + yoffset
                    If Not (isInsideBoard(iterateCol, iterateRow)) Then Exit Do
                    If (tile(iterateRow, iterateCol) = inTurnPiece) Then
                        legalDirection = True
                        continueIterate = False
                    ElseIf (tile(iterateRow, iterateCol) = piece.NONE) Then
                        continueIterate = False
                    Else
                        continueIterate = True
                        capturedPieces = capturedPieces + 1
                    End If
                Loop While (continueIterate)
            End If
        End If
        Return legalDirection
    End Function

    Public Function isLegalMove(ByVal inTurnPiece As Integer, ByVal targetCol As Integer, _
                                ByVal targetRow As Integer, _
                                Optional ByRef totalCapturedPieces As Integer = 0) As Boolean

        Dim xoffset As Integer, yoffset As Integer
        Dim legalMove As Boolean = False
        Dim capturedPieces As Integer = 0
        If (tile(targetRow, targetCol) <> piece.NONE) Then Return False
        For xoffset = -1 To 1
            For yoffset = -1 To 1
                If Not ((xoffset = 0) And (yoffset = 0)) Then
                    If (isLegalDirection(inTurnPiece, xoffset, yoffset, targetCol, targetRow, capturedPieces)) Then
                        legalMove = True
                        totalCapturedPieces = totalCapturedPieces + capturedPieces
                    End If
                End If
            Next
        Next
        Return legalMove
    End Function

    Public Function whoIsWinner() As Integer
        Call calculatePieceNum()
        If (blackPieceNum > whitePieceNum) Then
            Return piece.BLACK
        ElseIf (blackPieceNum < whitePieceNum) Then
            Return piece.WHITE
        Else
            Return piece.NONE
        End If
    End Function

    Public Function isGameOver() As Boolean
        Call calculatePieceNum()
        If (whitePieceNum = 0) Or (blackPieceNum = 0) Then Return True
        If (whitePieceNum + blackPieceNum) = (MAXCOL * MAXROW) Then Return True
        If (isForfeit(piece.WHITE) And isForfeit(piece.BLACK)) Then Return True
        Return False
    End Function

    Public Function drawPiece(ByVal pieceColor As Integer) As Bitmap
        Const padding = 5
        Dim lgBrush As System.Drawing.Drawing2D.LinearGradientBrush
        Dim borderPen As Pen
        Dim g As Graphics, b As Bitmap
        Dim optRect As New RectangleF(padding, padding, TILEWIDTH - (2 * padding), TILEHEIGHT - (2 * padding))
        b = New Bitmap(TILEWIDTH, TILEHEIGHT)
        g = Graphics.FromImage(b)
        g.FillRectangle(New SolidBrush(bgColor), New RectangleF(0, 0, TILEWIDTH, TILEHEIGHT))
        g.DrawRectangle(New Pen(Color.Black, 2), New Rectangle(0, 0, TILEWIDTH, TILEHEIGHT))
        Select Case pieceColor
            Case piece.WHITE
                lgBrush = New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(TILEWIDTH, TILEHEIGHT), Color.White, Color.Gray)
                borderPen = New Pen(Color.FromArgb(128, 128, 128, 128), 3)
                g.FillEllipse(lgBrush, optRect)
                g.DrawEllipse(borderPen, optRect)
            Case piece.BLACK
                lgBrush = New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(TILEWIDTH, TILEHEIGHT), Color.Gray, Color.Black)
                borderPen = New Pen(Color.FromArgb(128, 64, 64, 64), 3)
                g.FillEllipse(lgBrush, optRect)
                g.DrawEllipse(borderPen, optRect)
            Case piece.NONE
                '*** none
        End Select
        g.Dispose()
        Return b
    End Function

End Class
