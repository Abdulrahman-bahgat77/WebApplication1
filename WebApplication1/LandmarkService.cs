using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using WebApplication1;

public class LandmarkService
{
    public List<Landmark> GetLandmarks(string filePath)
    {
        var landmarks = new List<Landmark>();
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip header
            {
                // Safely parse Rating
                if (!double.TryParse(worksheet.Cells[row, 7].Text, out double rating))
                {
                    // Handle invalid Rating
                    Console.WriteLine($"Invalid Rating format in row {row}. Setting default value to 0.");
                    rating = 0; // Default value
                }

                // Safely parse Cost
                if (!double.TryParse(worksheet.Cells[row, 18].Text, out double cost))
                {
                    // Handle invalid Cost
                    Console.WriteLine($"Invalid Cost format in row {row}. Setting default value to 0.");
                    cost = 0; // Default value
                }


                var landmark = new Landmark
                {
                    Name = worksheet.Cells[row, 1].Text,
                    Address = worksheet.Cells[row, 2].Text,
                    Type = worksheet.Cells[row, 4].Text,
                   // Category = worksheet.Cells[row, 18].Text,
                    Rating = rating, // Use the safely parsed value
                    Description = worksheet.Cells[row, 13].Text,
                    ImageUrl = worksheet.Cells[row, 16].Text,
                    Cost = cost // Use the safely parsed value
                };

                landmarks.Add(landmark);
            }

            return landmarks;
        }
    }
}