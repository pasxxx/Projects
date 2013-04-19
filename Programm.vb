Module Module1
    Enum CallResult
        Succeeded
    End Enum

    Enum Stores
        Metro
        YourHome
    End Enum

    Public Class Drink

    End Class

    Public Const MaxFailCount As Integer = 10

    Private Class CloseFriend
        Implements IDisposable

        Public Property FailCount As Integer
        Public Property IsNotSleeping As Boolean

        Function CallBySkype() As CallResult
        End Function

        Function SendSms() As CallResult
        End Function

        Function CallByPhone() As CallResult
        End Function

        Function DoDrink(drink As Drink)

        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class


    Public Class Store
        Public Property Drinks As List(Of Drink)
    End Class

    Private _friends As List(Of CloseFriend)

    Private _acceptedFriends As New List(Of CloseFriend)

    Private [Me] As CloseFriend

    Sub Main()
        Select Case Today.DayOfWeek
            Case DayOfWeek.Monday
                DoWork()
            Case DayOfWeek.Tuesday
                DoWork()
            Case DayOfWeek.Wednesday
                DoWork()
            Case DayOfWeek.Thursday
                DoWork()
            Case DayOfWeek.Friday
                Dim bestFriends As New List(Of CloseFriend)
                _friends.Where(Function(friendItem) TypeOf friendItem Is CloseFriend).AsParallel.ForAll(
                    Sub(item)
                        Dim ok As Boolean = False
                        While Not ok And Now.Hour < 23
                            If item.CallBySkype = CallResult.Succeeded Then
                                ok = True
                            ElseIf item.SendSms = CallResult.Succeeded Then
                                ok = True
                            ElseIf item.CallByPhone = CallResult.Succeeded Then
                                ok = True
                            End If
                            If ok Then
                                bestFriends.Add(item)
                                item.FailCount = 0
                            End If

                            System.Threading.Thread.Sleep(30 * 60 * 1000)
                        End While
                        If Not ok Then
                            item.FailCount += 1
                            If item.FailCount > MaxFailCount Then
                                _friends.Remove(item)
                                item.Dispose()
                                item = Nothing
                                GC.Collect()
                            End If
                        End If
                    End Sub)
                Dim rnd As New Random
                If _friends.Count > 0 Then
                    Dim store As Store = GoToStore(If((New Random).Next() > 0.5, Stores.Metro, Stores.YourHome))

                    Dim drink As Drink = store.Drinks.FirstOrDefault()

                    GoToHome()

                    While bestFriends.Any(Function(item) item.IsNotSleeping) And Now.DayOfWeek <= DayOfWeek.Saturday And Now.Hour < 6
                        For Each item In bestFriends.Where(Function(friendItem) friendItem.IsNotSleeping).ToList
                            item.DoDrink(drink)
                        Next
                        [Me].DoDrink(drink)
                        System.Threading.Thread.Sleep(5 * 60 * 1000)
                    End While
                End If
            Case DayOfWeek.Saturday
                System.Threading.Thread.Sleep(24 * 60 * 60 * 1000)
            Case DayOfWeek.Sunday
                System.Threading.Thread.Sleep(12 * 60 * 60 * 1000)
                PrepareForWork()
        End Select
    End Sub


    Public Function GoToStore(store As Stores) As Store
    End Function

    Public Function DoWork()
    End Function

    Public Function PrepareForWork()
    End Function

    Private Sub GoToHome()
        Throw New NotImplementedException
    End Sub

End Module

