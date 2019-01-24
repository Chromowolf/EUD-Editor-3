﻿Module GlobalObj
    Public pgData As ProgramData
    Public pjData As ProjectData
    Public scData As StarCraftData



    Public SettiingForm As SettingWindows



    Public Function InitProgram() As Boolean
        Tool.Init()


        Try
            pgData = New ProgramData
        Catch ex As Exception
            Tool.ErrorMsgBox(Tool.GetText("Error ProgramInit Fail"), ex.ToString)
            Application.Current.Shutdown()
            Return False
        End Try
        scData = New StarCraftData
        Try

        Catch ex As Exception
            Tool.ErrorMsgBox(Tool.GetText("Error LoadStarCraftData Fail"), ex.ToString)
            Application.Current.Shutdown()
            Return False
        End Try


        '언어 설정
        If pgData.Setting(ProgramData.TSetting.Language) = Nothing Then
            pgData.Setting(ProgramData.TSetting.Language) = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString()
        End If
        pgData.SetLanguage(pgData.Setting(ProgramData.TSetting.Language))
        'MsgBox(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString())



        '색 설정
        If pgData.Setting(ProgramData.TSetting.Theme) = "Light" Then
            pgData.SetTheme(True)
        ElseIf pgData.Setting(ProgramData.TSetting.Theme) = "Dark" Then
            pgData.SetTheme(False)
        Else
            pgData.Setting(ProgramData.TSetting.Theme) = "Light"
            pgData.SetTheme(True)
        End If



        Dim colorStr As String = pgData.Setting(ProgramData.TSetting.DefaultData)
        If colorStr Is Nothing Then
            pgData.PFiledDefault = ColorConverter.ConvertFromString("#60DDCDCD")
        Else
            pgData.PFiledDefault = ColorConverter.ConvertFromString(colorStr)
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.MapEditorData)
        If colorStr Is Nothing Then
            pgData.PFiledMapEditColor = ColorConverter.ConvertFromString("#60A1C7FF")
        Else
            pgData.PFiledMapEditColor = ColorConverter.ConvertFromString(colorStr)
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.EditedData)
        If colorStr Is Nothing Then
            pgData.PFiledEditColor = ColorConverter.ConvertFromString("#60FFA3FA")
        Else
            pgData.PFiledEditColor = ColorConverter.ConvertFromString(colorStr)
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.CheckedData)
        If colorStr Is Nothing Then
            pgData.PFiledFalgColor = ColorConverter.ConvertFromString("#80FF8F6A")
        Else
            pgData.PFiledFalgColor = ColorConverter.ConvertFromString(colorStr)
        End If


        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueLightBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueLightBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueMidBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueMidBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueDarkBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueDarkBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueLightForegroundBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueLightForegroundBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueMidForegroundBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueMidForegroundBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.PrimaryHueDarkForegroundBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("PrimaryHueDarkForegroundBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.SecondaryAccentBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("SecondaryAccentBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If

        colorStr = pgData.Setting(ProgramData.TSetting.SecondaryAccentForegroundBrush)
        If colorStr IsNot Nothing Then
            Application.Current.Resources("SecondaryAccentForegroundBrush") = New SolidColorBrush(ColorConverter.ConvertFromString(colorStr))
        End If



        'Dim helper As New MaterialDesignThemes.Wpf.PaletteHelper
        'helper.ReplacePrimaryColor(DefaultPalettName(index))

        'Try
        '    helper.ReplaceAccentColor(DefaultPalettName(index))
        'Catch ex As Exception
        '    helper.ReplaceAccentColor("lime")
        'End Try


        '세팅파일
        If pgData.Setting(ProgramData.TSetting.euddraft) = Nothing Then
            pgData.SaveSetting()
        End If


        Try
            Tool.SetRegistry()
        Catch ex As Exception

        End Try


        If Environment.GetCommandLineArgs.Count > 1 Then
            Dim filename As String = Environment.GetCommandLineArgs(1)
            ProjectData.Load(filename, pjData)
            'MsgBox(filename & " 다른파일로 열림")
        End If
        Return True
    End Function

    Public Function ShutDownProgram() As Boolean
        Try
            pgData.SaveSetting()
        Catch ex As Exception
            Tool.ErrorMsgBox(Tool.GetText("Error SettingSave Fail"))
        End Try
        Return False '종료 허락
    End Function
End Module
