using System.Drawing;
using QRCoder;
using System.IO;
using MyWebApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System;

namespace MyWebApi.Services
{
    public class QRCodeService : IBaseService<Code>, IQRCodeService<Code>
    {

        private readonly IMongoCollection<Code> _codes;
        private readonly QRCodeGenerator _generator;
        public QRCodeService(IMongoDBSettings config)
        {
            MongoClient client = new MongoClient(config.ConnectionString);
            IMongoDatabase databases = client.GetDatabase(config.DatabaseName);
            _codes = databases.GetCollection<Code>(nameof(Code));
            _generator = new QRCodeGenerator();
        }

        /// <summary>
        /// 绘制二维码
        /// </summary>
        /// <param name="unionid">存储内容id</param>
        /// <param name="url">存储内容</param>
        /// <param name="pixel">像素大小</param>
        /// <returns></returns>
        public Code InitCode(string unionid, string url, int pixel)
        {
            QRCodeData codeData = _generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCoder.QRCode qrcode = new QRCoder.QRCode(codeData);
            Bitmap qrImage = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);
            MemoryStream stream = new MemoryStream();
            qrImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bytedata = stream.ToArray();
            Code code = new Code();
            DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
            var unixTime = dto.ToUnixTimeSeconds();
            code.Id = unixTime.ToString();
            code.UnionId = unionid;
            code.Info = url;
            code.Size = pixel;
            code.CodeImg = bytedata;
            _codes.InsertOne(code);
            return code;
        }

        #region 存储二维码
        public List<Code> GetTs() =>
            _codes.Find<Code>(code => true).ToList();

        public Code GetT(string id) =>
            _codes.Find<Code>(code => code.Id == id).FirstOrDefault();
        public Code GetUnionCode(string unionid)
        {
            Code carcode = _codes.Find<Code>(code => code.UnionId == unionid).FirstOrDefault();
            return carcode;
        }

        public BaseService Create(Code code)
        {
            try
            {
                _codes.InsertOne(code);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }
        public BaseService Update(string id, Code codeIn)
        {
            try
            {
                _codes.ReplaceOne(code => code.Id == id, codeIn);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }

        }

        public BaseService Remove(string id)
        {
            try
            {
                _codes.DeleteOne(car => car.Id == id);
                return new BaseService();
            }
            catch (Exception e)
            {
                return new BaseService($"An error occurred : {e.Message}");
            }
        }

        #endregion
    }
}