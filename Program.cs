using System;
using System.Collections.Generic;


namespace CalcConsoleApp
{
    class Program
    {
        static List<CalcSet> calcsetList = new List<CalcSet>();
        static CalcSet calset2 = new CalcSet();

        static void Main(string[] args)
        {
            MainDisplay();


        }//Main

        public static void MainDisplay()
        {
            Console.Clear();
            Console.WriteLine(" ");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("1) 이전계산식 전체조회");
            Console.WriteLine(" ");
            Console.WriteLine("2) 계산기");
            Console.WriteLine(" ");
            Console.WriteLine("3) 종료");
            Console.WriteLine(" ");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("원하는 메뉴를 선택하세요.");

            var inputStr = Console.ReadLine();

            switch (inputStr)
            {
                case "1":
                    ShowHistorY();
                    break;
                case "2":
                    Calculate();
                    break;
                case "3":
                    return;

            }
        }//MainDisplay()


        public static void SecondDisplay(string arg)
        {
            string main_str = "";

            Console.Clear();
            Console.WriteLine(" 계산기 ");
            Console.WriteLine("   ");
            //Console.WriteLine("현재 입력 식 : {0} ", calset2.expr );
            Console.WriteLine("   ");
            Console.WriteLine(" 숫자 혹은 연산자(+,-,/,*)를 하나씩 입력 후 엔터를 입력하세요");
            Console.WriteLine(" 마지막에 = 입력 후 엔터를 입력하시면 결과값을 확인하실 수 있습니다 ");

            main_str = string.Format(" 입력 --> {0}", arg);
            Console.WriteLine(main_str);

        }

        private static void Calculate()
        {

            string tempStr = "";
            
            while (true)
            {

                SecondDisplay(calset2.expr);
                tempStr = Console.ReadLine();

                if (tempStr != "=")
                {
                    switch (Validate(tempStr))
                    {
                    case 0:
                    case 1:
                    case 2:
                        calset2.AddCalcSet(tempStr);
                        calset2.expr += tempStr;
                        break;
                    case 3:
                    //숫자+문자 조합시 넣지 말것.
                        break;
                    }
                }
                else
                {
                    //히스토리 넣기
                    calcsetList.Add(calset2);

                   //마지막이니 계산식 최종호출
                    calset2.CalculateExpr();

                    Console.Clear();

                    Console.WriteLine("계산식 : {0} =", calset2.expr);
                    Console.WriteLine("결과 : {0}", calset2.resultVal);
                    Console.WriteLine("아무키나 입력하시면 뒤로 갑니다. ");

                    //마지막에, dispose 해야됨.
                    calset2.expr = "";
                    calset2.resultVal = "";


                    Console.ReadLine();

                    MainDisplay();
                }
            }

        }

        private static int Validate(string input)
        {
            int rtnVal = 0;
            
            /*  리턴코드 기준
             *  0 : 숫자/연산자/=
             *  1 : 숫자OK
             *  2 : 연산자
             *  3 : 숫자+연산자 같이 적음
             *  9 : 숫자가 아님

            */

            //숫자다
            if(numericCheck(input))
            {
                rtnVal = 1;
            }
            else
            {
            //숫자가 아니다


                //숫자+연산자조합 체크
                if (input.Length > 1)
                {
                    rtnVal = 3;   
                }else if (input == "+" || input == "-" || input == "*" || input == "/")
                {
                    //연산자 Only
                    rtnVal = 2;
                }
            }

            return rtnVal;
        }

        //숫자여부 체크
        private static bool numericCheck(string strNumber)
        {
            try
            {
                int iNumber = Convert.ToInt32(strNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private static void ShowHistorY()
        {
            Console.Clear();
            Console.WriteLine("   ");
            Console.WriteLine("이전 계산식 조회");
            Console.WriteLine("   ");

            if (calcsetList.Count > 0)
            {
                for (int i = 0; i < calcsetList.Count; i++)
                {

                    //구현필요
                    //Console.WriteLine(calcsetList.ToString());
                    CalcSet temp = calcsetList[i];
                    Console.WriteLine("{0}번 입력값 및 결과 >>>  {1} = {2} ", i.ToString(), temp.expr, temp.resultVal);
                    Console.WriteLine(" ");
                }
            }
            else 
            {
                Console.WriteLine(" No Data....");
                Console.WriteLine(" 뒤로 가려면 아무키나 누르세요!");
                var input = Console.ReadLine();

                MainDisplay();

                //if(input == "X" || input == "x")
                //{
                //    MainDisplay();
                //}
                //else
                //{
                //    Console.Clear();
                //    Console.WriteLine("잘못 누르셨습니다. 뒤로가려면 X");                    
                //    var input3 = Console.ReadLine();
                //}
            }
        }


    }//class

}//namespace