using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{

    internal class BinaryStream
    {
        private BinaryReader reader;
        private MemoryStream stream;
        private BinaryWriter writer;

        public BinaryStream()
        {
            this.stream = new MemoryStream();
            this.writer = new BinaryWriter(this.stream);
        }

        public BinaryStream(byte[] data)
        {
            this.stream = new MemoryStream(data);
            this.reader = new BinaryReader(this.stream);
        }

        public BinaryStream(Stream outStream)
        {
            this.stream = new MemoryStream();
            byte[] buffer = new byte[0x400];
            for (int i = outStream.Read(buffer, 0, 0x400); i > 0; i = outStream.Read(buffer, 0, 0x400))
            {
                this.stream.Write(buffer, 0, i);
            }
            this.stream.Seek(0L, SeekOrigin.Begin);
            this.reader = new BinaryReader(this.stream);
        }

        public void close()
        {
            if (this.reader != null)
            {
                this.reader.Close();
            }
            if (this.writer != null)
            {
                this.writer.Close();
            }
            if (this.stream != null)
            {
                this.stream.Close();
            }
        }

        public byte read()
        {
            return (byte)this.reader.Read();
        }

        public bool readBoolean()
        {
            return this.reader.ReadBoolean();
        }

        public byte[] readBytes(int count)
        {
            return this.reader.ReadBytes(count);
        }

        public double readDouble()
        {
            byte[] array = this.reader.ReadBytes(8);
            Array.Reverse(array);
            return BitConverter.ToDouble(array, 0);
        }

        public float readFloat()
        {
            byte[] array = this.reader.ReadBytes(4);
            Array.Reverse(array);
            return BitConverter.ToSingle(array, 0);
        }

        public int readInt()
        {
            byte[] array = this.reader.ReadBytes(4);
            Array.Reverse(array);
            return BitConverter.ToInt32(array, 0);
        }

        public long readLong()
        {
            byte[] array = this.reader.ReadBytes(8);
            Array.Reverse(array);
            return BitConverter.ToInt64(array, 0);
        }

        public short readShort()
        {
            byte[] array = this.reader.ReadBytes(2);
            Array.Reverse(array);
            return BitConverter.ToInt16(array, 0);
        }

        public string readUTF()
        {
            int count = this.readShort();
            byte[] bytes = this.readBytes(count);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public void seek(int offset, SeekOrigin orgin)
        {
            this.stream.Seek((long)offset, orgin);
        }

        public byte[] toBytes()
        {
            return this.stream.ToArray();
        }

        public void write(int value)
        {
            this.writer.Write((byte)value);
        }

        public void writeBoolean(bool value)
        {
            this.writer.Write(value);
        }

        public void writeBytes(byte[] value)
        {
            this.writer.Write(value, 0, value.Length);
        }

        public void writeDouble(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            this.writer.Write(bytes);
        }

        public void writeFloat(float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            this.writer.Write(bytes);
        }

        public void writeInt(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            this.writer.Write(bytes);
        }

        public void writeLong(long value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            this.writer.Write(bytes);
        }

        public void writeShort(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            this.writer.Write(bytes);
        }

        public void writeUTF(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            this.writeShort((short)bytes.Length);
            this.writeBytes(bytes);
        }

        public int Length
        {
            get
            {
                return (int)this.stream.Length;
            }
        }
    }
}
