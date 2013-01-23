Option Explicit On
Option Strict On
Public Class AlgoritmoLiga

    Private numeroEquipos As Integer
    Private numeroRodas As Integer
    Private partidos As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
    Private ListaDeEquipos As List(Of String) = New List(Of String)
    Private Jornada As List(Of List(Of partidoAuxiliar)) = New List(Of List(Of partidoAuxiliar))
    Private partidosJornada As List(Of partidoAuxiliar)
    Dim pasar As Boolean = False
    Dim iteracionesMaximas As Boolean = False
    Dim numeroPartidoRonda As Integer
    Dim numeroDeVueltas As Integer

    ''' <summary>
    ''' Constructor del algoritmo de liga. Recibe una lista de todos los equipos
    ''' participantes.
    ''' En el constructor se determina también, el número de partidos por ronda y el número de equipos
    ''' que compondrán la liga.
    ''' </summary>
    ''' <param name="p1">Lista de equipos que componen la liga</param>
    ''' <remarks></remarks>
    Sub New(p1 As List(Of String))
        ListaDeEquipos = p1
        'Numero de equipos en verdad seria el count de la lista de equipos, que es lo que recibe
        numeroEquipos = ListaDeEquipos.Count
        If ((numeroEquipos Mod 2).Equals(0)) Then
            numeroPartidoRonda = CInt((numeroEquipos) / 2)
        Else
            numeroPartidoRonda = CInt((numeroEquipos - 1) / 2)
        End If

    End Sub

    'Public Sub Ordenacion()
    '    Dim paux As partidoAuxiliar
    '    For i = 0 To numeroEquipos - 1
    '        For j = i + 1 To numeroEquipos - 1
    '            paux = New partidoAuxiliar
    '            paux.Equipo1 = ListaDeEquipos.Item(i)
    '            paux.Equipo2 = ListaDeEquipos.Item(j)
    '            partidos.Add(paux)
    '        Next
    '    Next
    'End Sub

    ''' <summary>
    ''' El método de ordenación lo que hace es generar una lista con todas las permutaciones de partidos posibles, y los guarda en la lista partidos.
    ''' Además, utiliza la fórmula n!/(m!*(m-n)!) para calcular el número de agrupaciones que se pueden realizar por rondas. M = 2 para agrupaciones de 
    ''' partidos de 2 equipos.
    ''' </summary>
    ''' <remarks></remarks>
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
        numeroRodas = CInt(numeroRodas / numeroPartidoRonda)

    End Sub
    ''' <summary>
    ''' Método para el cálculo del número factorial
    ''' </summary>
    ''' <param name="num">Número a calcular el factorial</param>
    ''' <returns>El resultado</returns>
    ''' <remarks></remarks>
    Public Function Factorial(ByVal num As Integer) As Integer
        If (num.Equals(1)) Then
            Return num
        Else
            Return num * Factorial(num - 1)
        End If
    End Function

    ''' <summary>
    ''' Método encargado de que el algoritmo se ejecute hasta que se alcance una solución adecuada. Además, llama a la función vueltas
    ''' la cual añade los partidos según las vueltas que se pidan.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Liga()
        Do
            CrearLiga()
        Loop While iteracionesMaximas = False
        vueltas()
    End Sub
    ''' <summary>
    ''' Algoritmo de calculo. Su funcionamiento se puede resumir en los siguientes pasos:
    ''' 1º Preparación de variables. Se limpia la lista jornada por si se ha llamado de una iteración anterior.
    ''' 2º Segun el número de rondas que hay , y según el número de partidos que se juegan por ronda, se van introduciendo
    ''' los partidos, y se eliminan de la lista auxiliar xpartidos. Dado que la llamada para buscar partidos se realiza a la propia lista
    ''' xpartidos, cada vez hay menos partidos que elegir. En caso de que el algoritmo se meta en un punto sin salida, hay un máximo de iteraciones
    ''' que hacen que el algoritmo se ejecute de nuevo con otra entrada.
    ''' 3º Una vez se acaban los partidos en xpartidos, el algoritmo se ha ejecutado con exito y cambia la variable iteracionesMaximas, permitiendo
    ''' el fin del loop en el método liga
    ''' </summary>
    ''' <returns>Boolean True/False</returns>
    ''' <remarks></remarks>
    Public Function CrearLiga() As Boolean
        Try
            Jornada.Clear()
            Dim contador = 0
            Dim xpartidos As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
            xpartidos = partidos.ToList
            Dim paux As partidoAuxiliar = New partidoAuxiliar
            For i = 0 To numeroRodas - 1
                For j = 0 To numeroPartidoRonda - 1
                    Dim Ran As New Random()
                    Dim index As Integer
                    index = Ran.Next(0, xpartidos.Count)
                    If (j.Equals(0)) Then
                        partidosJornada = New List(Of partidoAuxiliar)
                        partidosJornada.Add(xpartidos.Item(index))
                        xpartidos.RemoveAt(index)
                    Else
                        Do
                            Try
                                index = Ran.Next(0, xpartidos.Count)
                                For k = 0 To partidosJornada.Count - 1
                                    paux = partidosJornada.Item(k)
                                    If (paux.Comparar(paux, xpartidos.Item(index))) Then
                                    Else
                                        Throw New ArgumentException
                                    End If
                                Next
                                pasar = True
                            Catch ex As ArgumentException
                                contador = contador + 1
                                If contador > numeroEquipos * 20 Then
                                    Throw New ArgumentException
                                End If
                                pasar = False
                            End Try

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
    End Function

    ''' <summary>
    ''' Dependiendo de cuantas vueltas han de realizarse, este método añade los partidos de "ida/vuelta", es decir, si el primer partido es A Vs B, el 
    ''' primer partido de vuelta sera B vs A, y asi sucesivamente, dependiendo de las vueltas que se pidan
    ''' </summary>
    ''' <remarks></remarks>
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

    'Public Function CrearLiga() As Boolean
    '    numeroRodas = numeroEquipos - 1
    '    partidosPorRonda = CInt(numeroEquipos / 2)
    '    Dim partidosLiga(numeroRodas - 1, partidosPorRonda - 1) As partidoAuxiliar
    '    Try
    '        Dim contador As Integer = 0
    '        Dim xpartidos As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
    '        xpartidos = partidos.ToList
    '        Dim paux As partidoAuxiliar = New partidoAuxiliar
    '        Dim pasar As Boolean = False

    '        For i = 0 To numeroRodas - 1
    '            For j = 0 To partidosPorRonda - 1
    '                Dim Ran As New Random()
    '                Dim index As Integer
    '                index = Ran.Next(0, xpartidos.Count)
    '                If (j.Equals(0)) Then
    '                    partidosLiga(i, j) = xpartidos(index)
    '                    xpartidos.RemoveAt(index)
    '                Else
    '                    Do
    '                        If (j.Equals(1)) Then
    '                            If (paux.Comparar(partidosLiga(i, j - 1), xpartidos(index))) Then
    '                                partidosLiga(i, j) = xpartidos(index)
    '                                xpartidos.RemoveAt(index)
    '                                pasar = True
    '                            Else
    '                                index = Ran.Next(0, xpartidos.Count)
    '                                pasar = False
    '                                contador = contador + 1
    '                                If contador > 500 Then
    '                                    Throw New ArgumentException
    '                                End If
    '                            End If
    '                        End If
    '                        If (j.Equals(2)) Then
    '                            If (paux.Comparar(partidosLiga(i, j - 1), xpartidos(index))) Then
    '                                If (paux.Comparar(partidosLiga(i, j - 2), xpartidos(index))) Then
    '                                    partidosLiga(i, j) = xpartidos(index)
    '                                    xpartidos.RemoveAt(index)
    '                                    pasar = True
    '                                Else
    '                                    index = Ran.Next(0, xpartidos.Count)
    '                                    pasar = False
    '                                    contador = contador + 1
    '                                    If contador > 500 Then
    '                                        Throw New ArgumentException
    '                                    End If
    '                                End If
    '                            Else
    '                                index = Ran.Next(0, xpartidos.Count)
    '                                pasar = False
    '                                contador = contador + 1
    '                                If contador > 500 Then
    '                                    Throw New ArgumentException
    '                                End If
    '                            End If
    '                        End If
    '                    Loop Until pasar = True
    '                End If
    '            Next
    '        Next
    '        'CopiarArrayMultidimensional(partidosLiga)
    '        CopiarALista(partidosLiga)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Public Sub CopiarALista(ByVal liga(,) As partidoAuxiliar)
    '    Dim Equipo1 As String
    '    Dim Equipo2 As String
    '    Dim partidox As partidoAuxiliar
    '    For k = 0 To numeroDevueltas - 1
    '        For i = 0 To liga.GetUpperBound(0)
    '            For j = 0 To liga.GetUpperBound(1)
    '                If ((k Mod 2).Equals(0)) Then
    '                    partidosJornada.Add(liga(i, j))
    '                Else
    '                    Equipo1 = liga(i, j).Equipo2
    '                    Equipo2 = liga(i, j).Equipo1
    '                    partidox = New partidoAuxiliar(Equipo1, Equipo2)
    '                    partidosJornada.Add(partidox)
    '                End If

    '            Next
    '            _Jornadas.Add(partidosJornada)
    '            partidosJornada = New List(Of partidoAuxiliar)

    '        Next
    '    Next
    'End Sub

    'Public Sub NumeroRondas()

    'End Sub
    'Public ReadOnly Property Jornadas As List(Of List(Of partidoAuxiliar))
    '    Get
    '        Return _Jornadas
    '    End Get
    'End Property

    'Public Sub CopiarArrayMultidimensional(ByVal liga(,) As partidoAuxiliar)
    '    ReDim _partidoLigaFinal(liga.GetUpperBound(0), liga.GetUpperBound(1))
    '    Array.Copy(liga, _partidoLigaFinal, liga.Length)
    'End Sub









    ''Public Sub CrearLiga()
    ''    Dim cont As Integer = 0
    ''    Dim contb As Integer = 0
    ''    Dim partidosaux As List(Of partidoAuxiliar) = partidos
    ''    Dim partidosLiga As List(Of List(Of partidoAuxiliar)) = New List(Of List(Of partidoAuxiliar))
    ''    Dim partidosRonda As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)
    ''    For Each ronda As List(Of partidoAuxiliar) In partidosLiga
    ''        For Each partidoEnRonda As partidoAuxiliar In ronda
    ''            For Each partidoaintroducir As partidoAuxiliar In partidosaux
    ''                If (partidoaintroducir.Comparar(partidoaintroducir, partidoEnRonda)) Then
    ''                    partidosRonda.Add(partidoaintroducir)
    ''                    partidosaux.Remove(partidoaintroducir)
    ''                    cont += 1
    ''                End If
    ''            Next
    ''        Next

    ''    Next
    ''End Sub

    'Public Sub Ordenacion()
    '    For i = 1 To numeroEquipos
    '        For j = i + 1 To numeroEquipos
    '            Dim arrayAux() As Integer = {i, j}
    '            partidos.Add(arrayAux)
    '        Next
    '    Next
    'End Sub

    'Public Sub creacionLiga()
    '    numeroRodas = numeroEquipos - 1
    '    partidosPorRonda = CInt(numeroEquipos / 2)
    '    Dim array(numeroRodas - 1, partidosPorRonda - 1) As Array
    '    Dim i, j As Integer
    '    i = 0
    '    j = 0
    '    Dim listaAuxuliar As List(Of Array) = New List(Of Array)
    '    Dim arrayAuxiliar As Array
    '    Do
    '        Dim rand As New Random()
    '        Dim index As Integer
    '        index = rand.Next(0, listaAuxuliar.Count)
    '        arrayAuxiliar = listaAuxuliar(index)

    '    Loop

    'End Sub
    'Public Function PartidoDefinido(ByVal array As Array, arrayAuxiliar As Array) As Boolean
    '    For i = 0 To numeroRodas - 1
    '        For j = 0 To partidosPorRonda - 1
    '            If (array.get

    '            End If
    '        Next
    '    Next
    'End Function

    'Public Sub partidosPorVuelta()


    'End Sub

End Class
