namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void Solve(object sender, EventArgs e)
        {
            try
            {
                if(equation.Text.Length  == 1 && equation.Text[0] == '-')
                {

                }
                if (equation.Text.Length > 0)
                {
                    string StarterEquation = equation.Text;

                    List<char> chars = new List<char>();

                    for (int i = 0; i < StarterEquation.Length; i++)
                    {
                        if (StarterEquation[i] == '*')
                        {
                            chars.Add(StarterEquation[i]);
                        }
                        else if (StarterEquation[i] == '/')
                        {
                            chars.Add(StarterEquation[i]);
                        }
                    }

                    string[] equationsTab = StarterEquation.Split('*', '/');

                    if(equationsTab.Length == 0)
                    {
                        equationsTab = new string[1];
                        equationsTab[0] = equation.Text;
                    }

                    List<string> equations = new List<string>();

                    foreach (string equation in equationsTab)
                    {
                        equations.Add(equation);
                    }

                    float num1LenghtToSkip = 0; 

                    for (int i = 1; i < chars.Count + 1; i++)
                    {
                        float result = 0;

                        string num1 = equations[i - 1];
                        

                        string num2 = equations[i];

                        string[] num1t = num1.Split('-', '+');

                        string[] num2t = num2.Split('-', '+');

                        num1LenghtToSkip += num1t[num1t.Length - 1].Length;

                        string num2t2 = (num2t[0] == "") ? num2t[1] : num2t[0];

                        if (chars[i - 1] == '*')
                        {
                            result = float.Parse(num1t[num1t.Length - 1]) * float.Parse(num2t2);
                        }
                        else if (chars[i - 1] == '/')
                        {
                            result = float.Parse(num1t[num1t.Length - 1]) / float.Parse(num2t2);
                        }

                        bool minusSkip = false;

                        if (num1[(num1.Length - 1) - (num1t[num1t.Length - 1].Length - 1)] == '-')
                        {
                            minusSkip = true;
                            result *= -1;
                        }

                        if (num2[(num2t2.Length - 1)] == '-')
                        {
                            result *= -1;
                        }

                        string finalResult = "";

                        bool finded = false;

                       

                        for (int j = num1.Length - 1; j >= 0; j-=1)
                        {
                            if (num1[j] == '-' || num1[j] == '+')
                            {
                                finded = true;


                                if (num1[j] == '-' && minusSkip)
                                {
                                    minusSkip = false;
                                    continue;
                                }

                                  
                            }

                            if (finded)
                            {
                                finalResult += num1[j];
                            }
                        }

                        finalResult = Reverse(finalResult);

                        finalResult += result;

                        finded = false;

                        for (int j = 0 + num2t2.Length; num2.Length > j; j++)
                        {
                            if (num2[j] == '-' || num2[j] == '+')
                            {
                                finded = true;
                            }

                            if (finded)
                            {
                                finalResult += num2[j];
                            }
                        }

                        equations[i] = finalResult.ToString();

                        equations.RemoveAt(i - 1);

                        chars.RemoveAt(0);

                        i -= 1;
                    }

                    for (int i = 0; i < equations.Count; i++)
                    {
                        string[] eqs = equations[i].Split('+');

                        float sum = 0;

                        for (int j = 0; j < eqs.Length; j++)
                        {  
                                if (eqs[j][0] == '-' && eqs[j].Length == 1)
                                {
                                 
                                }

                                string[] eqs2 = eqs[j].Split("-");

                                List<string> eqs3 = new List<string>();

                                float sum_ = 0;

                                foreach (string eq2 in eqs2)
                                {
                                    eqs3.Add(eq2);
                                }

                                for(int x = 1; x < eqs3.Count ; x += 1)
                                {
                                    float result = 0;
                                    
                                    if(eqs3[x - 1] == null || eqs3[x - 1] == string.Empty)
                                    {
                                        eqs3[x - 1] = "0";
                                    }
                                    if (eqs3[x] == null || eqs3[x] == string.Empty)
                                    {
                                        eqs3[x] = "0";
                                    }

                                    result = float.Parse(eqs3[x - 1]) - float.Parse(eqs3[x]);

                                    eqs3[x] = result.ToString();

                                    eqs3.RemoveAt(x - 1);

                                    x -= 1;
                                }

                                eqs[j] = eqs3[0];

                                sum += float.Parse(eqs[j]);
                            

                            equations[i] = sum.ToString();
                        }
                    }

                    

                    equation.Text = equations[0].ToString();
                }
            }
            catch (DivideByZeroException ex) 
            {
                equation.Text = "Nie dziel przez 0!!!";
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }

        }

        private void Clear(object sender, EventArgs e)
        {
            equation.Text = "";
        }

        private void Write(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var text = btn.Text;

            if (equation.Text == "Nie dziel przez 0!!!")
            {
                equation.Text = "";
            }

            if (text == "0")
            {
                if(equation.Text.Length > 0)
                {
                    if (equation.Text[equation.Text.Length - 1] == '0' || equation.Text[equation.Text.Length - 1] == '/')
                    {

                    }
                    else
                    {
                        equation.Text += "0";
                    }
                }
                else
                {
                    equation.Text += "0";
                }
            }
            else
            {
                if (equation.Text == "0")
                {
                    equation.Text = "";
                }

                equation.Text += btn.Text;
            } 
        }

        private void GoBack(object sender, EventArgs e)
        {
            if (equation.Text == "Nie dziel przez 0!!!")
            {
                equation.Text = "";
            }

            string equationText = "";

            for(int i = 0; i < equation.Text.Length - 1; i++)
            {
                equationText += equation.Text[i];
            }

            equation.Text = equationText;
        }

        private void AddChar(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            if(equation.Text == "Nie dziel przez 0!!!")
            {
                equation.Text = "";
            }

            if(btn.Text == "-")
            {
                if(equation.Text.Length == 0)
                {
                    equation.Text = "-";
                }
                else if (equation.Text[equation.Text.Length - 1] != '-' &&
                         equation.Text[equation.Text.Length - 1] != '+' &&
                         equation.Text[equation.Text.Length - 1] != ','
                    )
                {
                    equation.Text += "-";
                }
            }
            else if (btn.Text == ",")
            {
                if (equation.Text.Length == 0)
                {
                    
                }
                else if (equation.Text[equation.Text.Length - 1] != '*' &&
                        equation.Text[equation.Text.Length - 1] != '/' &&
                        equation.Text[equation.Text.Length - 1] != '+' &&
                        equation.Text[equation.Text.Length - 1] != '-' &&
                        equation.Text[equation.Text.Length - 1] != ','
                    )
                {
                    equation.Text += ",";
                }
            }
            else
            {
                if(equation.Text.Length > 0)
                {
                    if (equation.Text[equation.Text.Length - 1] != '*' &&
                        equation.Text[equation.Text.Length - 1] != '/' &&
                        equation.Text[equation.Text.Length - 1] != '+' &&
                        equation.Text[equation.Text.Length - 1] != '-' &&
                        equation.Text[equation.Text.Length - 1] != ','
                       )
                    {
                        equation.Text += btn.Text;
                    }
                }
            }
        }
    }
}