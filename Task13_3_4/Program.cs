namespace Task13_3_4
{
    internal class Program
    {
        static void Main()
        {
            //  создаём пустой список с типом данных Contact
            var phoneBook = new List<Contact>();

            // добавляем контакты
            phoneBook.Add(new Contact("Игорь", 79990000000, "igor2@example.com"));
            phoneBook.Add(new Contact("Андрей", 79990000001, "andrew@example.com"));

            AddUnique(new Contact("Игорь", 79990000000, "igor2@example.com"), phoneBook); //Полный дубль
            Console.WriteLine();

            AddUnique(phoneBook[0], phoneBook); //Полный дубль Игоря, но передаем уже существующую ссылку в коллекции и тогда должен сработать IndexOf
            Console.WriteLine();
            
            AddUnique(new Contact("Игорь", 79990000002, "igor1@example.com"), phoneBook); //Теска
            Console.WriteLine();
            AddUnique(new Contact("Борис", 79990000003, "Boris@example.com"), phoneBook);
            Console.WriteLine();

            Console.ReadKey (); 
        }

        public class Contact 
        {
            public Contact(string name, long phoneNumber, String email) // метод-конструктор
            {
                Name = name;
                PhoneNumber = phoneNumber;
                Email = email;
            }

            public String Name { get; }
            public long PhoneNumber { get; }
            public String Email { get; }
        }

        static void AddUnique(Contact contact, List<Contact> phoneBook)
        {
            int idx = phoneBook.IndexOf(contact);
            if (idx == -1)
            {
                Console.WriteLine("Так со ссылочным типом не сработает, если именно его ссылки нет в коллекции");
            }
            else
            {
                Console.WriteLine($"Передал ссылку на экземпляр класса в коллекции - {idx}");
            }

            //Использую для поиска полного совпадения анонимный метод
            if (phoneBook.FindIndex(delegate (Contact entry)
                {
                    return (
                            (entry.Name == contact.Name) && (entry.Email == contact.Email) && (entry.PhoneNumber == contact.PhoneNumber)
                           );
                }
                ) == -1)
            {
                phoneBook.Add(contact);
            }

            #region Сортировка по имени контакта
            //phoneBook.Sort((entry1, entry2) => String.Compare(entry1.Name, entry2.Name, StringComparison.Ordinal)); //Лямбда будет короче

            //phoneBook.Sort(delegate (Contact entry1, Contact entry2)
            //     {
            //         return String.Compare(entry1.Name, entry2.Name, StringComparison.Ordinal);
            //     }
            //    );
            #endregion Сортировка по имени контакта

            #region Сортировка по имени и email
            phoneBook.Sort(delegate (Contact entry1, Contact entry2)
            {
                int res = String.Compare(entry1.Name, entry2.Name, StringComparison.Ordinal);
                if (res != 0)
                {
                    return String.Compare(entry1.Name, entry2.Name, StringComparison.Ordinal);
                }
                else //хотя else тут и не нужен
                {
                    return String.Compare(entry1.Email, entry2.Email, StringComparison.Ordinal);
                }
            });
            #endregion Сортировка по имени и email

            foreach (Contact contact2 in phoneBook)
            {
                Console.WriteLine(contact2.Name + " " + contact2.PhoneNumber + " " + contact2.Email);
            }
        }
    }
}