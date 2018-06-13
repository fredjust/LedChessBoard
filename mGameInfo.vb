Module mGameInfo


    Public Structure InfoPGN
        Dim Event_str As String
        Dim Site As String
        Dim Round As String
        Dim Date_str As String
        Dim White As String
        Dim Black As String
        Dim WhiteElo As String
        Dim BlackElo As String
        Dim Result As String
        Dim TimeControl As String
        Dim LocalPIECE As String
    End Structure

    Public InfoGame As New InfoPGN

    Public echiquier As Rectangle
    Public IniFile As String = ""

    Public LastReceive As Integer

End Module
