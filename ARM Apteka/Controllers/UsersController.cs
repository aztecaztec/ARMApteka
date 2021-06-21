using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ARM_Apteka.Models;
using ARM_Apteka.Properties;

namespace ARM_Apteka.Controllers
{
    /// <summary>
    /// Класс, содержащий взаимодействия с таблицей Users
    /// </summary>
    public class UsersController
    {
        Core db= new Core(); //подключение к базе данных
        /// <summary>
        /// проверка авторизации пользователя
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns>
        /// Истинна, если авторизация успешна
        /// Исключение, если авторизация не удалась
        /// </returns>
        public bool Auth(string login, string password)
        {
            var userLogin = db.context.Users.Where(x => x.login == login && x.password == password).FirstOrDefault();

            if (userLogin == null)
            {
                throw new Exception("Пользователя с такими данными нет!");
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// Получение информации о роли пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>
        /// Значение роли пользователя
        /// Исключение в случае ошибочного результата
        /// </returns>
        public int GetCurrentUsersRole(string login)
        {
            
            int result = db.context.Users.Where(x => x.login == login).Count();
            if (result == 1)
            {
                int userRole = Convert.ToInt32(db.context.Users.Where(x => x.login == login).First().user_group);
                return userRole;
            }
            else
            {
                throw new Exception("Пользователя с таким именем несуществует");
            }
        }
        /// <summary>
        /// Получение Массива данных с информацией о пользователях
        /// </summary>
        /// <returns>Массив данных</returns>
        public List<Users> GetUsers()
        {
            List<Users> value = db.context.Users.Where(x=>x.user_group > 1).ToList();
            return value;
        }
        /// <summary>
        /// Изменение информации о группе пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="groupid">id новой группы</param>
        /// <returns>
        /// true, если изменение успешно
        /// else или исключение, если изменение провалено
        /// </returns>
        public bool UpdateUserGroup(string login, int groupid)
        {
            if (String.IsNullOrWhiteSpace(login) == false
                    && String.IsNullOrWhiteSpace(groupid.ToString()) == false)
            {
                if (db.context.Users.Where(x=>x.login == login).Count()>0)
                {
                    var con = db.context.Users.Where(x=>x.login == login).FirstOrDefault();
                    con.user_group = groupid;
                    db.context.SaveChanges();
                    if (db.context.SaveChanges() == 0) return true; else return false;
                }
                else throw new Exception("Данны неправильные значения обновления");
            }
            else throw new Exception("Данны неправильные значения для обновления");
        }


        /// <summary>
        /// Удаление пользователя из базы данных
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>
        /// true, если удаление успешно
        /// else или исключение, если удаление провалено
        /// </returns>
        public bool DeleteUser(string login)
        {
            try
            {
                db.context.Users.Remove(db.context.Users.Where(x => x.login == login).FirstOrDefault());
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0) return true; else return false;
            }
            catch { throw new Exception("ошибка удаления из бд"); }
        }
        /// <summary>
        /// Добавить пользователя в базу данных с отправкой авторизационных данных
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="name">Имя пользователя</param>
        /// <param name="email">Почта пользователя</param>
        /// <param name="group_id">id группы</param>
        /// <returns>
        /// true, если добавление успешно
        /// else или исключение, если добавление провалено
        /// </returns>
        public bool AddUsers(string login, string name, string email, int group_id)
        {
            //try
            //{
                if (String.IsNullOrWhiteSpace(name) == false
                    && String.IsNullOrWhiteSpace(login) == false
                    && String.IsNullOrWhiteSpace(email) == false
                    && String.IsNullOrWhiteSpace(group_id.ToString()) == false)
                {
                    if (login.Length > 3 && login.Contains(" ") == false && email.Length > 3 && email.Contains(" ") == false)
                    {

                        if (db.context.Users.Where(x=>x.login == login).Count() == 0)
                        {
                            

                            //Задание адресов отправки
                            MailAddress from = new MailAddress("TestARMApteka@gmail.com", "Аптека");
                            MailAddress to = new MailAddress(email);

                            
                            // генерация случайного пароля
                            Random random = new Random();
                            string charstring = "qwertyuiopasdfghjklzxcvbnm0123456789QWERTYUIOPASDFGHJKLZXCBVNM";
                            var arrChar = charstring.ToCharArray();
                            
                            
                            string password = "";
                            while (password.Length <= random.Next(5, 10))
                            {
                                int rndTopIndex = random.Next(0, arrChar.Length);
                                password += Char.ConvertFromUtf32(arrChar[rndTopIndex]);
                            }

                        



                            //TestARMApteka@gmail.com
                            //Testpass1
                            Users objUser = new Users
                            {
                                login = login,
                                name = name,
                                email = email,
                                password = password,
                                user_group = group_id
                            };
                            db.context.Users.Add(objUser);
                            db.context.SaveChanges();
                            if (db.context.SaveChanges() == 0)
                            {
                                //Письмо
                                MailMessage msg = new MailMessage(from, to);
                                msg.Subject = "Авторизационное письмо";
                                msg.Body = "Ваш логин: "+login+"<br>Ваш пароль: "+password;
                                msg.IsBodyHtml = true;

                                //smtp клиент
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                                smtp.Credentials = new NetworkCredential("TestARMApteka@gmail.com", "Testpass1");
                                smtp.EnableSsl = true;
                                smtp.Send(msg);
                                return true;
                            }
                            else return false;
                        }
                        else throw new Exception("Уже есть пользователь с данным логином");
                    }
                    else throw new Exception("Данны неправильные значения для добавления");

                }
                else throw new Exception("Данны неправильные значения для добавления");

            //}
            //catch { throw new Exception("ошибка добавления в бд"); }

        }



    }
}
