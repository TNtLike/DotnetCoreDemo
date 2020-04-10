using System.Drawing;
using QRCoder;
using System.IO;
using MyWebApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System;

namespace MyWebApi.Services
{
    public class QRCodeService : IQRCodeService<Code>, IDBService<Code>
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
        /// <param name="carid">存储内容id</param>
        /// <param name="url">存储内容</param>
        /// <param name="pixel">像素大小</param>
        /// <returns></returns>
        public Code InitCode(string carid, string url, int pixel)
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
            code.CarId = carid;
            code.Info = url;
            code.Size = pixel;
            code.CodeImg = bytedata;
            _codes.InsertOne(code);
            return code;
        }


        #region 存储二维码
        public List<Code> Get() =>
            _codes.Find<Code>(code => true).ToList();

        public Code Get(string id) =>
            _codes.Find<Code>(code => code.Id == id).FirstOrDefault();
        public Code GetCarCode(string carid)
        {
            Code carcode = _codes.Find<Code>(code => code.CarId == carid).FirstOrDefault();
            return carcode;
        }

        public Code Create(Code code)
        {
            _codes.InsertOne(code);
            return code;
        }
        public void Update(string id, Code codeIn) =>
            _codes.ReplaceOne(code => code.Id == id, codeIn);

        public void Remove(Code codeIn) =>
            _codes.DeleteOne(code => code.Id == codeIn.Id);

        public void Remove(string id) =>
            _codes.DeleteOne(car => car.Id == id);

        #endregion
    }
}