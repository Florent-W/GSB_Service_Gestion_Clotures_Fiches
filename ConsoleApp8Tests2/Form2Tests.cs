using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Tests
{
    [TestClass()]
    public class Form2Tests
    {

        [TestMethod()]
        public void getMoisPrecedentTestMoisHuitTestSansSurchargeSupplementaire()
        {
            string moisPrecedent = ConsoleApp8.Form2.getMoisPrecedent();
            Assert.AreEqual(DateTime.Now.AddMonths(-1).ToString("MM"), moisPrecedent);
        }

        [TestMethod()]
        public void getMoisPrecedentTestMoisHuit()
        {
            DateTime date = new DateTime(2017, 8, 2);

            string moisPrecedent = ConsoleApp8.Form2.getMoisPrecedent(date);

            Assert.AreEqual("07", moisPrecedent);
        }

        [TestMethod()]
        public void getMoisPrecedentTestMoisUn()
        {
            DateTime date = new DateTime(2018, 01, 2);

            string moisPrecedent = ConsoleApp8.Form2.getMoisPrecedent(date);

            Assert.AreEqual("12", moisPrecedent);
        }

        [TestMethod()]
        public void getMoisPrecedentTestMoisUnTest2()
        {
            DateTime date = new DateTime(2018, 01, 2);

            string moisPrecedent = ConsoleApp8.Form2.getMoisPrecedent(date);

            Assert.AreNotEqual("00", moisPrecedent);
        }

        [TestMethod()]
        public void getMoisSuivantTestMoisHuitTestSansSurchargeSupplementaire()
        {
            string moisSuivant = ConsoleApp8.Form2.getMoisSuivant();
            Assert.AreEqual(DateTime.Now.AddMonths(+1).ToString("MM"), moisSuivant);
        }

        [TestMethod()]
        public void getMoisSuivantTestMoisHuit()
        {
            DateTime date = new DateTime(2017, 8, 2);

            string moisSuivant = ConsoleApp8.Form2.getMoisSuivant(date);

            Assert.AreEqual("09", moisSuivant);
        }

        [TestMethod()]
        public void getMoisSuivantTestMoisDouzeTest()
        {
            DateTime date = new DateTime(2017, 12, 2);

            string moisSuivant = ConsoleApp8.Form2.getMoisSuivant(date);

            Assert.AreNotEqual("13", moisSuivant);
        }

        [TestMethod()]
        public void getMoisSuivantTestMoisDouzeTest2()
        {
            DateTime date = new DateTime(2017, 12, 2);

            string moisSuivant = ConsoleApp8.Form2.getMoisSuivant(date);

            Assert.AreEqual("01", moisSuivant);
        }

        [TestMethod()]
        public void entreTestSansSurchargeSupplementaire()
        {

            int jour1 = 2, jour2 = 12;

            Boolean jourEntre = ConsoleApp8.Form2.entre(jour1, jour2);

            if (DateTime.Now.Day <= jour1 && DateTime.Now.Day >= jour2) {
                Assert.AreEqual(true, jourEntre);
            }
            else
            {
                Assert.AreNotEqual(true, jourEntre);
            }
        }

        [TestMethod()] /// Datetime actuel non testable car le résultat du test et de la méthode change selon le jour actuel
        public void entreTest2et12()
        {
            int jour1 = 2, jour2 = 12;

            DateTime date = new DateTime(2017, 2, 12);

            Boolean jourEntre = ConsoleApp8.Form2.entre(jour1, jour2, date);

            Assert.AreEqual(true, jourEntre);
        }

        [TestMethod()] 
        public void entreTest2et20()
        {
            int jour1 = 2, jour2 = 20;

            DateTime date = new DateTime(2017, 2, 21);

            Boolean jourEntre = ConsoleApp8.Form2.entre(jour1, jour2, date);

            Assert.AreEqual(false, jourEntre);
        }

        [TestMethod()]
        public void entreTest8et2()
        {
            int jour1 = 8, jour2 = 2;

            DateTime date = new DateTime(2017, 2, 5);

            Boolean jourEntre = ConsoleApp8.Form2.entre(jour1, jour2, date);

            Assert.AreNotEqual(true, jourEntre);
        }

    }
}