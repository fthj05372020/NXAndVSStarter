'启动NX+VS面板，先设置好用于调试的环境变量，然后启动软件
Public Class Form1
    Dim aa As Integer = My.Computer.Clock.TickCount
    Private nx10dir As String = ""
    Private nx11dir As String = ""
    Private nx12dir As String = ""
    Private nx1847dir As String = ""
    Private vs2012dir As String = ""
    Private vs2013dir As String = ""
    Private vs2015dir As String = ""
    Private vs2017dir As String = ""
    Private userdir As String = ""

    Private Sub ButtonSet_Click(sender As Object, e As EventArgs) Handles ButtonSet.Click
        '设置窗口打开关闭
        If GroupBox1.Visible = True Then
            GroupBox1.Hide()
            Me.Height = 135
        Else
            GroupBox1.Show()
            Me.Height = 575
        End If

    End Sub
    '窗口加载
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim allUserDirs() As String = IO.File.ReadAllLines("userdirs.txt")
        For Each tmp As String In allUserDirs
            ComboBoxUserDir.Items.Add(tmp)
        Next
        ComboBoxUserDir.Text = ComboBoxUserDir.Items(0)
        userdir = ComboBoxUserDir.Text

        '读取设置文件
        Dim allLines() As String = IO.File.ReadAllLines("config.txt")
        If allLines.Length() >= 8 Then

            TextBoxNX10.Text = allLines(0)
            nx10dir = TextBoxNX10.Text

            TextBoxNX11.Text = allLines(1)
            nx11dir = TextBoxNX11.Text

            TextBoxNX12.Text = allLines(2)
            nx12dir = TextBoxNX12.Text

            TextBoxNX1847.Text = allLines(3)
            nx1847dir = TextBoxNX1847.Text

            TextBoxVS2012.Text = allLines(4)
            vs2012dir = TextBoxVS2012.Text

            TextBoxVS2013.Text = allLines(5)
            vs2013dir = TextBoxVS2013.Text

            TextBoxVS2015.Text = allLines(6)
            vs2015dir = TextBoxVS2015.Text

            TextBoxVS2017.Text = allLines(7)
            vs2017dir = TextBoxVS2017.Text
        End If

        '隐藏设置窗口
        GroupBox1.Hide()
        Me.Height = 135

    End Sub
    '设置确定按钮
    Private Sub ButtonConf_Click(sender As Object, e As EventArgs) Handles ButtonConf.Click

        userdir = ComboBoxUserDir.Text

        nx10dir = TextBoxNX10.Text

        nx11dir = TextBoxNX11.Text

        nx12dir = TextBoxNX12.Text

        nx1847dir = TextBoxNX1847.Text

        vs2012dir = TextBoxVS2012.Text

        vs2013dir = TextBoxVS2013.Text

        vs2015dir = TextBoxVS2015.Text

        vs2017dir = TextBoxVS2017.Text

        '隐藏设置窗口
        GroupBox1.Hide()
        Me.Height = 135
    End Sub
    '执行批处理
    Private Sub ExecCmd(cmdstr As String, Optional Ishide As Boolean = True)
        If Ishide Then
            Shell("cmd.exe /c " + cmdstr, AppWinStyle.Hide)
        Else
            Shell("cmd.exe /c " + cmdstr, AppWinStyle.NormalFocus)
        End If
    End Sub
    '启动NX
    Private Sub StartNX(NXkey As String, nxdir As String)

        Dim Nxexe As String = nxdir + "\UGII\ugraf.exe"

        Dim rootDir = Mid(nxdir, 1, nxdir.IndexOf("\"))

        If NXkey <> "NX10" Then
            Nxexe = nxdir + "\NXBIN\ugraf.exe"
        End If

        Dim exedir = Mid(Nxexe, 1, Nxexe.LastIndexOf("\") + 1)

        If IO.File.Exists(Nxexe) Then

            IO.File.Delete(".\cmdFile.bat")

            Dim allcomdStrs As New List(Of String)

            allcomdStrs.Add("set UGII_USER_DIR=" + userdir + vbCrLf)
            allcomdStrs.Add("set OUTPUT_DIR=%UGII_USER_DIR%\application" + vbCrLf)

            allcomdStrs.Add("set UGII_BASE_DIR=" + nxdir + vbCrLf)
            allcomdStrs.Add("set UGII_ROOT_DIR=" + nxdir + "\UGII" + vbCrLf)
            allcomdStrs.Add("set UGII_UGRAF_DIR=%UGII_BASE_DIR%\NXBIN" + vbCrLf)
            allcomdStrs.Add("set UGII_DISPLAY_DEBUG=1" + vbCrLf)

            '英文选项打开
            If CheckBoxEn.Checked Then
                'ExecCmd("set UGII_LANG=english")
                allcomdStrs.Add("set UGII_LANG=english" + vbCrLf)
            Else
                allcomdStrs.Add("set UGII_LANG=simpl_chinese" + vbCrLf)
            End If
            allcomdStrs.Add(rootDir)

            allcomdStrs.Add("cd " + Chr(34) + exedir + Chr(34) + vbCrLf)
            'allcomdStrs.Add(Chr(34) + Nxexe + Chr(34))
            'ugraf.exe
            allcomdStrs.Add("ugraf.exe")
            IO.File.WriteAllLines(".\cmdFile.bat", allcomdStrs.ToArray())
            ExecCmd("call .\cmdFile.bat")
            allcomdStrs.Clear()
        Else
            MsgBox("没有发现可执行文件！")
        End If
        ' set INC_DIR_THIRD=%WORK_DIR%\Third_Part_NX10
        'set LIB_DIR_THIRD=%WORK_DIR%\Third_Part_NX10
        'set LIB_DIR=%OUTPUT_DIR% 
        '%VS2012_EXE%
    End Sub


    Private Sub ButtonNX10_Click(sender As Object, e As EventArgs) Handles ButtonNX10.Click
        userdir = ComboBoxUserDir.Text
        StartNX("NX10", nx10dir)

    End Sub

    Private Sub ButtonNX11_Click(sender As Object, e As EventArgs) Handles ButtonNX11.Click
        userdir = ComboBoxUserDir.Text
        StartNX("NX11", nx11dir)
    End Sub

    Private Sub ButtonNX12_Click(sender As Object, e As EventArgs) Handles ButtonNX12.Click
        userdir = ComboBoxUserDir.Text
        StartNX("NX12", nx12dir)
    End Sub

    Private Sub ButtonNX1847_Click(sender As Object, e As EventArgs) Handles ButtonNX1847.Click
        userdir = ComboBoxUserDir.Text
        StartNX("NX1847", nx1847dir)
    End Sub

    '启动VS
    Private Sub StartVS(NXkey As String, nxdir As String, vsdir As String)

        Dim Nxexe As String = nxdir + "\UGII\ugraf.exe"

        If NXkey <> "NX10" Then
            Nxexe = nxdir + "\NXBIN\ugraf.exe"
        End If

        If IO.File.Exists(Nxexe) Then

            IO.File.Delete(".\cmdFile.bat")

            Dim allcomdStrs As New List(Of String)

            allcomdStrs.Add("set UGII_USER_DIR=" + userdir + vbCrLf)
            allcomdStrs.Add("set OUTPUT_DIR=%UGII_USER_DIR%\application" + vbCrLf)

            allcomdStrs.Add("set UGII_BASE_DIR=" + nxdir + vbCrLf)
            allcomdStrs.Add("set UGII_ROOT_DIR=" + nxdir + "\UGII" + vbCrLf)
            allcomdStrs.Add("set UGII_UGRAF_DIR=%UGII_BASE_DIR%\NXBIN" + vbCrLf)
            allcomdStrs.Add("set UGII_DISPLAY_DEBUG=1" + vbCrLf)

            '英文选项打开
            If CheckBoxEn.Checked Then
                'ExecCmd("set UGII_LANG=english")
                allcomdStrs.Add("set UGII_LANG=english" + vbCrLf)
            End If

            allcomdStrs.Add(Chr(34) + vsdir + Chr(34) + vbCrLf)
            IO.File.WriteAllLines(".\cmdFile.bat", allcomdStrs.ToArray())
            ExecCmd("call .\cmdFile.bat")
            allcomdStrs.Clear()
        Else
            MsgBox("没有发现可执行文件！")
        End If

        'set INC_DIR_THIRD=%WORK_DIR%\Third_Part_NX10
        'set LIB_DIR_THIRD=%WORK_DIR%\Third_Part_NX10
        'set LIB_DIR=%OUTPUT_DIR% 
        '%VS2012_EXE%
    End Sub

    Private Sub ButtonVS2012_Click(sender As Object, e As EventArgs) Handles ButtonVS2012.Click
        userdir = ComboBoxUserDir.Text
        StartVS("NX10", nx10dir, vs2012dir)
    End Sub

    Private Sub ButtonVS2013_Click(sender As Object, e As EventArgs) Handles ButtonVS2013.Click
        userdir = ComboBoxUserDir.Text
        StartVS("NX11", nx11dir, vs2013dir)
    End Sub

    Private Sub ButtonVS2015_Click(sender As Object, e As EventArgs) Handles ButtonVS2015.Click
        userdir = ComboBoxUserDir.Text
        StartVS("NX12", nx12dir, vs2015dir)
    End Sub

    Private Sub ButtonVS2017_Click(sender As Object, e As EventArgs) Handles ButtonVS2017.Click
        userdir = ComboBoxUserDir.Text
        StartVS("NX10", nx1847dir, vs2017dir)
    End Sub

    Private Sub ButtonNX10_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNX10.MouseDown
        ButtonNX10.BackColor = SystemColors.Control

    End Sub

    Private Sub ButtonNX10_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonNX10.MouseUp
        ButtonNX10.BackColor = Color.DarkSeaGreen

    End Sub

    Private Sub ButtonNX11_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNX11.MouseDown
        ButtonNX11.BackColor = SystemColors.Control

    End Sub

    Private Sub ButtonNX11_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonNX11.MouseUp
        ButtonNX11.BackColor = Color.DarkSeaGreen

    End Sub

    Private Sub ButtonNX12_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNX12.MouseDown
        ButtonNX12.BackColor = SystemColors.Control
    End Sub

    Private Sub ButtonNX12_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonNX12.MouseUp
        ButtonNX12.BackColor = Color.DarkSeaGreen
    End Sub

    Private Sub ButtonNX1847_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonNX1847.MouseDown

    End Sub

    Private Sub ButtonNX1847_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonNX1847.MouseUp

    End Sub

    Private Sub ButtonVS2012_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonVS2012.MouseDown
        ButtonVS2012.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub ButtonVS2012_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonVS2012.MouseUp
        ButtonVS2012.BackColor = SystemColors.Control
    End Sub

    Private Sub ButtonVS2013_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonVS2013.MouseDown
        ButtonVS2013.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub ButtonVS2013_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonVS2013.MouseUp
        ButtonVS2013.BackColor = SystemColors.Control
    End Sub

    Private Sub ButtonVS2015_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonVS2015.MouseDown
        ButtonVS2015.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub ButtonVS2015_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonVS2015.MouseUp
        ButtonVS2015.BackColor = SystemColors.Control
    End Sub

    Private Sub ButtonVS2017_MouseDown(sender As Object, e As MouseEventArgs) Handles ButtonVS2017.MouseDown
        ButtonVS2017.BackColor = SystemColors.ControlDark
    End Sub

    Private Sub ButtonVS2017_MouseUp(sender As Object, e As MouseEventArgs) Handles ButtonVS2017.MouseUp
        ButtonVS2017.BackColor = SystemColors.Control

    End Sub
End Class
