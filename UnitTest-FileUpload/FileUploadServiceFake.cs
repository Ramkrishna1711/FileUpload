using FileUpload.Model;
using FileUpload.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTest_FileUpload
{
    public class FileUploadServiceFake : IFileService
    {
        private readonly AccountDetail _accountDetail;

        public FileUploadServiceFake()
        {

        }

        public async Task<FileUploadResult> FileUpload(string filePath)
        {
            try
            {
                var response = new FileUploadResult();
                response.FileValid = true;

                FileStream fileStream = new FileStream(filePath, FileMode.Open);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = "";
                    int i = 0;

                    List<string> errorString = new List<string>();

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        if (words.Length == 2) //One line should contain only two words(Name & Number) if other than it then line is not valid
                        {
                            string firstNameRegex = "^[A-Z]+[a-zA-Z]*$";
                            Match nameMatch = Regex.Match(words[0], firstNameRegex);

                            string accountRegex = "^[3-4][0-9]{6}[p]?$";
                            Match accountMatch = Regex.Match(words[1], accountRegex);

                            if (!nameMatch.Success && !accountMatch.Success)
                            {
                                errorString.Add("Account name, account number -not valid for " + (i + 1) + " line " + line);
                                response.FileValid = false;
                            }
                            else if (!nameMatch.Success)
                            {
                                errorString.Add("Account name -not valid for " + (i + 1) + " line " + line);
                                response.FileValid = false;
                            }
                            else if (!accountMatch.Success)
                            {
                                errorString.Add("Account number -not valid for " + (i + 1) + " line " + line);
                                response.FileValid = false;
                            }
                        }
                        else
                        {
                            errorString.Add("Not valid line " + (i + 1) + line);
                            response.FileValid = false;
                        }

                        i++;
                    }
                    response.InvalidLines = errorString;
                }

                if (response.FileValid)
                {
                    response.Response = new Response { StatusCode = MessageStatusCode.Success, MessageDescription = "File Upload success" };
                    return response;
                }
                else
                {
                    response.Response = new Response { StatusCode = MessageStatusCode.BadRequest, MessageDescription = "File Upload error" };
                    return response;
                }
            }

            catch (Exception ex)
            {
                return new FileUploadResult { Response = new Response { StatusCode = MessageStatusCode.Error, MessageDescription = ex.Message } };
            }
        }

    }
}