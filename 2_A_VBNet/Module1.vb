Module Module1

    Sub Cube(ByVal n1 As Integer, ByVal n2 As Integer)
        n1 = n1 * n1 * n1
        n2 = n2 * n2 * n2
        Console.WriteLine("a and b inside Cube() function")
        Console.WriteLine(n1 & " " & n2)
    End Sub

    Sub Cube2(ByVal n1 As MyInt, ByVal n2 As MyInt)
        n1.value = n1.value * n1.value * n1.value
        n2.value = n2.value * n2.value * n2.value
        Console.WriteLine("MyInt a and b inside Cube() function")
        Console.WriteLine(n1.value & " " & n2.value)
    End Sub
    Sub Main()
        Dim Num1 As Integer = 5
        Dim Num2 As Integer = 10
        Console.WriteLine("num1 and num2 in Main() function")
        Console.WriteLine(Num1 & " " & Num2)
        Cube(Num1, Num2)
        Console.WriteLine("num1 and num2 after Cube() was called")
        Console.WriteLine(Num1 & " " & Num2)


        Dim Num1_r As MyInt = New MyInt()
        Dim Num2_r As MyInt = New MyInt()
        Num1_r.value = 5
        Num2_r.value = 10
        Console.WriteLine("MyInt Num1 and num2 in Main() function")
        Console.WriteLine(Num1_r.value & " " & Num2_r.value)
        Cube2(Num1_r, Num2_r)
        Console.WriteLine("MyInt Num1 and Num2 after Cube() was called")
        Console.WriteLine(Num1_r.value & " " & Num2_r.value)
        Console.WriteLine("Press a key to Exit..")
        Console.ReadLine()
    End Sub

End Module
