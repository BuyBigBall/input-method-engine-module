Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Win32


Public Class frmMain

    'TODO
    ' How to write =?

    '#DONE
    ' Create icon
    ' Check setting file on startup
    ' Add to startup script
    ' Optimze tray code
    ' Caps lock bug

    ' Global variables
    Dim swapmode As Boolean
    Dim uppercase As Boolean
    Dim currentkey As Char = vbNullChar
    Dim swapdict As New Dictionary(Of String, String())
    Dim swapindex As Integer
    Dim allow As Boolean


    Private WithEvents kbHook As New KeyboardHook

    Private LatestKey As Keys = Keys.Oemplus
    Private CfgEditCtrl As TextBox = Nothing

    Private Function GetKeyName(ByVal Key As Keys) As String
        Select Case Key
            Case Keys.D0
                Return "0"
            Case Keys.D1
                Return "1"
            Case Keys.D2
                Return "2"
            Case Keys.D3
                Return "3"
            Case Keys.D4
                Return "4"
            Case Keys.D5
                Return "5"
            Case Keys.D6
                Return "6"
            Case Keys.D7
                Return "7"
            Case Keys.D8
                Return "8"
            Case Keys.D9
                Return "9"
            Case Keys.Oemcomma
                Return ","
            Case Keys.OemPeriod
                Return "."
            Case Keys.OemPipe
                Return "|"
            Case Keys.Oemplus
                Return "="
            Case Keys.Oemtilde
                Return "~"
            Case Keys.OemMinus
                Return "-"
            Case Keys.OemOpenBrackets
                Return "["
            Case Keys.OemCloseBrackets
                Return "]"
            Case Keys.OemQuestion
                Return "?"
            Case Keys.OemQuotes
                Return """"
            Case Keys.OemSemicolon
                Return ";"
            Case Keys.OemClear
                Return "Clear"
            Case Else
                Return Key.ToString()
        End Select
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        swapmode = False
        load_config()

        ' Minimize to tray
        tray.Visible = True
        'tray.Icon = SystemIcons.Application
        tray.BalloonTipIcon = ToolTipIcon.Info
        tray.BalloonTipTitle = "Keyswap"
        tray.BalloonTipText = "Press " & GetKeyName(KeyboardHook.ChangeKey) & " to change character"
        tray.ShowBalloonTip(1000)
        'Me.Hide()
        ShowInTaskbar = False
        kbHook.keyenable = True

    End Sub

    Dim strLastSendKey As String = ""
    Private Sub kbHook_KeyDown(ByVal Key As Keys) Handles kbHook.KeyDown
        Debug.WriteLine("Pressed key = " & Key.ToString)

        If CfgEditCtrl IsNot Nothing Then
            CfgEditCtrl.Text = GetKeyName(Key)
            LatestKey = Key
        End If

        ' Check if shift key is pressed
        If Control.IsKeyLocked(Keys.CapsLock) Then
            If (Key = Keys.LShiftKey Or Key = Keys.RShiftKey) Then
                'uppercase = False
            Else
                uppercase = True
            End If
        Else
            If (Key = Keys.LShiftKey Or Key = Keys.RShiftKey) Then
                uppercase = True
            Else
                'uppercase = False
            End If
        End If

        ' Check if = key is pressed
        If Key = KeyboardHook.ChangeKey Then
            'Keys.Oemplus Then
            Dim swapchar As String() = Nothing
            Dim tmpchar As Char = currentkey


            ' Check if prevkey has swap candidates
            If (swapdict.TryGetValue(currentkey, swapchar)) Then
                swapmode = True
                lblDebug.ForeColor = Color.Red

                swapindex = (swapindex + 1) Mod swapchar.Length ' Circular-rotate swap character
                SendKeys.Send("{BS}") ' Erase previous character

                Dim sendKeyChar As String
                If Control.IsKeyLocked(Keys.CapsLock) And swapindex = 0 Then
                    sendKeyChar = swapchar(swapindex).ToLower ' Convert to lowercase to avoid capslock bug         
                Else
                    sendKeyChar = swapchar(swapindex)
                End If
                Dim bt As Byte()
                Dim btLast As Byte()
                bt = System.Text.Encoding.UTF8.GetBytes(sendKeyChar)
                btLast = System.Text.Encoding.UTF8.GetBytes(strLastSendKey)
                If swapindex <> 1 And (bt(0) <= 127 Or bt.Length >= 4) Then '' swapindex=0 or swapindex>1
                    'If Not (bt.Length = 1 And bt(0) <= 127) Then
                    Debug.WriteLine("{BS}= " & swapindex & " " & bt(0) & " " & bt.Length & " " & btLast.Length)
                    If swapindex = 0 Then
                        If btLast.Length > 0 And btLast(0) <= 127 Then
                            SendKeys.Send("{BS}")
                            Debug.Print(" bs1 : " & btLast(0))
                        ElseIf btLast.Length = 5 Then   ' ḍ̱
                            If (btLast(0) = &HE1 And btLast(1) = &HB8 And btLast(2) = &H8D And btLast(3) = &HCC And btLast(4) = &HB1) Then  ''d > ḏ > ḍ > ḍ̱
                                Debug.Print(" bs4 : " & btLast(0))
                                SendKeys.Send("{BS}")
                            End If
                            If (btLast(0) = &HE1 And btLast(1) = &HB9 And btLast(2) = &HA3 And btLast(3) = &HCC And btLast(4) = &H84) Then  ''f > ṣ > ṣ̱ > ṣ̄ 
                                Debug.Print(" bs9 : " & btLast(0))
                                SendKeys.Send("{BS}")
                            End If
                            If (btLast(0) = &HE1 And btLast(1) = &HB9 And btLast(2) = &HAD And btLast(3) = &HCC And btLast(4) = &H81) Then  ''t > ṭ > t́ > ṭ́
                                Debug.Print(" bs9 : " & btLast(0))
                                SendKeys.Send("{BS}")
                            End If
                        ElseIf btLast.Length = 4 Then
                            If (btLast(0) = &HC5 And btLast(1) = &H9B And btLast(2) = &HCC And btLast(3) = &H84) Then '' x > ś̄
                                SendKeys.Send("{BS}")
                                Debug.Print(" bs7 : " & btLast(0))
                            End If
                        End If
                    Else

                        If bt.Length = 5 Then
                            '' As this is a special case, the Char code must be registered
                            If Not (bt(0) = &HE1 And bt(1) = &HB8 And bt(2) = &H8D And bt(3) = &HCC And bt(4) = &HB1) Then  ''d > ḍ̱
                                If Not (bt(0) = &HE1 And bt(1) = &HB9 And bt(2) = &HA3 And bt(3) = &HCC And bt(4) = &HB1) Then  ''f > ṣ > ṣ̱ 
                                    Debug.Print(" bs2 : " & btLast(0))
                                    SendKeys.Send("{BS}")
                                End If
                            End If
                        ElseIf bt.Length = 3 Then
                            If Not (bt(0) = &H6B And bt(1) = &HCC And bt(2) = &H84) Then  ''k > ḵ > k̄ > ḱ
                                If Not (bt(0) = &H72 And bt(1) = &HCC And bt(2) = &HA5) Then  ''r > ṛ > ṝ > r̥
                                    If Not (bt(0) = &H74 And bt(1) = &HCC And bt(2) = &H81) Then  ''t > ṭ > t́ 
                                        SendKeys.Send("{BS}")
                                    End If
                                End If
                            End If
                        ElseIf bt.Length = 4 Then
                            If Not (bt(0) = &HC5 And bt(1) = &H9B And bt(2) = &HCC And bt(3) = &HB1) Then  ''x>ś̱ 
                                SendKeys.Send("{BS}")
                            End If
                        Else
                            Debug.Print(" bs3 : " & btLast(0))
                            SendKeys.Send("{BS}")
                        End If
                    End If
                ElseIf swapindex <> 1 Then
                    If btLast.Length = 3 Then
                        Debug.Print(" bs6 : " & btLast(0))
                        If (btLast(0) = &H6B And btLast(1) = &HCC And btLast(2) = &H84) Then  ''k̄
                            SendKeys.Send("{BS}")
                        End If
                    End If
                End If


                SendKeys.Send(sendKeyChar) ' Swap character
                Debug.WriteLine("sendKeyChar = " & sendKeyChar)
                strLastSendKey = sendKeyChar
                swapmode = False
                'Else
                'lblDebug.ForeColor = Color.Black
                kbHook.keyenable = False
            Else
                kbHook.keyenable = True

            End If

            Exit Sub
        End If

        ' Update previous key
        If Not swapmode And Key >= Keys.A And Key <= Keys.Z Then
            swapmode = False
            swapindex = 0
            If (uppercase) Then
                currentkey = Key.ToString.ToUpper
            Else
                currentkey = Key.ToString.ToLower
            End If

        Else
            If Not swapmode Then
                currentkey = vbNullChar
            End If
        End If

        'lblDebug.Text = currentkey
    End Sub

    Private Sub kbHook_KeyUp(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyUp
        ' Shift is released
        If Key = Keys.CapsLock Then
            If uppercase = True Then
                uppercase = False
            Else
                uppercase = True
            End If
        End If
        If (Key = Keys.LShiftKey Or Key = Keys.RShiftKey) Then
            uppercase = False
        End If

    End Sub


    Private Sub load_config()
        Dim configpath As String
        Dim configpath2 As String

        'configpath = "C:\Users\admin\Desktop\config.txt"
        configpath = Application.StartupPath() + "\config.txt"
        configpath2 = Application.StartupPath() + "\key.bin"

        Try
            ' Read config file
            Using r As StreamReader = New StreamReader(configpath, System.Text.Encoding.Default)
                Dim line As String
                line = r.ReadLine

                ' Loop each line
                Do While (Not line Is Nothing)
                    ' Parse config line
                    Dim parts As String() = line.Replace(" ", "").Split(New Char(), ">")

                    ' Add to dictionary
                    swapdict.Add(parts(0), parts)

                    ' Proceed to next line
                    line = r.ReadLine
                Loop
            End Using

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            End
        End Try

        Try
            Using s As New FileStream(configpath2, FileMode.Open)
                Using r As New BinaryReader(s)
                    KeyboardHook.ChangeKey = r.ReadInt32()
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("swapmode: " + swapmode.ToString)
        MsgBox("swapindex: " + swapindex.ToString)

    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then

        End If
    End Sub



    Private Sub tray_MouseClick(sender As Object, e As MouseEventArgs) Handles tray.MouseClick
        ' Display menu
        If e.Button = MouseButtons.Right Then
            Me.Show()
            Me.Activate()
            Me.Width = 1
            Me.Height = 1

            tray_menu.Show(Cursor.Position)
            Me.Left = tray_menu.Left + 1
            Me.Top = tray_menu.Top + 1

        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ts_exit.Click
        ' Close program
        tray.Visible = False
        End
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ts_about.Click
        MsgBox("Created by Andre V." + vbNewLine + "Contact at speedsuccess2@gmail.com", vbInformation)
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Add program to startup
        Try
            Dim tmpKey As String = "Software\Microsoft\Windows\CurrentVersion\Run"
            Dim startupkey As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(tmpKey, True)

            startupkey.DeleteValue("swapkey")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ts_startup_Click(sender As Object, e As EventArgs) Handles ts_startup.Click
        Try
            Registry.SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run", "swapkey", Application.ExecutablePath, RegistryValueKind.String)
            MsgBox("Added to startup", vbInformation)
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Private Sub ts_configure_Click(sender As Object, e As EventArgs) Handles ts_configure.Click
        Using form As New frmConfigure
            Dim configpath2 As String
            configpath2 = Application.StartupPath() + "\key.bin"

            form.txtTrigger.Text = GetKeyName(KeyboardHook.ChangeKey)
            CfgEditCtrl = form.txtTrigger
            form.ShowDialog()
            KeyboardHook.ChangeKey = LatestKey
            tray.BalloonTipText = "Press " & GetKeyName(KeyboardHook.ChangeKey) & " to change character"
            tray.ShowBalloonTip(1000)
            CfgEditCtrl = Nothing

            Using s As New FileStream(configpath2, FileMode.Create)
                Using w As New BinaryWriter(s)
                    w.Write(KeyboardHook.ChangeKey)
                End Using
            End Using
        End Using
    End Sub
End Class
