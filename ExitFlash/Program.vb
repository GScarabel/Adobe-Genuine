Imports System
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Module Program

    <DllImport("kernel32.dll")> Private Function GetConsoleWindow() As IntPtr
    End Function
    <DllImport("user32.dll")> Private Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOW As Integer = 5

    Sub Main(args As String())
        Dim path = Reflection.Assembly.GetEntryAssembly().Location
        Console.WriteLine(path)
        path = path.Replace(".dll", ".exe")
        Console.WriteLine(path)

        Dim MyKey As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        MyKey.SetValue("ExitFlash", path)
        MyKey.Close()

        Dim handle = GetConsoleWindow()
        ShowWindow(handle, SW_HIDE)

        aTimer.AutoReset = True
        aTimer.Interval = 100
        AddHandler aTimer.Elapsed, AddressOf tick
        aTimer.Start()
        Console.ReadKey()
    End Sub

    Dim aTimer As New System.Timers.Timer

    Private Sub tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        Dim proc = Process.GetProcessesByName("AdobeGCClient")
        For i As Integer = 0 To proc.Count - 1
            proc(i).CloseMainWindow()
        Next i
    End Sub


End Module
