Public Class Form1
    Private PartidosTabla As List(Of List(Of partidoAuxiliar)) = New List(Of List(Of partidoAuxiliar))
    Private PartidoRonda As List(Of partidoAuxiliar) = New List(Of partidoAuxiliar)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lista As List(Of String) = New List(Of String)
        lista.Add("A")
        lista.Add("B")
        lista.Add("C")
        lista.Add("D")
        lista.Add("E")
        lista.Add("F")
        lista.Add("G")
        lista.Add("H")
        lista.Add("I")
        'lista.Add("J")
        'Dim l1 As AlgoritmoLigaImpar = New AlgoritmoLigaImpar(lista)
        'l1.Ordenacion()
        'l1.Liga()
        Dim l2 As AlgoritmoLiga = New AlgoritmoLiga(lista)
        l2.Ordenacion()
        l2.Liga()

    End Sub


End Class
