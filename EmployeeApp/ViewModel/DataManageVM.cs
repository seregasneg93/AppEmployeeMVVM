using EmployeeApp.Model;
using EmployeeApp.View;
using EmployeeApp.VIew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmployeeApp.ViewModel
{
    public class DataManageVM : INotifyPropertyChanged
    {
        private List<Department> _allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return _allDepartments; }
            set
            {
                _allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

        private List<Position> _allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return _allPositions; }
            set
            {
                _allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }

        private List<User> _allUsers = DataWorker.GetAllUsers();
        public List<User> AllUsers
        {
            get { return _allUsers; }
            set
            {
                _allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }
        //отдел
        public static string DepartmentName { get; set; }
        // позиция
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public Department PositionDepartment { get; set; }
        //сотрудник
        public static string UserName { get; set; }
        public static string UserSurName { get; set; }
        public static string UserPhone { get; set; }
        public static Position UserPosition { get; set; }

        //свойства для выделенных елемнетов
        public TabItem SelectedTabItem {get;set;}
        public static User SelectedUser { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #region COMMANDS TO ADD

        private RealyCommand _addNewDepartment;
        public RealyCommand addNewDepartment
        {
            get
            {
                return _addNewDepartment ?? new RealyCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string result = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    else
                    {
                        result = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllData();
                        ShowMessageToUser(result);
                        SetNullValuesToPropetries();
                        wnd.Close();
                    }
                });
            }
        }

        private RealyCommand _addNewPosition;
        public RealyCommand AddNewPosition
        {
            get
            {
                return _addNewPosition ?? new RealyCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string result = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if(PositionSalary == 0)
                    {
                        SetRedBlockControll(wnd, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControll(wnd, "MaxNumberBlock");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел!");
                    }
                    else
                    {
                        result = DataWorker.CreatePosition(PositionName, PositionSalary , PositionMaxNumber , PositionDepartment);
                        UpdateAllData();
                        ShowMessageToUser(result);
                        SetNullValuesToPropetries();
                        wnd.Close();
                    }
                });
            }
        }

        private RealyCommand _addNewUser;
        public RealyCommand AddNewUser
        {
            get
            {
                return _addNewUser ?? new RealyCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string result = "";
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "NameBlock");
                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "SurnameBlock");
                    }
                    if (UserPhone == null || UserPhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, "PhoneBlock");
                    }
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите позицию!");
                    }
                    else
                    {
                        result = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                        UpdateAllData();
                        ShowMessageToUser(result);
                        SetNullValuesToPropetries();
                        wnd.Close();
                    }
                });
            }
        }


        #endregion

        #region EDIT COMMAND

        private RealyCommand _editUser;
        public RealyCommand EditUser
        {
            get
            {
                return _editUser ?? new RealyCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "Не выбран сотрудник";
                    string noPosStr = "Не выбрана должность!";

                    if (SelectedUser != null)
                    {
                        if(UserPosition != null)
                        {
                            result = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition);

                            UpdateAllData();
                            SetNullValuesToPropetries();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                        else ShowMessageToUser(noPosStr);
                    }
                    else ShowMessageToUser(result);
                });
            }
        }

        private RealyCommand _editPosition;
        public RealyCommand EditPosition
        {
            get
            {
                return _editPosition ?? new RealyCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "Не выбранa позиция";
                    string noPosStr = "Не выбран отдел!";

                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            result = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxNumber, PositionSalary, PositionDepartment);

                            UpdateAllData();
                            SetNullValuesToPropetries();
                            ShowMessageToUser(result);
                            window.Close();
                        }
                        else ShowMessageToUser(noPosStr);
                    }
                    else ShowMessageToUser(result);
                });
            }
        }

        private RealyCommand _editDepartment;
        public RealyCommand EditDepartment
        {
            get
            {
                return _editDepartment ?? new RealyCommand(obj =>
                {
                    Window window = obj as Window;
                    string result = "Не выбран отдел";

                    if (SelectedDepartment != null)
                    {
                        result = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

                        UpdateAllData();
                        SetNullValuesToPropetries();
                        ShowMessageToUser(result);
                        window.Close();
                    }
                    else ShowMessageToUser(result);
                });
            }
        }
        #endregion

        private RealyCommand _deleteItem;
        public RealyCommand DeleteItem
        {
            get
            {
                return _deleteItem ?? new RealyCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано.";

                    if(SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        UpdateAllData();
                    }
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllData();
                    }
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllData();
                    }
                    SetNullValuesToPropetries();
                    ShowMessageToUser(resultStr);
                });     
            }
        }

        #region COMMANDS TO OPEN WINDOWS

        private RealyCommand _openAddNewDepartment;
        public RealyCommand openAddNewDepartment
        {
            get 
            {
                return _openAddNewDepartment ?? new RealyCommand(obj =>
                    {
                        OpenAddDepartmentWindowMethod();
                    });
            }
        }

        private RealyCommand _openAddNewPosition;
        public RealyCommand openAddNewPosition
        {
            get
            {
                return _openAddNewPosition ?? new RealyCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                });
            }
        }

        private RealyCommand _openAddNewUser;
        public RealyCommand openAddNewUser
        {
            get
            {
                return _openAddNewUser ?? new RealyCommand(obj =>
                {
                    OpenAddUserWindowMethod();
                });
            }
        }

        private RealyCommand _openEditItemWnd;
        public RealyCommand OpenEditItemWnd
        {
            get
            {
                return _openEditItemWnd ?? new RealyCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано.";

                    if (SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                    {
                        OpenEditUserWindowMethod(SelectedUser);
                    }
                    if (SelectedTabItem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                }
                );
            }
        }
        #endregion


        #region Methods To Open WINDOW
        // ОТкрытие окон
        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newDepartmentWindow = new AddNewPositionWindow();
            SetCenterPositionOpen(newDepartmentWindow);
        }
        private void OpenAddUserWindowMethod()
        {
            AddNewUserWindow newDepartmentWindow = new AddNewUserWindow();
            SetCenterPositionOpen(newDepartmentWindow);
        }

        // окна редактирования
        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow newDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionOpen(newDepartmentWindow);
        }
        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow newDepartmentWindow = new EditPositionWindow(position);
            SetCenterPositionOpen(newDepartmentWindow);
        }
        private void OpenEditUserWindowMethod(User user)
        {
            EditUserWindow newDepartmentWindow = new EditUserWindow(user);
            SetCenterPositionOpen(newDepartmentWindow);
        }
        private void SetCenterPositionOpen(Window window)       
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region UPDATE VIEWS
        private void SetNullValuesToPropetries()
        {
            UserName = null;
            UserSurName = null;
            UserPhone = null;
            UserPosition = null;
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;
            DepartmentName = null;
        }
        private void UpdateAllData()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllUsersView();
        }
        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }
        private void UpdateAllUsersView()
        {
            AllUsers = DataWorker.GetAllUsers();
            MainWindow.AllUsersView.ItemsSource = null;
            MainWindow.AllUsersView.Items.Clear();
            MainWindow.AllUsersView.ItemsSource = AllUsers;
            MainWindow.AllUsersView.Items.Refresh();
        }
        #endregion

        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        private void ShowMessageToUser(string message)
        {
            MessageView view = new MessageView(message);
            SetCenterPositionOpen(view);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propetryName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propetryName));
            }
        }
    }
}
