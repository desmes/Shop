﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shop.WebUi.Tests.Mocks
{
    public class MyFileBase : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public MyFileBase(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override int ContentLength
        {
            get { return (int)stream.Length; }
        }

        public override string FileName
        {
            get { return fileName; }
        }
        public override Stream InputStream
        {
            get { return stream; }
        }
        public override void SaveAs(string filename)
        {
            using (var file = File.Open(filename, FileMode.CreateNew))
            {
                stream.CopyTo(file);
            }
        }
    }
}
