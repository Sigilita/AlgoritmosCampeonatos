Option Explicit On
Option Strict On

Public Class partidoAuxiliar
    Implements IComparable(Of partidoAuxiliar)

    Private _Equipo1 As String
    Private _Equipo2 As String
    Private _NumeroEquipos As Integer
    Public Sub New(ByVal _Equipo1 As String, ByVal _Equipo2 As String)
        Me._Equipo1 = _Equipo1
        Me._Equipo2 = _Equipo2
    End Sub
    Public Sub New()

    End Sub
    Public Property Equipo1 As String
        Get
            Return _Equipo1
        End Get
        Set(value As String)
            _Equipo1 = value
        End Set
    End Property

    Public Property Equipo2 As String
        Get
            Return _Equipo2
        End Get
        Set(value As String)
            _Equipo2 = value
        End Set
    End Property

    Public Function Comparar(ByVal p1 As partidoAuxiliar, ByVal p2 As partidoAuxiliar) As Boolean
        If (p1.Equipo1.Equals(p2.Equipo1)) Then
            Return False
        ElseIf (p1.Equipo1.Equals(p2.Equipo2)) Then
            Return False
        ElseIf (p1.Equipo2.Equals(p2.Equipo1)) Then
            Return False
        ElseIf (p1.Equipo2.Equals(p2.Equipo2)) Then
            Return False
        Else : Return True
        End If
    End Function

    Public Function CompareTo(obj As partidoAuxiliar) As Integer Implements IComparable(Of partidoAuxiliar).CompareTo
        Return Me._Equipo1.CompareTo(obj._Equipo1)
    End Function
End Class
