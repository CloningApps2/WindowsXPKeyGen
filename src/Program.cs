using System;
using Microsoft.Win32;

class Program
{
    static void Main()
    {
        // Display the program description
        Console.WriteLine("Windows XP Product Key Modifier Tool");
        Console.WriteLine("This tool will modify the Product Key in the Windows XP registry.\n");

        // Get current product key and new key from user
        string currentKey = GetInput("Enter the current Product Key (25 characters):");
        string newKey = GetInput("Enter the new Product Key you want to replace with (25 characters):");

        // Validate product keys
        if (!ValidateKeyFormat(currentKey) || !ValidateKeyFormat(newKey))
        {
            Console.WriteLine("Invalid product key format. Ensure both keys are 25 characters and in the correct format.");
            return;
        }

        // Attempt to modify the registry
        try
        {
            // Path to the registry where the ProductKey is stored
            string registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";

            // Open the registry key with write access
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(registryPath, writable: true))
            {
                if (regKey == null)
                {
                    Console.WriteLine("Could not open the registry. Ensure you have administrative privileges.");
                    return;
                }

                // Get the current product key from the registry
                object productKey = regKey.GetValue("ProductKey");
                if (productKey == null)
                {
                    Console.WriteLine("No ProductKey found in the registry.");
                    return;
                }

                // Compare the current key with the one entered by the user
                string existingKey = productKey.ToString().Trim();
                if (existingKey != currentKey)
                {
                    Console.WriteLine("The entered current product key does not match the one in the registry.");
                    return;
                }

                // Replace the existing product key with the new key
                regKey.SetValue("ProductKey", newKey);
                Console.WriteLine("Product key updated successfully!");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("You need to run this program as an administrator.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Function to get user input
    static string GetInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine().Trim();
    }

    // Validate the product key format (25 alphanumeric characters)
    static bool ValidateKeyFormat(string key)
    {
        return key.Length == 25 && key.Replace("-", "").Length == 25;
    }
}

