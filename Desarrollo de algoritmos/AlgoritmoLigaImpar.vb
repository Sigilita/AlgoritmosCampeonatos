Option Explicit On
Option Strict On
Public Class AlgoritmoLigaImpar

    Private numeroEquipos As Integer
    Private numeroRodas As Integer
    Private partidos As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
    Private ListaDeEquipos As List(Of String) = New List(Of String)
    Private numeroPartidosRonda As Integer
    Private Jornada As List(Of List(Of partidoAuxiliar)) = New List(Of List(Of partidoAuxiliar))
    Private partidosJornada As List(Of partidoAuxiliar)
    Dim pasar As Boolean = False
    Dim iteracionesMaximas As Boolean = False
    Dim numeroDeVueltas As Integer

    Sub New(p1 As List(Of String))
        ListaDeEquipos = p1
        'Numero de equipos en verdad seria el count de la lista de equipos, que es lo que recibe
        numeroEquipos = ListaDeEquipos.Count
        numeroPartidosRonda = CInt((numeroEquipos) / 2)

    End Sub

    Public Sub Ordenacion()
        Dim paux As partidoAuxiliar
        For i = 0 To numeroEquipos - 1
            For j = i + 1 To numeroEquipos - 1
                paux = New partidoAuxiliar
                paux.Equipo1 = ListaDeEquipos.Item(i)
                paux.Equipo2 = ListaDeEquipos.Item(j)
                partidos.Add(paux)
            Next
        Next
        numeroRodas = CInt(Factorial(numeroEquipos) / (Factorial(2) * Factorial(numeroEquipos - 2)))
        numeroRodas = CInt(numeroRodas / numeroPartidosRonda)

    End Sub

    Public Function Factorial(ByVal num As Integer) As Integer
        If (num.Equals(1)) Then
            Return num
        Else
            Return num * Factorial(num - 1)
        End If
    End Function

    Public Sub Liga()
        Do
            CrearLiga()
        Loop While iteracionesMaximas = False
        vueltas()
    End Sub

    Public Function CrearLiga() As Boolean
        Try
            Jornada.Clear()
            Dim contador = 0
            Dim xpartidos As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
            xpartidos = partidos.ToList
            Dim paux As partidoAuxiliar = New partidoAuxiliar
            For i = 0 To numeroRodas
                For j = 0 To numeroPartidosRonda - 1
                    Dim Ran As New Random()
                    Dim index As Integer
                    index = Ran.Next(0, xpartidos.Count)
                    If (j.Equals(0)) Then
                        partidosJornada = New List(Of partidoAuxiliar)
                        partidosJornada.Add(xpartidos.Item(index))
                        xpartidos.RemoveAt(index)
                    Else
                        Do
                            index = Ran.Next(0, xpartidos.Count)
                            For k = 0 To partidosJornada.Count - 1
                                paux = partidosJornada.Item(k)
                                If (paux.Comparar(paux, xpartidos.Item(index))) Then
                                    pasar = True
                                Else
                                    index = Ran.Next(0, xpartidos.Count)
                                    pasar = False
                                    contador = contador + 1
                                    If contador > numeroEquipos * 20 Then
                                        Throw New ArgumentException
                                    End If
                                End If
                            Next
                        Loop Until pasar = True
                        partidosJornada.Add(xpartidos.Item(index))
                        xpartidos.RemoveAt(index)
                    End If
                Next
                Jornada.Add(partidosJornada)
            Next
            iteracionesMaximas = True
        Catch ex As ArgumentException
            iteracionesMaximas = False
        End Try
        iteracionesMaximas = True
    End Function

    Public Sub vueltas()
        Dim listaux As List(Of List(Of partidoAuxiliar))
        Dim listaux2 As List(Of partidoAuxiliar)
        Dim listaux3 As List(Of partidoAuxiliar)
        Dim Equipo1 As String
        Dim Equipo2 As String
        Dim paux As partidoAuxiliar
        numeroDeVueltas = 3
        Dim contador As Integer = 0

        listaux = Jornada.ToList
        If (numeroDeVueltas > 1) Then
            Do
                If ((contador Mod 2).Equals(0)) Then
                    For i = 0 To listaux.Count - 1
                        listaux2 = New List(Of partidoAuxiliar)
                        listaux3 = New List(Of partidoAuxiliar)
                        listaux2 = listaux.Item(i)
                        For j = 0 To listaux2.Count - 1
                            Equipo2 = listaux2.Item(j).Equipo1
                            Equipo1 = listaux2.Item(j).Equipo2
                            paux = New partidoAuxiliar(Equipo1, Equipo2)
                            listaux3.Add(paux)
                        Next
                        Jornada.Add(listaux3)
                    Next
                Else
                    For k = 0 To listaux.Count - 1
                        listaux2 = New List(Of partidoAuxiliar)
                        listaux3 = New List(Of partidoAuxiliar)
                        listaux2 = listaux.Item(k)
                        For l = 0 To listaux2.Count - 1
                            listaux3.Add(listaux2.Item(l))
                        Next
                        Jornada.Add(listaux3)
                    Next
                End If
                contador = contador + 1
            Loop While (contador < numeroDeVueltas - 1)
        End If
    End Sub

End Class
