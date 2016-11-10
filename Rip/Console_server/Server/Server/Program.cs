// SocketServer.cs
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;


namespace SocketServer
{
    class Server
    {
        static void Main(string[] args)
        {
            // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;


                    // Мы дождались клиента, пытающегося с нами соединиться

                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");

                    string reply = "Ответ " + RPN.Calculate(data);

                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }

    //Калькулятор
     public   class RPN
        {
            //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
            static private bool IsDelimeter(char c)
            {
                if ((" =".IndexOf(c) != -1))
                    return true;
                return false;
            }
            //Метод возвращает true, если проверяемый символ - оператор
            static private bool IsOperator(char с)
            {
                if (("+-/*^()".IndexOf(с) != -1))
                    return true;
                return false;
            }
            //Метод возвращает приоритет оператора
            static private byte GetPriority(char s)
            {
                switch (s)
                {
                    case '(': return 0;
                    case ')': return 1;
                    case '+': return 2;
                    case '-': return 3;
                    case '*': return 4;
                    case '/': return 4;
                    case '^': return 5;
                    default: return 6;
                }
            }
            //"Входной" метод класса
            static public double Calculate(string input)
            {
                string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
                double result = Counting(output); //Решаем полученное выражение
                return result; //Возвращаем результат
            }
            static private string GetExpression(string input)
            {
                string output = string.Empty; //Строка для хранения выражения
                Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

                for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
                {
                    //Разделители пропускаем
                    if (IsDelimeter(input[i]))
                        continue; //Переходим к следующему символу

                    //Если символ - цифра, то считываем все число
                    if (Char.IsDigit(input[i])) //Если цифра
                    {
                        //Читаем до разделителя или оператора, что бы получить число
                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            output += input[i]; //Добавляем каждую цифру числа к нашей строке
                            i++; //Переходим к следующему символу

                            if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                        }

                        output += " "; //Дописываем после числа пробел в строку с выражением
                        i--; //Возвращаемся на один символ назад, к символу перед разделителем
                    }

                    //Если символ - оператор
                    if (IsOperator(input[i])) //Если оператор
                    {
                        if (input[i] == '(') //Если символ - открывающая скобка
                            operStack.Push(input[i]); //Записываем её в стек
                        else if (input[i] == ')') //Если символ - закрывающая скобка
                        {
                            //Выписываем все операторы до открывающей скобки в строку
                            char s = operStack.Pop();

                            while (s != '(')
                            {
                                output += s.ToString() + ' ';
                                s = operStack.Pop();
                            }
                        }
                        else //Если любой другой оператор
                        {
                            if (operStack.Count > 0) //Если в стеке есть элементы
                                if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                    output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                            operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                        }
                    }
                }

                //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
                while (operStack.Count > 0)
                    output += operStack.Pop() + " ";

                return output; //Возвращаем выражение в постфиксной записи
            }
            static private double Counting(string input)
            {
                double result = 0; //Результат
                Stack<double> temp = new Stack<double>(); //Dhtvtyysq стек для решения

                for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
                {
                    //Если символ - цифра, то читаем все число и записываем на вершину стека
                    if (Char.IsDigit(input[i]))
                    {
                        string a = string.Empty;

                        while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                        {
                            a += input[i]; //Добавляем
                            i++;
                            if (i == input.Length) break;
                        }
                        temp.Push(double.Parse(a)); //Записываем в стек
                        i--;
                    }
                    else if (IsOperator(input[i])) //Если символ - оператор
                    {
                        //Берем два последних значения из стека
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i]) //И производим над ними действие, согласно оператору
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': result = b / a; break;
                            case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                        }
                        temp.Push(result); //Результат вычисления записываем обратно в стек
                    }
                }
                return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
            }
        }
}