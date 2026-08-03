//using DVDL_Logic_layer.Person;
//using DVLD_DTOs;
//using System;
//using System.Data;

//namespace TestLayer
//{
//    internal class clsTestPeople
//    {
//        private static clsPersonRepository _personRepository = new clsPersonRepository();

//        static void Main(string[] args)
//        {
//            Console.WriteLine("=== 🚀 Starting Direct Test (Person) ===\n");

//            // Uncomment the method you want to test:

//            //TestAddPerson();
//            // TestUpdatePerson();
//            // TestGetPersonById(1); 
//            // TestGetPersonList();
//            TestDeletePerson(2004);

//            Console.ReadKey();
//        }

//        static void TestAddPerson()
//        {
//            clsPerson newPerson = new clsPerson()
//            {
//                NationalNo = "N9988778",
//                FirstName = "Ahmed",
//                SecondName = "Mohamed",
//                ThirdName = "Abdallah",
//                LastName = "Rawi",
//                DateOfBirth = new DateTime(2000, 1, 1),
//                Gendor = 0,
//                Address = "Iraq",
//                Phone = "07701234567",
//                Email = "ahmed@email.com",
//                NationalityCountryID = 1,
//                ImagePath = ""
//            };

//            _personRepository.AddPerson(newPerson);
//            Console.WriteLine("✅ Add person command executed.");
//        }

//        static void TestUpdatePerson()
//        {
//            clsPerson updatedPerson = new clsPerson()
//            {
//                PersonID = 1004,
//                NationalNo = "N5",
//                FirstName = "Ahmed",
//                SecondName = "Mohamed",
//                ThirdName = "Abdallah",
//                LastName = "Updated",
//                DateOfBirth = new DateTime(2000, 1, 1),
//                Gendor = 0,
//                Address = "Iraq",
//                Phone = "07700000000",
//                Email = "ahmed@email.com",
//                NationalityCountryID = 1,
//                ImagePath = ""
//            };

//            _personRepository.UpdatePerson(updatedPerson);
//            Console.WriteLine("✅ Update person command executed.");
//        }

//        static void TestGetPersonById(int personId)
//        {
//            clsPersonDTO personDTO = _personRepository.GetPersonById(personId);

//            Console.WriteLine("✅ Person data retrieved:");
//            Console.WriteLine($"   Full Name: {personDTO.FirstName} {personDTO.LastName}");
//            Console.WriteLine($"   National No: {personDTO.NationalNo}");
//        }

//        static void TestGetPersonList()
//        {
//            DataTable dt = _personRepository.GetPersonList();

//            foreach (DataRow row in dt.Rows)
//            {
//                Console.WriteLine($"ID: {row["PersonID"]} | Name: {row["FirstName"]} {row["LastName"]}");
//            }
//        }

//        static void TestDeletePerson(int personId)
//        {
//            _personRepository.DeletePerson(personId);
//            Console.WriteLine($"✅ Delete command executed for ID: {personId}");
//        }
//    }
//}