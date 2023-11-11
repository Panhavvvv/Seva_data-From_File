using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\nGender: {Gender}\nAge: {Age}\nSalary: {Salary:C}\n";
    }
}

class Program
{
    static List<Employee> employees = new List<Employee>();
    static string dataFilePath = "employee_data.txt";

    static void Main(string[] args)
    {
        ReadDataFromFile();
        while (true)
        {
            Console.WriteLine("Employee Management System Menu:");
            Console.WriteLine("1. Input Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. Search Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("5. Update Employee");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        InputEmployee();
                        break;
                    case 2:
                        ViewAllEmployees();
                        break;
                    case 3:
                        SearchEmployee();
                        break;
                    case 4:
                        DeleteEmployee();
                        break;
                    case 5:
                        UpdateEmployee();
                        break;
                    case 6:
                        SaveDataToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid menu choice.");
            }
        }
    }

    static void InputEmployee()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Gender: ");
        string gender = Console.ReadLine();
        Console.Write("Enter Age: ");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            Console.Write("Enter Salary: ");
            if (double.TryParse(Console.ReadLine(), out double salary))
            {
                Employee employee = new Employee
                {
                    Name = name,
                    Gender = gender,
                    Age = age,
                    Salary = salary
                };
                employees.Add(employee);
                Console.WriteLine("Employee information added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid salary. Please enter a valid salary.");
            }
        }
        else
        {
            Console.WriteLine("Invalid age. Please enter a valid age.");
        }
    }

    static void ViewAllEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
        }
        else
        {
            Console.WriteLine("List of Employees:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
                Console.WriteLine("-----------");
            }
        }
    }

    static void SearchEmployee()
    {
        Console.Write("Enter Name to search: ");
        string searchName = Console.ReadLine();
        var employee = employees.FirstOrDefault(e => e.Name == searchName);
        if (employee != null)
        {
            Console.WriteLine("Employee Found:");
            Console.WriteLine(employee);
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }
    }

    static void DeleteEmployee()
    {
        Console.Write("Enter Name to delete: ");
        string deleteName = Console.ReadLine();
        var employee = employees.FirstOrDefault(e => e.Name == deleteName);
        if (employee != null)
        {
            employees.Remove(employee);
            Console.WriteLine("Employee deleted successfully.");
        }
        else
        {
            Console.WriteLine("Employee not found for deletion.");
        }
    }

    static void UpdateEmployee()
    {
        Console.Write("Enter Name to update: ");
        string updateName = Console.ReadLine();
        var employee = employees.FirstOrDefault(e => e.Name == updateName);
        if (employee != null)
        {
            Console.Write("Enter New Name: ");
            employee.Name = Console.ReadLine();
            Console.Write("Enter New Gender: ");
            employee.Gender = Console.ReadLine();
            Console.Write("Enter New Age: ");
            if (int.TryParse(Console.ReadLine(), out int newAge))
            {
                employee.Age = newAge;
                Console.Write("Enter New Salary: ");
                if (double.TryParse(Console.ReadLine(), out double newSalary))
                {
                    employee.Salary = newSalary;
                    Console.WriteLine("Employee information updated successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid salary. Employee information not updated.");
                }
            }
            else
            {
                Console.WriteLine("Invalid age. Employee information not updated.");
            }
        }
        else
        {
            Console.WriteLine("Employee not found for updating.");
        }
    }

    static void ReadDataFromFile()
    {
        if (File.Exists(dataFilePath))
        {
            using (StreamReader reader = new StreamReader(dataFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (data.Length == 4)
                    {
                        employees.Add(new Employee
                        {
                            Name = data[0],
                            Gender = data[1],
                            Age = int.Parse(data[2]),
                            Salary = double.Parse(data[3])
                        });
                    }
                }
            }
        }
    }

    static void SaveDataToFile()
    {
        using (StreamWriter writer = new StreamWriter(dataFilePath))
        {
            foreach (var employee in employees)
            {
                writer.WriteLine($"{employee.Name},{employee.Gender},{employee.Age},{employee.Salary}");
            }
        }
    }
}
