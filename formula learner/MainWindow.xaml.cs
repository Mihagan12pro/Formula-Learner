using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;

namespace formula_learner
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string condition = "mainMenu";
        string pathFamousFormula = @"..\..\ProgrammFiles\famous formula button.txt";
        string pathTittles = @"..\..\ProgrammFiles\for tests\tittles.txt";
        string pathFormulas = @"..\..\ProgrammFiles\for tests\formulas.txt";//с помощью ..\..\ мы можем писать неполные пути, не пихая папку в папку Debug

        List<string> formulasForTest = new List<string>();
        List<string> tittlesForTest = new List<string>();

        //List<string> formulasAndTittle = new List<string>();
        int formulasLast;
        int indexOfFormula = 0;
        List<string> rightAnswer = new List<string>();
        List<string> unRightAnswer = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
        }
        private void hideMainMenu()
        {
            famousFormulaButton.Visibility = Visibility.Hidden;
            testButton.Visibility = Visibility.Hidden;
            newFormula.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
        }
        private void showMainMenu()
        {
            famousFormulaButton.Visibility = Visibility.Visible;
            testButton.Visibility = Visibility.Visible;
            newFormula.Visibility = Visibility.Visible;
        }


        private void famousFormulaButton_Click(object sender, RoutedEventArgs e)
        {
            formulasList.Visibility = Visibility.Visible;

            back.Visibility = Visibility.Visible;

            hideMainMenu();


            StreamReader listOfFormulas = new StreamReader(pathFamousFormula);
            string[] formulas = listOfFormulas.ReadToEnd().Split('\n');
            int n = 0;
            foreach (var i in formulas)
            {

                formulasList.Items.Insert(n, i);
                n++;
            }


        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            
            if (formulasList.Visibility == Visibility.Visible)
            {
                showMainMenu();
                back.Visibility = Visibility.Hidden;
                formulasList.Visibility = Visibility.Hidden;
                formulasList.Items.Clear();//очищаем listbox "formulasList"
            }
            else if (addNewFormulaCanvas.Visibility == Visibility.Visible)
            {
                showMainMenu();
                back.Visibility = Visibility.Hidden;
                addNewFormulaCanvas.Visibility = Visibility.Hidden;
                formulaTextBox.Text = "";
                tittleTextBox.Text = "";

            }
            else if (startTestLocalCanvas.Visibility == Visibility.Visible)
            {
                showMainMenu();
                back.Visibility = Visibility.Hidden;
                testCanvas.Visibility = Visibility.Hidden;
                startTestLocalCanvas.Visibility = Visibility.Hidden;

            }
            else if (watchTheResultsCanvas.Visibility==Visibility.Visible)
            {
                startTestLocalCanvas.Visibility = Visibility.Visible;
                back.Content = "←";
                watchTheResultsCanvas.Visibility = Visibility.Hidden;

                rightListBox.Items.Clear();
                unrightListBox.Items.Clear();
               addedFormulasAndRTittlesListbox.Items.Clear();

                List<List<string>> lists = new List<List<string>>() {rightAnswer,unRightAnswer,formulasForTest,tittlesForTest };
                foreach(var i in lists)
                {


                    i.Clear();


                }
            }

        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            hideMainMenu();
            testCanvas.Visibility = Visibility.Visible;
            startTestLocalCanvas.Visibility = Visibility.Visible;


        }

        private void newFormula_Click(object sender, RoutedEventArgs e)
        {
            hideMainMenu();
            addNewFormulaCanvas.Visibility = Visibility.Visible;
        }

        private void addNewWorldButton_Click(object sender, RoutedEventArgs e)
        {

            string tittle = tittleTextBox.Text;
            string formula = formulaTextBox.Text;


            using (StreamWriter fileForTittle = new StreamWriter(pathTittles, true))
            {

                fileForTittle.WriteLine(tittle);

            }
            using (StreamWriter fileForFormula = new StreamWriter(pathFormulas, true))
            {

                fileForFormula.WriteLine(formula);

            }
            tittleTextBox.Text = "";
            formulaTextBox.Text = "";
        }





        public void GetFromFilesTittlesAndFormulas()
        {

            
            using (StreamReader sr = new StreamReader(pathFormulas))
            {

                string line = "";

                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {

                    formulasForTest.Add(line);
                    
                }
                sr.Close();


            }
           // MessageBox.Show("1");
            using (StreamReader sr2 = new StreamReader(pathTittles))
            {

                string line = "";

                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr2.ReadLine()) != null)
                {

                    tittlesForTest.Add(line);
                    //MessageBox.Show(line);
                }

                sr2.Close();

            }





        }
        public void askAQuestion()
        {
            indexOfFormula++;
            //MessageBox.Show(Convert.ToString(tittlesForTest.Count), Convert.ToString(indexOfFormula));
            formulaQuestionLabel.Content = "Напишите формулу " + tittlesForTest[indexOfFormula] + ": ";
        }
        public void addToThreeListBoxTest()
        {
            for(int i=0;i<formulasForTest.Count;i++) 
            {

                addedFormulasAndRTittlesListbox.Items.Add(tittlesForTest[i]+" :" +" "+formulasForTest[i]);

            }



            void adding(List<string>l , ListBox ls)
            {
                

                    for (int i = 0; i < l.Count; i++)
                    {

                        ls.Items.Add(l[i]);

                    }
                

                

            }
            adding(rightAnswer, rightListBox);
            adding(unRightAnswer, unrightListBox);



        }

        


        private void startTestButton_Click(object sender, RoutedEventArgs e)
        {
            startTestLocalCanvas.Visibility = Visibility.Hidden;
            processOfTestLocalCanvas.Visibility = Visibility.Visible;
            GetFromFilesTittlesAndFormulas();
            indexOfFormula = -1;
            askAQuestion();
        }



        private void answerButton_Click_1(object sender, RoutedEventArgs e)
        {
            string answer = formulaAnswerTextBox.Text;
            if (answer == formulasForTest[indexOfFormula])
            {
                rightAnswer.Add(tittlesForTest[indexOfFormula]+": "+answer);
            }
            else
            {
                MessageBox.Show("Правильный ответ: " + formulasForTest[indexOfFormula],"Вы ошиблись!");
                unRightAnswer.Add(tittlesForTest[indexOfFormula] + ": " + answer);
            }

            formulaAnswerTextBox.Text = "";
            

            if(indexOfFormula<formulasForTest.Count-1)
            {
                if (indexOfFormula+1>= formulasForTest.Count - 1)
                {

                    processOfTestLocalCanvas.Visibility = Visibility.Hidden;
                    watchTheResultsCanvas.Visibility = Visibility.Visible;
                    back.Visibility = Visibility.Visible;
                    back.Content = "→";
                    addToThreeListBoxTest();

                }
                else
                {

                    askAQuestion();

                }

               


            }
            
    
            
        }































        /*
    private void startTestButton_Click(object sender, RoutedEventArgs e)
    {
        void copyALL(List<string>list,List<string>listFromCopy)
        {

            foreach(var i in listFromCopy)
            {

                list.Add(i);

            }

        }

        back.Visibility=Visibility.Hidden;
        startTestButton.Visibility=Visibility.Hidden;
        processOfTestLocalCanvas.Visibility=Visibility.Visible;

        StreamReader srFormulas = new StreamReader(pathFormulas);
        StreamReader srTittles = new StreamReader(pathTittles);

        formulasForTest = srFormulas.ReadToEnd().Split('\n').ToList();
        tittlesForTest = srTittles.ReadToEnd().Split('\n').ToList();

        List<string>CrutchList = new List<string>();
        foreach(var i in formulasForTest)
        {
            if (i=='\n')
            {

                MessageBox.Show(i);

            }


            if (i.Length>=4)
            {

                CrutchList.Add(i);

            }



        }
        formulasForTest.Clear();
        copyALL(formulasForTest,CrutchList);
        CrutchList.Clear();

        foreach (var i in tittlesForTest)
        {

            if (i.Length >= 4)
            {
                CrutchList.Add(i);

            }



        }
        tittlesForTest.Clear();
        copyALL(tittlesForTest,CrutchList);
        CrutchList.Clear();


        //MessageBox.Show(Convert.ToString(formulasForTest.Count));



        for (int i=0;i<formulasForTest.Count;i++)
        {

            if (formulasForTest.Count()>1 && tittlesForTest.Count()>1)
            {
                formulasAndTittle.Add(tittlesForTest[i] + "迷" + formulasForTest[i]);
                //MessageBox.Show(tittlesForTest[i] + formulasForTest[i], Convert.ToString((tittlesForTest[i] + "迷" + formulasForTest[i]).Length));
            }

        }




        formulasLast = formulasForTest.Count();
        formulaLastLabel.Content = "Осталось "+ Convert.ToString(formulasLast);



        formulaQuestionLabel.Content = tittlesForTest[_IndexOfFormula];

    }


    private void answerButton_Click(object sender, RoutedEventArgs e)
    {
        void AddInfoToResultListBox(List<string>list,ListBox ls,string right)
        {
            if (list.Count<1)
            {
                switch(right)
                {
                    case "right":
                        ls.Items.Add("Нет правильных ответов");
                        break;


                }


            }
            else
            {

                for (int i = 0; i < list.Count; i++)
                {

                    ls.Items.Insert(i, list[i]);

                }

            }


        }

        string answerFormula = formulaAnswerTextBox.Text;

        if (formulasForTest[_IndexOfFormula]!=answerFormula)
        {


            unRightAnswer.Add(tittlesForTest[_IndexOfFormula] + " :" + answerFormula);
            MessageBox.Show(answerFormula , formulasForTest[_IndexOfFormula]);



        }
        else
        {
            rightAnswer.Add(tittlesForTest[_IndexOfFormula] + " :" + answerFormula);
        }
        _IndexOfFormula++;

        if (_IndexOfFormula < tittlesForTest.Count()) 
        {

            formulaQuestionLabel.Content = tittlesForTest[_IndexOfFormula];
            formulaLastLabel.Content = "Осталось " + Convert.ToString(tittlesForTest.Count()-_IndexOfFormula);

        }
        else
        {

            processOfTestLocalCanvas.Visibility = Visibility.Hidden;
            watchTheResultsCanvas.Visibility = Visibility.Visible;

            AddInfoToResultListBox(rightAnswer, rightListBox,"right");

        }
        */






    }
}
