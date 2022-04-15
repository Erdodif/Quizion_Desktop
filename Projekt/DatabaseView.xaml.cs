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
        static string baseURL = "http://127.0.0.1:8000";
        string token;
        public string Token { get => token; set => token = value; }
        public DatabaseView()
        {
            InitializeComponent();
            datagrid.IsReadOnly = true;
            AdminPrivilegeButtonHidden();
        }
        private async Task<string> GetClientConnection(string url)
        {
            datagrid.Columns.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string reply = await client.GetStringAsync(url);
            return reply;
        }

        private async Task QuizListing(string url)
        {
            try
            {
                string reply = await GetClientConnection(url);
                List<Quiz> quiz = JsonConvert.DeserializeObject<List<Quiz>>(reply);
                datagrid.ItemsSource = quiz;               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async Task QuestionListing(string url)
        {
            try
            {
                string reply = await GetClientConnection(url);
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
                string reply = await GetClientConnection(url);
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
                string reply = await GetClientConnection(url);
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
                string reply = await GetClientConnection(url);
                List<Admin> admin = JsonConvert.DeserializeObject<List<Admin>>(reply);
                datagrid.ItemsSource = admin;
                btn_create.Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }           
        }
         private void LabelContent(string lb1, string lb2, string lb3)
        {
            lb_00.Content = lb1;
            lb_01.Content = lb2;
            lb_02.Content = lb3;
        }
        private void QuizClick(object sender, RoutedEventArgs e)
        {
            QuizListing(baseURL + "/admin/quizzes/all");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxInvisible();
            LabelContent("", "Header ", "Description ");
            AdminPrivilegeButtonHidden();
            btn_create.Visibility = Visibility.Visible;
            tbx_00.Visibility = Visibility.Hidden;
            cbx_quiz.IsSelected = true;
            cbx_quiz.Visibility = Visibility.Hidden;
        }

        private void QuestionClick(object sender, RoutedEventArgs e)
        {
            QuestionListing(baseURL + "/admin/questions");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxInvisible();
            TextBoxVisibled();
            LabelContent("QuizId ", "Content ", "Point ");
            AdminPrivilegeButtonHidden();
            btn_create.Visibility = Visibility.Visible;
            cbx_question.IsSelected = true;
            cbx_question.Visibility = Visibility.Hidden;
        }

        private void AnswerClick(object sender, RoutedEventArgs e)
        {
            AnswerListing(baseURL + "/admin/answers");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxInvisible();
            TextBoxVisibled();
            LabelContent("QuestionId ", "Content ", "IsRight ");
            AdminPrivilegeButtonHidden();
            btn_create.Visibility = Visibility.Visible;
            cbx_answer.IsSelected = true;
            cbx_answer.Visibility = Visibility.Hidden;          
        }

        private void AdminClick(object sender, RoutedEventArgs e)
        {
            AdminClickMethod();            
        }

        private void AdminClickMethod()
        {
            AdminListing(baseURL + "/admin/admins");
            EmptyInputs();
            ComboBoxInvisible();
            LabelContent("", "Id ", "UserId ");
            btn_update.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;
            btn_adminPrivilege.Visibility = Visibility.Visible;
            btn_adminPrivilege.Content = "Remove privilege";
            tbx_00.Visibility = Visibility.Hidden;
        }

        private void UserClick(object sender, RoutedEventArgs e)
        {
            UserListing(baseURL + "/admin/users");
            EmptyInputs();
            UpdateDeleteButtonVisibled();
            ComboBoxInvisible();
            TextBoxVisibled();
            LabelContent("UserId ", "Name ", "XP ");
            btn_adminPrivilege.Content = "Admin privilege";
            btn_adminPrivilege.Visibility = Visibility.Visible;         
        }

        private int IndexSearch(int id)
        {
            string selected = datagrid.SelectedItem.ToString();
            string[] st = selected.Split(';');
            return Convert.ToInt32(st[id]);
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex == -1)
            {
                MessageBox.Show("No items are selected from the list before the update!", "Invalid update", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int index = IndexSearch(0);
                MessageBoxResult result = MessageBox.Show($"Are you sure to update this item: {datagrid.SelectedItem} ", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (datagrid.SelectedItem is Quiz)
                    {
                        tbx_00.Visibility = Visibility.Hidden;
                        if (tbx_01.Text.Length < 3)
                        {
                            MessageBox.Show($"The header of Quiz is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                            
                        }
                        else if (tbx_02.Text.Length < 4)
                        {
                            MessageBox.Show($"The description of Quiz is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            UpdateQuiz(index);
                            EmptyInputs();
                            QuizListing(baseURL + "/admin/quizzes/all");
                        }
                    }
                    else if (datagrid.SelectedItem is Question)
                    {
                        TextBoxVisibled();
                        if (tbx_01.Text.Length < 3)
                        {
                            MessageBox.Show($"The content of Question is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);                            
                        }
                        else
                        {
                            UpdateQuestion(index);
                            EmptyInputs();
                            QuestionListing(baseURL + "/admin/questions");
                        }
                    }
                    else if (datagrid.SelectedItem is Answer)
                    {
                        TextBoxVisibled();
                        if (tbx_01.Text.Length < 3)
                        {
                            MessageBox.Show($"The content of Answer is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            UpdateAnswer(index);
                            EmptyInputs();
                            AnswerListing(baseURL + "/admin/answers");
                        }                      
                    }
                    else if (datagrid.SelectedItem is User)
                    {
                        TextBoxVisibled();
                        if (tbx_01.Text.Length < 3)
                        {
                            MessageBox.Show($"The name of User is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);                           
                        }
                        else
                        {
                            UpdateUser(index);
                            EmptyInputs();
                            UserListing(baseURL + "/admin/users");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error, the implementation of update is not successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);                        
                    }
                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk módosítani!
                }
            }
        }

        private async Task<HttpClient> PutClientConnection()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        private async Task UpdateQuiz(int id)
        {
            PutClientConnection();
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/quizzes/{id}", stringContent);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                MessageBox.Show("Successful update!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The update is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
                       
        }
        private async Task UpdateQuestion(int id)
        {
            PutClientConnection();
            JObject jObject = new JObject();
            jObject.Add("quiz_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/questions/{id}", stringContent);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                MessageBox.Show("Successful update!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The update is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task UpdateAnswer(int id)
        {
            PutClientConnection();
            JObject jObject = new JObject();
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/answers/{id}", stringContent);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                MessageBox.Show("Successful update!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The update is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private async Task UpdateUser(int id)
        {
            PutClientConnection();
            JObject jObject = new JObject();
            jObject.Add("user_id", tbx_00.Text);
            jObject.Add("name", tbx_01.Text);
            jObject.Add("xp", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"admin/users/{id}", stringContent);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                MessageBox.Show("Successful update!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The update is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex == -1)
            {
                MessageBox.Show("No items are selected from the list before the delete!", "Invalid delete", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int index = IndexSearch(0);
                MessageBoxResult result = MessageBox.Show($"Are you sure to delete this item: {datagrid.SelectedItem} ", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (datagrid.SelectedItem is Quiz)
                    {
                        DeleteQuiz(index);
                        EmptyInputs();
                        QuizListing(baseURL + "/admin/quizzes/all");
                    }
                    else if (datagrid.SelectedItem is Question)
                    {
                        DeleteQuestion(index);
                        EmptyInputs();
                        QuestionListing(baseURL + "/admin/questions");
                    }
                    else if (datagrid.SelectedItem is Answer)
                    {
                        DeleteAnswer(index);
                        EmptyInputs();
                        AnswerListing(baseURL + "/admin/answers");
                    }
                    else if (datagrid.SelectedItem is User)
                    {
                        DeleteUser(index);
                        EmptyInputs();
                        UserListing(baseURL + "/admin/users");
                    }
                    else
                    {
                        MessageBox.Show($"Error, the implementation of delete is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);                       
                    }
                    MessageBox.Show("The delete of selected item is successful!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
                }
                else
                {
                    //Nem történik semmi, ha nem szeretnénk törölni!
                }
            }
        }
        private async Task<HttpClient> DeleteClientConnection()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private async Task DeleteQuiz(int id)
        {
            DeleteClientConnection();
            await client.DeleteAsync($"admin/quizzes/{id}");          
        }

        private async Task DeleteQuestion(int id)
        {
            DeleteClientConnection();
            await client.DeleteAsync($"admin/questions/{id}");
        }

        private async Task DeleteAnswer(int id)
        {
            DeleteClientConnection();
            await client.DeleteAsync($"admin/answers/{id}");
        }

        private async Task DeleteUser(int id)
        {
            DeleteClientConnection();
            await client.DeleteAsync($"admin/users/{id}");           
        }

        private async Task<HttpClient> PostClientConnection(string url)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
        private async Task InsertQuiz()
        {
            PostClientConnection("/admin/quizzes/");
            JObject jObject = new JObject();
            jObject.Add("header", tbx_01.Text);
            jObject.Add("description", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/admin/quizzes/", stringContent);
            if (response.StatusCode.ToString().Equals("Created"))
            {
                MessageBox.Show("Successful addition!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The addition is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task InsertQuestion()
        {
            PostClientConnection("/admin/questions/");
            JObject jObject = new JObject();
            jObject.Add("quiz_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("point", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("admin/questions/", stringContent);
            if (response.StatusCode.ToString().Equals("Created"))
            {
                MessageBox.Show("Successful addition!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The addition is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task InsertAnswer()
        {
            PostClientConnection("/admin/answers/");
            JObject jObject = new JObject();
            jObject.Add("question_id", tbx_00.Text);
            jObject.Add("content", tbx_01.Text);
            jObject.Add("is_right", tbx_02.Text);
            string content = JsonConvert.SerializeObject(jObject);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/admin/answers/", stringContent);
            if (response.StatusCode.ToString().Equals("Created"))
            {
                MessageBox.Show("Successful addition!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The addition is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            if (cbx_quiz.IsSelected == true)
            {
               
                if (tbx_01.Text.Length < 3)
                {
                    MessageBox.Show($"The header of Quiz is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
                else if (tbx_02.Text.Length < 4)
                {
                    MessageBox.Show($"The description of Quz is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning); 
                }
                else
                {
                    InsertQuiz();
                    EmptyInputs();
                    QuizListing(baseURL + "/admin/quizzes/all");
                }
            }
            else if (cbx_question.IsSelected == true)
            {
                if (tbx_01.Text.Length < 3)
                {
                    MessageBox.Show($"The content of Question is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);                    
                }
                else
                {
                    InsertQuestion();
                    EmptyInputs();
                    QuestionListing(baseURL + "/admin/questions");
                }
            }
            else if (cbx_answer.IsSelected == true)
            {
                if (tbx_01.Text.Length < 3)
                {
                    MessageBox.Show($"The content of Answer is not correct character length!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    InsertAnswer();
                    EmptyInputs();
                    AnswerListing(baseURL + "/admin/answers");
                }                
            }
            else
            {
                MessageBox.Show($"Error, the implementation of addition is not successful!", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);               
            }
        }

        private void cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbx.SelectedItem == cbx_quiz)
            {
                tbx_00.Visibility = Visibility.Hidden;
            }
            else if (cbx.SelectedItem == cbx_question)
            {
                TextBoxVisibled();
            }
            else if (cbx.SelectedItem == cbx_answer)
            {
                TextBoxVisibled();
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                if (datagrid.SelectedItem is Quiz)
                {
                    tbx_00.Visibility = Visibility.Hidden;
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    LabelContent("", "Header ", "Description ");
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }
                else if (datagrid.SelectedItem is Question)
                {
                    TextBoxVisibled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    LabelContent("QuizId ", "Content ", "Point ");
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }
                else if (datagrid.SelectedItem is Answer)
                {
                    TextBoxVisibled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    LabelContent("QuestionId ", "Content ", "IsRight ");
                    tbx_00.Text = st[1];
                    tbx_01.Text = st[2];
                    tbx_02.Text = st[3];
                }
                else if (datagrid.SelectedItem is User)
                {
                    TextBoxVisibled();
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    LabelContent("UserId ", "Name ", "XP ");
                    tbx_00.Text = st[0];
                    tbx_01.Text = st[1];
                    tbx_02.Text = st[2];
                }
                else if (datagrid.SelectedItem is Admin)
                {
                    tbx_00.Visibility = Visibility.Hidden;
                    string selected = datagrid.SelectedItem.ToString();
                    string[] st = selected.Split(';');
                    LabelContent("", "Id ", "UserId ");
                    tbx_01.Text = st[0];
                    tbx_02.Text = st[1];
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
                    int index = IndexSearch(0);
                    GrantAdminPrivilege(index);
                    AdminClickMethod();
                    Sleep(500);
                    btn_adminPrivilege.IsEnabled = true;
                }
                else
                {
                    int index = IndexSearch(1);
                    RevokeAdminPrivilege(index);
                    EmptyInputs();
                    AdminListing(baseURL + "/admin/admins");
                    Sleep(500);
                    btn_adminPrivilege.IsEnabled = true;
                }
            }               
        }

        private void Sleep(int sleepTime)
        {
            System.Threading.Thread.Sleep(sleepTime);
            btn_adminPrivilege.IsEnabled = false;
        }
        private async Task GrantAdminPrivilege(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync($"admin/users/grant/{id}", new StringContent(""));
            if (response.StatusCode.ToString().Equals("NoContent"))
            {
                MessageBox.Show("Successful grant admin privilege!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The grant privilege is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task RevokeAdminPrivilege(int id)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync($"admin/users/revoke/{id}",new StringContent(""));
            if (response.StatusCode.ToString().Equals("NoContent"))
            {
                MessageBox.Show("Successful revoke admin privilege!", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("The revoke privilege is not successful!", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EmptyInputs()
        {
            tbx_00.Text = "";
            tbx_01.Text = "";
            tbx_02.Text = "";
        }

        private void ComboBoxInvisible()
        {
            cbx.Visibility = Visibility.Hidden;
        }

        private void TextBoxVisibled()
        {
            tbx_00.Visibility = Visibility.Visible;
        }
        private void UpdateDeleteButtonVisibled()
        {
            btn_update.Visibility = Visibility.Visible;
            btn_delete.Visibility = Visibility.Visible;
        }
        private void AdminPrivilegeButtonHidden()
        {
            btn_adminPrivilege.Visibility = Visibility.Hidden;
        }
    }
}

