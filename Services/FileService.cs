using System.Drawing;
using QRCoder;
using System.IO;
using MyWebApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace MyWebApi.Services
{
    public class FileService : IFileService<UserFile>
    {

        private readonly IMongoCollection<UserFile> _files;
        private readonly QRCodeGenerator _generator;
        public FileService(MongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _files = databases.GetCollection<UserFile>(nameof(UserFile));
            _generator = new QRCodeGenerator();
        }

        public bool InitUserFile(IFormCollection formDate)
        {
            try
            {
                IFormFile file = formDate.Files[0];
                if (file.Length > 0)
                {
                    MemoryStream stream = new MemoryStream();
                    file.CopyToAsync(stream);
                    byte[] bytedata = stream.ToArray();
                    UserFile userFile = new UserFile
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        UserId = formDate["userid"],
                        Key = formDate["key"],
                        Tag = formDate["filetag"],
                        Index = formDate["index"],
                        ByteFile = bytedata
                    };
                    _files.InsertOne(userFile);

                }
                return true;

            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));

            }
        }


        #region 存储二进制文件
        public UserFile Get(string id) =>
            _files.Find<UserFile>(e => e.Id == id).FirstOrDefault();

        public ServiceResponse Create(UserFile file)
        {
            try
            {
                _files.InsertOne(file);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }
        }
        public ServiceResponse Update(string id, UserFile fileIn)
        {
            try
            {
                _files.ReplaceOne(e => e.Id == id, fileIn);
                return new ServiceResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"An error occurred : {e.Message}.");
            }

        }

        #endregion
    }
}