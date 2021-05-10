Imports System.IO
Imports System.Drawing
Public Class Form1
    Dim Bg, Bg1, Img As CImage
    Dim spritemap, spritemask As CImage
    Dim bmp As Bitmap
    Dim BC As CCharacter
    Dim ListChar As New List(Of CCharacter)
    Dim BCIntro As CArrFrame

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Bg = New CImage

        Bg.OpenImage("C:\Users\user\Downloads\bg.bmp")
        Bg.CopyImg(Img)
        Bg.CopyImg(Bg1)

        bmp = New Bitmap(Img.Width, Img.Height)

        SpriteMap = New CImage
        SpriteMap.OpenImage("C:\Users\user\Downloads\BC_spritesheet.bmp")

        spritemap.CreateMask(spritemask)

        BCIntro = New CArrFrame
        '17 intro frame
        BCIntro.Insert(100, 200, 1, 13, 65, 79, 3)
        BCIntro.Insert(100, 200, 134, 13, 204, 79, 3)
        BCIntro.Insert(100, 200, 274, 0, 343, 77, 5)
        BCIntro.Insert(100, 200, 134, 13, 204, 79, 3)
        BCIntro.Insert(100, 200, 1, 13, 65, 79, 5)
        BCIntro.Insert(100, 200, 2, 160, 75, 233, 3)
        'BCIntro.Insert(100, 100, 156, 144, 194, 191, 1)

        DisplayImg()
        ResizeImg()

        BC = New CCharacter
        ReDim BC.ArrSprites(5)
        BC.ArrSprites(0) = BCIntro

        BC.PosX = 75
        BC.PosY = 306
        BC.Vx = -5
        BC.Vy = 0
        BC.State(StateBubbleCrab.Intro, 0)
        BC.FDir = FaceDir.Left

        ListChar.Add(BC)


        bmp = New Bitmap(Img.Width, Img.Height)

        DisplayImg()
        ResizeImg()

        Timer1.Enabled = True
    End Sub
    Sub PutSprites()
        Dim cc As CCharacter
        Dim i, j As Integer
        'set background
        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                Img.Elmt(i, j) = Bg.Elmt(i, j)
            Next
        Next


        For Each cc In ListChar
            Dim EF As CElmtFrame = cc.ArrSprites(cc.IdxArrSprites).Elmt(cc.FrameIdx)
            Dim spritewidth = EF.Right - EF.Left
            Dim spriteheight = EF.Bottom - EF.Top
            If cc.FDir = FaceDir.Left Then
                Dim spriteleft As Integer = cc.PosX - EF.CtrPoint.x + EF.Left
                Dim spritetop As Integer = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), spritemask.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), spritemap.Elmt(EF.Left + i, EF.Top + j))
                    Next
                Next
            Else 'facing right
                Dim spriteleft = cc.PosX + EF.CtrPoint.x - EF.Right
                Dim spritetop = cc.PosY - EF.CtrPoint.y + EF.Top
                'set mask
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpAnd(Img.Elmt(spriteleft + i, spritetop + j), spritemask.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

                'set sprite
                For i = 0 To spritewidth
                    For j = 0 To spriteheight
                        Img.Elmt(spriteleft + i, spritetop + j) = OpOr(Img.Elmt(spriteleft + i, spritetop + j), spritemap.Elmt(EF.Right - i, EF.Top + j))
                    Next
                Next

            End If

        Next


    End Sub
    Sub DisplayImg()
        'display bg and sprite on picturebox
        Dim i, j As Integer
        PutSprites()

        For i = 0 To Img.Width - 1
            For j = 0 To Img.Height - 1
                bmp.SetPixel(i, j, Img.Elmt(i, j))
            Next
        Next

        PictureBox1.Refresh()

        PictureBox1.Image = bmp
        PictureBox1.Width = bmp.Width
        PictureBox1.Height = bmp.Height
        PictureBox1.Top = 0
        PictureBox1.Left = 0

    End Sub
    Sub ResizeImg()
        Dim w, h As Integer

        w = PictureBox1.Width
        h = PictureBox1.Height

        Me.ClientSize = New Size(w, h)

    End Sub
End Class
