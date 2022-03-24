using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Projekt
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        static HttpClient client = new HttpClient();
        static ColorsOfQuizion quizionColors = new ColorsOfQuizion();
        string token;

        public string Token { get => token; set => token = value; }
        public DatabaseView()
        {
            InitializeComponent();
            ColorSetter();
            btn_adminPrivilege.Visibility = Visibility.Hidden;
        }

        private void ColorSetter()
        {
            btn_quiz.BorderBrush = quizionColors.OnSecondary;
            btn_quiz.Foreground = quizionColors.OnPrimary;
            btn_quiz.Background = quizionColors.Primary;
            btn_question.Background = quizionColors.Primary;
            btn_question.Foreground = quizionColors.OnPrimary;
            btn_question.BorderBrush = quizionColors.OnSecondary;
            btn_answer.Background = quizionColors.Primary;
            btn_answer.BorderBrush = quizionColors.OnSecondary;
            btn_answer.Foreground = quizionColors.OnPrimary;
            btn_user.Background = quizionColors.Primary;
            btn_user.BorderBrush = quizionColors.OnSecondary;
            btn_user.Foreground = quizionColors.OnPrimary;
            btn_admin.Background = quizionColors.Primary;
            btn_admin.BorderBrush = quizionColors.OnSecondary;
            btn_admin.Foreground = quizionColors.OnPrimary;
        }
       
        private async Task QuizListing(string url)
        {
            try
            {
                datagrid.Columns.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string reply = await client.GetStringAsync(url);
                List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(reply);
                datagrid.ItemsSource = quiz;
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async Task QuestionListing(string url)
        {
            try
            {
                datagrid.Columns.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string reply = await client.GetStringAsync(url);
                List<Question> question = JsonConvert.DeserializeObject<List<Question>>(reply);
                datagrid.ItemsSource = question;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async Task AnswerListing(string url)
        {
            try
            {
                datagrid.Columns.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string reply = await client.GetStringAsync(url);
                List<Answer> answer = JsonConvert.DeserializeObject<List<Answer>>(reply);
                datagrid.ItemsSource = answer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }           
        }

        private async Task UserListing(string url)
        {
            try
            {
                datagrid.Columns.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string reply = await client.GetStringAsync(url);
                List<User> user = JsonConvert.DeserializeObject<List<User>>(reply);
                datagrid.ItemsSource = user;
                btn_create.Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }           
        }

        
        private async Task AdminListing(string url)
        {
            try
            {
                datagrid.Columns.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string reply = await client.GetStringAsync(url);
                List<Admin> admin = JsonConvert.DeserializeObject<List<Admin>>(reply);
                datagrid.ItemsSource = admin;
                btn_create.Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }           
        }
        


        private void QuizClick(object sender, RoutedEventArgs e)
        {
            //QuizListing("http://quizion.hu/admin/quizzes/all");
            QuizListing("http://127.0.0.1:8000/admin/quizzes/all");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxEnable();
            btn_create.Visibility = Visibility.Visible;
            btn_adminPrivilege.Visibility = Visibility.Hidden;
            tbx_00.IsEnabled = false;
            lb_00.Content = "";
            lb_01.Content = "Header ";
            lb_02.Content = "Description ";
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            //QuestionListing("http://quizion.hu/admin/questions");
            QuestionListing("http://127.0.0.1:8000/admin/questions");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxEnable();
            btn_create.Visibility = Visibility.Visible;
            btn_adminPrivilege.Visibility = Visibility.Hidden;
            TextBoxEnabled();
            lb_00.Content = "QuizId ";
            lb_01.Content = "Content ";
            lb_02.Content = "Point ";

        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            //AnswerListing("http://quizion.hu/admin/answers");
            AnswerListing("http://127.0.0.1:8000/admin/answers");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxEnable();
            btn_create.Visibility = Visibility.Visible;
            btn_adminPrivilege.Visibility = Visibility.Hidden;
            TextBoxEnabled();
            lb_00.Content = "QuestionId ";
            lb_01.Content = "Content ";
            lb_02.Content = "IsRight ";
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            AdminListing("http://127.0.0.1:8000/admin/admins");
            //AdminListing("http://quizion.hu/admin/admins");
            EmptyInputs();
            btn_update.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;
            cbx.IsEnabled = false;
            btn_adminPrivilege.Visibility = Visibility.Visible;
            btn_adminPrivilege.Content = "Remove privilege";
            TextBoxEnabled();
            lb_00.Content = "Id ";
            lb_01.Content = "UserId";
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            //UserListing("http://quizion.hu/admin/users");
            UserListing("http://127.0.0.1:8000/admin/users");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            cbx.IsEnabled = false;
            btn_adminPrivilege.Content = "Admin privilege";
            btn_adminPrivilege.Visibility = Visibility.Visible;
            TextBoxEnabled();
            lb_00.Content = "UserId ";
            lb_01.Content = "Name ";
            lb_02.Content = "XP ";
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex == -1)
            {
                MessageBox.Show("No items are selected from the list before the update!", "Invalid update", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                string selected = datagrid.SelectedItem.ToString();
                string[] st = selected.Split(';');
                int index = Convert.ToInt32(st[0]);
                MessageBoxResult result = MessageBox.Show($"Are you sure to update this item: {datagrid.SelectedItem} ", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (datagrid.SelectedItem is Quiz)
                    {
                        tbx_00.IsEnabled = false;
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "The header of Quiz is not correct character length!";
                            RedErrorMessage();
                        }
                        else if (tbx_02.Text.Length < 4)
                        {
                            tbl_status.Text = "The description of Quiz is not correct character length!";
                            RedErrorMessage();
                        }
                        else
                        {
                            UpdateQuiz(index);
                            EmptyInputs();
                            QuizListing("http://127.0.0.1:8000/admin/quizzes/all");
                            //QuizListing("http://quizion.hu/admin/quizzes/all");
                        }
                    }
                    else if (datagrid.SelectedItem is Question)
                    {
                        TextBoxEnabled();
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "The content of Question is not correct character length!";
                            RedErrorMessage();
                        }
                        else
                        {
                            UpdateQuestion(index);
                            EmptyInputs();
                            QuestionListing("http://127.0.0.1:8000/admin/questions");
                            //QuestionListing("http://quizion.hu/admin/questions");
                        }
                    }
                    else if (datagrid.SelectedItem is Answer)
                    {
                        TextBoxEnabled();
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "The content of Answer is not correct character length!";
                            RedErrorMessage();
                        }
                        else
                        {
                            UpdateAnswer(index);
                            EmptyInputs();
                            AnswerListing("http://127.0.0.1:8000/admin/answers");
                            //AnswerListing("http://quizion.hu/admin/answers");
                        }                      
                    }

                    else if (datagrid.SelectedItem is User)
                    {
                        TextBoxEnabled();
                        if (tbx_01.Text.Length < 3)
                        {
                            tbl_status.Text = "The name of User is not correct character length!";
                            RedErrorMessage();
                        }
                        else
                        {
                            UpdateUser(index);
                            EmptyInputs();
                            UserListing("http://127.0.0.1:8000/admin/users");
                            //UserListing("http://quizion.hu/admin/users");
                        }
                    }
                    else
                    {
                        tbl_status.Text = "Error, the implementation of update is not successful!";
                        RedErrorMessage();
                    }
                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk módosítani!
                }
            }
        }

        private async Task UpdateQuiz(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string url = "http://quizion.hu/admin/quizzes";
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/quizzes/{id}", stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task UpdateQuestion(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //string url = "http://quizion.hu/admin/questions";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject = new JObject();
            jObject.Add("quiz_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/questions/{id}", stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task UpdateAnswer(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string url = "http://quizion.hu/admin/answers";
            JObject jObject = new JObject();
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/answers/{id}", stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task UpdateUser(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string url = "http://quizion.hu/admin/users";
            JObject jObject = new JObject();
            jObject.Add("user_id", tbx_00.Text);
            jObject.Add("name", tbx_01.Text);
            jObject.Add("xp", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/users/{id}", stringContent);
            tbl_status.Text = response.ToString();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex == -1)
            {
                MessageBox.Show("No items are selected from the list before the delete!", "Invalid delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string selected = datagrid.SelectedItem.ToString();
                string[] st = selected.Split(';');
                int index = Convert.ToInt32(st[0]);
                MessageBoxResult result = MessageBox.Show($"Are you sure to delete this item: {datagrid.SelectedItem} ", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (datagrid.SelectedItem is Quiz)
                    {
                        DeleteQuiz(index);
                        EmptyInputs();
                        QuizListing("http://127.0.0.1:8000/admin/quizzes/all");
                        //QuizListing("http://quizion.hu/admin/quizzes/all");
                    }
                    else if (datagrid.SelectedItem is Question)
                    {
                        DeleteQuestion(index);
                        EmptyInputs();
                        QuestionListing("http://127.0.0.1:8000/admin/questions");
                        //QuestionListing("http://quizion.hu/admin/questions");
                    }
                    else if (datagrid.SelectedItem is Answer)
                    {
                        DeleteAnswer(index);
                        EmptyInputs();
                        AnswerListing("http://127.0.0.1:8000/admin/answers");
                        //AnswerListing("http://quizion.hu/admin/answers");
                    }
                    else if (datagrid.SelectedItem is User)
                    {
                        DeleteUser(index);
                        EmptyInputs();
                        UserListing("http://127.0.0.1:8000/admin/users");
                        //UserListing("http://quizion.hu/admin/users");
                    }
                    else
                    {
                        tbl_status.Text = "Error, the implementation of delete is not successful!";
                        RedErrorMessage();
                    }
                    MessageBox.Show("The delete of selected item is successful!", "Successful delete", MessageBoxButton.OK, MessageBoxImage.Question);
                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk törölni!
                }
            }

        }

        private async Task DeleteQuiz(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"admin/quizzes/{id}");
            tbl_status.Text = response.ToString();
        }

        private async Task DeleteQuestion(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //client.BaseAddress = new Uri("http://quizion.hu/");
            var response = await client.DeleteAsync($"admin/questions/{id}");
            tbl_status.Text = response.ToString();
        }

        private async Task DeleteAnswer(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"admin/answers/{id}");
            tbl_status.Text = response.ToString();
        }

        private async Task DeleteUser(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            //client.BaseAddress = new Uri("http://quizion.hu/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.DeleteAsync($"admin/users/{id}");
            tbl_status.Text = response.ToString();
        }

        private async Task InsertQuiz()
        {
            client = new HttpClient();
            string url = "/admin/quizzes/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task InsertQuestion()
        {
            client = new HttpClient();
            string url = "/admin/questions/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject = new JObject();
            jObject.Add("quiz_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task InsertAnswer()
        {
            client = new HttpClient();
            string url = "/admin/answers/";
            // client.BaseAddress = new Uri("http://quizion.hu");
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            JObject jObject = new JObject();
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, stringContent);
            tbl_status.Text = response.ToString();
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (cbx.SelectedItem == cbx_quiz)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = "The header of Quiz is not correct character length!";
                    RedErrorMessage();
                }
                else if (tbx_02.Text.Length < 4)
                {
                    tbl_status.Text = "The description of Quz is not correct character length!";
                    RedErrorMessage();
                }
                else
                {
                    InsertQuiz();
                    EmptyInputs();
                    QuizListing("http://127.0.0.1:8000/admin/quizzes/all");
                    //QuizListing("http://quizion.hu/admin/quizzes/all");
                }
            }

            else if (cbx.SelectedItem == cbx_question)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = "The content of Question is not correct character length!";
                    RedErrorMessage();
                }
                else
                {
                    InsertQuestion();
                    EmptyInputs();
                    QuestionListing("http://127.0.0.1:8000/admin/questions");
                    //QuestionListing("http://quizion.hu/admin/questions");
                }
            }

            else if (cbx.SelectedItem == cbx_answer)
            {
                if (tbx_01.Text.Length < 3)
                {
                    tbl_status.Text = " The content of Answer is not correct character length!";
                    RedErrorMessage();
                }
                else
                {
                    InsertAnswer();
                    EmptyInputs();
                    AnswerListing("http://127.0.0.1:8000/admin/answers");
                    //AnswerListing("http://quizion.hu/admin/answers");
                }                
            }
            else
            {
                tbl_status.Text = "Error, the implementation of addition is not successful";
                RedErrorMessage();
            }
        }

        private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx.SelectedItem == cbx_quiz)
            {
                tbx_00.IsEnabled = false;
            }
            else if (cbx.SelectedItem == cbx_question)
            {
                TextBoxEnabled();
            }
            else if (cbx.SelectedItem == cbx_answer)
            {
                TextBoxEnabled();
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                if (datagrid.SelectedItem is Quiz)
                {
                    tbx_00.IsEnabled = false;
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    lb_01.Content = "Header ";
                    lb_02.Content = "Description ";
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }
                else if (datagrid.SelectedItem is Question)
                {
                    TextBoxEnabled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    lb_00.Content = "QuizId ";
                    lb_01.Content = "Content ";
                    lb_02.Content = "Point ";
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }
                else if (datagrid.SelectedItem is Answer)
                {
                    TextBoxEnabled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    lb_00.Content = "QuestionId ";
                    lb_01.Content = "Content ";
                    lb_02.Content = "IsRight ";
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }
                else if (datagrid.SelectedItem is User)
                {
                    TextBoxEnabled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    lb_00.Content = "UserId ";
                    lb_01.Content = "Name ";
                    lb_02.Content = "XP ";
                    tbx_00.Text = st[0];
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }
                else if (datagrid.SelectedItem is Admin)
                {
                    TextBoxEnabled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    lb_00.Content = "Id ";
                    lb_01.Content = "UserId";
                    lb_02.Content = "";
                    tbx_00.Text = st[0];
                    tbx_01.Text = st[1];
                }
            }
        }

        private void AdminPrivilege(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex == -1)
            {
                MessageBox.Show("No items are selected from the list before the admin operation!" , "Not succesful admin operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {              
                if (datagrid.SelectedItem is User)
                {
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    int index = Convert.ToInt32(st[0]);
                    GrantAdminPrivilege(index);
                    EmptyInputs();
                    AdminListing("http://127.0.0.1:8000/admin/admins");
                }
                else
                {
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    int index = Convert.ToInt32(st[1]);
                    RevokeAdminPrivilege(index);
                    EmptyInputs();
                    AdminListing("http://127.0.0.1:8000/admin/admins");
                }
            }               
        }

        private async Task GrantAdminPrivilege(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string url = "http://quizion.hu/admin/users";
            string content = "Admin privilege addition";
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"admin/users/grant/{id}", stringContent);
            tbl_status.Text = response.ToString();
        }

        private async Task RevokeAdminPrivilege(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //string url = "http://quizion.hu/admin/users";
            /*string content = "Revoke admin privilege";
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");*/
            var response = await client.PostAsync($"admin/users/revoke/{id}",new StringContent("")/* stringContent*/);
            tbl_status.Text = response.ToString();
        }

        private void EmptyInputs()
        {
            tbx_00.Text = "";
            tbx_01.Text = "";
            tbx_02.Text = "";
        }

        private void RedErrorMessage()
        {
            tbl_status.Foreground = quizionColors.Warning;
        }

        private void ComboBoxEnable()
        {
            cbx.IsEnabled = true;
        }

        private void TextBoxEnabled()
        {
            tbx_00.IsEnabled = true;
        }

        private void UpdateDeleteButtonVisibled()
        {
            btn_update.Visibility = Visibility.Visible;
            btn_delete.Visibility = Visibility.Visible;
        }
    }
}
