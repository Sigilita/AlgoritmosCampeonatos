Option Explicit On
Option Strict On
Public Class AlgoritmoCampeonato
    Private equiposParticipantes As List(Of String) = New List(Of String)
    Private numeroRondas As Integer
    Private contador As Integer = 0
    Private equiposPorRonda As List(Of String) = New List(Of String)
    Private partidosPorRonda As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)

    Public Sub New(ByVal equiposParticipantes As List(Of String))
        Me.equiposParticipantes = equiposParticipantes
    End Sub
    Public Sub New()

    End Sub

    Public Function calculoNumeroRondas(ByVal numeroEquipos As Integer) As Integer
        If (numeroEquipos.Equals(1)) Then
            Return (contador)
        Else
            contador = contador + 1
            Return calculoNumeroRondas(CInt(numeroEquipos / 2))

        End If
    End Function

    Public Sub primeraAsignacion()
        Dim paux As partidoAuxiliar
        Do
            Dim Ran As New Random()
            Dim index As Integer
            index = Ran.Next(0, equiposParticipantes.Count)
            equiposPorRonda.Add(equiposParticipantes.Item(index))
            equiposParticipantes.RemoveAt(index)
        Loop While (equiposParticipantes.Count > 0)
        For i = 0 To (equiposPorRonda.Count - 1) Step 2
            paux = New partidoAuxiliar()
            paux.Equipo1 = (equiposPorRonda.Item(CInt(i)))
            paux.Equipo2 = equiposPorRonda.Item(CInt(i + 1))
            partidosPorRonda.Add(paux)
        Next
    End Sub

End Class
