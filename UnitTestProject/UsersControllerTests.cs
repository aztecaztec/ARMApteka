using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARM_Apteka.Controllers;


namespace UnitTestProject
{
    [TestClass]
    public class UsersControllerTests
    {
        UsersController uc = new UsersController();

        [TestMethod]
        public void CheckAuth_Correct_trueRetuned()
        {
            //Arrange
            string login = "farma";
            string password = "3";

            //Act
            
            bool result = uc.Auth(login, password);

            //Assert

            Assert.IsTrue(result);

        }
        [TestMethod]
        public void CheckAuth_InCorrect_falseRetuned()
        {
            //Arrange
            string login = "farma";
            string password = "2";

            //Act
            bool result = uc.Auth(login, password);

            //Assert

            Assert.IsFalse(result);

        }
    }
}
