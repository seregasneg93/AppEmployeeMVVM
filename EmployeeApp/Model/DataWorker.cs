using EmployeeApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Model
{
    public static class DataWorker
    {
        public static string CreateDepartment(string name)
        {
            string res = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(x => x.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    res = "Добавлено!";
                }
                return res; 
            }
        }

        public static string CreatePosition(string name , decimal salary,int maxnumber,Department department)
        {
            string res = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Positions.Any(x => x.Name == name && x.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPositions = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxnumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPositions);
                    db.SaveChanges();
                    res = "Добавлено!";
                }
                return res;
            }
        }

        public static string CreateUser(string name, string surname, string phone, Position position)
        {
            string res = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(x => x.Name == name && x.SurName == surname && x.Position == position);
                if (!checkIsExist)
                {
                    User newUsers = new User
                    {
                        Name = name,
                        SurName = surname,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUsers);
                    db.SaveChanges();
                    res = "Добавлено!";
                }
                return res;
            }
        }

        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отдела нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = $"Отдел {department.Name} удален!";
            }
            return result;
        }

        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = $"Позиция {position.Name} удалена!";
            }
            return result;
        }

        public static string DeleteUser(User user)
        {
            string result = "Такого сотрудника нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = $"Сoтрудник {user.Name} удален!";
            }
            return result;
        }

        public static string EditDepartment(Department olddepartment , string newName)
        {
            string result = "Такого отдела нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department1 = db.Departments.FirstOrDefault(x => x.Id == olddepartment.Id);
                if(department1 != null)
                {
                    department1.Name = newName;
                    db.SaveChanges();
                    result = "Департамент изменен.";
                }
            }
            return result;
        }
        public static string EditPosition(Position oldPosition, string newName , int newMsxnumber , decimal newSalary , Department newDepartment)
        {
            string result = "Такой позиции нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(x => x.Id == oldPosition.Id);
                if (position != null)
                {
                    position.Name = newName;
                    position.Salary = newSalary;
                    position.MaxNumber = newMsxnumber;
                    position.DepartmentId = newDepartment.Id;
                    db.SaveChanges();
                    result = "Позиция изменена.";
                }
            }
            return result;
        }

        public static string EditUser(User oldUser, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого работника нет!";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Id == oldUser.Id);
                if(user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = "Работник изменен.";
                }
            }
            return result;
        }

        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }

        public static Position GetPositionById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(x => x.Id == id);
                return pos;
            }
        }

        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department dep = db.Departments.FirstOrDefault(x => x.Id == id);
                return dep;
            }
        }

        public static List<User> GetAllUserByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> pos = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return pos;
            }
        }
    }
}
