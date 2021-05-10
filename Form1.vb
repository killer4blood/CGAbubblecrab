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
        spritemap.OpenImage("C:\Users\user\Downloads\bubble crab.bmp")

        spritemap.CreateMask(spritemask)

        BCIntro = New CArrFrame
        '17 intro frame
        BCIntro.Insert(360, 480, 640, 200, 740, 300, 1) 'fall
        BCIntro.Insert(360, 455, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 430, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 405, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 380, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 355, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 330, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 305, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 280, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 255, 640, 200, 740, 300, 1)
        BCIntro.Insert(360, 230, 640, 200, 740, 300, 1)
        BCIntro.Insert(357, 7, 640, 25, 740, 100, 1) 'reach floor
        BCIntro.Insert(145, 361, 400, 350, 530, 500, 1) 'crouch
        BCIntro.Insert(270, 361, 540, 300, 670, 500, 1) 'mini claw
        BCIntro.Insert(390, 361, 660, 300, 780, 500, 1) 'big claw



        'BCIntro.Insert(130, 100, 400, 100, 545, 200, 1) 'ini bc standing on the right side of screen
        'BCIntro.Insert(360, 480, 640, 200, 740, 300, 1) 'ini bc falling from top on the right side of screen

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
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        PictureBox1.Refresh()
        BC.Update()
        DisplayImg()


    End Sub
End Class
