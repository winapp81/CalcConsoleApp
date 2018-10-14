using System;
using System.Collections.Generic;
using System.Text;

namespace CalcConsoleApp
{
    class CalcSet
    {
 
            //계산식
        public string expr { set; get; }
        public string resultVal { set; get; }
        public string calcTime { set; get; }
        public static List<string> exprList = new List<string>();

        public CalcSet()
        {
            this.expr = "";
            this.resultVal = "";
            this.calcTime = "";
        }

        public CalcSet(string a, string b, string c)
        {
            this.expr = a;
            this.resultVal = b;
            this.calcTime = c;
        }
        
        //소멸자
        ~CalcSet()
        {
            this.expr = "";
            this.resultVal = "";
            this.calcTime = "";
        }

        public void AddCalcSet(string arg)
        {
            exprList.Add(arg);
        }

        public void CalculateExpr()
        {
            //연산자 우선순위별로, 루프진행
            //1번 루프돌 때 마다, 원본 List 데이터 가공(지우고, 계산결과 넣기)
            //계산시간 넣고, 결과값 넣기
            int resultInt = 0;

            List<int> tempList = HaveOperator("*");
            
            for (int i = 0; i < tempList.Count+1; i++)
            {
                if (tempList.Count > 0)
                {
                    var first = int.Parse(tempList[0].ToString()) - 1;
                    var second = int.Parse(tempList[0].ToString()) + 1;

                    var one = int.Parse(exprList[first].ToString());
                    var two = int.Parse(exprList[second].ToString());

                    resultInt = (one * two);

                    //exprList.RemoveRange(0, 3);
                    //좌표에 있는 값을 날려줘야됨 -> 아래와 같이 변경
                    
                    exprList.RemoveRange(first, 3);

                    //넣을때 앞뒤 연산자 처리 필요?
                    exprList.Insert(first, resultInt.ToString());
                }

                tempList = HaveOperator("*");
                if (tempList.Count == 0)
                    break;
            }

            tempList = HaveOperator("/");

            for (int i = 0; i < tempList.Count + 1; i++)
            {
                if (tempList.Count > 0)
                {
                    var first = int.Parse(tempList[0].ToString()) - 1;
                    var second = int.Parse(tempList[0].ToString()) + 1;

                    var one = int.Parse(exprList[int.Parse(tempList[0].ToString()) - 1].ToString());
                    var two = int.Parse(exprList[int.Parse(tempList[0].ToString()) + 1].ToString());
                    resultInt = (one / two);

                    exprList.RemoveRange(first, 3);
                    exprList.Insert(first, resultInt.ToString());
                }

                tempList = HaveOperator("/");
                if (tempList.Count == 0)
                    break;
            }

            for (int i = 0; i < exprList.Count + 1; i++)
            {
                if (exprList.Count > 0)
                {
                    var one = int.Parse(exprList[0].ToString());
                    var two = int.Parse(exprList[2].ToString());
                    if(exprList[1].ToString() == "+")
                    {
                        resultInt = one + two;
                    }else if(exprList[1].ToString() == "-")
                    {
                        resultInt = one - two;
                    }
                    exprList.RemoveRange(0, 3);
                    exprList.Insert(0, resultInt.ToString());
                }                
                if (exprList.Count == 1)
                    break;
            }


            this.resultVal = resultInt.ToString();
        }

        // 곱하기 포함되어 있다며,곱하기의 좌표를 리턴
        public List<int> HaveOperator(string arg_operator)
        {
            List<int> intList = new List<int>();

            if (exprList.Count > 2)
            {
                for(int i = 0; i< exprList.Count; i++)
                {
                    if(exprList[i].ToString() == arg_operator)
                    {
                        intList.Add(i);
                    }
                }
                
            }
            return intList;

        }
        
                
    }
}
